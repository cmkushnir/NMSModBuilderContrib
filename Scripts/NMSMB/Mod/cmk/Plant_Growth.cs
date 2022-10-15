//=============================================================================
// Adjust plant growth times.
//=============================================================================

public class Plant_Growth : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var regex = new Regex(">[1-9]");
		
		var paths = IPakItemCollection.FindInfoRegex(
			"MODELS/PLANETS/BIOMES/COMMON/INTERACTIVEFLORA/.*/ENTITIES/PLANTINTERACTION.ENTITY.MBIN"
		);		
		foreach( var info in paths ) {
			var mbin  = ExtractMbin<TkAttachmentData>(info.Path);
			if( mbin == null ) continue;
			
			var trigger  = mbin.Components.FindFirst<GcSimpleInteractionComponentData>();
			if( trigger == null ) continue;

			// 3 steps, each w/ an enter and complete time.
			// set second step at 30 sec, third step at 60 sec.
			trigger.BaseBuildingTriggerActions[2].Time = 29;
			trigger.BaseBuildingTriggerActions[3].Time = 30;
			trigger.BaseBuildingTriggerActions[4].Time = 59;
			trigger.BaseBuildingTriggerActions[5].Time = 60;
			
			foreach( var identifier in NMS.Game.Language.Identifier.List ) {
				if( identifier != Game.LanguageId ) continue;			

				var language = Game.PCBANKS.Languages.Get(identifier);
				var text     = language.GetText(trigger.ScanData);
				
				var matches = regex.Matches(text);
				if( matches.IsNullOrEmpty() ) continue;
				
				var start = matches[0].Index;						
				var end   = text.IndexOf('<', start);
				if( end < 0 ) continue;
				
				var prefix = text.Substring(0, start + 1);
				var suffix = text.Substring(end);
				
				SetLanguageText(identifier, trigger.ScanData, $"{prefix}60 sec{suffix}");
			}
		}
	}
}

//=============================================================================
