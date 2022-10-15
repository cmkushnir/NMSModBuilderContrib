//=============================================================================

public class ModTest : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		Script<InstantActions>().IsExecutable                       = true;
        Script<InstantTextDisplay>().IsExecutable                   = true;

        var InstantScan = Script<InstantScan>();
        InstantScan.IsExecutable                                    = true;
        InstantScan.ScanTime                                        = 0f;
	}

	//...........................................................
}

//=============================================================================
