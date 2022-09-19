//=============================================================================
// You claim to love the varied beauty of all life,
// but really just like playing with cute furry creatures.
//=============================================================================

public class Zoologist : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		// Starting ship:
		// - shuttle, large
		
		// Starting Multitool
		// - vanilla (random)
		// - max scanning
		
		// Inventory
		// - vanilla
		
		// - can make all fauna pets
		// - can ride all fauna
		// - buildable egg sequencer
		Script<Creature_Pet>()           .IsExecutable = true;
		Script<Creature_Ride>()          .IsExecutable = true;
		Script<Placeable_Egg_Sequencer>().IsExecutable = true;

		// - auto-mark ?
	}
}

//=============================================================================
