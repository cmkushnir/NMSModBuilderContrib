//=============================================================================
// You have deployed the Blob bioweapon.
// They're all dead now. They will not spawn in the game.
//=============================================================================

public class NoBlobs : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Script<Blob_Bioweapon>()         .IsExecutable = true;
		// - auto-mark ?
	}
}

//=============================================================================
