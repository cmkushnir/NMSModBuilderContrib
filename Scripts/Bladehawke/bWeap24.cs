//=============================================================================

//=============================================================================

public class bWeap24 : cmk.NMS.Script.ModClass
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
    var maxInv = mbin.WeaponInventoryMaxUpgradeSize.MaxInventoryCapacity;

    for (var i = SizeTypeEnum.WeaponSmall; i <= SizeTypeEnum.WeaponLarge; i++)
    {
      data[(int)i].MinSlots = 24;
      data[(int)i].MaxSlots = 24;
    }
    for (var i = InventoryClassEnum.C; i <= InventoryClassEnum.S; i++)
    {
      maxInv[(int)i] = 48;
    }
  }
}


//=============================================================================
