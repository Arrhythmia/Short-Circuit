using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float speed = 5f;
    public float endLocation = -78f;
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * -speed);


        //self-cleaning
        if (transform.position.z <= endLocation)
        {
            Destroy(gameObject);
        }
    }
}
