using UnityEngine;

public class BiomeGenerator : MonoBehaviour
{
    public NoiseSettings noiseSettings;
    public DomainWarping domainWarping;
    public TreePlacement treePlacement;

    public ChunkData ProcessColumn(ChunkData chunkData, int x, int z)
    {
        World world = World.Instance;
        TerrainGenerator terrainGenerator = World.Instance.terrainGenerator;

        float heightNoise = terrainGenerator.heightMap[new Vector3Int(chunkData.worldPosition.x + x, 0, chunkData.worldPosition.z + z)];
        int groundHeight = OctavePerlin.RemapValue01ToInt(heightNoise, 0, world.chunkHeight);

        for (int y = 0; y < world.chunkHeight; y++)
        {
            BlockType voxelType = BlockType.Dirt;
            if (y > groundHeight)
            {
                if (y > world.waterThreshold)
                {
                    voxelType = BlockType.Air;
                }
                else
                {
                    voxelType = BlockType.Water;
                }
            }
            else
            {
                if (y < world.waterThreshold + 3)
                {
                    voxelType = BlockType.Sand;
                }
                else if (y == groundHeight)
                {
                    voxelType = BlockType.Grass;
                }
                else if (y < groundHeight)
                {
                    voxelType = BlockType.Dirt;
                }
            }

            Chunk.SetBlock(chunkData, new Vector3Int(x, y, z), voxelType);

            // terrainGenerator.biomeGenerators[0].treePlacement.Generate(chunkData, x, z, groundHeight);
        }


        return chunkData;
    }
}
