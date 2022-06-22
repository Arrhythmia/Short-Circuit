using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject[] spawnPosX = new GameObject[4];
    public float spawnPosY = 1.69f;
    public float spawnPosZ = 136;
    public int platformLength = 70;

    System.Random random = new System.Random();
    void Start()
    {
        Vector3 spawnPos = new Vector3(0, spawnPosY, spawnPosZ);
        for (int i = 0; i <= platformLength; i++)
        {
            for (int j = 0; j < spawnPosX.Length; j++)
            {
                if (random.Next(1, 12) == 1)
                {
                    spawnPos.x = spawnPosX[j].transform.position.x;
                    GameObject spawnedObstacle = Instantiate(obstacle, spawnPos, new Quaternion(0, 0, 0, 0));
                    spawnedObstacle.transform.SetParent(transform, true);
                }
            }
            spawnPos.z += 2.0073f;
        }
    }
}
