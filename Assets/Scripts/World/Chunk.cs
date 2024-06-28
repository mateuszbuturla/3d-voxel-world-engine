using UnityEditor;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    private Voxel[,,] voxels;
    public World world;
    private Color gizmoColor;

    public void Initialize(World world)
    {
        this.world = world;
        voxels = new Voxel[world.chunkSize, world.chunkHeight, world.chunkSize];
        gizmoColor = new Color(Random.value, Random.value, Random.value, 0.4f);
        InitializeVoxels();
    }

    void Start()
    {
        voxels = new Voxel[world.chunkSize, world.chunkHeight, world.chunkSize];
        InitializeVoxels();
    }

    private void InitializeVoxels()
    {
        for (int x = 0; x < world.chunkSize; x++)
        {
            for (int y = 0; y < world.chunkHeight; y++)
            {
                for (int z = 0; z < world.chunkSize; z++)
                {
                    voxels[x, y, z] = new Voxel(transform.position + new Vector3(x, y, z), Color.white);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (voxels != null)
        {
            for (int x = 0; x < world.chunkSize; x++)
            {
                for (int y = 0; y < world.chunkSize; y++)
                {
                    for (int z = 0; z < world.chunkSize; z++)
                    {
                        if (voxels[x, y, z].isActive)
                        {
                            Gizmos.color = gizmoColor;
                            Gizmos.DrawCube(transform.position + new Vector3(x, y, z), Vector3.one);
                        }
                    }
                }
            }
        }
    }
}