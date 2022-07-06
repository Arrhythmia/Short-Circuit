using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject[] spawnPosX = new GameObject[4];
    public float spawnPosY = 0.65f;
    public float spawnPosZ = 136;
    public float nextPosZ = 2.0073f;
    public int platformLength = 70;

    System.Random random = new System.Random();
    void Start()
    {
        SpawnRandomObstacles();
    }

    void SpawnRandomObstacles()
    {
        List<GameObject> obstacleRow = new List<GameObject>();
        Vector3 spawnPos = new Vector3(0, spawnPosY, spawnPosZ);
        for (int i = 0; i <= platformLength; i++)
        {
            for (int j = 0; j < spawnPosX.Length; j++)
            {
                if (random.Next(1, 15) == 1)
                {
                    spawnPos.x = spawnPosX[j].transform.position.x;
                    GameObject spawnedObstacle = Instantiate(obstacle, spawnPos, new Quaternion(0, 0, 0, 0));
                    obstacleRow.Add(spawnedObstacle);
                    spawnedObstacle.transform.SetParent(transform, true);
                }
                if (obstacleRow.Count >= 4)
                {
                    Destroy(obstacleRow[random.Next(0, 3)]);
                }
            }
            spawnPos.z += nextPosZ; // Prepare next spawning position
        }
    }
}
