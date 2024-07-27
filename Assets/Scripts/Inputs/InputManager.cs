using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    #region Events
    public static event Action<Vector2, float> OnStartTouch;
    public static event Action<Vector2, float> OnEndTouch;
    #endregion

    Inputs playerInputs;

    protected override void Awake()
    { 
        base.Awake();
        playerInputs = new Inputs();      
    }

    private void OnEnable()
    {
        playerInputs.Enable();
        playerInputs.Touch.PrimaryContact.started += (ctx) => OnStartTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
        playerInputs.Touch.PrimaryContact.canceled += (ctx) => OnEndTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
    }

    private void OnDisable()
    {
        playerInputs.Disable();
        playerInputs.Touch.PrimaryContact.started -= (ctx) => OnStartTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
        playerInputs.Touch.PrimaryContact.canceled -= (ctx) => OnEndTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
    }
    
    public Vector2 PrimaryPosition()
    {
        return ScreenToWorld(playerInputs.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    Vector3 ScreenToWorld(Vector3 pos)
    {
        pos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(pos);
    }
    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
