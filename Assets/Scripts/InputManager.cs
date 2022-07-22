using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.EventSystems;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    public delegate void StartTouchEvent(Vector2 position);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position);
    public event EndTouchEvent OnEndTouch;
    public delegate void TouchCancelledEvent();
    public event TouchCancelledEvent OnCancelTouch;

    private TouchControls touchControls;

    public Vector2 touchPos;
    public bool fingerOnScreen;


    
    private void Awake()
    {
        touchControls = new TouchControls();
    }
    private void OnEnable()
    {
        touchControls.Enable();
    }
    private void OnDisable()
    {
        touchControls.Disable();
    }

    private void Start()
    {
        touchPos = new Vector2(Screen.width / 2, Screen.height / 2);
        touchControls.Touch.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch.TouchPress.canceled += ctx => EndTouch(ctx);
    }
    private void FixedUpdate()
    {
        if (fingerOnScreen && !EventSystem.current.IsPointerOverGameObject())
        {
            touchPos = touchControls.Touch.TouchPosition.ReadValue<Vector2>();
        }
    }
    private void StartTouch(InputAction.CallbackContext context)
    {
        fingerOnScreen = true;
        OnStartTouch?.Invoke(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
    }
    private void EndTouch(InputAction.CallbackContext context)
    {
        fingerOnScreen = false;
        OnEndTouch?.Invoke(touchControls.Touch.TouchPosition.ReadValue<Vector2>());
        OnCancelTouch?.Invoke();
    }
    public bool FingerInValidPlace()
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);

        RaycastHit[] hits = Physics.RaycastAll(ray, 200f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("UI"))
            {
                return false;
            }
            if (hit.collider.CompareTag("Platform"))
            {
                if (fingerOnScreen)
                    return true;
            }
        }
        return false;
    }
}
