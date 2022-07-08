using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public GameObject platformPrefab;
    public float currentPlatformEnd = 63.03f;
    public float nextPlatformSpawn = 204.8f;
    float defaultSpeed = 30f;
    public float acceleration = 1f;
    void Update()
    {
        SpawnPlatform();
    }
    void SpawnPlatform()
    {
        if (transform.GetChild(transform.childCount - 1).position.z <= currentPlatformEnd)
        {
            GameObject newPlatform = Instantiate(platformPrefab, new Vector3(0, -0.81f, nextPlatformSpawn), new Quaternion(0, 0, 0, 0), transform);
            PlaneMovement planeMovement = newPlatform.GetComponent<PlaneMovement>();
            defaultSpeed += acceleration;
            planeMovement.speed = defaultSpeed;
        }
    }
}
