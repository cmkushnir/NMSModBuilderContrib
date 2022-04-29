//=============================================================================

//=============================================================================

//	using SizeTypeEnum = GcInventoryLayoutSizeType.SizeTypeEnum;
	
	public class bExo48 : cmk.NMS.Script.ModClass
	{
		protected override void Execute()
		{
			GcInventoryTable();
		}

		//...........................................................

		protected void GcInventoryTable()
		{
			var mbin = ExtractMbin<GcInventoryTable>(
				"METADATA/REALITY/TABLES/INVENTORYTABLE.MBIN"
			);
			var data = mbin.GenerationData.GenerationDataPerSizeType;
			
			for (var i = SizeTypeEnum.VehicleSmall; i <= SizeTypeEnum.VehicleLarge; i++) {
				data[(int)i].MinSlots  = 48;
				data[(int)i].MaxSlots  = 48;
			}
		}
	}


//=============================================================================
