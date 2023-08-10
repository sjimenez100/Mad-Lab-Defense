using System.Linq;
using UnityEngine;

[System.Serializable]
public struct SpawnObject 
{
    public GameObject spawnObject;

    [Range(0, 1)]
    public float probability;

    public static SpawnObject[] Normalize(SpawnObject[] spawnObjects)
    {

        float acc = spawnObjects.Sum(s => s.probability);

        return spawnObjects.Select(s => { s.probability /= acc; return s; }).ToArray();

    }

    // must be normalized prior to generation
    public static GameObject GetRandom(GameObject[] gameObjects, float[] weights)
    {
        
        float randomValue = Random.value;
        float currentWeight = 0f;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            currentWeight += weights[i];

            if (randomValue <= currentWeight)
                return gameObjects[i];

        }

        return gameObjects[0];
    }

}
