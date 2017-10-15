using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DSService.WaitingQueue;
using ServiceCore;
using ServiceCore.CommonOperations;
using ServiceCore.DSServiceOperations;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace DSService.Processor
{
	internal class _CheatCommandProcessor : OperationProcessor<_CheatCommand>
	{
		private static Dictionary<string, MethodInfo> MethodDict { get; set; } = new Dictionary<string, MethodInfo>();

		static _CheatCommandProcessor()
		{
			Type typeFromHandle = typeof(_CheatCommandProcessor);
			foreach (MethodInfo methodInfo in typeFromHandle.GetMethods())
			{
				CheatFunction cheatFunction = methodInfo.GetCustomAttributes(typeof(CheatFunction), false).FirstOrDefault<object>() as CheatFunction;
				if (cheatFunction != null)
				{
					_CheatCommandProcessor.MethodDict.Add(cheatFunction.Command, methodInfo);
				}
			}
		}

		public _CheatCommandProcessor(DSService service, _CheatCommand op) : base(op)
		{
			this.service = service;
		}

		public override IEnumerable<object> Run()
		{
			MethodInfo method = _CheatCommandProcessor.MethodDict.TryGetValue(base.Operation.Command);
			if (method == null)
			{
				Log<_CheatCommandProcessor>.Logger.ErrorFormat("No Cheat Method [{0}]", base.Operation.Command);
				base.Finished = true;
				yield return new FailMessage("[_CheatCommandProcessor] method");
			}
			else
			{
				Log<_CheatCommandProcessor>.Logger.WarnFormat("Process CheatCommand [{0} {1} {2}]", this.service.GetType().Name, base.Operation.Command, base.Operation.Arg);
				bool processed = false;
				try
				{
					processed = (bool)method.Invoke(this, null);
				}
				catch (Exception ex)
				{
					Log<_CheatCommandProcessor>.Logger.Error("Exception while cheating", ex);
				}
				if (processed)
				{
					base.Finished = true;
					yield return new OkMessage();
				}
				else
				{
					base.Finished = true;
					yield return new FailMessage("[_CheatCommandProcessor] processed");
				}
			}
			yield break;
		}

		[CheatFunction("ds_state")]
		public bool DSState()
		{
			DSWaitingSystem dswaitingSystem = this.service.DSWaitingSystem;
			if (dswaitingSystem != null)
			{
				IEntityProxy frontendConn = this.service.Connect(this.service.DSServiceEntity, new Location(base.Operation.FrontendID, "FrontendServiceCore.FrontendService"));
				frontendConn.SendConsole(dswaitingSystem.ToString(), new object[0]);
			}
			else
			{
				DSService.RequestDSBossOperation(base.Operation);
			}
			return true;
		}

		[CheatFunction("ds_add_queue")]
		public bool AddQueue()
		{
			DSWaitingSystem dswaitingSystem = this.service.DSWaitingSystem;
			if (dswaitingSystem != null)
			{
				string argString = base.Operation.GetArgString(0, "");
				if (argString == "reset")
				{
					string @string = FeatureMatrix.GetString("DSQuestSetting");
					using (IEnumerator<string> enumerator = (from q in @string.Split(new char[]
					{
						','
					})
					where q != ""
					select q).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							dswaitingSystem.AddWaitingQueue(text.Trim(), -1, true);
						}
						return true;
					}
				}
				int argInt = base.Operation.GetArgInt(1, -1);
				dswaitingSystem.AddWaitingQueue(argString, argInt, true);
			}
			else
			{
				DSService.RequestDSBossOperation(base.Operation);
			}
			return true;
		}

		[CheatFunction("ds_remove_queue")]
		public bool RemoveQueue()
		{
			DSWaitingSystem dswaitingSystem = this.service.DSWaitingSystem;
			if (dswaitingSystem != null)
			{
				string argString = base.Operation.GetArgString(0, "");
				if (argString == "all")
				{
					using (List<KeyValuePair<string, DSWaitingQueue>>.Enumerator enumerator = dswaitingSystem.QueueDict.ToList<KeyValuePair<string, DSWaitingQueue>>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, DSWaitingQueue> keyValuePair = enumerator.Current;
							dswaitingSystem.RemoveWaitingQueue(keyValuePair.Key);
						}
						return true;
					}
				}
				dswaitingSystem.RemoveWaitingQueue(argString);
			}
			else
			{
				DSService.RequestDSBossOperation(base.Operation);
			}
			return true;
		}

		[CheatFunction("ds_sink_ship")]
		public bool SinkShip()
		{
			if (this.service.DSWaitingSystem == null)
			{
				DSService.RequestDSBossOperation(base.Operation);
			}
			return true;
		}

		[CheatFunction("ds_add_dummy")]
		public bool AddDummy()
		{
			DSWaitingSystem dswaitingSystem = this.service.DSWaitingSystem;
			if (dswaitingSystem != null)
			{
				string argString = base.Operation.GetArgString(0, "");
				List<DSPlayerInfo> list = new List<DSPlayerInfo>();
				int num = 1;
				for (;;)
				{
					int argInt = base.Operation.GetArgInt(num, -1);
					if (argInt <= 0)
					{
						break;
					}
					list.Add(new DSPlayerInfo((long)_CheatCommandProcessor.DummyClientID, -1L, argInt));
					_CheatCommandProcessor.DummyClientID--;
					num++;
				}
				dswaitingSystem.Register(argString, list, -1L, -1L, true, false);
				this.DSState();
			}
			else
			{
				DSService.RequestDSBossOperation(base.Operation);
			}
			return true;
		}

		[CheatFunction("ds_remove_dummy")]
		public bool RemoveDummy()
		{
			DSWaitingSystem dswaitingSystem = this.service.DSWaitingSystem;
			if (dswaitingSystem != null)
			{
				string argString = base.Operation.GetArgString(0, "");
				DSWaitingQueue dswaitingQueue = dswaitingSystem.QueueDict.TryGetValue(argString);
				if (dswaitingQueue != null)
				{
					int num = base.Operation.GetArgInt(1, 0);
					int num2 = 0;
					if (num >= 0)
					{
						using (LinkedList<DSWaitingParty>.Enumerator enumerator = dswaitingQueue.WaitingParties.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								DSWaitingParty dswaitingParty = enumerator.Current;
								foreach (KeyValuePair<long, DSPlayer> keyValuePair in dswaitingParty.Members)
								{
									if (keyValuePair.Key < 0L)
									{
										if (num2 == num)
										{
											dswaitingSystem.Unregister(keyValuePair.Key, false);
											this.DSState();
											return true;
										}
										num2++;
									}
								}
							}
							return true;
						}
					}
					num = -1 - num;
					foreach (DSShip dsship in dswaitingQueue.Ships)
					{
						foreach (KeyValuePair<long, DSPlayer> keyValuePair2 in dsship.Players)
						{
							if (keyValuePair2.Key < 0L)
							{
								if (num2 == num)
								{
									dswaitingSystem.Unregister(keyValuePair2.Key, false);
									this.DSState();
									return true;
								}
								num2++;
							}
						}
					}
					return true;
				}
			}
			else
			{
				DSService.RequestDSBossOperation(base.Operation);
			}
			return true;
		}

		private DSService service;

		private static int DummyClientID = -1;
	}
}
