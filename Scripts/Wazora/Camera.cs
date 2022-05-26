//=============================================================================

public class Camera : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcCameraGlobals>(
			"GCCAMERAGLOBALS.GLOBAL.MBIN"
		);
		
		Try(() => ModFollowCamera(mbin.CharacterUnarmedCam));
		Try(() => ModFollowCamera(mbin.CharacterRunCam));
		Try(() => ModFollowCamera(mbin.CharacterCombatCam));
		Try(() => ModFollowCamera(mbin.CharacterMiningCam));
		Try(() => ModFollowCamera(mbin.CharacterIndoorCam));
		Try(() => ModFollowCamera(mbin.CharacterAbandCombatCam));
		Try(() => ModFollowCamera(mbin.CharacterAbandCam));
		Try(() => ModFollowCamera(mbin.CharacterNexusCam));
		Try(() => ModFollowCamera(mbin.CharacterAirborneCam));
		Try(() => ModFollowCamera(mbin.CharacterMeleeBoostCam));
		Try(() => ModFollowCamera(mbin.CharacterRocketBootsCam));
		Try(() => ModFollowCamera(mbin.CharacterRocketBootsChargeCam));
		Try(() => ModFollowCamera(mbin.CharacterFallingCam));
		Try(() => ModFollowCamera(mbin.CharacterAirborneCombatCam));
		Try(() => ModFollowCamera(mbin.CharacterSpaceCam));
		Try(() => ModFollowCamera(mbin.CharacterSteepSlopeCam));
		Try(() => ModFollowCamera(mbin.CharacterUnderwaterCam));
		Try(() => ModFollowCamera(mbin.CharacterUnderwaterCombatCam));
		Try(() => ModFollowCamera(mbin.CharacterUnderwaterJetpackCam));
	}

	//...........................................................
	
	protected void ModFollowCamera( GcCameraFollowSettings gcCameraFollowSettings )
	{
		gcCameraFollowSettings.MinSpeed = 4;
		gcCameraFollowSettings.SpeedRange = 20;
		gcCameraFollowSettings.OffsetX = 0;
		gcCameraFollowSettings.OffsetY = 0;
		gcCameraFollowSettings.OffsetYAlt = 0;
		gcCameraFollowSettings.OffsetYSlopeExtra = 0.5f;
		gcCameraFollowSettings.OffsetZFlat = 0;
		gcCameraFollowSettings.BackMinDistance = 6;
		gcCameraFollowSettings.BackMaxDistance = 9;
		gcCameraFollowSettings.BackSlopeAdjust = 0;
		gcCameraFollowSettings.BackSlopeRotationAdjust = 0;
		gcCameraFollowSettings.UpMinDistance = 0;
		gcCameraFollowSettings.UpMaxDistance = 0;
		gcCameraFollowSettings.UpSlopeAdjust = 0;
		gcCameraFollowSettings.LeftMinDistance = 0;
		gcCameraFollowSettings.LeftMaxDistance = 0;
		gcCameraFollowSettings.OffsetYExtraMaxDistance = 0;
		gcCameraFollowSettings.PanNear = -1;
		gcCameraFollowSettings.PanFar = 3;
		gcCameraFollowSettings.UpGamma = 1.5f;
		gcCameraFollowSettings.HorizRotationAngleMaxPerFrame = 20;
		gcCameraFollowSettings.VertRotationSpeed = 15;
		gcCameraFollowSettings.VertRotationMin = -65;
		gcCameraFollowSettings.VertRotationMax = 65;
		gcCameraFollowSettings.VertRotationOffset = 1;
		gcCameraFollowSettings.VertRotationOffsetMinAngle = -150;
		gcCameraFollowSettings.VertRotationOffsetMaxAngle = 180;
		gcCameraFollowSettings.VertStartLookingDown = false;
		gcCameraFollowSettings.DistSpeed = 5;
		gcCameraFollowSettings.DistSpeedOutsideMainRange = 5;
		gcCameraFollowSettings.DistStiffness = 0.2f;
		gcCameraFollowSettings.SpringSpeed = 0.18f;
		gcCameraFollowSettings.LockToObjectOnIdle = false;
		gcCameraFollowSettings.CenterStartTime = 2.2f;
		gcCameraFollowSettings.CenterBlendTime = 0.8f;
		gcCameraFollowSettings.CenterMaxSpring = 0.66f;
		gcCameraFollowSettings.CenterMaxSpeed = 0.1f;
		gcCameraFollowSettings.VelocityAnticipate = 0.17f;
		gcCameraFollowSettings.VelocityAnticipateSpringSpeed = 0.4f;
		gcCameraFollowSettings.VertMaxSpring = 0.5f;
		gcCameraFollowSettings.CenterStartSpeed = 1;
		gcCameraFollowSettings.MinClose = 0.4f;
		gcCameraFollowSettings.MaxClose = 0.6f;
		gcCameraFollowSettings.CloseSpring = 3;
		gcCameraFollowSettings.LookStickLimitAngle = 0;
		gcCameraFollowSettings.EnableCollisionDetection = true;
		gcCameraFollowSettings.NumLRProbes = 10;
		gcCameraFollowSettings.LRProbesRange = 13;
		gcCameraFollowSettings.LRProbesRadius = 0.3f;
		gcCameraFollowSettings.NumUDProbes = 5;
		gcCameraFollowSettings.UDProbesRange = 1.5f;
		gcCameraFollowSettings.ProbeCenterX = 0;
		gcCameraFollowSettings.ProbeCenterY = -0.65f;
		gcCameraFollowSettings.PushForwardDropoffLR = 1.6f;
		gcCameraFollowSettings.PushForwardDropoffUD = 0.2f;
		gcCameraFollowSettings.AvoidCollisionLRSpeed = 0.005f;
		gcCameraFollowSettings.AvoidCollisionUDSpeed = 0.005f;
		gcCameraFollowSettings.AvoidCollisionPushSpeed = 0.01f;
		gcCameraFollowSettings.AvoidCollisionUDUseStickDelay = true;
		gcCameraFollowSettings.AvoidCollisionLRUseStickDelay = true;
		gcCameraFollowSettings.UseSpeedBasedSpring = true;
		gcCameraFollowSettings.UseCustomBlendTime = false;
		gcCameraFollowSettings.CustomBlendTime = 0.5f;
	}
}

//=============================================================================
