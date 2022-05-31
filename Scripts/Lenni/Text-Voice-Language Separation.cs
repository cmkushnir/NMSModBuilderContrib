//=============================================================================

public class Text_Voice_Language_Separation : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var english = Game.Languages.Get(LangId.English);
		
		foreach( var id in LangId.List ) {
			if( id == LangId.English ) continue;
			foreach( var key in english.List ) {
				SetLanguageText(id, key.Id, key.Text);
			}
		}
	}

	//...........................................................
}


//=============================================================================
