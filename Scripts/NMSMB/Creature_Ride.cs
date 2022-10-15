//=============================================================================
// Enable more creatures to become ridable, requires Creature_Pet to add GcCreatureComponentData.
// Ported from: Corvus - RideAllCreatures.lua
//=============================================================================

public class Creature_Ride : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		if( !Script<Creature_Pet>().IsExecutable ) return;
		GcCreatureDataTable();
		GcCreatureDataTable_RobotFix();
	}

	//...........................................................

	// non-default creature ride data
	protected class RideData {
		public string ID;
		public string Joint;
	}
	protected RideData [] CreatureRideData = new [] {
		new RideData{ ID = "FISH",           Joint = "joint1" },
		new RideData{ ID = "SWIMRODENT",     Joint = "GlobalJNT" },
		new RideData{ ID = "JELLYFISH",      Joint = "centre" },
		new RideData{ ID = "SHARK",          Joint = "Root" },
		new RideData{ ID = "BIRD",           Joint = "Root" },
		new RideData{ ID = "FLYINGLIZARD",   Joint = "Back1JNT" },
		new RideData{ ID = "BUTTERFLY",      Joint = "Root" },
		new RideData{ ID = "SMALLBIRD",      Joint = "Root" },
		new RideData{ ID = "MOLE",           Joint = "DiggerROOT" },
		new RideData{ ID = "PROTODIGGER",    Joint = "EyeStalkROOT" },
		new RideData{ ID = "DRILL",          Joint = "DiggerROOT" },
		new RideData{ ID = "WEIRDBUTTERFLY", Joint = "shellfly_ROOTSHJnt" },
		new RideData{ ID = "FIENDFISHSMALL", Joint = "joint1" },
		new RideData{ ID = "FLOATER",        Joint = "FrontJNT" },
		new RideData{ ID = "FIENDFISHBIG",   Joint = "joint1" },
		new RideData{ ID = "WALKER",         Joint = "Root" },
		new RideData{ ID = "QUAD",           Joint = "Root" },
	};

	//...........................................................
	
	protected void GcCreatureDataTable()
	{
		var mbin = ExtractMbin<GcCreatureDataTable>(
			"METADATA/SIMULATION/ECOSYSTEM/CREATUREDATATABLE.MBIN"
		);		
		mbin.Table.ForEach(CREATURE => {
			var existing  = CREATURE.Data.FindFirst<GcCreatureRidingData>();
			if( existing == null ) CREATURE.Data.Add(CreateRidingData(CREATURE.Id));
		});
	}
	
	//...........................................................

	protected void GcCreatureDataTable_RobotFix()
	{
		var mbin = ExtractMbin<GcCreatureDataTable>(
			"METADATA/SIMULATION/ECOSYSTEM/ROBOTDATATABLE.MBIN"
		);
		
		// add WALKER ride data
		var creature = mbin.Table.Find(CREATURE => CREATURE.Id == "WALKER");
		creature.Data.Add(CreateRidingData(creature.Id));
		
		// replace QUAD ride data
		creature = mbin.Table.Find(CREATURE => CREATURE.Id == "QUAD");
		creature.Data.RemoveAll(DATA => DATA is GcCreatureRidingData);
		creature.Data.Add(CreateRidingData(creature.Id));			
	}

	//...........................................................
	
	protected GcCreatureRidingData CreateRidingData( string CREATURE )
	{
		// check if CREATURE has non-default data specified
		var data = CreatureRideData.FindFirst(DATA => DATA.ID == CREATURE);		
		
		// from TWOLEGANTELOPE, evidently doesn't matter too much
		return new GcCreatureRidingData {
			JointName       = data?.Joint ?? "RootJNT",
			UprightStrength = 1,
			LegSpread       = true,
			ScaleForNeutralLegSpread = 1.25f,
			ScaleForMinLegSpread     = 0.5f,
			ScaleForMaxLegSpread     = 4.75f,
			IdleRidingAnim    = "RIDE_ANT_IDLE",
			DefaultRidingAnim = "RIDE_ANT_WALK",
		};
	}
}

//=============================================================================
