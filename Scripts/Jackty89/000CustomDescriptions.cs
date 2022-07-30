//=============================================================================

public class CustomDescriptions : cmk.NMS.Script.ModClass
{
	protected class CustomSubstanceDescription
    {
        public string SubstanceID;
        public List<Tuple<LangId, string>> CustomLangDescStrings;
    }
    private static List<CustomSubstanceDescription> CustomSubstanceDescriptions = new List<CustomSubstanceDescription>()
    {
        new CustomSubstanceDescription()
		{
			SubstanceID = "CATALYST1",
			CustomLangDescStrings = new List<Tuple<LangId, string>>()
            {
                new(LangId.English, "It's sodium baby."),
                new(LangId.French, "C'est sodium baby.")
            }			
		}
	};    
    
	protected class CustomProductDescription
    {
        public string ProductID;
        public List<Tuple<LangId, string>> CustomLangDescStrings;
    }
    private static List<CustomProductDescription> CustomProductDescriptions = new List<CustomProductDescription>()
    {
        new CustomProductDescription()
		{
			ProductID = "FOOD_CM_APPLE",
			CustomLangDescStrings = new List<Tuple<LangId, string>>()
            {
                new(LangId.English, "Apple Cake.")
            }			
		}
	};
	protected override void Execute()
	{
		foreach(CustomProductDescription productData in CustomProductDescriptions)
        {
			AddCustomProductDescription(productData);
		}
		
		foreach(CustomSubstanceDescription substanceData in CustomSubstanceDescriptions)
        {
			AddCustomSubstanceDescription(substanceData);
		}

	}
	
	protected void AddCustomProductDescription(CustomProductDescription productData)
	{
		var prod_mbin = ExtractMbin<GcProductTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCPRODUCTTABLE.MBIN");
		var product = prod_mbin.Table.Find(PRODUCT => PRODUCT.Id == productData.ProductID);
		var customDescriptionID = "C" + product.Description;
		
		product.Description = customDescriptionID;
		
		AddNewLanguageString(product.Id, customDescriptionID, productData.CustomLangDescStrings);
	}
	
	protected void AddCustomSubstanceDescription(CustomSubstanceDescription substanceData)
	{
		var subs_mbin = ExtractMbin<GcSubstanceTable>("METADATA/REALITY/TABLES/NMS_REALITY_GCSUBSTANCETABLE.MBIN");
		var substance = subs_mbin.Table.Find(SUBSTANCE => SUBSTANCE.ID == substanceData.SubstanceID);
		var customDescriptionID = "C" + substance.Description;
		
		substance.Description = customDescriptionID;
		
		AddNewLanguageString(substance.ID, customDescriptionID, substanceData.CustomLangDescStrings);
	}
	
	protected void AddNewLanguageString(string foodId, string customDescriptionID, List<Tuple<LangId, string>> languages)
	{
		foreach (var language in languages)
        {
			SetLanguageText(language.Item1, customDescriptionID, language.Item2);
		}
	}

	//...........................................................
}

//=============================================================================
