using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class World : MonoBehaviour
{
    public static World Instance { get; private set; }
    public int mapSizeInChunks = 6;
    public int chunkSize = 16;
    public int chunkHeight = 100;
    public int waterThreshold = 50;
    public GameObject chunkPrefab;
    public TerrainGenerator terrainGenerator;

    Dictionary<Vector3Int, ChunkData> chunkDataDictionary = new Dictionary<Vector3Int, ChunkData>();
    Dictionary<Vector3Int, ChunkRenderer> chunkDictionary = new Dictionary<Vector3Int, ChunkRenderer>();

    public bool useDomainWarping = false;

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

    public void GenerateWorld()
    {
        chunkDataDictionary.Clear();
        foreach (ChunkRenderer chunk in chunkDictionary.Values)
        {
            Destroy(chunk.gameObject);
        }
        chunkDictionary.Clear();
        terrainGenerator.heightMap = new Dictionary<Vector3Int, float>();

        StartCoroutine(GenerateChunks());
    }

    internal BlockType GetBlockFromChunkCoordinates(ChunkData chunkData, int x, int y, int z)
    {
        Vector3Int pos = Chunk.ChunkPositionFromBlockCoords(this, x, y, z);
        ChunkData containerChunk = null;

        chunkDataDictionary.TryGetValue(pos, out containerChunk);

        if (containerChunk == null)
        {
            return BlockType.Nothing;
        }

        Vector3Int blockInCHunkCoordinates = Chunk.GetBlockInChunkCoordinates(containerChunk, new Vector3Int(x, y, z));
        return Chunk.GetBlockFromChunkCoordinates(containerChunk, blockInCHunkCoordinates);
    }

    public IEnumerator GenerateChunks()
    {
        List<Vector3Int> chunksToGenerate = WorldHelper.GetChunksPos(this);

        terrainGenerator.GenerateData(chunksToGenerate);

        for (int i = 0; i < chunksToGenerate.Count; i++)
        {
            ChunkData data = new ChunkData(
                chunkSize,
                chunkHeight,
                this,
                chunksToGenerate[i]
            );
            data = terrainGenerator.GenerateChunkData(data);

            chunkDataDictionary.Add(data.worldPosition, data);

            MeshData meshData = Chunk.GetChunkMeshData(data);
            GameObject chunkObject = Instantiate(chunkPrefab, data.worldPosition, Quaternion.identity);
            ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();
            chunkDictionary.Add(data.worldPosition, chunkRenderer);
            chunkRenderer.InitializeChunk(data);
            chunkRenderer.UpdateChunk(meshData);
            yield return new WaitForSeconds(0f);
        }

        // foreach (ChunkData data in chunkDataDictionary.Values)
        // {
        //     MeshData meshData = Chunk.GetChunkMeshData(data);
        //     GameObject chunkObject = Instantiate(chunkPrefab, data.worldPosition, Quaternion.identity);
        //     ChunkRenderer chunkRenderer = chunkObject.GetComponent<ChunkRenderer>();
        //     chunkDictionary.Add(data.worldPosition, chunkRenderer);
        //     chunkRenderer.InitializeChunk(data);
        //     chunkRenderer.UpdateChunk(meshData);
        // }

        // foreach (var pos in chunkDataDictionary.Keys)
        // {
        //     ChunkRenderer chunkRenderer = chunkDictionary[pos].gameObject.GetComponent<ChunkRenderer>();
        //     MeshData meshData = Chunk.GetChunkMeshData(chunkDataDictionary[pos]);
        //     chunkRenderer.UpdateChunk(meshData);
        // }
    }
}
