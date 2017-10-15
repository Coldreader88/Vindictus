using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.CompilerServices;
using Utility;

namespace ServiceCore.HeroesContents
{
    [Table(Name = "dbo.StatusEffectGroupInfo")]
    public class StatusEffectGroupInfo
    {
        private List<StatusEffectGroupInfo.Effect> effects;

        private StatusEffectType statusEffectType;

        private Dictionary<string, int> statAdder;

        private string _Type;

        private int? _Level;

        private int? _Abslevel;

        private string _IDTag;

        private int? _IgnoreTag;

        private int? _Duration;

        private int? _CombatCount;

        private int? _IsCurse;

        private int? _Dispellable;

        private int? _CheckCaster;

        private int? _RemoveOnDeath;

        private int? _RemoveOnSectorChange;

        private int? _RemoveOnMouseClick;

        private int? _InfectiousRadius;

        private int? _InfectiousTime;

        private int? _MaxInfectiousCount;

        private int? _InfectAll;

        private int? _InfecteePropagation;

        private int? _InfecteeDuration;

        private int? _InfectPersistency;

        private int? _ToolTipState;

        private string _Effect1;

        private string _Effect1_Arg1;

        private string _Effect1_Arg2;

        private string _Effect1_Arg3;

        private string _Effect2;

        private string _Effect2_Arg1;

        private string _Effect2_Arg2;

        private string _Effect2_Arg3;

        private string _Effect3;

        private string _Effect3_Arg1;

        private string _Effect3_Arg2;

        private string _Effect3_Arg3;

        private string _IconType;

        private string _NameLocalizedText;

        private string _DescLocalizedText;

        private string _StartEFX;

        private string _StartSound;

        private string _EndEFX;

        private string _EndSound;

        private string _Facial;

        private int? _ServerSide;

        private string _ServerEffect1;

        private string _ServerEffect1_Arg1;

        private string _ServerEffect1_Arg2;

        private string _ServerEffect1_Arg3;

        private string _ServerEffect2;

        private string _ServerEffect2_Arg1;

        private string _ServerEffect2_Arg2;

        private string _ServerEffect2_Arg3;

        private string _ServerEffect3;

        private string _ServerEffect3_Arg1;

        private string _ServerEffect3_Arg2;

        private string _ServerEffect3_Arg3;

        private int? _RealTimePeriod;

        private string _ClientEffect;

        private string _ServerEffect;

        [Column(Storage = "_Abslevel", DbType = "Int")]
        public int? Abslevel
        {
            get
            {
                return _Abslevel;
            }
            set
            {
                int? nullable = _Abslevel;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _Abslevel = value;
                }
            }
        }

        [Column(Storage = "_CheckCaster", DbType = "Int")]
        public int? CheckCaster
        {
            get
            {
                return _CheckCaster;
            }
            set
            {
                int? nullable = _CheckCaster;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _CheckCaster = value;
                }
            }
        }

        [Column(Storage = "_ClientEffect", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ClientEffect
        {
            get
            {
                return _ClientEffect;
            }
            set
            {
                if (_ClientEffect != value)
                {
                    _ClientEffect = value;
                }
            }
        }

        [Column(Storage = "_CombatCount", DbType = "Int")]
        public int? CombatCount
        {
            get
            {
                return _CombatCount;
            }
            set
            {
                int? nullable = _CombatCount;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _CombatCount = value;
                }
            }
        }

        [Column(Storage = "_DescLocalizedText", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string DescLocalizedText
        {
            get
            {
                return _DescLocalizedText;
            }
            set
            {
                if (_DescLocalizedText != value)
                {
                    _DescLocalizedText = value;
                }
            }
        }

        [Column(Storage = "_Dispellable", DbType = "Int")]
        public int? Dispellable
        {
            get
            {
                return _Dispellable;
            }
            set
            {
                int? nullable = _Dispellable;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _Dispellable = value;
                }
            }
        }

        [Column(Storage = "_Duration", DbType = "Int")]
        public int? Duration
        {
            get
            {
                return _Duration;
            }
            set
            {
                int? nullable = _Duration;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _Duration = value;
                }
            }
        }

        [Column(Storage = "_Effect1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect1
        {
            get
            {
                return _Effect1;
            }
            set
            {
                if (_Effect1 != value)
                {
                    _Effect1 = value;
                }
            }
        }

        [Column(Storage = "_Effect1_Arg1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect1_Arg1
        {
            get
            {
                return _Effect1_Arg1;
            }
            set
            {
                if (_Effect1_Arg1 != value)
                {
                    _Effect1_Arg1 = value;
                }
            }
        }

        [Column(Storage = "_Effect1_Arg2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect1_Arg2
        {
            get
            {
                return _Effect1_Arg2;
            }
            set
            {
                if (_Effect1_Arg2 != value)
                {
                    _Effect1_Arg2 = value;
                }
            }
        }

        [Column(Storage = "_Effect1_Arg3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect1_Arg3
        {
            get
            {
                return _Effect1_Arg3;
            }
            set
            {
                if (_Effect1_Arg3 != value)
                {
                    _Effect1_Arg3 = value;
                }
            }
        }

        [Column(Storage = "_Effect2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect2
        {
            get
            {
                return _Effect2;
            }
            set
            {
                if (_Effect2 != value)
                {
                    _Effect2 = value;
                }
            }
        }

        [Column(Storage = "_Effect2_Arg1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect2_Arg1
        {
            get
            {
                return _Effect2_Arg1;
            }
            set
            {
                if (_Effect2_Arg1 != value)
                {
                    _Effect2_Arg1 = value;
                }
            }
        }

        [Column(Storage = "_Effect2_Arg2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect2_Arg2
        {
            get
            {
                return _Effect2_Arg2;
            }
            set
            {
                if (_Effect2_Arg2 != value)
                {
                    _Effect2_Arg2 = value;
                }
            }
        }

        [Column(Storage = "_Effect2_Arg3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect2_Arg3
        {
            get
            {
                return _Effect2_Arg3;
            }
            set
            {
                if (_Effect2_Arg3 != value)
                {
                    _Effect2_Arg3 = value;
                }
            }
        }

        [Column(Storage = "_Effect3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect3
        {
            get
            {
                return _Effect3;
            }
            set
            {
                if (_Effect3 != value)
                {
                    _Effect3 = value;
                }
            }
        }

        [Column(Storage = "_Effect3_Arg1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect3_Arg1
        {
            get
            {
                return _Effect3_Arg1;
            }
            set
            {
                if (_Effect3_Arg1 != value)
                {
                    _Effect3_Arg1 = value;
                }
            }
        }

        [Column(Storage = "_Effect3_Arg2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect3_Arg2
        {
            get
            {
                return _Effect3_Arg2;
            }
            set
            {
                if (_Effect3_Arg2 != value)
                {
                    _Effect3_Arg2 = value;
                }
            }
        }

        [Column(Storage = "_Effect3_Arg3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Effect3_Arg3
        {
            get
            {
                return _Effect3_Arg3;
            }
            set
            {
                if (_Effect3_Arg3 != value)
                {
                    _Effect3_Arg3 = value;
                }
            }
        }

        public List<StatusEffectGroupInfo.Effect> Effects
        {
            get
            {
                if (effects == null)
                {
                    Build();
                }
                return effects;
            }
        }

        [Column(Storage = "_EndEFX", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string EndEFX
        {
            get
            {
                return _EndEFX;
            }
            set
            {
                if (_EndEFX != value)
                {
                    _EndEFX = value;
                }
            }
        }

        [Column(Storage = "_EndSound", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string EndSound
        {
            get
            {
                return _EndSound;
            }
            set
            {
                if (_EndSound != value)
                {
                    _EndSound = value;
                }
            }
        }

        [Column(Storage = "_Facial", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Facial
        {
            get
            {
                return _Facial;
            }
            set
            {
                if (_Facial != value)
                {
                    _Facial = value;
                }
            }
        }

        [Column(Storage = "_IconType", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string IconType
        {
            get
            {
                return _IconType;
            }
            set
            {
                if (_IconType != value)
                {
                    _IconType = value;
                }
            }
        }

        [Column(Storage = "_IDTag", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string IDTag
        {
            get
            {
                return _IDTag;
            }
            set
            {
                if (_IDTag != value)
                {
                    _IDTag = value;
                }
            }
        }

        [Column(Storage = "_IgnoreTag", DbType = "Int")]
        public int? IgnoreTag
        {
            get
            {
                return _IgnoreTag;
            }
            set
            {
                int? nullable = _IgnoreTag;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _IgnoreTag = value;
                }
            }
        }

        [Column(Storage = "_InfectAll", DbType = "Int")]
        public int? InfectAll
        {
            get
            {
                return _InfectAll;
            }
            set
            {
                int? nullable = _InfectAll;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _InfectAll = value;
                }
            }
        }

        [Column(Storage = "_InfecteeDuration", DbType = "Int")]
        public int? InfecteeDuration
        {
            get
            {
                return _InfecteeDuration;
            }
            set
            {
                int? nullable = _InfecteeDuration;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _InfecteeDuration = value;
                }
            }
        }

        [Column(Storage = "_InfecteePropagation", DbType = "Int")]
        public int? InfecteePropagation
        {
            get
            {
                return _InfecteePropagation;
            }
            set
            {
                int? nullable = _InfecteePropagation;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _InfecteePropagation = value;
                }
            }
        }

        [Column(Storage = "_InfectiousRadius", DbType = "Int")]
        public int? InfectiousRadius
        {
            get
            {
                return _InfectiousRadius;
            }
            set
            {
                int? nullable = _InfectiousRadius;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _InfectiousRadius = value;
                }
            }
        }

        [Column(Storage = "_InfectiousTime", DbType = "Int")]
        public int? InfectiousTime
        {
            get
            {
                return _InfectiousTime;
            }
            set
            {
                int? nullable = _InfectiousTime;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _InfectiousTime = value;
                }
            }
        }

        [Column(Storage = "_InfectPersistency", DbType = "Int")]
        public int? InfectPersistency
        {
            get
            {
                return _InfectPersistency;
            }
            set
            {
                int? nullable = _InfectPersistency;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _InfectPersistency = value;
                }
            }
        }

        [Column(Storage = "_IsCurse", DbType = "Int")]
        public int? IsCurse
        {
            get
            {
                return _IsCurse;
            }
            set
            {
                int? nullable = _IsCurse;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _IsCurse = value;
                }
            }
        }

        [Column(Name = "[Level]", Storage = "_Level", DbType = "Int")]
        public int? Level
        {
            get
            {
                return _Level;
            }
            set
            {
                int? nullable = _Level;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _Level = value;
                }
            }
        }

        [Column(Storage = "_MaxInfectiousCount", DbType = "Int")]
        public int? MaxInfectiousCount
        {
            get
            {
                return _MaxInfectiousCount;
            }
            set
            {
                int? nullable = _MaxInfectiousCount;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _MaxInfectiousCount = value;
                }
            }
        }

        [Column(Storage = "_NameLocalizedText", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string NameLocalizedText
        {
            get
            {
                return _NameLocalizedText;
            }
            set
            {
                if (_NameLocalizedText != value)
                {
                    _NameLocalizedText = value;
                }
            }
        }

        [Column(Storage = "_RealTimePeriod", DbType = "Int")]
        public int? RealTimePeriod
        {
            get
            {
                return _RealTimePeriod;
            }
            set
            {
                int? nullable = _RealTimePeriod;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _RealTimePeriod = value;
                }
            }
        }

        [Column(Storage = "_RemoveOnDeath", DbType = "Int")]
        public int? RemoveOnDeath
        {
            get
            {
                return _RemoveOnDeath;
            }
            set
            {
                int? nullable = _RemoveOnDeath;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _RemoveOnDeath = value;
                }
            }
        }

        [Column(Storage = "_RemoveOnMouseClick", DbType = "Int")]
        public int? RemoveOnMouseClick
        {
            get
            {
                return _RemoveOnMouseClick;
            }
            set
            {
                int? nullable = _RemoveOnMouseClick;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _RemoveOnMouseClick = value;
                }
            }
        }

        [Column(Storage = "_RemoveOnSectorChange", DbType = "Int")]
        public int? RemoveOnSectorChange
        {
            get
            {
                return _RemoveOnSectorChange;
            }
            set
            {
                int? nullable = _RemoveOnSectorChange;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _RemoveOnSectorChange = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect
        {
            get
            {
                return _ServerEffect;
            }
            set
            {
                if (_ServerEffect != value)
                {
                    _ServerEffect = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect1
        {
            get
            {
                return _ServerEffect1;
            }
            set
            {
                if (_ServerEffect1 != value)
                {
                    _ServerEffect1 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect1_Arg1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect1_Arg1
        {
            get
            {
                return _ServerEffect1_Arg1;
            }
            set
            {
                if (_ServerEffect1_Arg1 != value)
                {
                    _ServerEffect1_Arg1 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect1_Arg2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect1_Arg2
        {
            get
            {
                return _ServerEffect1_Arg2;
            }
            set
            {
                if (_ServerEffect1_Arg2 != value)
                {
                    _ServerEffect1_Arg2 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect1_Arg3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect1_Arg3
        {
            get
            {
                return _ServerEffect1_Arg3;
            }
            set
            {
                if (_ServerEffect1_Arg3 != value)
                {
                    _ServerEffect1_Arg3 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect2
        {
            get
            {
                return _ServerEffect2;
            }
            set
            {
                if (_ServerEffect2 != value)
                {
                    _ServerEffect2 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect2_Arg1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect2_Arg1
        {
            get
            {
                return _ServerEffect2_Arg1;
            }
            set
            {
                if (_ServerEffect2_Arg1 != value)
                {
                    _ServerEffect2_Arg1 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect2_Arg2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect2_Arg2
        {
            get
            {
                return _ServerEffect2_Arg2;
            }
            set
            {
                if (_ServerEffect2_Arg2 != value)
                {
                    _ServerEffect2_Arg2 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect2_Arg3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect2_Arg3
        {
            get
            {
                return _ServerEffect2_Arg3;
            }
            set
            {
                if (_ServerEffect2_Arg3 != value)
                {
                    _ServerEffect2_Arg3 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect3
        {
            get
            {
                return _ServerEffect3;
            }
            set
            {
                if (_ServerEffect3 != value)
                {
                    _ServerEffect3 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect3_Arg1", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect3_Arg1
        {
            get
            {
                return _ServerEffect3_Arg1;
            }
            set
            {
                if (_ServerEffect3_Arg1 != value)
                {
                    _ServerEffect3_Arg1 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect3_Arg2", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect3_Arg2
        {
            get
            {
                return _ServerEffect3_Arg2;
            }
            set
            {
                if (_ServerEffect3_Arg2 != value)
                {
                    _ServerEffect3_Arg2 = value;
                }
            }
        }

        [Column(Storage = "_ServerEffect3_Arg3", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string ServerEffect3_Arg3
        {
            get
            {
                return _ServerEffect3_Arg3;
            }
            set
            {
                if (_ServerEffect3_Arg3 != value)
                {
                    _ServerEffect3_Arg3 = value;
                }
            }
        }

        [Column(Storage = "_ServerSide", DbType = "Int")]
        public int? ServerSide
        {
            get
            {
                return _ServerSide;
            }
            set
            {
                int? nullable = _ServerSide;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _ServerSide = value;
                }
            }
        }

        [Column(Storage = "_StartEFX", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string StartEFX
        {
            get
            {
                return _StartEFX;
            }
            set
            {
                if (_StartEFX != value)
                {
                    _StartEFX = value;
                }
            }
        }

        [Column(Storage = "_StartSound", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string StartSound
        {
            get
            {
                return _StartSound;
            }
            set
            {
                if (_StartSound != value)
                {
                    _StartSound = value;
                }
            }
        }

        public Dictionary<string, int> StatAdder
        {
            get
            {
                if (effects == null)
                {
                    Build();
                }
                return statAdder;
            }
        }

        public StatusEffectType StatusEffectType
        {
            get
            {
                if (effects == null)
                {
                    Build();
                }
                return statusEffectType;
            }
        }

        [Column(Storage = "_ToolTipState", DbType = "Int")]
        public int? ToolTipState
        {
            get
            {
                return _ToolTipState;
            }
            set
            {
                int? nullable = _ToolTipState;
                int? nullable1 = value;
                if ((nullable.GetValueOrDefault() != nullable1.GetValueOrDefault() ? true : nullable.HasValue != nullable1.HasValue))
                {
                    _ToolTipState = value;
                }
            }
        }

        [Column(Storage = "_Type", DbType = "Text", UpdateCheck = UpdateCheck.Never)]
        public string Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                }
            }
        }

        public StatusEffectGroupInfo()
        {
        }

        public void Build()
        {
            effects = new List<StatusEffectGroupInfo.Effect>();
            if (ServerEffect1 != null)
            {
                effects.Add(new StatusEffectGroupInfo.Effect(ServerEffect1, ServerEffect1_Arg1, ServerEffect1_Arg2, ServerEffect1_Arg3));
            }
            if (ServerEffect2 != null)
            {
                effects.Add(new StatusEffectGroupInfo.Effect(ServerEffect2, ServerEffect2_Arg1, ServerEffect2_Arg2, ServerEffect2_Arg3));
            }
            if (ServerEffect3 != null)
            {
                effects.Add(new StatusEffectGroupInfo.Effect(ServerEffect3, ServerEffect3_Arg1, ServerEffect3_Arg2, ServerEffect3_Arg3));
            }
            if (ServerEffect != null)
            {
                string[] strArrays = ServerEffect.Split(new char[] { ',' });
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    StatusEffectGroupInfo.Effect effect = new StatusEffectGroupInfo.Effect(strArrays[i]);
                    if (effect.EffectKey != "")
                    {
                        effects.Add(effect);
                    }
                }
            }
            int? serverSide = ServerSide;
            int valueOrDefault = serverSide.GetValueOrDefault();
            if (serverSide.HasValue)
            {
                switch (valueOrDefault)
                {
                    case 0:
                        {
                            statusEffectType = StatusEffectType.Client;
                            goto LabelstatusEffect;
                        }
                    case 1:
                        {
                            int? realTimePeriod = RealTimePeriod;
                            if ((realTimePeriod.GetValueOrDefault() != 1 ? true : !realTimePeriod.HasValue))
                            {
                                statusEffectType = StatusEffectType.UseRemainTime;
                                goto LabelstatusEffect;
                            }
                            else
                            {
                                statusEffectType = StatusEffectType.UseExpireTime;
                                goto LabelstatusEffect;
                            }
                        }
                }
            }
            statusEffectType = StatusEffectType.DeleteOnLogOut;


            LabelstatusEffect:
            statAdder = new Dictionary<string, int>();
            foreach (StatusEffectGroupInfo.Effect effect1 in effects)
            {
                if (effect1.EffectKey != "stat_adder")
                {
                    continue;
                }
                string argString = effect1.GetArgString(1, null);
                string str = effect1.GetArgString(2, null);
                if (argString == null || str == null)
                {
                    continue;
                }
                char[] chrArray = new char[] { ';' };
                string[] strArrays1 = argString.Trim().Split(chrArray);
                string[] strArrays2 = str.Trim().Split(chrArray);
                if (1 >= (int)strArrays1.Length || (int)strArrays1.Length != (int)strArrays2.Length)
                {
                    StatAdder.AddOrIncrease<string>(argString, Convert.ToInt32(str));
                }
                else
                {
                    int num = 0;
                    string[] strArrays3 = strArrays1;
                    for (int j = 0; j < (int)strArrays3.Length; j++)
                    {
                        string str1 = strArrays3[j];
                        int num1 = num;
                        num = num1 + 1;
                        StatAdder.AddOrIncrease<string>(str1, Convert.ToInt32(strArrays2[num1]));
                    }
                }
            }
        }

        public class Effect
        {
            public List<string> ArgList
            {
                get;
                set;
            }

            public string EffectKey
            {
                get
                {
                    if (ArgList.Count <= 0)
                    {
                        return "";
                    }
                    return ArgList[0];
                }
            }

            public Effect(string argList)
            {
                string str = argList.Trim();
                char[] chrArray = new char[] { ' ' };
                ArgList = (
                    from x in str.Split(chrArray)
                    select x.Trim()).ToList<string>();
            }

            public Effect(string effect, string arg1, string arg2, string arg3)
            {
                ArgList = new List<string>()
                {
                    effect,
                    arg1,
                    arg2,
                    arg3
                };
            }

            public float GetArgFloat(int index, float defaultValue)
            {
                float single;
                if (index < 0 || ArgList.Count <= index)
                {
                    return defaultValue;
                }
                if (float.TryParse(ArgList[index], out single))
                {
                    return single;
                }
                return defaultValue;
            }

            public int GetArgInt(int index, int defaultValue)
            {
                int num;
                if (index < 0 || ArgList.Count <= index)
                {
                    return defaultValue;
                }
                if (int.TryParse(ArgList[index], out num))
                {
                    return num;
                }
                return defaultValue;
            }

            public string GetArgString(int index, string defaultValue)
            {
                if (index < 0 || ArgList.Count <= index)
                {
                    return defaultValue;
                }
                if (ArgList[index] == null)
                {
                    return defaultValue;
                }
                return ArgList[index];
            }
        }
    }
}