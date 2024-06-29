using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldHelper
{
    public static List<Vector3Int> GetChunksPos(World world)
    {
        List<Vector3Int> chunks = new List<Vector3Int>();

        for (int x = 0; x < world.mapSizeInChunks; x++)
        {
            for (int z = 0; z < world.mapSizeInChunks; z++)
            {
                chunks.Add(new Vector3Int(x * world.chunkSize, 0, z * world.chunkSize));
            }
        }

        return chunks;
    }
}
