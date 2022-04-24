//=============================================================================
// Dump list of all game products to a file.
//=============================================================================

public class Export_Products : cmk.NMS.Script.QueryClass
{
	protected override void Execute()
	{
		var save_path = Dialog.SaveFile(null, "Export_Products.log");
		if( save_path.IsNullOrEmpty() ) return;
		
		var file = System.IO.File.CreateText(save_path);
		
		foreach( var product in Game.Products.List ) {
			file.WriteLine($"{product.Id} {product.Name}");
		}
		
		file.Flush();
		Log.AddSuccess("Finished");
	}
}

//=============================================================================
