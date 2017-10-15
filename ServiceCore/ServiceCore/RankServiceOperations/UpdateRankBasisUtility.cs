using System;
using Devcat.Core;
using UnifiedNetwork.Cooperation;
using UnifiedNetwork.Entity;

namespace ServiceCore.RankServiceOperations
{
	public class UpdateRankBasisUtility
	{
		private static void UpdateRankBasis(Service service, IEntity entity, long cID, string rankID, long score)
		{
			if (FeatureMatrix.IsEnable("CharacterRank") && service != null && entity != null)
			{
				IEntityProxy connection = service.Connect(entity, new Location
				{
					ID = cID,
					Category = "RankService.RankService"
				});
				UpdateRankBasis updateRankBasis = new UpdateRankBasis(cID, rankID, score, 0L);
				updateRankBasis.OnFail += delegate(Operation _)
				{
				};
				updateRankBasis.OnComplete += delegate(Operation _)
				{
					connection.Close();
				};
				connection.ConnectionSucceeded += delegate(object _, EventArgs<IEntityProxy> __)
				{
					connection.RequestOperation(updateRankBasis);
				};
			}
		}

		private static void UpdateRankBasis(IEntityProxy connection, long cID, string rankID, long score)
		{
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, rankID, score, 0L);
		}

		private static void UpdateRankBasis(IEntityProxy connection, long cID, string rankID, long score, long entityID)
		{
			if (FeatureMatrix.IsEnable("CharacterRank") && connection != null)
			{
				UpdateRankBasis op = new UpdateRankBasis(cID, rankID, score, entityID);
				connection.RequestOperation(op);
			}
		}

		private static void UpdateRankBasis(IEntityProxy connection, long gID, string rankID, long score, string guildName)
		{
			if (FeatureMatrix.IsEnable("CharacterRank") && connection != null)
			{
				UpdateRankBasis op = new UpdateRankBasis(gID, rankID, score, guildName);
				connection.RequestOperation(op);
			}
		}

		public static void UpdateRankBasis_LevelUp(Service service, IEntity entity, long cID, long score)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_lvl_up", score);
		}

		public static void UpdateRankBasis_UseAP(Service service, IEntity entity, long cID, long score)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_use_ap", score);
		}

		public static void UpdateRankBasis_GetTitle(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_get_title", 1L);
		}

		public static void UpdateRankBasis_TryDyeing(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_try_dyeing", 1L);
		}

		public static void UpdateRankBasis_TryEnchant(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_try_enchant", 1L);
		}

		public static void UpdateRankBasis_TryEnhance(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_try_enhance", 1L);
		}

		public static void UpdateRankBasis_ProduceCook(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_produce_cook", 1L);
		}

		public static void UpdateRankBasis_ProduceEquipment(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_produce_equipment", 1L);
		}

		public static void UpdateRankBasis_GetItem(IEntityProxy connection, long cID, string itemClass, long score)
		{
			string text = "";
			long score2 = 1L;
			if (itemClass == "gold")
			{
				text = "GetItem_gold";
				score2 = score;
			}
			else if (itemClass.EndsWith("_erg_crystal"))
			{
				text = "GetItem_erg_crystal";
			}
			else if (itemClass.StartsWith("cloth_lvl"))
			{
				text = "GetItem_cloth";
			}
			else if (itemClass.StartsWith("silk_lvl"))
			{
				text = "GetItem_silk";
			}
			else if (itemClass.StartsWith("skin_lvl"))
			{
				text = "GetItem_skin";
			}
			else if (itemClass.StartsWith("iron_ore_lvl"))
			{
				text = "GetItem_iron_ore";
			}
			else if (itemClass.Contains("sign_lvl"))
			{
				text = "GetItem_sign";
			}
			else if (itemClass.StartsWith("enhance_stone_lvl"))
			{
				text = "GetItem_enhance_stone";
			}
			if (text.Length > 0)
			{
				UpdateRankBasisUtility.UpdateRankBasis(connection, cID, text, score2);
			}
		}

		public static void UpdateRankBasis_SuccessFishing(IEntityProxy connection, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, "GamePlay_success_fishing", 1L);
		}

		public static void UpdateRankBasis_ReviveParty(IEntityProxy connection, long cID, long score)
		{
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, "GamePlay_revive_party", score);
		}

		public static void UpdateRankBasis_UseItem(IEntityProxy connection, long cID, string itemClass)
		{
			string text = "";
			if (itemClass.StartsWith("javelin"))
			{
				text = "UseItem_javelin";
			}
			else if (itemClass.StartsWith("handbomb"))
			{
				text = "UseItem_handbomb";
			}
			else if (itemClass.StartsWith("flashbang"))
			{
				text = "UseItem_flashbang";
			}
			else if (itemClass.StartsWith("mining_bomb"))
			{
				text = "UseItem_mining_bomb";
			}
			else if (itemClass.StartsWith("sticky_bomb"))
			{
				text = "UseItem_sticky_bomb";
			}
			if (text.Length > 0)
			{
				UpdateRankBasisUtility.UpdateRankBasis(connection, cID, text, 1L);
			}
		}

		public static void UpdateRankBasis_CompleteBattle(IEntityProxy connection, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, "GamePlay_complete_battle", 1L);
		}

		public static void UpdateRankBasis_HelpBeginner(IEntityProxy connection, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, "GamePlay_help_beginner", 1L);
		}

		public static void UpdateRankBasis_PartyTimeAttack(IEntityProxy connection, long cID, string questId, long score, long entityID)
		{
			string text = "PartyTimeAttack_";
			text += questId;
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, text, score, entityID);
		}

		public static void UpdateRankBasis_SoloTimeAttack(IEntityProxy connection, long cID, string questId, long score)
		{
			string text = "SoloTimeAttack_";
			text += questId;
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, text, score);
		}

		public static void UpdateRankBasis_SuccessPartDestruction(IEntityProxy connection, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, "GamePlay_success_part_destruction", 1L);
		}

		public static void UpdateRankBasis_SuccessMining(IEntityProxy connection, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, "GamePlay_success_mining", 1L);
		}

		public static void UpdateRankBasis_CompleteStory(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_complete_story", 1L);
		}

		public static void UpdateRankBasis_TakeCarriage(Service service, IEntity entity, long cID)
		{
			UpdateRankBasisUtility.UpdateRankBasis(service, entity, cID, "GamePlay_take_carriage", 1L);
		}

		public static void UpdateRankBasis_Pvp(IEntityProxy connection, long gID, string rankID, int score, string guildName)
		{
			string text = "Pvp_";
			text += rankID;
			UpdateRankBasisUtility.UpdateRankBasis(connection, gID, text, (long)score, guildName);
		}

		public static void UpdateRankBasis_Pvp(IEntityProxy connection, long cID, string rankID)
		{
			string text = "Pvp_";
			text += rankID;
			UpdateRankBasisUtility.UpdateRankBasis(connection, cID, text, 1L);
		}
	}
}
