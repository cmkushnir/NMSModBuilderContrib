//=============================================================================

public class Search_SPV : cmk.NMS.Script.QueryClass
{
    protected override void Execute()
    {
        var save_path = Dialog.SaveFile(null, "Search_SPV.log");
        if( save_path.IsNullOrEmpty() ) return;

        var file = System.IO.File.CreateText(save_path);

        var regex = new Regex("gPlanetCloudParamsVec4",
            RegexOptions.Singleline | RegexOptions.Compiled,
            System.TimeSpan.FromSeconds(1)
        );

        // Game.PCBANKS.ForEachInfo - only look through game paks
        // Game.MODS.ForEachInfo - look through all mod paks
        // Game.MODS.FindFileFromName(name).ForEachInfo - look through specific mod pak
        // Game.ForEachInfo - look through all mod and game paks
        var collection = Game.PCBANKS;
        var searched   = 0;
        var count      = 0;

        collection.ForEachInfo((INFO, LOG, CANCEL) => {
            if( INFO.Path.Extension != ".SPV" ) return;
            Interlocked.Increment(ref count);
        });

        // ~6 min to go through 59233 spv
        collection.ForEachInfo((INFO, CANCEL, LOG) => {
            if( INFO.Path.Extension != ".SPV" ) return;

            Interlocked.Increment(ref searched);
            ProgressReport($"{searched} / {count}");

            // each extract spawns a spirv-cross process to convert bin to text
            var data = INFO.ExtractData<NMS.PAK.SPV.Data>();
            if( regex.IsMatch(data.Text) ) {
                file.WriteLine(data.Path);
            }
            
            // could use data.RawToTextVeldridCompute(data.Raw)
            // instead of data.Text, to use the linked Veldrid dll
            // instead of spawning a spirv-cross process.  Much faster,
            // but resulting shader text not suitable to convert back to spv,
            // but is good enough to search.  This is what Search text tab does.
            
        }, Log, Cancel);

        file.Flush();
        Log.AddSuccess("Finished");
    }
}

//=============================================================================
