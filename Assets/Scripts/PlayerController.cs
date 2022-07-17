using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float zPos = -6.23f;
    public float lerpValue = 10f;

    Vector3 newPos;
    Rigidbody rb;

    public GameObject gameManagerObject;
    private InputManager inputManager;
    private TimeManager timeManager;
    private GameManager gameManager;

    public ParticleSystem landingParticles;
    public ParticleSystem deathParticles;

    public bool godMode = false;
    [Header("Movement values")]
    public float speed = 5f;
    public float edge = -4.5f;
    public float gravity = 0.81f;
    public float jumpForce = 10f;
    public float groundLevel = 0.19f;
    public float groundDetection = 0.6f;

    [Header("Camera Shake")]
    public CameraShake cameraShake;
    public float duration;
    public float magnitude;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeManager = gameManagerObject.GetComponent<TimeManager>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
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
        return (Physics.Raycast(transform.position + new Vector3 (0f, 0.1f, 0f), Vector3.down, groundDetection * transform.localScale.x));
    }
    bool hasTappedSinceLastJump = true;
    void MovePlayer()
    {
        if (hasTappedSinceLastJump)
        {
            //newPos = new Vector3(newPos.x, groundLevel, newPos.z);
            transform.position = new Vector3(newPos.x, groundLevel, newPos.z);
        }
        if (newPos.x <= edge)
        {
            newPos = new Vector3(edge, newPos.y, newPos.z);
        }
        if (newPos.x >= -edge)
        {
            newPos = new Vector3(-edge, newPos.y, newPos.z);
        }

        if (transform.position.x <= edge)
        {
            transform.position = new Vector3(edge, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= -edge)
        {
            transform.position = new Vector3(-edge, transform.position.y, transform.position.z);
        }
        transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y, zPos), transform.rotation); // Stop player from rotating unexpectedly
    }
    bool hasJumped = false;
    void Update()
    {
        UpdateNewPos(inputManager.touchPos);

        if (!IsGrounded() && inputManager.fingerOnScreen)
        {
            hasTappedSinceLastJump = true;
        }

        if (IsGrounded())
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(Mathf.Clamp(rb.velocity.y, -50, 50) * -5, 0, 0)); // Player jump rotation
        }
    }
    
    void Land()
    {
        StartCoroutine(cameraShake.Shake(duration, magnitude));
        landingParticles.Play();
        hasJumped = false;
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, newPos, lerpValue);
        ApplyGravity();
    }
    private void LateUpdate()
    {
        MovePlayer();

        Debug.DrawRay(transform.position + new Vector3 (0f, 0.1f, 0f), Vector3.down * groundDetection, Color.red);
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
        {
            hasTappedSinceLastJump = false;
            rb.AddForce(jumpForce * Time.fixedDeltaTime * Vector3.up, ForceMode.Impulse);
            hasJumped = true;
        }

    }
    void ApplyGravity()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle") && !godMode)
        {
            gameManager.Die();
            timeManager.SlowDown();
            deathParticles.Play();
        }

        if (collision.collider.CompareTag("Ground"))
        {
            if (hasJumped && hasTappedSinceLastJump)
            {
                Land();
            }
            hasTappedSinceLastJump = false;
        }
    }
}
