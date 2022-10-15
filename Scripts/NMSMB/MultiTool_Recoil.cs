//=============================================================================
// Low|no recoil for weapons, intended for play-as Fighter.
//=============================================================================

public class MultiTool_Recoil : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Try(() => GcPlayerGlobals());
		Try(() => GcTechnologyTable());
	}

	//...........................................................
	
	protected void GcPlayerGlobals()
	{
		var mbin = ExtractMbin<GcPlayerGlobals>(
			"GCPLAYERGLOBALS.GLOBAL.MBIN"
		);
		mbin.WeaponZoomRecoilMultiplier  = 1;      // 2
		mbin.GunRecoil                   = 0.01f;  // 5
		mbin.LaserRecoil                 = 0.01f;  // 2
		mbin.BeamRecoil                  = 0.01f;  // 6
		mbin.GrenadeRecoil               = 0.01f;  // 10
		mbin.GunRecoilSpring             = 0.01f;  // 0.33
		mbin.BlastRecoilSpring           = 0.01f;  // 0.3
		mbin.RailRecoilSpring            = 0.01f;  // 0.1
		mbin.PulseRecoilSpring           = 0.01f;  // 0.19
		mbin.CannonRecoilSpring          = 0.01f;  // 0.3
		mbin.GunRecoilSettleSpring       = 0.01f;  // 0.4
		mbin.ThirdPersonRecoilMultiplier = 1;      // 1.5
		mbin.GunRecoilMin                = 0.01f;  // 0.15
		mbin.GunRecoilMax                = 0.01f;  // 1.6
	}
	
	//...........................................................
	
	protected void GcTechnologyTable()
	{
		var mbin = ExtractMbin<GcTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
		);
		foreach( var tech in mbin.Table ) {
			tech.StatBonuses.RemoveAll(BONUS =>
				BONUS.Stat.StatsType == StatsTypeEnum.Weapon_Laser_Recoil ||
				BONUS.Stat.StatsType == StatsTypeEnum.Weapon_Projectile_Recoil
			);
		}
	}	
}

//=============================================================================
