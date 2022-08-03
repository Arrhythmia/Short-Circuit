using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject[] spawnPosX = new GameObject[4];
    public float spawnPosY = 0.65f;
    public float spawnPosZ = 136;
    public float nextPosZ = 2.0073f;
    public int platformLength = 70;
    public int spawnPercentage = 20;

    System.Random random = new System.Random();
    void Start()
    {
        SpawnRandomObstacleAtRandomPosition();
    }

    void SpawnRandomObstacleAtRandomPosition()
    {
        Vector3 spawnPos = new Vector3(0, spawnPosY, spawnPosZ);
        for (int i = 0; i <= platformLength; i++)
        {
            for (int j = 0; j < spawnPosX.Length; j++)
            {
                if (random.Next(0, 100) <= spawnPercentage)
                {
                    spawnPos.x = spawnPosX[j].transform.position.x;
                    RaycastHit[] hits = Physics.SphereCastAll(spawnPos, 1f, transform.forward);
                    bool posTaken = false;
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.CompareTag("Obstacle"))
                        {
                            posTaken = true;
                        }
                    }
                    if (!posTaken)
                    {
                        int randomObjectIndex = SpawnRandomObstacle(spawnPos, j);

                        if (randomObjectIndex == 6)
                        {
                            j++;
                        }
                    }
                    

                    
                }
            }
            spawnPos.z += nextPosZ; // Prepare next spawning position
        }
    }
    int SpawnRandomObstacle(Vector3 spawnPos, int index)
    {
        int randomObjectIndex = random.Next(0, obstacles.Length);
        GameObject obstacle = obstacles[randomObjectIndex];
        Quaternion rotation;
        if (index == 0 || index == spawnPosX.Length - 1)
        {
            rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            rotation = obstacle.transform.rotation;
        }
        GameObject spawnedObstacle = Instantiate(obstacle, spawnPos, rotation);
        spawnedObstacle.transform.SetParent(transform, true);
        return randomObjectIndex;
    }
}
