using System;
using System.ServiceModel;
using WcfChatRelay.Whisper;

namespace WcfChatRelay.Server.Whisper
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, UseSynchronizationContext = false)]
	internal class ServiceCallback : IServiceCallback
	{
		public event EventHandler<WhisperAsyncEventArg> WhisperedAsync;

		public event EventHandler<WhisperEventArg> Whispered;

		public event EventHandler WebClosed;

		public event EventHandler<WhisperResultAsyncEventArg> WhisperResultAsync;

		public void WebClose()
		{
			if (this.WebClosed != null)
			{
				this.WebClosed(this, EventArgs.Empty);
			}
		}

		public IAsyncResult BeginAsyncWhisperResultFromApp(long toCID, int resultNo, string receiverName, AsyncCallback callback, object asyncState)
		{
			WhisperAsyncResult whisperAsyncResult = new WhisperAsyncResult(callback, asyncState);
			if (this.WhisperedAsync != null)
			{
				WhisperResultAsyncEventArg e = new WhisperResultAsyncEventArg
				{
					ToCID = toCID,
					ResultNo = resultNo,
					ReceiverName = receiverName,
					AsyncResult = whisperAsyncResult,
					Callback = new WhisperCompleted(this.WhisperCallback)
				};
				this.WhisperResultAsync(this, e);
				return whisperAsyncResult;
			}
			IAsyncResult result;
			try
			{
				result = whisperAsyncResult;
			}
			finally
			{
				whisperAsyncResult.Complete();
			}
			return result;
		}

		public bool EndAsyncWhisperResultFromApp(IAsyncResult result)
		{
			WhisperAsyncResult whisperAsyncResult = result as WhisperAsyncResult;
			return whisperAsyncResult != null && whisperAsyncResult.Result;
		}

		private void WhisperCallback(bool result, IAsyncResult ar)
		{
			WhisperAsyncResult whisperAsyncResult = ar as WhisperAsyncResult;
			if (whisperAsyncResult != null)
			{
				whisperAsyncResult.Result = result;
				whisperAsyncResult.Complete();
			}
		}

		public IAsyncResult BeginAsyncWhisperFromApp(string from, long toCID, string message, AsyncCallback callback, object asyncState)
		{
			WhisperAsyncResult whisperAsyncResult = new WhisperAsyncResult(callback, asyncState);
			if (this.WhisperedAsync != null)
			{
				WhisperAsyncEventArg e = new WhisperAsyncEventArg
				{
					From = from,
					ToCID = toCID,
					Message = message,
					AsyncResult = whisperAsyncResult,
					Callback = new WhisperCompleted(this.WhisperCallback)
				};
				this.WhisperedAsync(this, e);
				return whisperAsyncResult;
			}
			IAsyncResult result;
			try
			{
				result = whisperAsyncResult;
			}
			finally
			{
				whisperAsyncResult.Complete();
			}
			return result;
		}

		public bool EndAsyncWhisperFromApp(IAsyncResult result)
		{
			WhisperAsyncResult whisperAsyncResult = result as WhisperAsyncResult;
			return whisperAsyncResult != null && whisperAsyncResult.Result;
		}

		public void WhisperFromApp(string from, long toCID, string message)
		{
			if (this.Whispered != null)
			{
				WhisperEventArg e = new WhisperEventArg
				{
					From = from,
					ToCID = toCID,
					Message = message
				};
				this.Whispered(this, e);
			}
		}
	}
}
