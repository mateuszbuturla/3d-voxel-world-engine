using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePlacement : MonoBehaviour
{
    public NoiseSettings noiseSettings;

    [Range(0f, 1.0f)]
    public float threshold;
    public GameObject prefab;

    List<Vector2Int> dircetionsToCheck = new List<Vector2Int>() {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
    };

    public void Generate(int x, int z, int groundHeight)
    {
        bool shouldGenerate = IsLocalMaxima(new Vector2Int(x, z));

        if (shouldGenerate)
        {
            GameObject tree = Instantiate(prefab, new Vector3(x, groundHeight, z), Quaternion.identity);
        }
    }

    bool IsLocalMaxima(Vector2Int pos)
    {
        float currentValue = GeneratePerlinNoiseValue(pos);
        float[] neighborValues = new float[dircetionsToCheck.Count];

        for (int i = 0; i < dircetionsToCheck.Count; i++)
        {
            neighborValues[i] = GeneratePerlinNoiseValue(pos + dircetionsToCheck[i]);
        }

        foreach (float neighborValue in neighborValues)
        {
            if (currentValue <= neighborValue)
            {
                return false;
            }
        }

        return currentValue > threshold;
    }

    float GeneratePerlinNoiseValue(Vector2Int pos)
    {
        return OctavePerlin.Noise(pos.x, pos.y, noiseSettings);
    }
}
