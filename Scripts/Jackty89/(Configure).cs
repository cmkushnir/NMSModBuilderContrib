//=============================================================================

public class Global : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		
	    switch (2)
	    {
	        case 1:
				PlayAs<MyModList>();
	            break;	
	        case 2:
    			PlayAs<ExpeditionModList>();
	            break;	
	        default:
	            break;
	    }
	}

	//...........................................................
	
	// set initial state for scripts
	protected void Presets()
	{
		
		
	}

	//...........................................................
	
	// force script states, may override play-as settings
	protected void Postsets()
	{
	}

	
}

//=============================================================================
