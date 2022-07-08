using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public GameObject player;
    public GameObject textObject;
    public float score;

    PlayerController playerController;
    TextMeshProUGUI text;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        text = textObject.GetComponent<TextMeshProUGUI>();
    }
    private void FixedUpdate()
    {
        if (player != null)
        {
            if (playerController.IsGrounded())
            {
                score += 0.2f;
            }
            text.text = Mathf.Round(score).ToString();
        }
    }
}
