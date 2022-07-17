using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerTracker : MonoBehaviour
{
    public Transform trackerTransform;
    public float yAxis = -0.34f;
    void Update()
    {
        trackerTransform.position = new Vector3(trackerTransform.position.x, yAxis, trackerTransform.position.z);
    }
}
