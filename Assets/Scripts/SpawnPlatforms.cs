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
    public float platformYAxis = -0.81f;
    int platformCount; // Platform count helps alternate Y axis for each platform to prevent Z-fighting
    private void Start()
    {
        platformCount = 0;
    }
    void Update()
    {
        SpawnPlatform();
    }
    void SpawnPlatform()
    {
        if (transform.GetChild(transform.childCount - 1).position.z <= currentPlatformEnd)
        {
            GameObject newPlatform;
            if (platformCount % 2 != 0)
            {
                newPlatform = Instantiate(platformPrefab, new Vector3(0, platformYAxis, nextPlatformSpawn), new Quaternion(0, 0, 0, 0), transform);
            }
            else
            {
                newPlatform = Instantiate(platformPrefab, new Vector3(0, platformYAxis + 0.001f, nextPlatformSpawn), new Quaternion(0, 0, 0, 0), transform);
            }
            PlaneMovement planeMovement = newPlatform.GetComponent<PlaneMovement>();
            defaultSpeed += acceleration;
            planeMovement.speed = defaultSpeed;
            platformCount++;
        }
    }
}
