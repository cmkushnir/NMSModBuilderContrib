//=============================================================================
// Programmatically control what scripts, or parts of scripts, get executed.
//
// Any mod script with a name enclosed in () cannot be disabled, it's always Executed.
// Any mod script with a name enclosed in [] is a play-as script, it will only be
// Executed if explicitly called from another script via PlayAs<script>().
//
// All mod scripts have a subfolder with the same name as the script file
// e.g. <app>/Scripts/Mod/cmk/(Configure)/
//
// Any files placed in those folders will be included in the mod pak if the
// corresponding mod is executed without throwing an exception.
// Later scripts may replace included files of the same name added by prior scripts;
// if this happens a warning will be displayed in the Build log.
//
// The files must be placed in the proper pak sub-folder
// e.g. <app>/Scripts/Mod/cmk/(Configure)/Textures/ColourLUT.dds
// would be added to the mod pak as "TEXTURES/COLOURLUT.DDS".
//
// (Configure) can be used as a place to put loose files that you don't want tied to a mod,
// i.e. not tied to script code that you may want to disable.
//
// Mod scripts are executed in the order they appear in the list.
// If the execute order matters then rename scripts (double-click on name in list)
// to get desired order.
//
//=============================================================================

public class Configure : NMS.Script.ModClass
{
	protected override void Execute()
	{	
		// ensure current Execute run is only hook
		ModFiles.AfterBuildPakStream -= OnAfterBuildPakStream;
		ModFiles.AfterBuildPakStream += OnAfterBuildPakStream;
		
		Presets();

		if( true ) {
			// call 0+ play-as scripts:
			Execute<Archaeologist>();
			Execute<Cartographer>();
			Execute<Explorer>();
			Execute<Fighter>();
			Execute<Miner>();
			Execute<Salvager>();
			Execute<Settler>();
			Execute<Zoologist>();
			Execute<Custom>();
		}

		Postsets();
	}

	//...........................................................

	// OnDispose is called when this ModClass instance is about to be released|disposed.
	// Authors are responsible for unhooking any events here that may remain after Execute.
	public override void OnDispose()
	{
		// if user only Executes but does not Build, then OnAfterBuildPakStream
		// would remain hooked and pin the current mod Assembly.
		ModFiles.AfterBuildPakStream -= OnAfterBuildPakStream;
	}
	
	//...........................................................
	
	// dump a list of modified items, e.g. mbin's, that were extracted by multiple scripts
	// to the Build tab Log window after the mod pak stream is built, but before it's saved to disk.
	protected static void OnAfterBuildPakStream( NMS.Script.ModFiles SCRIPTS, CancellationToken CANCEL )
	{
		// since we unhook both in Execute and OnDispose, unhooking here is optional as AfterBuildPakStream
		// will only happen once per Execute, Building again will just prompt to save current pak stream.
		SCRIPTS.AfterBuildPakStream -= OnAfterBuildPakStream;

		// Log is for this script, it's only set while script Execute is running, so will be null here.
		// ModFiles.Log is what you see in the mod script editor log window.
		// ModFiles.ExecuteLog is what you see in the build log window.
		// Log.Add*() methods can take 2 optional args, if the first is an info or data object
		// then you can double-click it to open the item in the left PAK Items tab; however,
		// in this case it will open the game version not the mod version since the data is
		// from the DataCache and not a loaded pak file.
		
		SCRIPTS.ExecuteLog.AddInformation("");
		SCRIPTS.ExecuteLog.AddHeading("List modified items extracted by multiple scripts");
		
	
		// DataCache entries sorted by Data.Path.
		// includes created and cloned items.
		foreach( var entry in SCRIPTS.DataCache ) {
			if( CANCEL.IsCancellationRequested ) break;
			var data = entry.Data;  // e.g. mbin, dds, ... extracted data wrapper
			var mods = entry.Mods;  // in mod (script) call order
			if( !data.IsEdited ||
				mods.Count < 2 ||
				data.Path.Directory.StartsWith("_SCRIPTS/")
			)	continue;
			SCRIPTS.ExecuteLog.AddSuccess(data.Path.Full);
			foreach( var mod in mods ) {
				// all we know here is that at least one mod edited, not which one(s),
				// it's possible a given mod just extracted and didn't edit,
				SCRIPTS.ExecuteLog.AddInformation($"\t{mod.Name}");
			}			
		}
		
		SCRIPTS.ExecuteLog.AddHeading("Finished listing modified items extracted by multiple scripts");

		//OnAfterBuildPakStreamSave(SCRIPTS, CANCEL);
	}
	
	//...........................................................
	
	protected void OnAfterBuildPakStreamSave( NMS.Script.ModFiles SCRIPTS, CancellationToken CANCEL )
	{
		var save_path = Dialog.SaveFile(null, $"{SCRIPTS.Folder}.log");
		if( save_path.IsNullOrEmpty() ) return;
		
		var save_file  = System.IO.File.CreateText(save_path);
		if( save_file == null ) return;
			
		foreach( var entry in SCRIPTS.DataCache ) {
			if( CANCEL.IsCancellationRequested ) break;
			var data = entry.Data;
			var mods = entry.Mods;
			if( !data.IsEdited ||
				data.Path.Directory.StartsWith("_SCRIPTS/")
			)	continue;
			foreach( var mod in mods ) {
				save_file.WriteLine($"{data.Path} {mod.Name}");
			}			
		}
		
		save_file.Flush();
	}
	
	//...........................................................

	// set initial state for scripts
	protected void Presets()
	{
		// optionally set DefaultIPakItemCollection.
		// DefaultIPakItemCollection is initially set to Game.PCBANKS.
		// IPakItemCollection = DefaultIPakItemCollection is set before each script Execute.
		// DefaultIPakItemCollection = ...;

		// optionally set default prefix for new language file that new language ID's will be added to.
		// DefaultLanguageMbinPrefix is initially set to "NMSMB".
		// LanguageMbinPrefix = DefaultLanguageMbinPrefix is set before each script Execute.
		// DefaultLanguageMbinPrefix = ...;

		Script<Inventory_Stack>().MaxAmount = 100000;

		Script<Mission_Skip>().SkipMethod     = Mission_Skip.SkipMethodEnum.Mark;
		Script<Mission_Time>().MaxTimeSeconds = 10;

		Script<Planet>().FogStrengthMult = 0.1f;

		var Reward_MoreWords = Script<Reward_MoreWords>();
		Reward_MoreWords.BonusWords      = 3;
		Reward_MoreWords.BonusAtlasWords = 6;
		Reward_MoreWords.BonusWordChance      = 100.0f;
		Reward_MoreWords.BonusAtlasWordChance =  30.0f;

		// enable following mods in play-as scripts:	
		Script<Add_Tech_Rockets>()       .IsExecutable = false;  // Fighter
		Script<Base_Extractor>()         .IsExecutable = false;  // Settler
		Script<Base_Power>()             .IsExecutable = false;  // Settler
		Script<Creature_Pet>()           .IsExecutable = false;  // Zoologist
		Script<Creature_Ride>()          .IsExecutable = false;  // Zoologist
		Script<Harvester_Rate>()         .IsExecutable = false;  // Settler
		Script<MultiTool_Recoil>()       .IsExecutable = false;  // Fighter
		Script<Placeable_Egg_Sequencer>().IsExecutable = false;  // Zoologist
		Script<Placeable_Salvage>()      .IsExecutable = false;  // Salvager
		Script<Starcharts>()             .IsExecutable = false;  // Cartographer

		// or disable all by default and rely on scripts to call each other,
		// of course need at least 1 IsExecutable unless call from Configure.Execute.
		//foreach( var script in ModFiles.ModCollection ) {
		//	script.IsExecutable = false;
		//}
	}

	//...........................................................

	// force script states, may override play-as settings
	protected void Postsets()
	{
	}
}

//=============================================================================
