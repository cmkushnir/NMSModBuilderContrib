//=============================================================================
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
		
		// call 0+ play-as Execute:
		PlayAs<Custom>();
		PlayAs<Salvager>();

		Postsets();
	}

	//...........................................................
	
	// set initial state for scripts
	protected void Presets()
	{
		// Mission_Skip removes missions from mbins.
		// If a save had this enabled then it won't have stages for
		// the removed missions.  If you then remove the mod and load
		// the save it's like all those missions are treated as newly added
		// so they will likely all start with their first stage.
		// Defaults to false to make sure user aware of risk.
		Mod<Mission_Skip>().IsExecutable = false;
	}

	//...........................................................
	
	// force script states, may override play-as settings
	protected void Postsets()
	{
	}
}

//=============================================================================
