using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public NoiseSettings biomeNoiseSettings;
    public List<BiomeGenerator> biomeGenerators = new List<BiomeGenerator>();

    public Dictionary<Vector3Int, float> heightMap = new Dictionary<Vector3Int, float>();
    public bool useDomainWarping;

    public void GenerateData(List<Vector3Int> chunksToGenerate)
    {
        World world = World.Instance;
        foreach (Vector3Int chunkPos in chunksToGenerate)
        {
            for (int x = 0; x < world.chunkSize; x++)
            {
                for (int z = 0; z < world.chunkSize; z++)
                {
                    GenerateHeightMap(chunkPos.x + x, chunkPos.z + z);
                }
            }
        }
    }

    private float GenerateHeightMap(int x, int z)
    {
        float height = TerrainHelper.GetSurfaceHeight(this, x, z);
        heightMap.Add(new Vector3Int(x, 0, z), height);

        return height;
    }

    public ChunkData GenerateChunkData(ChunkData data)
    {
        BiomeGenerator biomeGenerator = biomeGenerators[0];

        for (int x = 0; x < data.chunkSize; x++)
        {
            for (int z = 0; z < data.chunkSize; z++)
            {
                data = biomeGenerator.ProcessColumn(data, x, z);
            }
        }

        return data;
    }
}
