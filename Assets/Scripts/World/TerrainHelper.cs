public static class TerrainHelper
{
    public static BiomeGenerator GetBiomeGenerator(TerrainGenerator terrainGenerator, int x, int z)
    {
        float noise = OctavePerlin.Noise(x, z, terrainGenerator.biomeNoiseSettings);

        if (noise < 0.5f)
        {
            return terrainGenerator.biomeGenerators[0];
        }
        else
        {
            return terrainGenerator.biomeGenerators[1];
        }
    }

    public static float GetSurfaceHeight(TerrainGenerator terrainGenerator, int x, int z)
    {
        float noiseValue;

        BiomeGenerator biomeGenerator = GetBiomeGenerator(terrainGenerator, x, z);

        if (terrainGenerator.useDomainWarping)
        {
            noiseValue = biomeGenerator.domainWarping.GenerateDomainNoise(x, z, biomeGenerator.noiseSettings);
        }
        else
        {
            noiseValue = OctavePerlin.Noise(x, z, biomeGenerator.noiseSettings);
        }

        return noiseValue;
    }
}
