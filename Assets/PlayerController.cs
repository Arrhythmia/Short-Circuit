using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float edge = 4.5f;
    public float gravity = 0.81f;
    public float jumpForce = 1f;
    float forwardInput;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    Vector3 newPos;
    Touch touch;
    void TouchInput()
    {
        bool jumpExecuted = true;
        if (Input.touchCount > 0)
        {
            jumpExecuted = false;
            touch = Input.GetTouch(0);

            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (IsGrounded())
                {
                    newPos = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                }
                else
                {
                    newPos = new Vector3(hit.point.x, 0.19f, transform.position.z);
                }
            }

            
        }
        // Touch Jump
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended && !jumpExecuted)
            {
                Jump();
                jumpExecuted = true;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, newPos, lerpValue);

            }
        }
    }
    public float lerpValue;
    bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, 1f)); // raycast down to look for ground is not detecting ground? only works if allowing jump when grounded = false; // return "Ground" layer as layer
    }
    void MovePlayer()
    {
        TouchInput();
        ApplyGravity();
        forwardInput = Input.GetAxis("Horizontal");
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, -6.23f), new Quaternion(0,0,0,0));
        if ((forwardInput > 0 && transform.position.x <= edge) || (forwardInput < 0 && transform.position.x >= -edge))
            transform.Translate(Vector3.right * Time.deltaTime * speed * forwardInput);
    }
    void Jump()
    {
        if (IsGrounded())
            rb.AddForce(Vector3.up * jumpForce);
    }
    void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity);
    }
}
