using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionParticleController : MonoBehaviour
{
    PlayerController playerController;
    public ParticleSystem particleSystem;
    LineRenderer lineRenderer;
    Trail trail;
    public GameObject trailGenerator;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        lineRenderer = trailGenerator.GetComponent<LineRenderer>();
        trail = trailGenerator.GetComponent<Trail>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleParticleSystem();
    }

    void HandleParticleSystem()
    {
        if (playerController.IsGrounded() && !particleSystem.isEmitting)
        {
            particleSystem.Play();
            lineRenderer.enabled = false;
            trail.enabled = false;
            ClearLineRenderer(lineRenderer);
        }
        if (!playerController.IsGrounded())
        {
            particleSystem.Stop();
            lineRenderer.enabled = true;
            trail.enabled = true;
        }
    }

    void ClearLineRenderer(LineRenderer lr)
    {
        for (int i = 0; i < lr.positionCount; i++)
        {
            lr.SetPosition(i, Vector3.zero);
        }
        lr.positionCount = 0;
    }
}
