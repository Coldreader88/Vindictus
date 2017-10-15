using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Text;
using AdminClientServiceCore.Messages;
using AdminClientServiceCore.Properties;
using Devcat.Core.Net.Message;
using Devcat.Core.Threading;
using ServiceCore;
using ServiceCore.EndPointNetwork;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.HeroesContents;
using Utility;

namespace AdminClientServiceCore
{
	[Database(Name = "heroes")]
	public class EventDataContext : DataContext
	{
		public static void Initialize(JobProcessor thread)
		{
			EventDataContext.Thread = thread;
			EventDataContext.Instance = new EventDataContext();
		}

		private void OnCreated()
		{
			Scheduler.Schedule(EventDataContext.Thread, Job.Create(new Action(EventDataContext.Watcher)), 10000);
		}

		public static void Watcher()
		{
			if (!EventDataContext.processing)
			{
				if (ServiceCore.FeatureMatrix.IsEnable("FeatureMatrixSyncService"))
				{
					EventDataContext.ProcessEvent();
					EventDataContext.processing = true;
					return;
				}
				if (AdminClientService.IsAllServiceReady())
				{
					EventDataContext.StartProcessing();
					return;
				}
				Scheduler.Schedule(EventDataContext.Thread, Job.Create(new Action(EventDataContext.Watcher)), 10000);
			}
		}

		public static void StartProcessing()
		{
			if (!EventDataContext.processing)
			{
				Scheduler.Schedule(EventDataContext.Thread, Job.Create(new Action(EventDataContext.ProcessEvent)), 180000);
				EventDataContext.processing = true;
			}
		}

		private static void ProcessEvent()
		{
			try
			{
				DateTime now = DateTime.Now;
				int num = (now.Hour * 60 + now.Minute) * 60;
				int num2 = 0;
				List<Event> list = new List<Event>();
				List<Event> list2 = new List<Event>();
				List<Event> list3 = new List<Event>();
				foreach (Event @event in EventDataContext.Instance.Event)
				{
					if (@event.StartTime == null && !(@event.StartCount > 0))
					{
						if (@event.EndTime == null || now < @event.EndTime)
						{
							list.Add(@event);
						}
						else
						{
							list3.Add(@event);
						}
					}
					else if (@event.StartTime != null)
					{
						if (now >= @event.StartTime)
						{
							if (@event.EndTime == null || now < @event.EndTime)
							{
								if (@event.PeriodBegin == null)
								{
									list.Add(@event);
								}
								else if (now.TimeOfDay >= @event.PeriodBegin)
								{
									if (@event.PeriodEnd == null || now.TimeOfDay < @event.PeriodEnd)
									{
										list.Add(@event);
									}
									else
									{
										list2.Add(@event);
									}
								}
								else
								{
									list2.Add(@event);
								}
							}
							else
							{
								list3.Add(@event);
							}
						}
						else
						{
							list2.Add(@event);
						}
					}
					else if (@event.StartCount > 0)
					{
						if (@event.StartCount <= EventDataContext.Instance.GetEventCount(@event.Name))
						{
							if (@event.EndTime == null || now < @event.EndTime)
							{
								if (@event.PeriodBegin == null)
								{
									list.Add(@event);
								}
								else if (now.TimeOfDay >= @event.PeriodBegin)
								{
									if (@event.PeriodEnd == null || now.TimeOfDay < @event.PeriodEnd)
									{
										list.Add(@event);
									}
									else
									{
										list2.Add(@event);
									}
								}
								else
								{
									list2.Add(@event);
								}
							}
							else
							{
								list3.Add(@event);
							}
						}
						else
						{
							list2.Add(@event);
						}
					}
				}
				foreach (Event event2 in list3)
				{
					EventDataContext.RemoveEvent(event2.Name);
				}
				foreach (Event event3 in list2)
				{
					EventDataContext.EndEvent(event3.Name);
				}
				foreach (KeyValuePair<string, Event> keyValuePair in EventDataContext.runnings)
				{
					Event value = keyValuePair.Value;
					if (value.NotifyMessage != null && value.NotifyMessage.Length > 0)
					{
						int num3 = 1800;
						if (value.NotifyInterval != null && value.NotifyInterval.Value >= 0)
						{
							num3 = value.NotifyInterval.Value;
						}
						num3 = num3 / 60 * 60;
						if (num3 != 0 && num % num3 == 0)
						{
							Scheduler.Schedule(EventDataContext.Thread, Job.Create<string, int>(new Action<string, int>(EventDataContext.BroadcastMessage), value.NotifyMessage, -1), ++num2 * 1000);
						}
					}
				}
				foreach (Event event4 in list)
				{
					EventDataContext.StartEvent(event4.Name, event4);
				}
				EventDataContext.processed = true;
				foreach (KeyValuePair<int, string> keyValuePair2 in EventDataContext.waitingServiceList)
				{
					Scheduler.Schedule(EventDataContext.Thread, Job.Create<int, string>(new Action<int, string>(EventDataContext.SendToFeatureMaxtrixUpdate), keyValuePair2.Key, keyValuePair2.Value), 10000);
				}
				EventDataContext.waitingServiceList.Clear();
			}
			finally
			{
				EventDataContext.ScheduleNextProcess();
			}
		}

		private static void ScheduleNextProcess()
		{
			long num = 600000000L - DateTime.Now.Ticks % 600000000L;
			if (num == 0L)
			{
				num = 600000000L;
			}
			Scheduler.Schedule(EventDataContext.Thread, Job.Create(new Action(EventDataContext.ProcessEvent)), (int)(num / 10000L));
		}

		public static void AddFeatureMatrixUpdate(int targetServiceID, string category)
		{
			if (EventDataContext.processed)
			{
				Scheduler.Schedule(EventDataContext.Thread, Job.Create<int, string>(new Action<int, string>(EventDataContext.SendToFeatureMaxtrixUpdate), targetServiceID, category), 10000);
				return;
			}
			if (!EventDataContext.waitingServiceList.ContainsKey(targetServiceID))
			{
				EventDataContext.waitingServiceList.Add(targetServiceID, category);
				Log<AdminClientService>.Logger.WarnFormat("FeaturMatrix update list added : {0}/{1}", category, targetServiceID);
			}
		}

		public static void SendToFeatureMaxtrixUpdate(int targetServiceID, string category)
		{
			foreach (KeyValuePair<string, Event> keyValuePair in EventDataContext.runnings)
			{
				EventDataContext.SendToStartEvent(keyValuePair.Key, keyValuePair.Value, targetServiceID);
			}
			Log<AdminClientService>.Logger.WarnFormat("Target service featurMatrix update : {0}/{1}", category, targetServiceID);
		}

		public static Table<Event> Events
		{
			get
			{
				return EventDataContext.Instance.Event;
			}
		}

		public static Event GetEvent(string name)
		{
			foreach (Event @event in EventDataContext.Instance.Event)
			{
				if (@event.Name == name)
				{
					return @event;
				}
			}
			return null;
		}

		public static bool AddEvent(Event instance)
		{
			bool result;
			try
			{
				EventDataContext.Instance.Event.InsertOnSubmit(instance);
				EventDataContext.Instance.SubmitChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Log<AdminClientService>.Logger.Error("AddEvent 에서 에러가 발생했습니다", ex);
				result = false;
			}
			return result;
		}

		public static bool ModifyEvent(Event instance)
		{
			bool result;
			try
			{
				EventDataContext.Instance.SubmitChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Log<AdminClientService>.Logger.Error("ModifyEvent 에서 에러가 발생했습니다", ex);
				result = false;
			}
			return result;
		}

		private static bool RemoveEvent(Event instance)
		{
			bool result;
			try
			{
				EventDataContext.Instance.Event.DeleteOnSubmit(instance);
				EventDataContext.Instance.SubmitChanges();
				result = true;
			}
			catch (Exception ex)
			{
				Log<AdminClientService>.Logger.Error("ModifyEvent 에서 에러가 발생했습니다", ex);
				result = false;
			}
			return result;
		}

		public static bool RemoveEvent(string name)
		{
			Event @event = EventDataContext.GetEvent(name);
			if (@event != null)
			{
				if (EventDataContext.IsRunning(name))
				{
					EventDataContext.EndEvent(name);
				}
				return EventDataContext.RemoveEvent(@event);
			}
			return false;
		}

		public static bool IsRunning(string name)
		{
			return EventDataContext.runnings.ContainsKey(name);
		}

		private static void StartEvent(string name, Event instance)
		{
			if (!EventDataContext.IsRunning(name))
			{
				EventDataContext.runnings.Add(name, instance);
				if (instance.StartMessage != null && instance.StartMessage.Length > 0)
				{
					EventDataContext.BroadcastMessage(instance.StartMessage, -1);
				}
				AdminClientService.Instance.UpdateFeatureMatrix(instance.Feature, true, -1);
				AdminClientService.Instance.ProcessScript(instance.StartScript, -1);
				EventDataContext.RearrangeFirstIncomeMessage(-1);
			}
		}

		private static void SendToStartEvent(string name, Event instance, int targetServiceID)
		{
			if (EventDataContext.IsRunning(name))
			{
				if (instance.StartMessage != null && instance.StartMessage.Length > 0)
				{
					EventDataContext.BroadcastMessage(instance.StartMessage, targetServiceID);
				}
				AdminClientService.Instance.UpdateFeatureMatrix(instance.Feature, true, targetServiceID);
				AdminClientService.Instance.ProcessScript(instance.StartScript, targetServiceID);
				EventDataContext.RearrangeFirstIncomeMessage(targetServiceID);
			}
		}

		private static void EndEvent(string name)
		{
			Event @event;
			if (EventDataContext.runnings.TryGetValue(name, out @event))
			{
				if (@event.EndMessage != null && @event.EndMessage.Length > 0)
				{
					EventDataContext.BroadcastMessage(@event.EndMessage, -1);
				}
				AdminClientService.Instance.UpdateFeatureMatrix(@event.Feature, false, -1);
				AdminClientService.Instance.ProcessScript(@event.EndScript, -1);
				EventDataContext.runnings.Remove(name);
				EventDataContext.RearrangeFirstIncomeMessage(-1);
			}
		}

		public static string IsExistFeature(string feature)
		{
			if (feature == null)
			{
				return null;
			}
			char[] separator = new char[]
			{
				';'
			};
			string[] array = feature.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				string value = text;
				if (text.IndexOf('[') > 0)
				{
					value = text.Substring(0, text.IndexOf('['));
				}
				foreach (Event @event in EventDataContext.Instance.Event)
				{
					if (@event.Feature != null)
					{
						string[] array3 = @event.Feature.Split(separator, StringSplitOptions.RemoveEmptyEntries);
						foreach (string text2 in array3)
						{
							string text3 = text2;
							if (text2.IndexOf('[') > 0)
							{
								text3 = text2.Substring(0, text2.IndexOf('['));
							}
							if (text3.Equals(value, StringComparison.CurrentCultureIgnoreCase))
							{
								return text;
							}
						}
					}
				}
			}
			return null;
		}

		public static string SendEventListAndDetail(string name, string comment, bool IsDetail = true)
		{
			int num = 0;
			StringBuilder stringBuilder = new StringBuilder("");
			stringBuilder.AppendFormat("\r\n========== {0} \"{1}\" ==========", comment, name);
			stringBuilder.Append("\r\n========== Event List ==========\r\n");
			foreach (Event @event in EventDataContext.Instance.Event)
			{
				stringBuilder.AppendFormat("{0} [{1}]\r\n", @event.Name, EventDataContext.IsRunning(@event.Name) ? "Running" : "Pending");
				num++;
			}
			stringBuilder.Append("========== Event List ==========");
			if (IsDetail)
			{
				Event event2 = EventDataContext.GetEvent(name);
				if (event2 != null)
				{
					stringBuilder.AppendFormat("\r\n========== Event Detail ==========\r\nStatus [{0}]\r\n{1}\r\n========== Event Detail ==========", EventDataContext.IsRunning(name) ? "Running" : "Pending", event2.ToStringX());
				}
			}
			stringBuilder.AppendFormat("\r\n========== {0} \"{1}\" ==========", comment, name);
			if (num >= 15)
			{
				stringBuilder.AppendFormat("\r\n========== Warning! Event is more than 15! [Current Count : {0}] ==========", num);
			}
			return stringBuilder.ToString();
		}

		public static bool RegisterEvent(string name, string template, string feature, string scriptstart, string scriptend, DateTime? startTime, DateTime? endTime, TimeSpan? periodBegin, TimeSpan? periodEnd, string msgstart, string msgnotify, string msgend, int msginterval, int startCont, string username, AdminClientServicePeer peer)
		{
			EventDataContext.StartProcessing();
			EventTemplate eventTemplate = null;
			bool flag = false;
			if (EventDataContext.IsRunning(name))
			{
				if (peer != null)
				{
					string msg = EventDataContext.SendEventListAndDetail(name, "Event is Already Running !", true);
					peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, msg)));
				}
				return false;
			}
			if (template != null)
			{
				eventTemplate = AdminContents.GetTemplate(template);
				if (eventTemplate == null)
				{
					if (peer != null)
					{
						peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("Cannot Find Event Template ! - \"{0}\"", template))));
					}
					return false;
				}
			}
			Event @event = EventDataContext.GetEvent(name);
			if (@event == null)
			{
				@event = new Event();
				@event.Name = name;
				flag = true;
			}
			if (eventTemplate != null)
			{
				@event.Feature = eventTemplate.Feature;
				@event.StartScript = eventTemplate.StartScript;
				@event.EndScript = eventTemplate.EndScript;
			}
			if (feature != null)
			{
				@event.Feature = feature;
			}
			if (scriptstart != null)
			{
				@event.StartScript = scriptstart;
			}
			if (scriptend != null)
			{
				@event.EndScript = scriptend;
			}
			if (startTime != null)
			{
				if (startTime < DateTime.Now)
				{
					if (peer != null)
					{
						peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("Reservation Time Error - \"{0}\"", startTime))));
					}
					return false;
				}
				@event.StartTime = startTime;
			}
			if (endTime != null)
			{
				if (endTime < DateTime.Now)
				{
					if (peer != null)
					{
						peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("Reservation Time Error - \"{0}\"", endTime))));
					}
					return false;
				}
				@event.EndTime = endTime;
			}
			if (periodBegin != null)
			{
				@event.PeriodBegin = periodBegin;
			}
			if (periodEnd != null)
			{
				@event.PeriodEnd = periodEnd;
			}
			if (msgstart != null)
			{
				@event.StartMessage = msgstart;
			}
			if (msgnotify != null)
			{
				@event.NotifyMessage = msgnotify;
			}
			if (msgend != null)
			{
				@event.EndMessage = msgend;
			}
			@event.NotifyInterval = new int?(msginterval);
			@event.StartCount = new int?(startCont);
			@event.UserName = username;
			if (flag)
			{
				if (feature != null)
				{
					string text = EventDataContext.IsExistFeature(feature);
					if (text != null)
					{
						if (peer != null)
						{
							peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("That feature is already exist ! - \"{0}\"", text))));
						}
						return false;
					}
				}
				if (!EventDataContext.AddEvent(@event))
				{
					if (peer != null)
					{
						peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("Event Register Failed ! - \"{0}\"", name))));
					}
					return false;
				}
			}
			else if (!EventDataContext.ModifyEvent(@event))
			{
				if (peer != null)
				{
					peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("Event Update Failed ! - \"{0}\"", name))));
				}
				return false;
			}
			if (@event.StartTime == null && !(@event.StartCount > 0))
			{
				EventDataContext.StartEvent(name, @event);
				if (peer != null)
				{
					string msg2 = EventDataContext.SendEventListAndDetail(name, "Event is Started !", true);
					peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.SUCCESS, msg2)));
				}
			}
			else if (peer != null)
			{
				string msg3 = EventDataContext.SendEventListAndDetail(name, "Event is Reserved !", true);
				peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.SUCCESS, msg3)));
			}
			return true;
		}

		public static bool EndEvent(string name, string scriptend, string msgend, AdminClientServicePeer peer)
		{
			EventDataContext.StartProcessing();
			Event @event = EventDataContext.GetEvent(name);
			if (@event == null)
			{
				if (peer != null)
				{
					peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.ERROR, string.Format("Event is Not Registered ! - \"{0}\"", name))));
				}
				return false;
			}
			if (scriptend != null)
			{
				@event.EndScript = scriptend;
			}
			if (msgend != null)
			{
				@event.EndMessage = msgend;
			}
			EventDataContext.EndEvent(name);
			EventDataContext.RemoveEvent(name);
			if (peer != null)
			{
				string msg = EventDataContext.SendEventListAndDetail(name, "Event is Ended !", false);
				peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.SUCCESS, msg)));
			}
			return true;
		}

		public static bool ListEvent(AdminClientServicePeer peer)
		{
			if (peer != null)
			{
				string text = "";
				foreach (Event @event in EventDataContext.Instance.Event)
				{
					text += string.Format("{0} [{1}]\r\n", @event.Name, EventDataContext.IsRunning(@event.Name) ? "Running" : "Pending");
				}
				peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.SUCCESS, string.Format("\r\n========== Event List ==========\r\n{0}========== Event List ==========", text))));
				return true;
			}
			return false;
		}

		public static bool ShowEvent(string name, AdminClientServicePeer peer)
		{
			if (peer != null)
			{
				Event @event = EventDataContext.GetEvent(name);
				if (@event != null)
				{
					peer.Transmit(SerializeWriter.ToBinary<AdminReportNotifyMessage>(new AdminReportNotifyMessage(NotifyCode.SUCCESS, string.Format("\r\n========== Event Detail ==========\r\nStatus [{0}]\r\n{1}\r\n========== Event Detail ==========", EventDataContext.IsRunning(name) ? "Running" : "Pending", @event.ToStringX()))));
					return true;
				}
			}
			return false;
		}

		private static void RearrangeFirstIncomeMessage(int targetServiceID)
		{
			string text = "";
			foreach (KeyValuePair<string, Event> keyValuePair in EventDataContext.runnings)
			{
				Event value = keyValuePair.Value;
				if (value.NotifyMessage != null && value.NotifyMessage.Length > 0)
				{
					text += value.NotifyMessage;
					text += "\r\n";
				}
			}
			if (EventDataContext.FirstIncomeMessage != text)
			{
				EventDataContext.FirstIncomeMessage = text;
				EventDataContext.SetFirstIncomeMessage(EventDataContext.FirstIncomeMessage, targetServiceID);
			}
		}

		private static void SetFirstIncomeMessage(string s, int targetServiceID)
		{
			foreach (int num in AdminClientService.Instance.FrontendServiceIDs())
			{
				if (targetServiceID == -1 || num == targetServiceID)
				{
					ExecCommand op = new ExecCommand("SetFirstIncomeMessage", s);
					AdminClientService.Instance.RequestOperation(num, op);
				}
			}
		}

		public static void BroadcastMessage(string s, int targetServiceID)
		{
			NotifyClient op = new NotifyClient
			{
				Category = SystemMessageCategory.Notice,
				Message = new HeroesString(s)
			};
			Log<EventDataContext>.Logger.Info(s);
			foreach (int num in AdminClientService.Instance.FrontendServiceIDs())
			{
				if (targetServiceID == -1 || num == targetServiceID)
				{
					AdminClientService.Instance.RequestOperation(num, op);
				}
			}
		}

		public EventDataContext() : base(Settings.Default.heroesConnectionString, EventDataContext.mappingSource)
		{
			this.OnCreated();
		}

		public EventDataContext(string connection) : base(connection, EventDataContext.mappingSource)
		{
			this.OnCreated();
		}

		public EventDataContext(IDbConnection connection) : base(connection, EventDataContext.mappingSource)
		{
			this.OnCreated();
		}

		public EventDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
			this.OnCreated();
		}

		public EventDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
			this.OnCreated();
		}

		public Table<Event> Event
		{
			get
			{
				return base.GetTable<Event>();
			}
		}

		[Function(Name = "dbo.InitConnectedInfo")]
		public int InitConnectedInfo()
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[0]);
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.HDV_AddQueuedItemCurrentConnections")]
		public int AddQueuedItemCurrentConnections([Parameter(Name = "ItemClassEx", DbType = "NVarChar(1024)")] string itemClassEx, [Parameter(Name = "Count", DbType = "Int")] int? count, [Parameter(DbType = "DateTime")] DateTime? ExpireTime)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				itemClassEx,
				count,
				ExpireTime
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.HDV_ExtendCashItems")]
		public int ExtendCashItems([Parameter(DbType = "DateTime2")] DateTime? fromDate, [Parameter(DbType = "Int")] int? minutes)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				fromDate,
				minutes
			});
			return (int)executeResult.ReturnValue;
		}

		[Function(Name = "dbo.GetEventCount")]
		public int GetEventCount([Parameter(Name = "EventName", DbType = "VarChar(256)")] string eventName)
		{
			IExecuteResult executeResult = base.ExecuteMethodCall(this, (MethodInfo)MethodBase.GetCurrentMethod(), new object[]
			{
				eventName
			});
			return (int)executeResult.ReturnValue;
		}

		private static bool processing = false;

		private static EventDataContext Instance;

		private static JobProcessor Thread;

		private static Dictionary<string, Event> runnings = new Dictionary<string, Event>();

		private static string FirstIncomeMessage = "";

		private static bool processed = false;

		private static Dictionary<int, string> waitingServiceList = new Dictionary<int, string>();

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
