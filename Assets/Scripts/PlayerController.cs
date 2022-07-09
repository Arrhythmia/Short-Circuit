using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float zPos = -6.23f;
    public float lerpValue = 10f;

    Vector3 newPos;
    Rigidbody rb;
    public GameObject GameManagerObject;
    private InputManager inputManager;
    private TimeManager timeManager;
    private GameManager gameManager;

    public bool godMode = false;
    [Header("Movement values")]
    public float speed = 5f;
    public float edge = -4.5f;
    public float gravity = 0.81f;
    public float jumpForce = 10f;
    public float groundLevel = 0.19f;
    public float groundDetection = 0.6f;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeManager = GameManagerObject.GetComponent<TimeManager>();
        gameManager = GameManagerObject.GetComponent<GameManager>();
        newPos = new Vector3(0, groundLevel, zPos);
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += UpdateNewPos;
        inputManager.OnCancelTouch += Jump;
    }
    private void OnDisable()
    {
        inputManager.OnEndTouch -= UpdateNewPos;
        inputManager.OnCancelTouch -= Jump;
    }
    public bool IsGrounded()
    {
        return (Physics.Raycast(transform.position, Vector3.down, groundDetection));
    }
    void MovePlayer()
    {
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

        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, zPos), new Quaternion(0, 0, 0, 0)); // Check later if necessary
    }
    void Update()
    {
        UpdateNewPos(inputManager.touchPos);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, lerpValue);
        ApplyGravity();
    }
    private void LateUpdate()
    {
        MovePlayer();

        Debug.DrawRay(transform.position, Vector3.down * groundDetection, Color.red);
    }
    public void UpdateNewPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        RaycastHit[] hits = Physics.RaycastAll(ray, 50f);
        
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                newPos = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                break;
            }
        }
    }
    void Jump()
    {
        if (IsGrounded())
            rb.AddForce(jumpForce * Time.fixedDeltaTime * Vector3.up, ForceMode.Impulse);
    }
    void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle") && !godMode)
        {
            gameManager.OnDeath();
            timeManager.SlowDown();
            gameObject.SetActive(false);
        }
    }
}
