//=============================================================================
// Sample to illustrate options for:
// ExtractData, ExtractOrCreateData, CloneData,
// ExtractMbin, ExtractOrCreateMbin, CloneMbin.
// If you use IPakItemCollection to create a patch pak you must make sure that
// it will load after the pak's it is patching, using the load order naming conventions
// i.e. treat path as upper-case and use ASCII compare: https://www.asciitable.com/,
// paths with leading '_' load after paths with leading 0-9, A-Z, a-z.
//=============================================================================

[NMSScriptDisabled]
public class Patch : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
	}

	//...........................................................
	
	protected void Execute1()
	{
		// item we want to Extract from a pak file.
		var path = "MODELS/PLANETS/BIOMES/WEIRD/BEAMSTONE/BEAMSTONE.SCENE.MBIN";

		// optionally set DefaultIPakItemCollection in an early script.
		// IPakItemCollection = DefaultIPakItemCollection is set before each script Execute.
		// DefaultIPakItemCollection = ...;
		
		// optionally set script IPakItemCollection to one of:
		
		// extract path using same logic as game
		// i.e. last PCBANKS/MODS/*.pak with path, or PCBANKS/*.pak.
		IPakItemCollection = Game;
		
		// extract from PCBANKS/*.pak, this is the default value for each script
		// if DefaultIPakItemCollection hasn't been set.
		IPakItemCollection = Game.PCBANKS;
		IPakItemCollection = null;  // sets to Game.PCBANKS
	
		// extract from last PCBANKS/MODS/*.pak with path.
		IPakItemCollection = Game.MODS;
		
		// extract from PCBANKS/MODS/mod.pak.
		IPakItemCollection = Game.MODS.FindFileFromName("mod");
		
		// optionally remove any cached data for path:
		// if a prior script has already extracted path then it will be in DataCache
		// and returned by Extract*|Clone*, regardless of IPakItemCollection value.
		// to force it to try and load from IPakItemCollection, remove from DataCache
		// before calling Extract*|Clone*.
		// you generally shouldn't do this, as it means thowing out changes made by prior scripts.
		var data = DataCache.Remove(path);
		
		// if path in DataCache it will return that mbin, else will try to extract
		// from IPakItemCollection, add to DataCache, and return decompiled mbin.		
		var mbin = ExtractMbin<TkSceneNodeData>(path);
		
		mbin.Transform.ScaleZ *= 1.1f;  // use mbin as normal
	}

	//...........................................................
	
	// minimal example for creating a compat pak for whatever mods are currently installed.
	// the pak created by nmsmb must be named so it loads last.
	protected void Execute2()
	{
		IPakItemCollection = Game;
		var mbin = ExtractMbin<TkSceneNodeData>(
			"MODELS/PLANETS/BIOMES/WEIRD/BEAMSTONE/BEAMSTONE.SCENE.MBIN"
		);
		mbin.Transform.ScaleZ *= 1.1f;  // use mbin as normal
	}	

	//...........................................................
	
	// minimal example for creating a patch pak for a specific mod.
	// the pak created by nmsmb must be named so it loads after the mod it patches.
	protected void Execute3()
	{
		IPakItemCollection = Game.MODS.FindFileFromName("mod");
		var mbin = ExtractMbin<TkSceneNodeData>(
			"MODELS/PLANETS/BIOMES/WEIRD/BEAMSTONE/BEAMSTONE.SCENE.MBIN"
		);
		mbin.Transform.ScaleZ *= 1.1f;  // use mbin as normal
	}
	
	//...........................................................
	
	// minimal example for creating a patch for a specific mod
	// that compiled it's mbin's using a different libmbin.dll 
	// than is being used by nmsmb.
	// since the type cannot be specified at runtime we need to 
	// use a dynamic mbin that is evaluated at runtime.  in doing 
	// so we loose the ability to validate fields at compile time.
	// it will use the same libmbin the mod used to decompile and recompile the mbin.
	protected void Execute4()
	{
		IPakItemCollection = Game.MODS.FindFileFromName("__MOD_Winder_HubFlag");

		var path = "GCDEBUGOPTIONS.GLOBAL.MBIN";
		DataCache.Remove(path);  // loose any changes by prior scripts

		// can't ExtractMbin as can't specify type from diff libmbin,
		// so extract mbin data wrapper then get generic object as dynamic.
		var data = ExtractData<NMS.PAK.MBIN.Data>(path);	
		dynamic mbin = data.Object();

		// no compile time check on field name or type.
		mbin.ForceScanEventsToGoPrime = true;
	}	
}

//=============================================================================
