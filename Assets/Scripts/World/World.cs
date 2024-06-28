using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }

    public Material voxelMaterial;

    public int worldSize = 4;
    public int chunkSize = 16;
    public int chunkHeight = 100;

    private Dictionary<Vector3, Chunk> chunks;

    void Start()
    {
        chunks = new Dictionary<Vector3, Chunk>();

        GenerateWorld();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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

    public Chunk GetChunkAt(Vector3 globalPosition)
    {
        Vector3Int chunkCoordinates = new Vector3Int(
            Mathf.FloorToInt(globalPosition.x / chunkSize) * chunkSize,
            Mathf.FloorToInt(globalPosition.y / chunkHeight) * chunkHeight,
            Mathf.FloorToInt(globalPosition.z / chunkSize) * chunkSize
        );

        if (chunks.TryGetValue(chunkCoordinates, out Chunk chunk))
        {
            return chunk;
        }

        return null;
    }
}
