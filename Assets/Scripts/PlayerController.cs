using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float edge = -4.5f;
    public float gravity = 0.81f;
    public float jumpForce = 10f;
    public float groundLevel = 0.19f;

    Vector3 newPos = new Vector3(0, 0.48f, -6.23f);

    Rigidbody rb;

    public float lerpValue = 10f;


    private InputManager inputManager;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
        inputManager.OnCancelTouch += Jump;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
        inputManager.OnCancelTouch -= Jump;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, 1f)); // raycast down to look for ground is not detecting ground? only works if allowing jump when grounded = false; // return "Ground" layer as layer
    }
    void MovePlayer()
    {
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, -6.23f), new Quaternion(0,0,0,0));


        if (inputManager.fingerOnScreen)
        {
            newPos = new Vector3(newPos.x, groundLevel, newPos.z);
        }
        if (newPos.x <= edge)
        {
            newPos = new Vector3(edge, newPos.y, newPos.z);
        }
        if (newPos.x >= -edge)
        {
            newPos = new Vector3(-edge, newPos.y, newPos.z);
        }
    }
    void Update()
    {
        Move(inputManager.touchPos);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, lerpValue);
        ApplyGravity();
    }
    private void LateUpdate()
    {
        MovePlayer();
    }
    public void Move(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            newPos = new Vector3(hit.point.x, transform.position.y, transform.position.z);
        }
    }
    void Jump()
    {
        if (IsGrounded())
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }
    void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity);
    }
}
