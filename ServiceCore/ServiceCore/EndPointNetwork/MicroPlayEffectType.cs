using System;

namespace ServiceCore.EndPointNetwork
{
	public enum MicroPlayEffectType
	{
		None,
		Revive,
		ReviveByClient,
		Repair,
		RepairByClient,
		GainHealth,
		GainHelathByClient,
		GetStatus,
		GetStatusByClient,
		RemoveHitRecord,
		Hurt,
		HurtByClient,
		SetHealth,
		SetHealthByClient,
		Kill,
		KillByClient,
		ReduceMinClearTime
	}
}
