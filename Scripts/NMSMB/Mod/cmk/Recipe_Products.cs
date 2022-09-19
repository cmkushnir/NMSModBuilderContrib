//=============================================================================
// Alter|add existing product requirements.
//=============================================================================

public class Recipe_Products : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var products = ExtractMbin<GcProductTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN"
		).Table;		
		//FRIG_BOOST_SPD(products);
		TECH_COMP      (products);
		WALKER_PROD    (products);
		ODD_EGG        (products);
		ALIEN_INV_TOKEN(products);
	}

	//...........................................................

	// Test: Fuel Oxidiser - change requirements
	// Replace Quad Servo and Gold w/ Mineral Compressor, Quantum Computer, Gold
	protected void FRIG_BOOST_SPD( List<GcProductData> PRODUCTS )
	{
		var product = PRODUCTS.Find(PRODUCT => PRODUCT.Id == "FRIG_BOOST_SPD");
		product.IsCraftable = true;
		product.Requirements.Clear();  // replace all current requirements
		product.Requirements.AddProduct  ("FRIG_BOOST_MIN", 1);  // Mineral Compressor
		product.Requirements.AddProduct  ("COMPUTER",       2);  // Quantum Computer
		product.Requirements.AddSubstance("ASTEROID2",     50);  // Gold
	}

	//...........................................................

	// Wiring Loom - craftable
	protected void TECH_COMP( List<GcProductData> PRODUCTS )
	{
		var product = PRODUCTS.Find(PRODUCT => PRODUCT.Id == "TECH_COMP");  // Wiring Loom
		product.IsCraftable = true;
		product.Requirements.Clear();
		product.Requirements.AddSubstance("ASTEROID2",  2);  // Gold
		product.Requirements.AddSubstance("LAND2",      4);  // Pure Ferrite
		product.Requirements.AddProduct  ("NANOTUBES", 10);  // Carbon Nanotubes
	}

	//...........................................................

	// Walker Brain - craftable
	protected void WALKER_PROD( List<GcProductData> PRODUCTS )
	{
		var product = PRODUCTS.Find(PRODUCT => PRODUCT.Id == "WALKER_PROD");  // Walker Brain
		product.IsCraftable = true;
		product.Requirements.Clear();
		product.Requirements.AddSubstance("TECHFRAG", 10000);  // Nanite Cluster
		product.Requirements.AddSubstance("ROBOT1",    1000);  // Pugneum
		product.Requirements.AddSubstance("SOULFRAG",   100);  // Fragmented Qualia
	}

	//...........................................................

	// Void Egg - craftable
	// somewhere around 3.98 stopped being able to buy void egg (anything vanilla thing with # counter)
	// create a void egg from any companion egg i.e. layed by a companion
	// todo: move to egg sequencer if possible
	protected void ODD_EGG( List<GcProductData> PRODUCTS )
	{
		var product = PRODUCTS.Find(PRODUCT => PRODUCT.Id == "ODD_EGG");  // Void Egg (start alien ship mission)
		product.IsCraftable = true;
		product.Requirements.Clear();
		product.Requirements.AddProduct  ("FOOD_V_DIPLO",  1);  // Giant Egg
		product.Requirements.AddProduct  ("ILLEGAL_PROD2", 1);  // Stolen DNA Samples
		product.Requirements.AddSubstance("TIMEDUST",   2000);  // Somnal Dust
	}

	//...........................................................

	// Spawning Sac - craftable
	protected void ALIEN_INV_TOKEN( List<GcProductData> PRODUCTS )
	{
		var product = PRODUCTS.Find(PRODUCT => PRODUCT.Id == "ALIEN_INV_TOKEN");  // Spawning Sac (additional storage)
		product.IsCraftable = true;
		product.Requirements.Clear();
		product.Requirements.AddProduct("CAVECUBE",      1);  // Vortex Cube
		product.Requirements.AddProduct("ILLEGAL_PROD2", 1);  // Stolen DNA Samples
		product.Requirements.AddProduct("NANOTUBES",    10);  // Carbon Nanotubes
	}
}

//=============================================================================
