﻿//=============================================================================
// Programmatically control what scripts, or parts of scripts, get executed.
// Cannot be disabled, always executed.
//
// All mod scripts have a subfolder with the same name as the script file
// e.g. <app>/Scripts/Mod/(Configure)/
//
// Any files placed in those folders will be included in the mod pak if the
// corresponding mod is executed without throwing an exception.
// Later scripts may replace included files of the same name added by prior scripts;
// if this happens a warning will be displayed in the Build log.
//
// The files must be placed in the proper pak sub-folder
// e.g. <app>/Scripts/Mod/(Configure)/Textures/ColourLUT.dds
// would be added to mod pak as "TEXTURES/COLOURLUT.DDS".
//
// (Configure) can be used as a place to put loose files that you don't want tied to a mod,
// i.e. not tied to script code that you may want to disable.
//
// Mod scripts are executed in the order they appear in the list.
// If the execute order matters then rename scripts (double-click on name in list)
// to get desired order.
//
//=============================================================================

public class Configure : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Presets();
		
		// call 0+ play-as scripts:
		PlayAs<Archaeologist>();
		PlayAs<Cartographer>();
		PlayAs<Explorer>();
		PlayAs<Fighter>();
		PlayAs<Miner>();
		PlayAs<Salvager>();
		PlayAs<Settler>();
		PlayAs<Zoologist>();
		PlayAs<Custom>();

		Postsets();
	}

	//...........................................................
	
	// set initial state for scripts
	protected void Presets()
	{
		Inventory_.MaxAmount = 100000;
		
		Mission_Skip.SkipMethod     = Mission_Skip.SkipMethodEnum.Mark;
		Mission_Time.MaxTimeSeconds = 10;
		
		Planet.FogStrengthMult = 0.1f;
		
		Reward_MoreWords.BonusWords      = 3;
		Reward_MoreWords.BonusAtlasWords = 3;
		Reward_MoreWords.BonusAtlasWordChance = 30.0f;
		
		// enable following mods in play-as scripts:	
		Mod<Add_Tech_Rockets>()       .IsExecutable = false;  // Fighter
		Mod<Base_Extractor>()         .IsExecutable = false;  // Settler
		Mod<Base_Power>()             .IsExecutable = false;  // Settler
		Mod<Creature_Pet>()           .IsExecutable = false;  // Zoologist
		Mod<Creature_Ride>()          .IsExecutable = false;  // Zoologist
		Mod<Harvester_Rate>()         .IsExecutable = false;  // Settler
		Mod<Placeable_Egg_Sequencer>().IsExecutable = false;  // Zoologist
		Mod<Placeable_Salvage>()      .IsExecutable = false;  // Salvager
		Mod<Starcharts>()             .IsExecutable = false;  // Cartographer
	}

	//...........................................................
	
	// force script states, may override play-as settings
	protected void Postsets()
	{
	}
}

//=============================================================================
