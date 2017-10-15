using System;
using System.Collections.Generic;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;
using ServiceCore.EndPointNetwork;
using UnifiedNetwork.Cooperation;

namespace ServiceCore.CharacterServiceOperations
{
	[Serializable]
	public sealed class WhoIs : Operation
	{
		public WhoIsOption Option { get; set; }

		public WhoIs(WhoIsOption option)
		{
			this.Option = option;
		}

		public int TransformCoolDownInt
		{
			get
			{
				if (this.TransformCoolDown == null)
				{
					return -1;
				}
				int num = (int)(this.TransformCoolDown.Value - DateTime.UtcNow).TotalSeconds;
				if (num < 0)
				{
					return 0;
				}
				return num;
			}
			set
			{
				if (value < 0)
				{
					this.TransformCoolDown = null;
				}
				this.TransformCoolDown = new DateTime?(DateTime.UtcNow.AddSeconds((double)value));
			}
		}

		public override OperationProcessor RequestProcessor()
		{
			return new WhoIs.Request(this);
		}

		[NonSerialized]
		public CharacterSummary Summary;

		[NonSerialized]
		public int Limited;

		[NonSerialized]
		public IDictionary<string, int> Stats;

		[NonSerialized]
		public IDictionary<int, int> Titles;

		[NonSerialized]
		public IDictionary<string, int> Skills;

		[NonSerialized]
		public IDictionary<int, string> SpSkills;

		[NonSerialized]
		public IDictionary<string, int> VocationSkills;

		[NonSerialized]
		public DateTime? TransformCoolDown;

		[NonSerialized]
		public IDictionary<int, int> ArmorDefMap;

		[NonSerialized]
		public IDictionary<int, int> ArmorHPMap;

		[NonSerialized]
		public List<StatusEffectElement> StatusEffects;

		[NonSerialized]
		public DateTime StatusEffectPivotTime;

		[NonSerialized]
		public int DestroyedDef;

		[NonSerialized]
		public List<MissionQuest> MissionQuestList;

		[NonSerialized]
		public List<long> MissionIDList;

		[NonSerialized]
		public int GuildLevel;

		[NonSerialized]
		public IDictionary<string, int> GuildLevelUpBonus;

		[NonSerialized]
		public IDictionary<string, int> ManufactureExpDic;

		[NonSerialized]
		public List<long> recommenderFriends;

		[NonSerialized]
		public long recommendedFriendCID;

		[NonSerialized]
		public IDictionary<string, BriefSkillEnhance> SkillEnhanceDic;

		private class Request : OperationProcessor<WhoIs>
		{
			public Request(WhoIs op) : base(op)
			{
			}

			public override IEnumerable<object> Run()
			{
				yield return null;
				if (base.Feedback is CharacterSummary)
				{
					base.Operation.Summary = (base.Feedback as CharacterSummary);
					yield return null;
					base.Operation.Limited = (int)base.Feedback;
					if (base.Operation.Option.Contains(WhoIsOption.Stat))
					{
						yield return null;
						base.Operation.Stats = (base.Feedback as IDictionary<string, int>);
					}
					if (base.Operation.Option.Contains(WhoIsOption.Title))
					{
						yield return null;
						base.Operation.Titles = (base.Feedback as IDictionary<int, int>);
					}
					if (base.Operation.Option.Contains(WhoIsOption.Skill))
					{
						yield return null;
						base.Operation.Skills = (base.Feedback as IDictionary<string, int>);
						yield return null;
						base.Operation.SpSkills = (base.Feedback as IDictionary<int, string>);
						yield return null;
						base.Operation.VocationSkills = (base.Feedback as IDictionary<string, int>);
						yield return null;
						base.Operation.TransformCoolDownInt = (int)base.Feedback;
					}
					if (base.Operation.Option.Contains(WhoIsOption.Equipment))
					{
						yield return null;
						base.Operation.ArmorDefMap = (base.Feedback as IDictionary<int, int>);
						yield return null;
						base.Operation.ArmorHPMap = (base.Feedback as IDictionary<int, int>);
						yield return null;
						base.Operation.DestroyedDef = (int)base.Feedback;
					}
					if (base.Operation.Option.Contains(WhoIsOption.StatusEffect))
					{
						yield return null;
						base.Operation.StatusEffectPivotTime = (DateTime)base.Feedback;
						yield return null;
						base.Operation.StatusEffects = (base.Feedback as List<StatusEffectElement>);
					}
					if (base.Operation.Option.Contains(WhoIsOption.Mission))
					{
						yield return null;
						base.Operation.MissionIDList = (base.Feedback as List<long>);
						yield return null;
						base.Operation.MissionQuestList = (base.Feedback as List<MissionQuest>);
					}
					if (base.Operation.Option.Contains(WhoIsOption.GuildLevelUpBonus))
					{
						yield return null;
						base.Operation.GuildLevel = (int)base.Feedback;
						yield return null;
						base.Operation.GuildLevelUpBonus = (base.Feedback as IDictionary<string, int>);
					}
					if (base.Operation.Option.Contains(WhoIsOption.ManufactureExp))
					{
						yield return null;
						base.Operation.ManufactureExpDic = (base.Feedback as IDictionary<string, int>);
					}
					if (base.Operation.Option.Contains(WhoIsOption.FriendRecommendedInfo))
					{
						yield return null;
						base.Operation.recommenderFriends = (base.Feedback as List<long>);
						yield return null;
						base.Operation.recommendedFriendCID = (long)base.Feedback;
					}
					if (base.Operation.Option.Contains(WhoIsOption.SkillEnhance))
					{
						yield return null;
						base.Operation.SkillEnhanceDic = (base.Feedback as Dictionary<string, BriefSkillEnhance>);
					}
				}
				else
				{
					base.Result = false;
				}
				yield break;
			}
		}
	}
}
