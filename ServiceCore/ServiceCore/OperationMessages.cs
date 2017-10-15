using System;
using System.Collections.Generic;
using System.Reflection;
using Devcat.Core.Net.Message;
using ServiceCore.CharacterServiceOperations;
using ServiceCore.CharacterServiceOperations.RandomMissionOperations;
using ServiceCore.DSServiceOperations;
using ServiceCore.EndPointNetwork;
using ServiceCore.EndPointNetwork.GuildService;
using ServiceCore.EndPointNetwork.Pvp;
using ServiceCore.FrontendServiceOperations;
using ServiceCore.GuildServiceOperations;
using ServiceCore.ItemServiceOperations;
using ServiceCore.MicroPlayServiceOperations;
using ServiceCore.PartyServiceOperations;
using ServiceCore.PlayerServiceOperations;
using ServiceCore.RankServiceOperations;
using UnifiedNetwork.Cooperation;

namespace ServiceCore
{
	public static class OperationMessages
	{
		public static IEnumerable<Type> Types
		{
			get
			{
				foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
				{
					if (!type.IsInterface && !type.IsAbstract && !type.IsGenericTypeDefinition && type.IsSerializable && type.IsSealed && !typeof(Operation).IsAssignableFrom(type) && !typeof(IMessage).IsAssignableFrom(type) && !typeof(IP2PMessage).IsAssignableFrom(type))
					{
						yield return type;
					}
				}
				yield return typeof(List<CharacterSummary>);
				yield return typeof(Dictionary<int, ConsumablesInfo>);
				yield return typeof(List<TitleSlotInfo>);
				yield return typeof(List<UpdatedStateElement>);
				yield return typeof(List<BriefMailInfo>);
				yield return typeof(List<MailItemInfo>);
				yield return typeof(List<PvPPartyMemberInfo>);
				yield return typeof(List<ShipInfo>);
				yield return typeof(LinkedList<ShipInfo>);
				yield return typeof(List<TransferredItemInfo>);
				yield return typeof(Dictionary<int, EquippedItemInfo>);
				yield return typeof(List<EquippedItemInfo>);
				yield return typeof(Dictionary<int, EquippedItemInfo>.ValueCollection);
				yield return typeof(List<StoryDropData>);
				yield return typeof(List<CashShopInventoryElement>);
				yield return typeof(List<FishingResultInfo>);
				yield return typeof(List<StatusEffectElement>);
				yield return typeof(List<MissionInfo>);
				yield return typeof(List<MissionQuest>);
				yield return typeof(MissionInfo);
				yield return typeof(List<ProgressInfo>);
				yield return typeof(List<RankResultInfo>);
				yield return typeof(List<RankAlarmInfo>);
				yield return typeof(List<InGameGuildInfo>);
				yield return typeof(List<TradeItemInfo>);
				yield return typeof(List<ColoredItem>);
				yield return typeof(List<AchieveGoalInfo>);
				yield return typeof(List<RandomRankResultInfo>);
				yield return typeof(Dictionary<int, DSInfo>);
				yield return typeof(GuildMemberKey);
				yield return typeof(List<PvpResultInfo>);
				yield return typeof(List<PvpChannelInfo>);
				yield return typeof(List<TransferElement>);
				yield return typeof(List<WishItemInfo>);
				yield return typeof(Dictionary<string, PriceRange>);
				yield return typeof(Dictionary<short, ShopTimeRestrictedResult>);
				yield return typeof(Dictionary<string, BriefSkillEnhance>);
				yield return typeof(List<DetailOption>);
				yield break;
			}
		}

		public static IDictionary<Type, int> TypeConverters
		{
			get
			{
				return OperationMessages.Types.GetConverter(12288);
			}
		}
	}
}
