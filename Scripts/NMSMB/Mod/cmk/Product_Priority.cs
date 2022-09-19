//=============================================================================
// Change priority of charge items, products listed first then substances.
//=============================================================================

public class Product_Priority : cmk.NMS.Script.ModClass
{
	protected override void Execute()
	{
		var mbin = ExtractMbin<GcTechnologyTable>(
			"METADATA/REALITY/TABLES/NMS_REALITY_GCTECHNOLOGYTABLE.MBIN"
		);
		foreach( var technology in mbin.Table ) {
			if( technology.ChargeBy.IsNullOrEmpty() ) continue;
			
			technology.ChargeBy.Sort((LHS, RHS) => {
				var lhs = Game.PCBANKS.FindItemData(LHS);
				var rhs = Game.PCBANKS.FindItemData(RHS);
				
				if( lhs == rhs  ) return  0;
				if( lhs == null ) return -1;
				if( rhs == null ) return  1;
				
				var lhs_is_prod = lhs.ItemType == InventoryTypeEnum.Product;
				var rhs_is_prod = rhs.ItemType == InventoryTypeEnum.Product;
				 
				if( lhs_is_prod == rhs_is_prod ) return  0;
				if( lhs_is_prod ) return -1;
				if( rhs_is_prod ) return  1;
				
				return 0;
			});
		}
	}
}

//=============================================================================
