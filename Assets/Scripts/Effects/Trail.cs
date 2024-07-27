using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Trail : MonoBehaviour
{
    TrailRenderer tr;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        tr.enabled = false;
    }

    private void OnEnable()
    {
        InputManager.OnStartTouch += UpdateGameObjectPosition;
        InputManager.OnEndTouch += TurnOffObject;
    }

    private void OnDisable()
    {
        InputManager.OnStartTouch -= UpdateGameObjectPosition;
        InputManager.OnEndTouch -= TurnOffObject;
    }

    private void TurnOffObject(Vector2 pos, float time)
    {
        transform.position = pos;
        tr.enabled = false;
    }

    private void UpdateGameObjectPosition(Vector2 pos, float time)
    {
        transform.position = pos;
        tr.enabled = true;
    }

    private void Update()
    {
        if (!tr.enabled)
            return;

        Vector2 curPos = InputManager.Instance.PrimaryPosition();
        transform.position = curPos;
    }
}


   

   
