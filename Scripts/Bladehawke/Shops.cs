//=============================================================================

public class Shops : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcRealityManagerData>(
			"METADATA/REALITY/DEFAULTREALITY.MBIN"
		);

		var ts = mbin.TradeSettings;

		var fields = ts.GetType().GetFields();

		foreach( var field in fields ) {
			Replacer((field.GetValue(ts) as GcTradeData).AlwaysPresentProducts);
			Replacer((field.GetValue(ts) as GcTradeData).OptionalProducts);
			AdjustValues(field.GetValue(ts) as GcTradeData);
		}
	}
	
	protected void Replacer( List<libMBIN.NMS.NMSString0x10> LIST )
	{
		for( var i = 0; i < LIST.Count; ++i ) {
			LIST[i] = Regex.Replace(LIST[i], "(U_[A-Z_]*)[1-4]", "$+4");
			LIST[i] = Regex.Replace(LIST[i], "ENERGY4", "ENERGY3");
			LIST[i] = Regex.Replace(LIST[i], "RAD4", "RAD3");
			LIST[i] = Regex.Replace(LIST[i], "TOX4", "TOX3");
			LIST[i] = Regex.Replace(LIST[i], "PROT4", "PROT3");
			LIST[i] = Regex.Replace(LIST[i], "UNW4", "UNW3");
		}
	}

	protected void AdjustValues( GcTradeData SHOP )
	{
		SHOP.MinItemsForSale *= 2;
		SHOP.MaxItemsForSale *= 2;
		for( int i = 0; i <= (int)WealthClassEnum.Pirate; ++i ) {
			SHOP.MinAmountOfProductAvailable[i] *= 2;
			SHOP.MaxAmountOfProductAvailable[i] *= 2;
			if( SHOP.MinAmountOfSubstanceAvailable[i] <= 1999 ) {
				SHOP.MinAmountOfSubstanceAvailable[i] *= 5;
			}
			if( SHOP.MaxAmountOfSubstanceAvailable[i] < 999 ) {
				SHOP.MaxAmountOfSubstanceAvailable[i] *= 10;
			}
		}
	}


	//...........................................................
}

//=============================================================================
