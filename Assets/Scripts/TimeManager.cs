using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    bool slowmo = false;
    public float slowmoTarget = 0f;
    public float slowAmount = 0.0001f;
    void Update()
    {
        if (slowmo && Time.timeScale > slowmoTarget)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowmoTarget, slowAmount);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        if (Time.timeScale == 0f)
        {
            slowmo = false;
        }
    }

    public void SlowDown()
    {
        slowmo = true;
    }

    public void ResetSpeed()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
