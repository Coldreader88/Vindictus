using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using log4net.Config;
using Nexon.Com.Playfeed.SDK.Utility;

namespace Nexon.Com.Playfeed.SDK
{
	public class PlayfeedWriter
	{
		public PlayfeedWriter(string serverAddress, uint port, string serviceCode)
		{
			this._serverAddress = serverAddress;
			this._port = port;
			this._serviceCode = serviceCode;
			PlayfeedWriter._sendCount = 0u;
			XmlConfigurator.Configure();
			this._worker = new Thread(new ThreadStart(this.Consume));
			this._worker.Start();
		}

		public PlayfeedWriter.ErrorCode Publishfeed(uint logType, uint userNo, uint feedCategory, uint feedTypeNo, string gamefeed, out string token)
		{
			token = string.Empty;
			PlayfeedWriter.ErrorCode result;
			if (gamefeed.Length > 2048)
			{
				Logger.Warn("Playfeed size is too big, this data will be discarded.");
				result = PlayfeedWriter.ErrorCode.kError_FeedSizeOver;
			}
			else
			{
				string item = FeedatomJsonSerializer.ToPlayfeedJson(this._serviceCode, feedTypeNo, logType, feedCategory, userNo, gamefeed);
				if (!this.EnqueueItem(item))
				{
					result = PlayfeedWriter.ErrorCode.kError_SendBufferFull;
				}
				else
				{
					result = PlayfeedWriter.ErrorCode.kError_None;
				}
			}
			return result;
		}

		private bool EnqueueItem(string item)
		{
			bool result = true;
			int count;
			lock (this._locker)
			{
				if (this._itemQ.Count < 4096)
				{
					this._itemQ.Enqueue(item);
				}
				else
				{
					Logger.Warn("Too many datas in queue, this data will be discarded.");
					result = false;
				}
				count = this._itemQ.Count;
				Monitor.Pulse(this._locker);
			}
			Logger.Log("Queue size = " + count);
			return result;
		}

		public void Close(bool waitForWorkers)
		{
			lock (this._locker)
			{
				if (!waitForWorkers)
				{
					this._itemQ.Clear();
				}
				this._itemQ.Enqueue(null);
				Monitor.Pulse(this._locker);
			}
			this._worker.Join();
		}

		private string HttpRequest(string requestData)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			string result;
			try
			{
				result = HttpRequestUtility.Post("http://api.nexon.com:" + this._port + "/game/gameserver.aspx", this._serverAddress, requestData);
			}
			catch (Exception exception)
			{
				Logger.Log(exception);
				return null;
			}
			Logger.Log("Request data:\r\n" + requestData);
			stopwatch.Stop();
			Logger.Log("Elasped time  " + stopwatch.Elapsed.TotalMilliseconds + "ms");
			return result;
		}

		private void Consume()
		{
			for (;;)
			{
				string text;
				int count;
				lock (this._locker)
				{
					while (this._itemQ.Count == 0)
					{
						Monitor.Wait(this._locker);
					}
					text = this._itemQ.Dequeue();
					count = this._itemQ.Count;
				}
				Logger.Log("Queue size = " + count);
				if (text == null)
				{
					break;
				}
				string text2 = this.HttpRequest(text);
				if (string.IsNullOrEmpty(text2))
				{
					Logger.Log((PlayfeedWriter._sendCount += 1u) + " feedatom sent to the middlware successfully");
				}
				else
				{
					Logger.Warn(text2);
				}
			}
		}

		private const int MaxQueueSize = 4096;

		private const int MaxGameFeedLen = 2048;

		private const string PredefinedUrl = "http://api.nexon.com";

		private const string PredefinedPage = "game/gameserver.aspx";

		private static uint _sendCount;

		private readonly Queue<string> _itemQ = new Queue<string>();

		private readonly object _locker = new object();

		private readonly uint _port;

		private readonly string _serverAddress;

		private readonly string _serviceCode;

		private readonly Thread _worker;

		public enum ErrorCode
		{
			kError_None,
			kError_NotInitialized,
			kError_NotSupported,
			kError_SendFail,
			kError_FeedSizeOver,
			kError_SendBufferFull
		}
	}
}
