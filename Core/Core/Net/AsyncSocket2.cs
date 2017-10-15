using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Devcat.Core.Collections.Concurrent;
using Devcat.Core.Properties;

namespace Devcat.Core.Net
{
    internal class AsyncSocket2 : IAsyncSocket
    {
        public event EventHandler<EventArgs<ArraySegment<byte>>> PacketReceive;

        public event EventHandler<EventArgs> SocketClose;

        public event EventHandler<EventArgs<Exception>> SocketException;

        public long TotalSent { get; private set; }

        public int TotalSentCount { get; private set; }

        public long TotalReceived { get; private set; }

        public int TotalReceivedCount { get; private set; }

        public bool Connected
        {
            get
            {
                Socket socket = this.socket;
                return socket != null && socket.Connected;
            }
        }

        public byte[] RemoteAddress { get; private set; }

        public int RemotePort { get; private set; }

        public static void Init()
        {
        }

        public AsyncSocket2(Socket socket) : this(socket, null)
        {
        }

        public AsyncSocket2(Socket socket, IPacketAnalyzer packetAnalyzer)
        {
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }
            if (packetAnalyzer == null)
            {
                throw new ArgumentNullException("packetAnalyzer");
            }
            if (!socket.Connected)
            {
                throw new ArgumentException("Can't activate on closed socket.", "socket");
            }
            if (socket.SocketType != SocketType.Stream || socket.AddressFamily != AddressFamily.InterNetwork || socket.ProtocolType != ProtocolType.Tcp)
            {
                throw new ArgumentException("Only TCP/IPv4 socket available.", "socket");
            }
            LingerOption lingerOption = socket.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Linger) as LingerOption;
            if (lingerOption == null || lingerOption.Enabled)
            {
                throw new ArgumentException("Linger option must be disabled.", "socket");
            }
            if (socket.UseOnlyOverlappedIO)
            {
                throw new ArgumentException("Socket must be bind on completion port.", "socket");
            }
            this.socket = socket;
            this.packetAnalyzer = packetAnalyzer;
            this.sendQueue = new ConcurrentQueue<ArraySegment<byte>>();
            IPEndPoint ipendPoint = (IPEndPoint)socket.RemoteEndPoint;
            this.RemoteAddress = ipendPoint.Address.GetAddressBytes();
            this.RemotePort = ipendPoint.Port;
            this.receiveArg = AsyncSocket2.receiveArgsPool.Pop();
            this.receiveArg.UserToken = this;
            this.sendArg = AsyncSocket2.sendArgsPool.Pop();
            this.sendSize = 0;
            this.sendArg.UserToken = this;
        }

        public void Activate()
        {
            if (Interlocked.CompareExchange(ref this.active, 1, 0) != 0)
            {
                throw new InvalidOperationException("Can't reactivate AsyncSocket instance!");
            }
            this.BeginReceive();
        }

        public void Shutdown()
        {
            if (this.active == 0)
            {
                return;
            }
            if (Interlocked.CompareExchange(ref this.closed, 1, 0) != 0)
            {
                return;
            }
            this.receiveArg.UserToken = null;
            AsyncSocket2.receiveArgsPool.Push(this.receiveArg);
            this.sendArg.UserToken = null;
            AsyncSocket2.sendArgsPool.Push(this.sendArg);
            this.socket.Close();
            if (this.SocketClose != null)
            {
                this.SocketClose(this, EventArgs.Empty);
            }
        }

        public void Send(ArraySegment<byte> data)
        {
            if (this.closed == 0 && data.Count > 0)
            {
                this.sendQueue.Enqueue(data);
                this.BeginSend();
            }
        }

        public void Send(IEnumerable<ArraySegment<byte>> dataList)
        {
            if (this.closed == 0)
            {
                foreach (ArraySegment<byte> item in dataList)
                {
                    this.sendQueue.Enqueue(item);
                }
                this.BeginSend();
            }
        }

        private void BeginReceive()
        {
            try
            {
                if (this.socket.Connected)
                {
                    this.TotalReceivedCount++;
                    if (!this.socket.ReceiveAsync(this.receiveArg))
                    {
                        this.OnReceive(this.receiveArg);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception exception)
            {
                this.OnException(exception);
            }
        }

        private static void OnReceive(object sender, SocketAsyncEventArgs e)
        {
            AsyncSocket2 asyncSocket = e.UserToken as AsyncSocket2;
            if (asyncSocket != null)
            {
                asyncSocket.OnReceive(e);
            }
        }

        private void OnReceive(SocketAsyncEventArgs e)
        {
            if (e.BytesTransferred == 0)
            {
                this.Shutdown();
                return;
            }
            if (e.SocketError == SocketError.Success)
            {
                try
                {
                    this.TotalReceived += (long)e.BytesTransferred;
                    this.packetAnalyzer.Add(new ArraySegment<byte>(e.Buffer, e.Offset, e.BytesTransferred));
                    foreach (ArraySegment<byte> value in this.packetAnalyzer)
                    {
                        if (this.PacketReceive != null)
                        {
                            this.PacketReceive(this, new EventArgs<ArraySegment<byte>>(value));
                        }
                    }
                    this.BeginReceive();
                    return;
                }
                catch (Exception exception)
                {
                    this.OnException(exception);
                    return;
                }
            }
            this.OnException(new SocketException((int)e.SocketError));
        }

        private bool AcquireSendLock()
        {
            return Interlocked.Increment(ref this.sending) == 1;
        }

        private bool ReleaseSendLock()
        {
            return Interlocked.Exchange(ref this.sending, 0) == 1;
        }

        private void BeginSend()
        {
            while (this.AcquireSendLock())
            {
                bool flag = false;
                if (Monitor.TryEnter(this))
                {
                    flag = this._BeginSend();
                    Monitor.Exit(this);
                }
                if (flag || this.ReleaseSendLock())
                {
                    return;
                }
            }
        }

        private bool _BeginSend()
        {
            int num;
            ArraySegment<byte> nums;
            bool flag;
            if (this.sendSize < 8192)
            {
                if (this.sendingPacket.Count > 0)
                {
                    num = System.Math.Min(this.sendingPacket.Count, 8192 - this.sendSize);
                    Buffer.BlockCopy(this.sendingPacket.Array, this.sendingPacket.Offset, this.sendArg.Buffer, this.sendArg.Offset + this.sendSize, num);
                    this.sendSize += num;
                    if (num < this.sendingPacket.Count)
                    {
                        goto Label1;
                    }
                    this.sendingPacket = new ArraySegment<byte>();
                }
                while (this.sendSize < 8192 && this.sendQueue.TryDequeue(out nums))
                {
                    if (nums.Count <= 0)
                    {
                        continue;
                    }
                    int count = 0;
                    if (nums.Count + this.sendSize > 8192)
                    {
                        count = 8192 - this.sendSize;
                        this.sendingPacket = new ArraySegment<byte>(nums.Array, nums.Offset + count, nums.Count - count);
                    }
                    else
                    {
                        count = nums.Count;
                    }
                    Buffer.BlockCopy(nums.Array, nums.Offset, this.sendArg.Buffer, this.sendArg.Offset + this.sendSize, count);
                    this.sendSize += count;
                }
                this.sendArg.SetBuffer(this.sendArg.Offset, this.sendSize);
            }
            if (this.sendSize == 0)
            {
                return false;
            }
            Label0:
            try
            {
                if (this.socket.Connected)
                {
                    AsyncSocket2 totalSentCount = this;
                    totalSentCount.TotalSentCount = totalSentCount.TotalSentCount + 1;
                    if (this.socket.SendAsync(this.sendArg))
                    {
                        return true;
                    }
                    else
                    {
                        this.OnSend(this.sendArg);
                        flag = false;
                    }
                }
                else
                {
                    ConcurrentQueue<ArraySegment<byte>> arraySegments = new ConcurrentQueue<ArraySegment<byte>>();
                    Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, arraySegments);
                    flag = false;
                }
            }
            catch (ObjectDisposedException)
            {
                ConcurrentQueue<ArraySegment<byte>> arraySegments1 = new ConcurrentQueue<ArraySegment<byte>>();
                Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, arraySegments1);
                flag = false;
            }
            catch (Exception exception)
            {
                this.OnException(exception);
                flag = false;
            }
            return flag;
            Label1:
            this.sendingPacket = new ArraySegment<byte>(this.sendingPacket.Array, this.sendingPacket.Offset + num, this.sendingPacket.Count - num);
            this.sendArg.SetBuffer(this.sendArg.Offset, this.sendSize);
            goto Label0;
        }

        private static void OnSend(object sender, SocketAsyncEventArgs e)
        {
            AsyncSocket2 asyncSocket = e.UserToken as AsyncSocket2;
            if (asyncSocket != null)
            {
                asyncSocket.OnSend(e);
            }
        }

        private void OnSend(SocketAsyncEventArgs e)
        {
            try
            {
                if (!this.socket.Connected)
                {
                    ConcurrentQueue<ArraySegment<byte>> value = new ConcurrentQueue<ArraySegment<byte>>();
                    Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, value);
                    this.sending = 0;
                    return;
                }
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    if (e.BytesTransferred < e.Count)
                    {
                        Buffer.BlockCopy(e.Buffer, e.Offset + e.BytesTransferred, e.Buffer, e.Offset, e.Count - e.BytesTransferred);
                        this.sendSize = e.Count - e.BytesTransferred;
                    }
                    else if (e.BytesTransferred <= e.Count)
                    {
                        this.sendSize = 0;
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                ConcurrentQueue<ArraySegment<byte>> value2 = new ConcurrentQueue<ArraySegment<byte>>();
                Interlocked.Exchange<ConcurrentQueue<ArraySegment<byte>>>(ref this.sendQueue, value2);
                this.ReleaseSendLock();
                return;
            }
            if (e.BytesTransferred == 0)
            {
                this.Shutdown();
                return;
            }
            if (e.SocketError == SocketError.Success)
            {
                bool flag = this.sendQueue.IsEmpty && this.sendingPacket.Count == 0 && this.sendSize == 0;
                if (!this.ReleaseSendLock() || !flag)
                {
                    this.BeginSend();
                    return;
                }
            }
            else
            {
                this.OnException(new SocketException((int)e.SocketError));
            }
        }

        private void OnException(Exception exception)
        {
            SocketException ex = exception as SocketException;
            if ((ex == null || (ex.SocketErrorCode != SocketError.ConnectionAborted && ex.SocketErrorCode != SocketError.ConnectionReset)) && this.SocketException != null)
            {
                try
                {
                    this.SocketException(this, new EventArgs<Exception>(exception));
                }
                catch
                {
                }
            }
            this.Shutdown();
        }

        internal const int BufferSize = 8192;

        private static AsyncSocket2.BufferManager bufferManager = new AsyncSocket2.BufferManager(Settings.Default.TcpClient2_InitCCU * 2, 8192);

        private static AsyncSocket2.SocketAsyncEventArgsPool receiveArgsPool = new AsyncSocket2.SocketAsyncEventArgsPool(Settings.Default.TcpClient2_InitCCU, AsyncSocket2.bufferManager, new EventHandler<SocketAsyncEventArgs>(AsyncSocket2.OnReceive));

        private static AsyncSocket2.SocketAsyncEventArgsPool sendArgsPool = new AsyncSocket2.SocketAsyncEventArgsPool(Settings.Default.TcpClient2_InitCCU, AsyncSocket2.bufferManager, new EventHandler<SocketAsyncEventArgs>(AsyncSocket2.OnSend));

        private int active;

        private int closed;

        private Socket socket;

        private SocketAsyncEventArgs receiveArg;

        private SocketAsyncEventArgs sendArg;

        private int sendSize;

        private ArraySegment<byte> sendingPacket = default(ArraySegment<byte>);

        private int sending;

        private ConcurrentQueue<ArraySegment<byte>> sendQueue;

        private IPacketAnalyzer packetAnalyzer;

        private class BufferManager
        {
            public int BufferCount { get; private set; }

            public int BufferSize { get; private set; }

            public BufferManager(int _bufferCount, int _bufferSize)
            {
                this.freeIndexPool = new ConcurrentStack<int>();
                this.buffers = new List<byte[]>();
                this.lockObj = new object();
                this.BufferCount = _bufferCount;
                this.BufferSize = _bufferSize;
                this.GrowBuffer();
            }

            public void SetBuffer(SocketAsyncEventArgs args)
            {
                int num;
                while (!this.freeIndexPool.TryPop(out num))
                {
                    this.GrowBuffer();
                }
                int index = num / this.BufferCount;
                num %= this.BufferCount;
                args.SetBuffer(this.buffers[index], num * this.BufferSize, this.BufferSize);
            }

            public void FreeBuffer(SocketAsyncEventArgs args)
            {
                this.freeIndexPool.Push(args.Offset);
                args.SetBuffer(null, 0, 0);
            }

            private void GrowBuffer()
            {
                lock (this.lockObj)
                {
                    if (this.freeIndexPool.IsEmpty)
                    {
                        byte[] item = new byte[this.BufferCount * this.BufferSize];
                        this.buffers.Add(item);
                        for (int i = 0; i < this.BufferCount; i++)
                        {
                            this.freeIndexPool.Push(i + (this.buffers.Count - 1) * this.BufferCount);
                        }
                    }
                }
            }

            private List<byte[]> buffers;

            private ConcurrentStack<int> freeIndexPool;

            private object lockObj;
        }

        private class SocketAsyncEventArgsPool
        {
            public int BlockCount { get; private set; }

            public SocketAsyncEventArgsPool(int _blockCount, AsyncSocket2.BufferManager _bufferManager, EventHandler<SocketAsyncEventArgs> _compeleteEvent)
            {
                this.BlockCount = _blockCount;
                this.bufferManager = _bufferManager;
                this.onComplete = _compeleteEvent;
                this.pool = new ConcurrentStack<SocketAsyncEventArgs>();
                for (int i = 0; i < this.BlockCount; i++)
                {
                    SocketAsyncEventArgs item = this.Create();
                    this.pool.Push(item);
                }
            }

            public SocketAsyncEventArgs Pop()
            {
                SocketAsyncEventArgs result;
                if (!this.pool.TryPop(out result))
                {
                    result = this.Create();
                }
                return result;
            }

            public void Push(SocketAsyncEventArgs arg)
            {
                this.pool.Push(arg);
            }

            private SocketAsyncEventArgs Create()
            {
                SocketAsyncEventArgs socketAsyncEventArgs = new SocketAsyncEventArgs();
                socketAsyncEventArgs.Completed += this.onComplete.Invoke;
                this.bufferManager.SetBuffer(socketAsyncEventArgs);
                return socketAsyncEventArgs;
            }

            private ConcurrentStack<SocketAsyncEventArgs> pool;

            private AsyncSocket2.BufferManager bufferManager;

            private EventHandler<SocketAsyncEventArgs> onComplete;
        }
    }
}
