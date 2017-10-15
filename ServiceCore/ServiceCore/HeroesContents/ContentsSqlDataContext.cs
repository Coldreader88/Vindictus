using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using ServiceCore.Properties;

namespace ServiceCore.HeroesContents
{
	[Database(Name = "heroesContents")]
	public class ContentsSqlDataContext : DataContext
	{
		public ContentsSqlDataContext() : base(Settings.Default.heroesContentsConnectionString, ContentsSqlDataContext.mappingSource)
		{
		}

		public ContentsSqlDataContext(string connection) : base(connection, ContentsSqlDataContext.mappingSource)
		{
		}

		public ContentsSqlDataContext(IDbConnection connection) : base(connection, ContentsSqlDataContext.mappingSource)
		{
		}

		public ContentsSqlDataContext(string connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public ContentsSqlDataContext(IDbConnection connection, MappingSource mappingSource) : base(connection, mappingSource)
		{
		}

		public Table<ActionRewardInfo> ActionRewardInfo
		{
			get
			{
				return base.GetTable<ActionRewardInfo>();
			}
		}

		public Table<WeakpointRewardInfo> WeakpointRewardInfo
		{
			get
			{
				return base.GetTable<WeakpointRewardInfo>();
			}
		}

		public Table<AlchemistBoxInfo> AlchemistBoxInfo
		{
			get
			{
				return base.GetTable<AlchemistBoxInfo>();
			}
		}

		public Table<AntiBindInfo> AntiBindInfo
		{
			get
			{
				return base.GetTable<AntiBindInfo>();
			}
		}

		public Table<AttendanceCondition> AttendanceCondition
		{
			get
			{
				return base.GetTable<AttendanceCondition>();
			}
		}

		public Table<AttendanceEvent> AttendanceEvent
		{
			get
			{
				return base.GetTable<AttendanceEvent>();
			}
		}

		public Table<AttendanceReward> AttendanceReward
		{
			get
			{
				return base.GetTable<AttendanceReward>();
			}
		}

		public Table<AttributeInfo> AttributeInfo
		{
			get
			{
				return base.GetTable<AttributeInfo>();
			}
		}

		public Table<AvatarSetBonusInfo> AvatarSetBonusInfo
		{
			get
			{
				return base.GetTable<AvatarSetBonusInfo>();
			}
		}

		public Table<BeardTypeInfo> BeardTypeInfo
		{
			get
			{
				return base.GetTable<BeardTypeInfo>();
			}
		}

		public Table<BingoEvent> BingoEvent
		{
			get
			{
				return base.GetTable<BingoEvent>();
			}
		}

		public Table<BingoNumberSetting> BingoNumberSetting
		{
			get
			{
				return base.GetTable<BingoNumberSetting>();
			}
		}

		public Table<BingoReward> BingoReward
		{
			get
			{
				return base.GetTable<BingoReward>();
			}
		}

		public Table<BriefDropItemInfo> BriefDropItemInfo
		{
			get
			{
				return base.GetTable<BriefDropItemInfo>();
			}
		}

		public Table<Cashshop_IngameSellInfo> Cashshop_IngameSellInfo
		{
			get
			{
				return base.GetTable<Cashshop_IngameSellInfo>();
			}
		}

		public Table<Cashshop_Tircoin_notsell_list> Cashshop_Tircoin_notsell_list
		{
			get
			{
				return base.GetTable<Cashshop_Tircoin_notsell_list>();
			}
		}

		public Table<CashShopCouponInfo> CashShopCouponInfo
		{
			get
			{
				return base.GetTable<CashShopCouponInfo>();
			}
		}

		public Table<CharacterBaseLoadInfo> CharacterBaseLoadInfo
		{
			get
			{
				return base.GetTable<CharacterBaseLoadInfo>();
			}
		}

		public Table<CharacterClassInfo> CharacterClassInfo
		{
			get
			{
				return base.GetTable<CharacterClassInfo>();
			}
		}

		public Table<ConditionReductionInfo> ConditionReductionInfo
		{
			get
			{
				return base.GetTable<ConditionReductionInfo>();
			}
		}

		public Table<CraftShopInfo> CraftShopInfo
		{
			get
			{
				return base.GetTable<CraftShopInfo>();
			}
		}

		public Table<CustomizeColorInfo> CustomizeColorInfo
		{
			get
			{
				return base.GetTable<CustomizeColorInfo>();
			}
		}

		public Table<CustomizeCouponInfo> CustomizeCouponInfo
		{
			get
			{
				return base.GetTable<CustomizeCouponInfo>();
			}
		}

		public Table<CustomizeDefaultInfo> CustomizeDefaultInfo
		{
			get
			{
				return base.GetTable<CustomizeDefaultInfo>();
			}
		}

		public Table<CustomizeItemInfo> CustomizeItemInfo
		{
			get
			{
				return base.GetTable<CustomizeItemInfo>();
			}
		}

		public Table<CustomizePriceInfo> CustomizePriceInfo
		{
			get
			{
				return base.GetTable<CustomizePriceInfo>();
			}
		}

		public Table<CustomizeSetItemInfo> CustomizeSetItemInfo
		{
			get
			{
				return base.GetTable<CustomizeSetItemInfo>();
			}
		}

		public Table<DailyItemProcessInfo> DailyItemProcessInfo
		{
			get
			{
				return base.GetTable<DailyItemProcessInfo>();
			}
		}

		public Table<DecompositionCategory> DecompositionCategory
		{
			get
			{
				return base.GetTable<DecompositionCategory>();
			}
		}

		public Table<DecompositionItemInfo> DecompositionItemInfo
		{
			get
			{
				return base.GetTable<DecompositionItemInfo>();
			}
		}

		public Table<DefaultItemInfo> DefaultItemInfo
		{
			get
			{
				return base.GetTable<DefaultItemInfo>();
			}
		}

		public Table<DefaultPetSkillInfo> DefaultPetSkillInfo
		{
			get
			{
				return base.GetTable<DefaultPetSkillInfo>();
			}
		}

		public Table<DefaultSkillInfo> DefaultSkillInfo
		{
			get
			{
				return base.GetTable<DefaultSkillInfo>();
			}
		}

		public Table<DomainCIDR> DomainCIDR
		{
			get
			{
				return base.GetTable<DomainCIDR>();
			}
		}

		public Table<DomainPermissions> DomainPermissions
		{
			get
			{
				return base.GetTable<DomainPermissions>();
			}
		}

		public Table<DropItemInfo> DropItemInfo
		{
			get
			{
				return base.GetTable<DropItemInfo>();
			}
		}

		public Table<EnchantDiceInfo> EnchantDiceInfo
		{
			get
			{
				return base.GetTable<EnchantDiceInfo>();
			}
		}

		public Table<EnchantStatInfo> EnchantStatInfo
		{
			get
			{
				return base.GetTable<EnchantStatInfo>();
			}
		}

		public Table<EnhanceInfo> EnhanceInfo
		{
			get
			{
				return base.GetTable<EnhanceInfo>();
			}
		}

		public Table<EnhanceStatInfo> EnhanceStatInfo
		{
			get
			{
				return base.GetTable<EnhanceStatInfo>();
			}
		}

		public Table<Entity> Entity
		{
			get
			{
				return base.GetTable<Entity>();
			}
		}

		public Table<EntityAttribute> EntityAttribute
		{
			get
			{
				return base.GetTable<EntityAttribute>();
			}
		}

		public Table<EquipPartInfo> EquipPartInfo
		{
			get
			{
				return base.GetTable<EquipPartInfo>();
			}
		}

		public Table<EventTemplate> EventTemplate
		{
			get
			{
				return base.GetTable<EventTemplate>();
			}
		}

		public Table<EyebrowTypeInfo> EyebrowTypeInfo
		{
			get
			{
				return base.GetTable<EyebrowTypeInfo>();
			}
		}

		public Table<EyeColorInfo> EyeColorInfo
		{
			get
			{
				return base.GetTable<EyeColorInfo>();
			}
		}

		public Table<FeatureMatrix> FeatureMatrix
		{
			get
			{
				return base.GetTable<FeatureMatrix>();
			}
		}

		public Table<FoodInfo> FoodInfo
		{
			get
			{
				return base.GetTable<FoodInfo>();
			}
		}

		public Table<ForbiddenNameInfo> ForbiddenNameInfo
		{
			get
			{
				return base.GetTable<ForbiddenNameInfo>();
			}
		}

		public Table<ForbiddenWords> ForbiddenWords
		{
			get
			{
				return base.GetTable<ForbiddenWords>();
			}
		}

		public Table<ForbiddenWords_en_EU> ForbiddenWords_en_EU
		{
			get
			{
				return base.GetTable<ForbiddenWords_en_EU>();
			}
		}

		public Table<ForbiddenWords_en_US> ForbiddenWords_en_US
		{
			get
			{
				return base.GetTable<ForbiddenWords_en_US>();
			}
		}

		public Table<ForbiddenWords_ja_JP> ForbiddenWords_ja_JP
		{
			get
			{
				return base.GetTable<ForbiddenWords_ja_JP>();
			}
		}

		public Table<ForbiddenWords_zh_CN> ForbiddenWords_zh_CN
		{
			get
			{
				return base.GetTable<ForbiddenWords_zh_CN>();
			}
		}

		public Table<ForbiddenWords_zh_TW> ForbiddenWords_zh_TW
		{
			get
			{
				return base.GetTable<ForbiddenWords_zh_TW>();
			}
		}

		public Table<Friend_LevelupReward> Friend_LevelupReward
		{
			get
			{
				return base.GetTable<Friend_LevelupReward>();
			}
		}

		public Table<Friend_TitleReward> Friend_TitleReward
		{
			get
			{
				return base.GetTable<Friend_TitleReward>();
			}
		}

		public Table<GameTimeGroupInfo> GameTimeGroupInfo
		{
			get
			{
				return base.GetTable<GameTimeGroupInfo>();
			}
		}

		public Table<GoldenBallDropInfo> GoldenBallDropInfo
		{
			get
			{
				return base.GetTable<GoldenBallDropInfo>();
			}
		}

		public Table<GroupNexonID> GroupNexonID
		{
			get
			{
				return base.GetTable<GroupNexonID>();
			}
		}

		public Table<GroupPermissions> GroupPermissions
		{
			get
			{
				return base.GetTable<GroupPermissions>();
			}
		}

		public Table<GuildLevelBonusInfo> GuildLevelBonusInfo
		{
			get
			{
				return base.GetTable<GuildLevelBonusInfo>();
			}
		}

		public Table<GuildLevelUpInfo> GuildLevelUpInfo
		{
			get
			{
				return base.GetTable<GuildLevelUpInfo>();
			}
		}

		public Table<GuildScoreEvaluateInfo> GuildScoreEvaluateInfo
		{
			get
			{
				return base.GetTable<GuildScoreEvaluateInfo>();
			}
		}

		public Table<HairColorInfo> HairColorInfo
		{
			get
			{
				return base.GetTable<HairColorInfo>();
			}
		}

		public Table<HairTypeInfo> HairTypeInfo
		{
			get
			{
				return base.GetTable<HairTypeInfo>();
			}
		}

		public Table<HousingPropClassInfo> HousingPropClassInfo
		{
			get
			{
				return base.GetTable<HousingPropClassInfo>();
			}
		}

		public Table<HuntingAutoDropItemInfo> HuntingAutoDropItemInfo
		{
			get
			{
				return base.GetTable<HuntingAutoDropItemInfo>();
			}
		}

		public Table<HuntingAutoProbabilityInfo> HuntingAutoProbabilityInfo
		{
			get
			{
				return base.GetTable<HuntingAutoProbabilityInfo>();
			}
		}

		public Table<HuntingDistributionInfo> HuntingDistributionInfo
		{
			get
			{
				return base.GetTable<HuntingDistributionInfo>();
			}
		}

		public Table<HuntingDropItemInfo> HuntingDropItemInfo
		{
			get
			{
				return base.GetTable<HuntingDropItemInfo>();
			}
		}

		public Table<HuntingMonsterInfo> HuntingMonsterInfo
		{
			get
			{
				return base.GetTable<HuntingMonsterInfo>();
			}
		}

		public Table<HuntingSiteInfo> HuntingSiteInfo
		{
			get
			{
				return base.GetTable<HuntingSiteInfo>();
			}
		}

		public Table<HuntingSpawnInfo> HuntingSpawnInfo
		{
			get
			{
				return base.GetTable<HuntingSpawnInfo>();
			}
		}

		public Table<IME> IME
		{
			get
			{
				return base.GetTable<IME>();
			}
		}

		public Table<InnerArmorColorInfo> InnerArmorColorInfo
		{
			get
			{
				return base.GetTable<InnerArmorColorInfo>();
			}
		}

		public Table<InnerArmorTypeInfo> InnerArmorTypeInfo
		{
			get
			{
				return base.GetTable<InnerArmorTypeInfo>();
			}
		}

		public Table<ItemAttributeInfo> ItemAttributeInfo
		{
			get
			{
				return base.GetTable<ItemAttributeInfo>();
			}
		}

		public Table<ItemClassExtraInfo> ItemClassExtraInfo
		{
			get
			{
				return base.GetTable<ItemClassExtraInfo>();
			}
		}

		public Table<ItemEffectInfo> ItemEffectInfo
		{
			get
			{
				return base.GetTable<ItemEffectInfo>();
			}
		}

		public Table<ItemSetInfo> ItemSetInfo
		{
			get
			{
				return base.GetTable<ItemSetInfo>();
			}
		}

		public Table<ItemStatInfo> ItemStatInfo
		{
			get
			{
				return base.GetTable<ItemStatInfo>();
			}
		}

		public Table<ItemTradeLimitInfo> ItemTradeLimitInfo
		{
			get
			{
				return base.GetTable<ItemTradeLimitInfo>();
			}
		}

		public Table<JumpingBufflist> JumpingBufflist
		{
			get
			{
				return base.GetTable<JumpingBufflist>();
			}
		}

		public Table<JumpingPresetItem> JumpingPresetItem
		{
			get
			{
				return base.GetTable<JumpingPresetItem>();
			}
		}

		public Table<JumpingPresetSkill> JumpingPresetSkill
		{
			get
			{
				return base.GetTable<JumpingPresetSkill>();
			}
		}

		public Table<LevelUpExpInfo> LevelUpExpInfo
		{
			get
			{
				return base.GetTable<LevelUpExpInfo>();
			}
		}

		public Table<LocalizedText> LocalizedText
		{
			get
			{
				return base.GetTable<LocalizedText>();
			}
		}

		public Table<LogKey> LogKey
		{
			get
			{
				return base.GetTable<LogKey>();
			}
		}

		public Table<MailTypeInfo> MailTypeInfo
		{
			get
			{
				return base.GetTable<MailTypeInfo>();
			}
		}

		public Table<MakeupTypeInfo> MakeupTypeInfo
		{
			get
			{
				return base.GetTable<MakeupTypeInfo>();
			}
		}

		public Table<ManufactureDependencyInfo> ManufactureDependencyInfo
		{
			get
			{
				return base.GetTable<ManufactureDependencyInfo>();
			}
		}

		public Table<ManufactureInfo> ManufactureInfo
		{
			get
			{
				return base.GetTable<ManufactureInfo>();
			}
		}

		public Table<ManufactureMaterialInfo> ManufactureMaterialInfo
		{
			get
			{
				return base.GetTable<ManufactureMaterialInfo>();
			}
		}

		public Table<ManufactureRecipeInfo> ManufactureRecipeInfo
		{
			get
			{
				return base.GetTable<ManufactureRecipeInfo>();
			}
		}

		public Table<MaterialGroupInfo> MaterialGroupInfo
		{
			get
			{
				return base.GetTable<MaterialGroupInfo>();
			}
		}

		public Table<MDRActionFilter> MDRActionFilter
		{
			get
			{
				return base.GetTable<MDRActionFilter>();
			}
		}

		public Table<MicroPlayEffectInfo> MicroPlayEffectInfo
		{
			get
			{
				return base.GetTable<MicroPlayEffectInfo>();
			}
		}

		public Table<NamedPropDropAmountInfo> NamedPropDropAmountInfo
		{
			get
			{
				return base.GetTable<NamedPropDropAmountInfo>();
			}
		}

		public Table<NamedPropItemInfo> NamedPropItemInfo
		{
			get
			{
				return base.GetTable<NamedPropItemInfo>();
			}
		}

		public Table<PaintingTypeInfo> PaintingTypeInfo
		{
			get
			{
				return base.GetTable<PaintingTypeInfo>();
			}
		}

		public Table<PatternInfo> PatternInfo
		{
			get
			{
				return base.GetTable<PatternInfo>();
			}
		}

		public Table<PcCafeCountBonusEffectInfo> PcCafeCountBonusEffectInfo
		{
			get
			{
				return base.GetTable<PcCafeCountBonusEffectInfo>();
			}
		}

		public Table<PeriodicEventInfo> PeriodicEventInfo
		{
			get
			{
				return base.GetTable<PeriodicEventInfo>();
			}
		}

		public Table<PetActiveTimePeriodInfo> PetActiveTimePeriodInfo
		{
			get
			{
				return base.GetTable<PetActiveTimePeriodInfo>();
			}
		}

		public Table<PetFeedInfo> PetFeedInfo
		{
			get
			{
				return base.GetTable<PetFeedInfo>();
			}
		}

		public Table<PetInfo> PetInfo
		{
			get
			{
				return base.GetTable<PetInfo>();
			}
		}

		public Table<PetSkillInfo> PetSkillInfo
		{
			get
			{
				return base.GetTable<PetSkillInfo>();
			}
		}

		public Table<PetStatusBalanceInfo> PetStatusBalanceInfo
		{
			get
			{
				return base.GetTable<PetStatusBalanceInfo>();
			}
		}

		public Table<PlayerActions> PlayerActions
		{
			get
			{
				return base.GetTable<PlayerActions>();
			}
		}

		public Table<PropItemExpectationInfo> PropItemExpectationInfo
		{
			get
			{
				return base.GetTable<PropItemExpectationInfo>();
			}
		}

		public Table<PropItemInfo> PropItemInfo
		{
			get
			{
				return base.GetTable<PropItemInfo>();
			}
		}

		public Table<PropItemWeightInfo> PropItemWeightInfo
		{
			get
			{
				return base.GetTable<PropItemWeightInfo>();
			}
		}

		public Table<PvpInfo> PvpInfo
		{
			get
			{
				return base.GetTable<PvpInfo>();
			}
		}

		public Table<PvpPresetInfo> PvpPresetInfo
		{
			get
			{
				return base.GetTable<PvpPresetInfo>();
			}
		}

		public Table<QualityDiceInfo> QualityDiceInfo
		{
			get
			{
				return base.GetTable<QualityDiceInfo>();
			}
		}

		public Table<QualityStatInfo> QualityStatInfo
		{
			get
			{
				return base.GetTable<QualityStatInfo>();
			}
		}

		public Table<QuestConstraintInfo> QuestConstraintInfo
		{
			get
			{
				return base.GetTable<QuestConstraintInfo>();
			}
		}

		public Table<QuestDifficultyInfo> QuestDifficultyInfo
		{
			get
			{
				return base.GetTable<QuestDifficultyInfo>();
			}
		}

		public Table<QuestGoalInfo> QuestGoalInfo
		{
			get
			{
				return base.GetTable<QuestGoalInfo>();
			}
		}

		public Table<QuestPeriodInfo> QuestPeriodInfo
		{
			get
			{
				return base.GetTable<QuestPeriodInfo>();
			}
		}

		public Table<QuestSettingsConditionInfo> QuestSettingsConditionInfo
		{
			get
			{
				return base.GetTable<QuestSettingsConditionInfo>();
			}
		}

		public Table<QuestSettingsInfo> QuestSettingsInfo
		{
			get
			{
				return base.GetTable<QuestSettingsInfo>();
			}
		}

		public Table<RandomItemRecipe> RandomItemRecipe
		{
			get
			{
				return base.GetTable<RandomItemRecipe>();
			}
		}

		public Table<RandomItemRecipeClass> RandomItemRecipeClass
		{
			get
			{
				return base.GetTable<RandomItemRecipeClass>();
			}
		}

		public Table<RandomItemV2RareItemReset> RandomItemV2RareItemReset
		{
			get
			{
				return base.GetTable<RandomItemV2RareItemReset>();
			}
		}

		public Table<RandomItemV2SequenceReset> RandomItemV2SequenceReset
		{
			get
			{
				return base.GetTable<RandomItemV2SequenceReset>();
			}
		}

		public Table<RankCategory> RankCategory
		{
			get
			{
				return base.GetTable<RankCategory>();
			}
		}

		public Table<RankDescription> RankDescription
		{
			get
			{
				return base.GetTable<RankDescription>();
			}
		}

		public Table<RankPeriodInfo> RankPeriodInfo
		{
			get
			{
				return base.GetTable<RankPeriodInfo>();
			}
		}

		public Table<RankTitle> RankTitle
		{
			get
			{
				return base.GetTable<RankTitle>();
			}
		}

		public Table<RecipeInfo> RecipeInfo
		{
			get
			{
				return base.GetTable<RecipeInfo>();
			}
		}

		public Table<RecipeMaterialInfo> RecipeMaterialInfo
		{
			get
			{
				return base.GetTable<RecipeMaterialInfo>();
			}
		}

		public Table<RepairEnchantInfo> RepairEnchantInfo
		{
			get
			{
				return base.GetTable<RepairEnchantInfo>();
			}
		}

		public Table<RepairEnhanceInfo> RepairEnhanceInfo
		{
			get
			{
				return base.GetTable<RepairEnhanceInfo>();
			}
		}

		public Table<RepairEquipClassInfo> RepairEquipClassInfo
		{
			get
			{
				return base.GetTable<RepairEquipClassInfo>();
			}
		}

		public Table<RepairQualityInfo> RepairQualityInfo
		{
			get
			{
				return base.GetTable<RepairQualityInfo>();
			}
		}

		public Table<RequiredCoin> RequiredCoin
		{
			get
			{
				return base.GetTable<RequiredCoin>();
			}
		}

		public Table<RouletteComponentInfo> RouletteComponentInfo
		{
			get
			{
				return base.GetTable<RouletteComponentInfo>();
			}
		}

		public Table<RouletteSystemInfo> RouletteSystemInfo
		{
			get
			{
				return base.GetTable<RouletteSystemInfo>();
			}
		}

		public Table<ScarTypeInfo> ScarTypeInfo
		{
			get
			{
				return base.GetTable<ScarTypeInfo>();
			}
		}

		public Table<SectorGroupInfo> SectorGroupInfo
		{
			get
			{
				return base.GetTable<SectorGroupInfo>();
			}
		}

		public Table<Sectors> Sectors
		{
			get
			{
				return base.GetTable<Sectors>();
			}
		}

		public Table<ServerConstants> ServerConstants
		{
			get
			{
				return base.GetTable<ServerConstants>();
			}
		}

		public Table<SetEffectInfo> SetEffectInfo
		{
			get
			{
				return base.GetTable<SetEffectInfo>();
			}
		}

		public Table<SetInfo> SetInfo
		{
			get
			{
				return base.GetTable<SetInfo>();
			}
		}

		public Table<SetItemInfo> SetItemInfo
		{
			get
			{
				return base.GetTable<SetItemInfo>();
			}
		}

		public Table<ShipYardItemClassInfo> ShipYardItemClassInfo
		{
			get
			{
				return base.GetTable<ShipYardItemClassInfo>();
			}
		}

		public Table<SkillArgumentInfo> SkillArgumentInfo
		{
			get
			{
				return base.GetTable<SkillArgumentInfo>();
			}
		}

		public Table<SkillCategory> SkillCategory
		{
			get
			{
				return base.GetTable<SkillCategory>();
			}
		}

		public Table<SkillConstraintInfo> SkillConstraintInfo
		{
			get
			{
				return base.GetTable<SkillConstraintInfo>();
			}
		}

		public Table<SkillRankEnum> SkillRankEnum
		{
			get
			{
				return base.GetTable<SkillRankEnum>();
			}
		}

		public Table<SkillRankInfo> SkillRankInfo
		{
			get
			{
				return base.GetTable<SkillRankInfo>();
			}
		}

		public Table<SkinColorInfo> SkinColorInfo
		{
			get
			{
				return base.GetTable<SkinColorInfo>();
			}
		}

		public Table<SpiritInjectionDiceInfo> SpiritInjectionDiceInfo
		{
			get
			{
				return base.GetTable<SpiritInjectionDiceInfo>();
			}
		}

		public Table<SpiritInjectionForbiddenStat> SpiritInjectionForbiddenStat
		{
			get
			{
				return base.GetTable<SpiritInjectionForbiddenStat>();
			}
		}

		public Table<SpiritInjectionRequiredInfo> SpiritInjectionRequiredInfo
		{
			get
			{
				return base.GetTable<SpiritInjectionRequiredInfo>();
			}
		}

		public Table<SpiritInjectionStatInfo> SpiritInjectionStatInfo
		{
			get
			{
				return base.GetTable<SpiritInjectionStatInfo>();
			}
		}

		public Table<Stage> Stage
		{
			get
			{
				return base.GetTable<Stage>();
			}
		}

		public Table<StageInfo> StageInfo
		{
			get
			{
				return base.GetTable<StageInfo>();
			}
		}

		public Table<StatInfo> StatInfo
		{
			get
			{
				return base.GetTable<StatInfo>();
			}
		}

		public Table<StoryGoalInfo> StoryGoalInfo
		{
			get
			{
				return base.GetTable<StoryGoalInfo>();
			}
		}

		public Table<StoryLineDropItemInfo> StoryLineDropItemInfo
		{
			get
			{
				return base.GetTable<StoryLineDropItemInfo>();
			}
		}

		public Table<StoryLinePeriodInfo> StoryLinePeriodInfo
		{
			get
			{
				return base.GetTable<StoryLinePeriodInfo>();
			}
		}

		public Table<StoryNpcTalkInfo> StoryNpcTalkInfo
		{
			get
			{
				return base.GetTable<StoryNpcTalkInfo>();
			}
		}

		public Table<StoryTokenInfo> StoryTokenInfo
		{
			get
			{
				return base.GetTable<StoryTokenInfo>();
			}
		}

		public Table<SynthesisInfo> SynthesisInfo
		{
			get
			{
				return base.GetTable<SynthesisInfo>();
			}
		}

		public Table<TempTitle> TempTitle
		{
			get
			{
				return base.GetTable<TempTitle>();
			}
		}

		public Table<TipOfTheDay> TipOfTheDay
		{
			get
			{
				return base.GetTable<TipOfTheDay>();
			}
		}

		public Table<TitleGoalInfo> TitleGoalInfo
		{
			get
			{
				return base.GetTable<TitleGoalInfo>();
			}
		}

		public Table<TitleStatInfo> TitleStatInfo
		{
			get
			{
				return base.GetTable<TitleStatInfo>();
			}
		}

		public Table<TowerModuleSectorInfo> TowerModuleSectorInfo
		{
			get
			{
				return base.GetTable<TowerModuleSectorInfo>();
			}
		}

		public Table<TowerModuleSettingsInfo> TowerModuleSettingsInfo
		{
			get
			{
				return base.GetTable<TowerModuleSettingsInfo>();
			}
		}

		public Table<TownCampfireStatusInfo> TownCampfireStatusInfo
		{
			get
			{
				return base.GetTable<TownCampfireStatusInfo>();
			}
		}

		public Table<TownVisualEffectInfo> TownVisualEffectInfo
		{
			get
			{
				return base.GetTable<TownVisualEffectInfo>();
			}
		}

		public Table<TreasureBoxDropItemInfo> TreasureBoxDropItemInfo
		{
			get
			{
				return base.GetTable<TreasureBoxDropItemInfo>();
			}
		}

		public Table<TreasureMapInfo> TreasureMapInfo
		{
			get
			{
				return base.GetTable<TreasureMapInfo>();
			}
		}

		public Table<TriggerDropInfo> TriggerDropInfo
		{
			get
			{
				return base.GetTable<TriggerDropInfo>();
			}
		}

		public Table<UserToStoryLine> UserToStoryLine
		{
			get
			{
				return base.GetTable<UserToStoryLine>();
			}
		}

		public Table<VocationLevelInfo> VocationLevelInfo
		{
			get
			{
				return base.GetTable<VocationLevelInfo>();
			}
		}

		public Table<VocationSkillConstraintInfo> VocationSkillConstraintInfo
		{
			get
			{
				return base.GetTable<VocationSkillConstraintInfo>();
			}
		}

		public Table<VocationSkillInfo> VocationSkillInfo
		{
			get
			{
				return base.GetTable<VocationSkillInfo>();
			}
		}

		public Table<AllUserGoalInfo> AllUserGoalInfo
		{
			get
			{
				return base.GetTable<AllUserGoalInfo>();
			}
		}

		public Table<SpiritInjectionConstraint> SpiritInjectionConstraint
		{
			get
			{
				return base.GetTable<SpiritInjectionConstraint>();
			}
		}

		public Table<SynthesisItemClass> SynthesisItemClass
		{
			get
			{
				return base.GetTable<SynthesisItemClass>();
			}
		}

		public Table<SynthesisSkillBonus> SynthesisSkillBonus
		{
			get
			{
				return base.GetTable<SynthesisSkillBonus>();
			}
		}

		public Table<SynthesisProbability> SynthesisProbability
		{
			get
			{
				return base.GetTable<SynthesisProbability>();
			}
		}

		public Table<SynthesisGradeStat> SynthesisGradeStat
		{
			get
			{
				return base.GetTable<SynthesisGradeStat>();
			}
		}

		public Table<EventDropItemInfo> EventDropItemInfo
		{
			get
			{
				return base.GetTable<EventDropItemInfo>();
			}
		}

		public Table<MonsterInfo> MonsterInfo
		{
			get
			{
				return base.GetTable<MonsterInfo>();
			}
		}

		public Table<EnhanceServerInfo> EnhanceServerInfo
		{
			get
			{
				return base.GetTable<EnhanceServerInfo>();
			}
		}

		public Table<VariableStatInfo> VariableStatInfo
		{
			get
			{
				return base.GetTable<VariableStatInfo>();
			}
		}

		public Table<GemstoneInfo> GemstoneInfo
		{
			get
			{
				return base.GetTable<GemstoneInfo>();
			}
		}

		public Table<RouletteBoardInfo> RouletteBoardInfo
		{
			get
			{
				return base.GetTable<RouletteBoardInfo>();
			}
		}

		public Table<StoryLineInfo> StoryLineInfo
		{
			get
			{
				return base.GetTable<StoryLineInfo>();
			}
		}

		public Table<Cashshop_PreviewInfo> Cashshop_PreviewInfo
		{
			get
			{
				return base.GetTable<Cashshop_PreviewInfo>();
			}
		}

		public Table<TitleInfo> TitleInfo
		{
			get
			{
				return base.GetTable<TitleInfo>();
			}
		}

		public Table<ItemClassInfo> ItemClassInfo
		{
			get
			{
				return base.GetTable<ItemClassInfo>();
			}
		}

		public Table<RandomItem> RandomItem
		{
			get
			{
				return base.GetTable<RandomItem>();
			}
		}

		public Table<RandomItemV2> RandomItemV2
		{
			get
			{
				return base.GetTable<RandomItemV2>();
			}
		}

		public Table<DormantReward> DormantReward
		{
			get
			{
				return base.GetTable<DormantReward>();
			}
		}

		public Table<ShopInfo> ShopInfo
		{
			get
			{
				return base.GetTable<ShopInfo>();
			}
		}

		public Table<MarbleDiceInfo> MarbleDiceInfo
		{
			get
			{
				return base.GetTable<MarbleDiceInfo>();
			}
		}

		public Table<MarbleNodeInfo> MarbleNodeInfo
		{
			get
			{
				return base.GetTable<MarbleNodeInfo>();
			}
		}

		public Table<MarbleBoardInfo> MarbleBoardInfo
		{
			get
			{
				return base.GetTable<MarbleBoardInfo>();
			}
		}

		public Table<DyeableParts> DyeableParts
		{
			get
			{
				return base.GetTable<DyeableParts>();
			}
		}

		public Table<EnhanceInsteadMaterialInfo> EnhanceInsteadMaterialInfo
		{
			get
			{
				return base.GetTable<EnhanceInsteadMaterialInfo>();
			}
		}

		public Table<EnhanceBonusServerInfo> EnhanceBonusServerInfo
		{
			get
			{
				return base.GetTable<EnhanceBonusServerInfo>();
			}
		}

		public Table<CouponShopItemInfo> CouponShopItemInfo
		{
			get
			{
				return base.GetTable<CouponShopItemInfo>();
			}
		}

		public Table<CouponShopVersionInfo> CouponShopVersionInfo
		{
			get
			{
				return base.GetTable<CouponShopVersionInfo>();
			}
		}

		public Table<ChangingMaterialGroupInfo> ChangingMaterialGroupInfo
		{
			get
			{
				return base.GetTable<ChangingMaterialGroupInfo>();
			}
		}

		public Table<AbilityClassGroupInfo> AbilityClassGroupInfo
		{
			get
			{
				return base.GetTable<AbilityClassGroupInfo>();
			}
		}

		public Table<CaptchaWordInfo> CaptchaWordInfo
		{
			get
			{
				return base.GetTable<CaptchaWordInfo>();
			}
		}

		public Table<CombineCraftInfo> CombineCraftInfo
		{
			get
			{
				return base.GetTable<CombineCraftInfo>();
			}
		}

		public Table<MonsterDropItemInfo> MonsterDropItemInfo
		{
			get
			{
				return base.GetTable<MonsterDropItemInfo>();
			}
		}

		public Table<SkillEnhanceInfo> SkillEnhanceInfo
		{
			get
			{
				return base.GetTable<SkillEnhanceInfo>();
			}
		}

		public Table<SkillEnhanceMaterialInfo> SkillEnhanceMaterialInfo
		{
			get
			{
				return base.GetTable<SkillEnhanceMaterialInfo>();
			}
		}

		public Table<SkillInfo> SkillInfo
		{
			get
			{
				return base.GetTable<SkillInfo>();
			}
		}

		public Table<SynthesisPackage> SynthesisPackage
		{
			get
			{
				return base.GetTable<SynthesisPackage>();
			}
		}

		public Table<EnchantInfo> EnchantInfo
		{
			get
			{
				return base.GetTable<EnchantInfo>();
			}
		}

		public Table<EnhanceRuneInfo> EnhanceRuneInfo
		{
			get
			{
				return base.GetTable<EnhanceRuneInfo>();
			}
		}

		public Table<EquipItemInfo> EquipItemInfo
		{
			get
			{
				return base.GetTable<EquipItemInfo>();
			}
		}

		public Table<CombineCraftPartsInfo> CombineCraftPartsInfo
		{
			get
			{
				return base.GetTable<CombineCraftPartsInfo>();
			}
		}

		public Table<StatusEffectGroupInfo> StatusEffectGroupInfo
		{
			get
			{
				return base.GetTable<StatusEffectGroupInfo>();
			}
		}

		public Table<HotSpringPotionInfo> HotSpringPotionInfo
		{
			get
			{
				return base.GetTable<HotSpringPotionInfo>();
			}
		}

		public Table<CaptchaWordInfo_zhCN> CaptchaWordInfo_zhCN
		{
			get
			{
				return base.GetTable<CaptchaWordInfo_zhCN>();
			}
		}

		public Table<AbilityClassInfo> AbilityClassInfo
		{
			get
			{
				return base.GetTable<AbilityClassInfo>();
			}
		}

		public Table<QuestInfo> QuestInfo
		{
			get
			{
				return base.GetTable<QuestInfo>();
			}
		}

		public Table<BurnCheckInput> BurnCheckInput
		{
			get
			{
				return base.GetTable<BurnCheckInput>();
			}
		}

		public Table<TodayMissionInfo> TodayMissionInfo
		{
			get
			{
				return base.GetTable<TodayMissionInfo>();
			}
		}

		public Table<EnhanceRuneExtraInfo> EnhanceRuneExtraInfo
		{
			get
			{
				return base.GetTable<EnhanceRuneExtraInfo>();
			}
		}

		public Table<BurnJackpot> BurnJackpot
		{
			get
			{
				return base.GetTable<BurnJackpot>();
			}
		}

		public Table<TodayMissionGoalInfo> TodayMissionGoalInfo
		{
			get
			{
				return base.GetTable<TodayMissionGoalInfo>();
			}
		}

		public Table<RandomItemV2Recipe> RandomItemV2Recipe
		{
			get
			{
				return base.GetTable<RandomItemV2Recipe>();
			}
		}

		public Table<FlexibleMapLoadingInfo> FlexibleMapLoadingInfo
		{
			get
			{
				return base.GetTable<FlexibleMapLoadingInfo>();
			}
		}

		public Table<GuildForbiddenWords> GuildForbiddenWords
		{
			get
			{
				return base.GetTable<GuildForbiddenWords>();
			}
		}

		public Table<StatusEffectGroupInfo_Extension> StatusEffectGroupInfo_Extension
		{
			get
			{
				return base.GetTable<StatusEffectGroupInfo_Extension>();
			}
		}

		private static MappingSource mappingSource = new AttributeMappingSource();
	}
}
