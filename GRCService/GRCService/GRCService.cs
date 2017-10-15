using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Devcat.Core;
using Devcat.Core.Threading;
using GRCService.Properties;
using GRCServiceCore.Processor;
using ServiceCore;
using ServiceCore.Configuration;
using ServiceCore.GRCServiceOperations;
using ServiceCore.HeroesContents;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;
using Utility;

namespace GRCServiceCore
{
	public class GRCService : Service
	{
		public static Service StartService(string ip, string portstr)
		{
			GRCService grcservice = new GRCService();
			ServiceInvoker.StartService(ip, portstr, grcservice);
			return grcservice;
		}

		public override int CompareAndSwapServiceID(long id, string category, int beforeID)
		{
			if (category != base.Category)
			{
				return -1;
			}
			int result;
			using (EntityDataContext entityDataContext = new EntityDataContext())
			{
				result = entityDataContext.AcquireService(new long?(id), base.Category, new int?(base.ID), new int?(beforeID));
			}
			return result;
		}

		public override void Initialize(JobProcessor thread)
		{
			ConnectionStringLoader.LoadFromServiceCore(Settings.Default);
			this.RequestIntervalSeconds = ServiceCoreSettings.Default.GRCRequestSeconds;
			base.Initialize(thread, GRCServiceOperations.TypeConverters);
			base.RegisterMessage(OperationMessages.TypeConverters);
			base.RegisterProcessor(typeof(GameResourceRespond), (Operation op) => new GameResourceRespondProcessor(this, op as GameResourceRespond));
			this.AssemblyServiceCore = Assembly.LoadFile(Path.Combine(Environment.CurrentDirectory, "ServiceCore.dll"));
			this.Content = this.LoadContent("ItemClassInfo");
			if (this.Content.Elements.Count<object>() == 0)
			{
				Log<GRCService>.Logger.InfoFormat("LoadContent() failed! Content.Elements.Count() == 0", new object[0]);
			}
		}

		private GRCService.ContentDB3 LoadContent(string tableName)
		{
			string name = string.Format("ServiceCore.HeroesContents.{0}", tableName);
			Type type = this.AssemblyServiceCore.GetType(name);
			if (type == null)
			{
				Log<GRCService>.Logger.InfoFormat("LoadContent() failed. [{0}]", tableName);
				return null;
			}
			GRCService.ContentDB3 contentDB = new GRCService.ContentDB3(this);
			contentDB.Properties = type.GetProperties();
			MethodInfo methodInfo = typeof(HeroesContentsLoader).GetMethod("GetTable", BindingFlags.Static | BindingFlags.Public);
			methodInfo = methodInfo.MakeGenericMethod(new Type[]
			{
				type
			});
			object obj = methodInfo.Invoke(null, null);
			contentDB.Elements = new List<object>();
			foreach (object item in ((IEnumerable)obj))
			{
				contentDB.Elements.Add(item);
			}
			return contentDB;
		}

		internal int NextMilliseconds()
		{
			return this.RequestIntervalSeconds * 1000;
		}

		protected override IEntity MakeEntity(long id, string category)
		{
			IEntity entity = base.MakeEntity(id, category);
			entity.Tag = new GRCClient(this, entity);
			entity.Used += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				if (entity.UseCount == 0)
				{
					entity.Close();
				}
			};
			entity.Using += delegate(object sender, EventArgs<IEntityAdapter> e)
			{
				IEntityAdapter value = e.Value;
				if (entity.Tag == null)
				{
					return;
				}
				if (value.RemoteCategory == "FrontendServiceCore.FrontendService")
				{
					GRCClient grcclient = entity.Tag as GRCClient;
					if (grcclient.FrontendConn != null)
					{
						grcclient.FrontendConn.Close();
					}
					grcclient.FrontendConn = this.Connect(entity, new Location(value.RemoteID, value.RemoteCategory));
					grcclient.FrontendConn.Closed += delegate(object _, EventArgs<IEntityProxy> __)
					{
						entity.Close();
					};
					grcclient.FrontendConn.OperationQueueOversized += delegate(object _, EventArgs<IEntityProxy> __)
					{
						entity.Close(true);
					};
				}
			};
			entity.Closed += delegate(object sender, EventArgs e)
			{
				try
				{
					EntityDataContext entityDataContext = new EntityDataContext();
					entityDataContext.AcquireService(new long?((sender as IEntity).ID), base.Category, new int?(-1), new int?(base.ID));
				}
				catch (Exception ex)
				{
					Log<GRCService>.Logger.ErrorFormat("Entity_Closed [EntityID : {0}] [ServiceID : {1}] [Category : {2}] - {3} ", new object[]
					{
						(sender as IEntity).ID,
						base.ID,
						base.Category,
						ex
					});
				}
			};
			Scheduler.Schedule(base.Thread, Job.Create(new Action((entity.Tag as GRCClient).MakeRequest)), this.NextMilliseconds());
			return entity;
		}

		private void TestContent()
		{
			for (int i = 0; i < this.Content.RowSize; i++)
			{
				for (int j = 0; j < this.Content.ColSize; j++)
				{
					Log<GRCService>.Logger.InfoFormat("TestContent() [{0}][{1}]", i, j);
					string text;
					string text2;
					this.Content.MakeQuestion(i, j, out text, out text2);
				}
			}
		}

		internal int RequestIntervalSeconds = 10;

		private readonly int[] cycle = new int[]
		{
			(int)(new TimeSpan(0, 1, 0).Ticks / 10000L),
			(int)(new TimeSpan(0, 0, 15).Ticks / 10000L)
		};

		public Random random = new Random();

		private Assembly AssemblyServiceCore;

		public GRCService.ContentDB3 Content;

		public class ContentDB3
		{
			public ContentDB3(GRCService service)
			{
				this.Service = service;
			}

			public int RowSize
			{
				get
				{
					return this.Elements.Count<object>();
				}
			}

			public int ColSize
			{
				get
				{
					return this.Properties.Count<PropertyInfo>();
				}
			}

			public string GetColName(int col)
			{
				return this.Properties[col].Name;
			}

			public void MakeQuestion(int row, int col, out string question, out string answer)
			{
				question = "";
				answer = "";
				try
				{
					object obj = this.Elements[row];
					PropertyInfo propertyInfo = this.Properties[col];
					string name = propertyInfo.Name;
					object value = propertyInfo.GetValue(obj, null);
					question = string.Format("ItemClassInfo,{0},{1},{2}", row, col, name);
					if (value == null)
					{
						answer = "null";
					}
					else if (value is string)
					{
						string text = value as string;
						answer = text.Substring(0, Math.Min(4, text.Length));
					}
					else
					{
						answer = value.ToString();
					}
				}
				catch (Exception)
				{
					Log<GRCService>.Logger.ErrorFormat("MakeQuestion() failed!", new object[0]);
				}
			}

			public void MakeQuestionRandom(out string question, out string answer)
			{
				int row = this.Service.random.Next(this.RowSize);
				int col = this.Service.random.Next(this.ColSize);
				this.MakeQuestion(row, col, out question, out answer);
			}

			private GRCService Service;

			public List<object> Elements;

			public PropertyInfo[] Properties;
		}
	}
}
