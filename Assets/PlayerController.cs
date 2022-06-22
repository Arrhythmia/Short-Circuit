using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float edge = 4.5f;
    float forwardInput;
    void Update()
    {
        forwardInput = Input.GetAxis("Horizontal");
        if ((forwardInput > 0 && transform.position.x <= edge) || (forwardInput < 0 && transform.position.x >= -edge))
            transform.Translate(Vector3.right * Time.deltaTime * speed * forwardInput);
    }
}
