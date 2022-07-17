using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCircuits : MonoBehaviour
{
    public GameObject middleCircuitPrefab;
    public Vector3 middleSpawnPos;
    public GameObject sideCircuitPrefab;
    public Vector3 sideSpawnPos;

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.transform.tag);
        if (other.CompareTag("MiddleCircuit"))
        {
            Instantiate(middleCircuitPrefab, middleSpawnPos, new Quaternion(0, 0, 0, 0));
        }
        else if (other.CompareTag("SideCircuit"))
        {
            Instantiate(sideCircuitPrefab, sideSpawnPos, new Quaternion(0, 0, 0, 0));
        }
    }
}
