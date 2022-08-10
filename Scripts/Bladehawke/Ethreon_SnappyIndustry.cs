//=============================================================================

public class SnappyIndustry : cmk.NMS.Script.ModClass
{
    protected override void Execute()
    {
        Try(() => IndustrialScenes());
        Try(() => Floors());
        Try(() => Planter());
        Try(() => ModuleSnaps());
    }

    protected void ModuleSnaps()
    {
        var mbin = ExtractMbin<TkSceneNodeData>(
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULES_SNAPSCENE.SCENE.MBIN"
        );

        var direction = new[] { "N", "E", "S", "W" };

        foreach (var dir in direction)
        {
            var data = BuildModSnapDir(dir);
            mbin.Children.Add(data);
        }
        for (var i = 1; i <= 4; ++i)
        {
            var data = BuildModSnapFloorCount(i);
            mbin.Children.Add(data);
        }
    }

    protected TkSceneNodeData BuildModSnapFloorCount(int NUM)
    {
        List<TkSceneNodeData> children = new();

        var names = new[] { "Planter_in_0", "NullSnap_Plant_0" };
        foreach (var name in names)
        {
            children.Add(BuildChild(name));
        }

        TkTransformData tfd =
            new()
            {
                TransY = -0.15f,
                ScaleX = 1,
                ScaleY = 1,
                ScaleZ = 1
            };

        switch (NUM)
        {
            case 1:
                tfd.TransX = -0.333333f;
                tfd.TransZ = 0.333333f;
                break;
            case 2:
                tfd.TransX = 0.333333f;
                tfd.TransX = 0.333333f;
                break;
            case 3:
                tfd.TransX = 0.333333f;
                tfd.TransZ = -0.333333f;
                break;
            case 4:
                tfd.TransX = -0.333333f;
                tfd.TransZ = -0.333333f;
                break;
        }

        TkSceneNodeData data = BuildChild("SnapPointLargeIndFloor_" + NUM.ToString(), tfd);
        data.Children = children;

        return data;
    }

    protected TkSceneNodeData BuildModSnapDir(string DIR)
    {
        List<TkSceneNodeData> children = new();

        var names = new[] { "IndLarge_Out_", "IndLarge_In_", "NullSnap_" + DIR };

        foreach (var name in names)
        {
            children.Add(BuildChild(name));
        }
        ;

        TkTransformData tfd =
            new()
            {
                ScaleX = 1,
                ScaleY = 1,
                ScaleZ = 1
            };

        switch (DIR)
        {
            case "N":
                tfd.TransX = -1.333333f;
                tfd.RotZ = -90;
                break;
            case "E":
                tfd.TransZ = -1.333333f;
                tfd.RotY = 180;
                break;
            case "S":
                tfd.TransX = 1.333333f;
                tfd.RotZ = 90;
                break;
            case "W":
                tfd.TransZ = 1.333333f;
                break;
        }

        TkSceneNodeData data = BuildChild("SnapPointIndustrialLarge_" + DIR, tfd);
        data.Children = children;

        return data;
    }

    protected void Planter()
    {
        var mbin = ExtractMbin<TkSceneNodeData>(
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/BASICPARTS/BASIC_FLOOR_QUARTER.SCENE.MBIN"
        );
        var remove = mbin.Children.Find(NAME => NAME.Name == "SnapPoint_Floor");
        mbin.Children.Remove(remove);

        TkSceneNodeData data = BuildChild("SnapPoint_PlanterSmall");
        data.Children = new();
        for (var i = 1; i <= 4; ++i)
        {
            var tfd = SetRotY(i);
            data.Children.Add(BuildChild("Planter_Out_" + i.ToString(), tfd));
        }
        mbin.Children.Add(data);

        data = BuildChild("SnapPoint_IndFloorSQrt_1");
        data.Children = new();
        for (var i = 1; i <= 4; ++i)
        {
            TkTransformData tfd = new() { RotY = 90 * (i - 1) };
            if (tfd.RotY > 180)
            {
                tfd.RotY -= 360;
            }
            data.Children.Add(BuildChild("IndustrialFloor_Out_" + i.ToString(), tfd));
        }
        mbin.Children.Add(data);
    }

    protected TkSceneNodeData BuildChild(string NAME, TkTransformData TFD = null)
    {
        if (TFD == null)
        {
            TFD = new();
        }
        TkSceneNodeData child = new();
        child.Name = NAME;
        child.NameHash = jenkins.Hash(NAME);
        child.Type = "LOCATOR";
        child.Transform = new()
        {
            TransX = TFD.TransX,
            TransY = TFD.TransY,
            TransZ = TFD.TransZ,
            RotX = TFD.RotX,
            RotY = TFD.RotY,
            RotZ = TFD.RotZ,
            ScaleX = 1,
            ScaleY = 1,
            ScaleZ = 1
        };

        return child;
    }

    protected void Floors()
    {
        const float transXZ = 1.333333f;
        const float transY = 0.15f;

        var mbin = ExtractMbin<TkSceneNodeData>(
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/BASICPARTS/BASIC_FLOOR.SCENE.MBIN"
        );
        List<TkSceneNodeData> ChildrenOfChildren = new();
        TkSceneNodeData data;

        for (var i = 1; i <= 4; ++i)
        {
            var tfd = SetRotY(i);
            ChildrenOfChildren.Add(BuildChild("IndustrialFloor_Out_" + i.ToString(), tfd));
        }
        for (var i = 1; i <= 4; ++i)
        {
            TkTransformData tfd = new() { TransY = transY };
            switch (i)
            {
                case 1:
                    tfd.TransX = -transXZ;
                    tfd.TransZ = transXZ;
                    break;
                case 2:
                    tfd.TransX = transXZ;
                    tfd.TransX = transXZ;
                    break;
                case 3:
                    tfd.TransX = transXZ;
                    tfd.TransZ = -transXZ;
                    break;
                case 4:
                    tfd.TransX = -transXZ;
                    tfd.TransZ = -transXZ;
                    break;
            }

            data = BuildChild("SnapPoint_IndFloorQrt_" + i.ToString(), tfd);
            data.Children = ChildrenOfChildren;
            mbin.Children.Add(data);
        }
        ChildrenOfChildren = new();

        for (var i = 1; i <= 4; ++i)
        {
            var tfd = SetRotY(i);
            ChildrenOfChildren.Add(BuildChild("IndustrialLargeFloor_Out_" + i.ToString(), tfd));
        }
        ChildrenOfChildren.Add(BuildChild("NullSnap_"));

        data = BuildChild("SnapPoint_IndLargeFloor_1");
        data.Children = ChildrenOfChildren;
        mbin.Children.Add(data);
    }

    protected TkTransformData SetRotY(int i)
    {
        TkTransformData tfd = new() { RotY = 90 * (i - 1) };
        if (tfd.RotY > 180)
        {
            tfd.RotY -= 360;
        }
        return tfd;
    }

    protected void IndustrialScenes()
    {
        var scenes = new[]
        {
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/COMMONPARTS/TELEPORTER_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/ANTIMATTERHARVESTER_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/CREATUREFOODMAKER_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/CREATUREHARVESTER_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/GASHARVESTER_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/HARVESTER_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/REFINER2_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/TECH/REFINER3_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULE_BATTERYS_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULE_BIOGENERATOR_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULE_GASEXTRACTOR_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULE_GENERATORS_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULE_PUMPS_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULE_SILOS_PLACEMENT.SCENE.MBIN",
            "MODELS/PLANETS/BIOMES/COMMON/BUILDINGS/PARTS/BUILDABLEPARTS/UTILITYPARTS/MODULE_SOLARPANELS_PLACEMENT.SCENE.MBIN",
        };

        foreach (var sceneFile in scenes)
        {
            var mbin = ExtractMbin<TkSceneNodeData>(sceneFile);

            List<TkSceneNodeData> SubChildren = new();

            var names = new[] { "IndLarge_Out_", "IndLarge_In_", "NullSnap_" };

            foreach (var name in names)
            {
                TkSceneNodeData child = BuildChild(name);

                if (name == "IndLarge_In_")
                {
                    child.Transform.RotY = 180;
                }
                SubChildren.Add(child);
            }

            var directions = new[] { "N", "E", "S", "W" };
            foreach (var dir in directions)
            {
                TkTransformData tfd = new();
                switch (dir)
                {
                    case "N":
                        tfd.TransX = -1.333333f;
                        tfd.RotY = -90;
                        break;
                    case "E":
                        tfd.TransZ = -1.333333f;
                        tfd.RotY = 180;
                        break;
                    case "S":
                        tfd.TransX = 1.3333333f;
                        tfd.RotY = 90;
                        break;
                    case "W":
                        tfd.TransZ = 1.33333f;
                        break;
                }
                if (
                    sceneFile.EndsWith("TELEPORTER_PLACEMENT.SCENE.MBIN")
                    || sceneFile.EndsWith("ANTIMATTERHARVESTER_PLACEMENT.SCENE.MBIN")
                    || sceneFile.EndsWith("REFINER3_PLACEMENT.SCENE.MBIN")
                )
                {
                    tfd.TransX *= 2;
                    tfd.TransY *= 2;
                    tfd.TransZ *= 2;
                }
                TkSceneNodeData child = BuildChild("SnapPoint_IndSelf_" + dir, tfd);
                child.Children = SubChildren;

                mbin.Children.Add(child);
            }

            TkSceneNodeData LastOne = BuildChild("SnapPoint_IndLargeFloor_1");
            LastOne.Children = new();
            LastOne.Children.Add(BuildChild("IndustrialLargeFloor_In_1"));
            LastOne.Children.Add(BuildChild("NullSnap_"));
            mbin.Children.Add(LastOne);
        }
    }
};


//...........................................................

//=============================================================================
