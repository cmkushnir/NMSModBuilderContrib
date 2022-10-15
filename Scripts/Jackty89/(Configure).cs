//=============================================================================

public class Global : cmk.NMS.Script.ModClass
{
    protected override void Execute()
    {
        switch (-2)
        {
            case 0:
                Execute<MustHave>();
                break;
            case 1:
                Execute<MyModList>();
                break;	
            case 2:
                Execute<ExpeditionModList>();
                break;
			case -1:
                Execute<ModTest>();
				break;                
            default:
                break;
        }
    }
}

//=============================================================================
