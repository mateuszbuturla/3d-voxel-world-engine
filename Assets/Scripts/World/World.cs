using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public int worldSize = 4;
    public int chunkSize = 16;
    public int chunkHeight = 100;

    private Dictionary<Vector3, Chunk> chunks;

    void Start()
    {
        chunks = new Dictionary<Vector3, Chunk>();

        GenerateWorld();
    }

    private void GenerateWorld()
    {
        for (int x = 0; x < worldSize; x++)
        {
            for (int z = 0; z < worldSize; z++)
            {
                Vector3 chunkPosition = new Vector3(x * chunkSize, 0, z * chunkSize);
                GameObject newChunkObject = new GameObject($"Chunk_{x}_{z}");
                newChunkObject.transform.position = chunkPosition;
                newChunkObject.transform.parent = this.transform;

                Chunk newChunk = newChunkObject.AddComponent<Chunk>();
                newChunk.Initialize(this);
                chunks.Add(chunkPosition, newChunk);
            }
        }
    }
}
