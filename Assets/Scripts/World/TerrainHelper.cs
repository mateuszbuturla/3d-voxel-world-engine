public static class TerrainHelper
{
    public static float GetSurfaceHeight(TerrainGenerator terrainGenerator, int x, int z)
    {
        float noiseValue;

        BiomeGenerator biomeGenerator = terrainGenerator.biomeGenerators[0];

        if (x > 150 && z > 150)
        {
            biomeGenerator = terrainGenerator.biomeGenerators[1];
        }

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
