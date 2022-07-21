using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitMovement : MonoBehaviour
{
    public float speed = 30f;
    public float endLocation = -100.19f;

    public Vector3 spawnNextPos;
    public Vector3 nextPos;

    bool spawnedNext = false;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * -speed);

        if (transform.position.z <= spawnNextPos.z && !spawnedNext)
        {
            float offset = transform.position.z - spawnNextPos.z;
            spawnedNext = true;
            GameObject nextObject = Instantiate(gameObject, nextPos + new Vector3(0, 0, offset), new Quaternion(0, 0, 0, 0));
            nextObject.name = gameObject.name;
            nextObject.transform.parent = transform.parent;
        }
        //self-cleaning
        if (transform.position.z <= endLocation)
        {
            Destroy(gameObject);
        }
    }
}
