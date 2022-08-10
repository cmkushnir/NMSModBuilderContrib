//=============================================================================

public class bDrawDistance : cmk.NMS.Script.ModClass
{
    protected static int MAX_SIZE = 9999;
    protected static float RADIUS_MULTIPLIER = 3; //objects draw distance multiplier (limited by engine's hard-limit)
    protected static float GRASS_RADIUS_MULTIPLIER = 2;
    protected static float LOD_DISTANCE_MULTIPLIER = 2;
    protected static float COVERAGE_MULTIPLIER = 1;

    protected static float SPAWNDISTANCE_MULTIPLIER = 4;

    protected static bool FORCE_UNCACHED_TERRAIN = true;
    protected static float SHADOW_LENGTH_MULTIPLIER = 3;

    protected static float BUILDINGS_FADE_MULTIPLIER = 2;

    protected static float LOD_ADJUST_MULTIPLIER = 2;
    protected static int REGIONLODRADIUS_ADD = 3;
    protected static float PLANET_LOD_MULTIPLIER = 10;

    protected override void Execute()
    {
        Try(() => TweakBiomeDrawDistance());
        Try(() => TweakCreatureSpawnDistance());
        Try(() => GcGraphicsGlobals());
        Try(() => GcBuildingGlobals());
        Try(() => GcEnvironmentGlobals());
    }

    protected void TweakBiomeDrawDistance()
    {
        var entities = Game.Mbinc.FindClass("GcExternalObjectList");
        foreach (var path in entities.PakItems)
        {
            if (!path.StartsWith("METADATA/SIMULATION/SOLARSYSTEM/BIOMES/"))
                continue;

            var mbin = ExtractMbin<GcExternalObjectList>(path);

            List<GcObjectSpawnData>[] lists =
            {
                mbin.Objects.Landmarks,
                mbin.Objects.Objects,
                mbin.Objects.DistantObjects,
                mbin.Objects.DetailObjects
            };

            foreach (var field in lists)
            {
                foreach (var entry in field)
                {
                    processLimitedObjectSpawnData(entry.QualityVariantData);
                    foreach (var variant in entry.QualityVariants)
                    {
                        processLimitedObjectSpawnData(variant);
                    }
                }

                var grass = field.Find(NAME => NAME.Placement == "GRASS");
                if (grass == null)
                    continue;
                processGrass(grass.QualityVariantData);
                foreach (var variant in grass.QualityVariants)
                {
                    processGrass(variant);
                }
            }
        }
    }

    protected void processGrass(GcObjectSpawnDataVariant OBJ)
    {
        OBJ.MaxRegionRadius = (int)(
            OBJ.MaxRegionRadius * 1.0f / RADIUS_MULTIPLIER * GRASS_RADIUS_MULTIPLIER
        );
        OBJ.MaxImposterRadius = (int)(
            OBJ.MaxImposterRadius * 1.0f / RADIUS_MULTIPLIER * GRASS_RADIUS_MULTIPLIER
        );
        OBJ.FadeOutStartDistance *= 1.0f / RADIUS_MULTIPLIER * GRASS_RADIUS_MULTIPLIER;
        OBJ.FadeOutEndDistance *= 1.0f / RADIUS_MULTIPLIER * GRASS_RADIUS_MULTIPLIER;
    }

    protected void processLimitedObjectSpawnData(GcObjectSpawnDataVariant OBJ)
    {
        for (int i = 0; i < 5; ++i)
        {
            OBJ.LodDistances[i] *= LOD_DISTANCE_MULTIPLIER;
        }
        OBJ.Coverage = verifiedMultiplier(OBJ.Coverage, COVERAGE_MULTIPLIER);
        OBJ.MaxRegionRadius = (int)verifiedMultiplier(OBJ.MaxRegionRadius, RADIUS_MULTIPLIER);
        OBJ.MaxImposterRadius = (int)verifiedMultiplier(OBJ.MaxImposterRadius, RADIUS_MULTIPLIER);
        OBJ.FadeOutStartDistance = verifiedMultiplier(OBJ.FadeOutStartDistance, RADIUS_MULTIPLIER);
        OBJ.FadeOutEndDistance = verifiedMultiplier(OBJ.FadeOutEndDistance, RADIUS_MULTIPLIER);
    }

    protected float verifiedMultiplier(float ORIGINAL, float MULTIPLIER)
    {
        return ((ORIGINAL < MAX_SIZE) ? ORIGINAL * MULTIPLIER : ORIGINAL);
    }

    protected void TweakCreatureSpawnDistance()
    {
        var entities = Game.Mbinc.FindClass("GcCreatureRoleDescriptionTable");
        var count = 0;
        foreach (var path in entities.PakItems)
        {
            if (!path.StartsWith("METADATA/SIMULATION/ECOSYSTEM/"))
                continue;
            var mbin = ExtractMbin<GcCreatureRoleDescriptionTable>(path);
            count++;
            foreach (var entry in mbin.RoleDescription)
            {
                entry.IncreasedSpawnDistance *= SPAWNDISTANCE_MULTIPLIER;
            }
        }
        Log.AddInformation("Processed " + count.ToString() + " creature files");
    }

    protected void GcGraphicsGlobals()
    {
        var mbin = ExtractMbin<GcGraphicsGlobals>("GCGRAPHICSGLOBALS.GLOBAL.MBIN");
        mbin.ForceUncachedTerrain = FORCE_UNCACHED_TERRAIN;
        mbin.ShadowLength *= SHADOW_LENGTH_MULTIPLIER;
        mbin.ShadowLengthShip *= SHADOW_LENGTH_MULTIPLIER;
        mbin.ShadowLengthSpace *= SHADOW_LENGTH_MULTIPLIER;
        mbin.ShadowLengthStation *= SHADOW_LENGTH_MULTIPLIER;
        mbin.ShadowLengthCameraView *= SHADOW_LENGTH_MULTIPLIER;
    }

    protected void GcBuildingGlobals()
    {
        var mbin = ExtractMbin<GcBuildingGlobals>("GCBUILDINGGLOBALS.GLOBAL.MBIN");
        mbin.FadeDistance *= BUILDINGS_FADE_MULTIPLIER;
    }

    protected void GcEnvironmentGlobals()
    {
        var mbin = ExtractMbin<GcEnvironmentGlobals>("GCENVIRONMENTGLOBALS.GLOBAL.MBIN");
        foreach (var setting in mbin.LODSettings)
        {
            for (var i = 0; i < setting.LODAdjust.Length; ++i)
            {
                setting.LODAdjust[i] *= LOD_ADJUST_MULTIPLIER;
            }
            for (var i = 0; i < setting.RegionLODRadius.Length; ++i)
            {
                setting.RegionLODRadius[i] += REGIONLODRADIUS_ADD;
            }
        }
        GcEnvironmentProperties[] EnvironmentProperties =
        {
            mbin.EnvironmentProperties,
            mbin.EnvironmentPrimeProperties
        };
        foreach (var property in EnvironmentProperties)
        {
            property.PlanetObjectSwitch *= PLANET_LOD_MULTIPLIER;
            property.PlanetLodSwitch0 *= PLANET_LOD_MULTIPLIER;
            property.PlanetLodSwitch0Elevation *= PLANET_LOD_MULTIPLIER;
            property.PlanetLodSwitch1 *= PLANET_LOD_MULTIPLIER;
            property.PlanetLodSwitch2 *= PLANET_LOD_MULTIPLIER;
            property.PlanetLodSwitch3 *= PLANET_LOD_MULTIPLIER;
        }
    }
    //...........................................................
}

//=============================================================================
