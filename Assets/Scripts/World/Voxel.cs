using UnityEngine;

public struct Voxel
{
    public Vector3 position;
    public VoxelType voxelType;
    public bool isActive;
    public Voxel(Vector3 position, VoxelType voxelType, bool isActive = true)
    {
        this.position = position;
        this.voxelType = voxelType;
        this.isActive = isActive;
    }
}