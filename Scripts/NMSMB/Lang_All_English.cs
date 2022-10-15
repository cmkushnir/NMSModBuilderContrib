//=============================================================================
// Copy the english text to all other language mbins.
// Run last to ensure any new language prefixes or ID's added by prior scripts
// are also updated.
//=============================================================================

[NMSScriptDisabled]  // intended as example for lang manip
public class Lang_All_English : cmk.NMS.Script.ModClass
{
	// ~18 sec
	protected override void Execute()
	{
		// get a list of current source entries string.Compare sorted on Id,
		// includes any entries added|modified by prior scripts.
		var source         = LanguageId.English;
		var source_entries = GetLanguageSnapshot(source);
		var source_count   = source_entries.Count;

		// go through all supported languages, skip source.
		foreach( var target in LanguageId.List ) {
			if( Cancel.IsCancellationRequested ) break;
			if( target == source ) continue;

			// ProgressReport will update both the script and build tab toolbars.
			ProgressReport(target.Text);
			Log.AddInformation(target.Text);

			// get a list of current target entries string.Compare sorted on Id,
			// includes any entries added|modified by prior scripts.
			var target_entries = GetLanguage(target);
			var index          = 0;

			foreach( var source_entry in source_entries ) {
				if( Cancel.IsCancellationRequested ) break;

				// don't flood progrss as it has to run in ui thread.
				if( (++index % 100) == 0 ) ProgressReport($"{target.Text} {index} / {source_count}");

				// since GetLanguage returns sorted lists we can Bsearch entry,
				// important that use same compare used to sort i.e. string.Compare.
				var target_entry = target_entries.Bsearch(source_entry.Id, (ENTRY, KEY) =>
					string.Compare(ENTRY.Id, KEY)
				);
				if( target_entry == null ) {
					// if target doesn't have source_entry then need to add new entry (slower)
					// instead of just updating the existing entry (faster).
					// happens if prior script added new id's to source but not target.
					SetLanguageText(target, source_entry.Id, source_entry.Ref(source));
					//Log.AddWarning($"{target.Text} missing {source_entry.Id.Value}");
				}
				else {
					// each entry has a field for each language, but only one is populated,
					// use Ref to get a reference to the correct field for the specified language.
					target_entry.Ref(target) = source_entry.English;
				}
			}
		}

		ProgressReport("");
	}
}

//=============================================================================
