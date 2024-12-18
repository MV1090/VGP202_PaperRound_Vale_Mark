using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public float minDist = 0.3f;
    public float maxTime = 0.8f;

    [Range(0, 1)]
    public float distThreshold = 0.9f;

    Vector2 startPos;
    Vector2 endPos;
    float startTime;
    float endTime;
       

    private void OnEnable()
    {
        InputManager.OnStartTouch += SwipeStart;
        InputManager.OnEndTouch += SwipeEnd;
    }    

    private void OnDisable()
    {
        InputManager.OnStartTouch -= SwipeStart;
        InputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 pos, float time)
    {
        startPos = pos;
        startTime = time;
    }
    private void SwipeEnd(Vector2 pos, float time)
    {
        endPos = pos;
        endTime = time;
        CheckSwipe();
    }

    void CheckSwipe()
    {
        if (Vector2.Distance(startPos, endPos) >= minDist && (endTime - startTime) <= maxTime)
        {
            Vector2 dir = (endPos - startPos).normalized;
            SwipeDirection(dir);
        }
    }

   public virtual void SwipeDirection(Vector2 dir)
    {
        if (Vector2.Dot(Vector2.up, dir) >= distThreshold)
            Debug.Log("Swipe up");
        if (Vector2.Dot(Vector2.down, dir) >= distThreshold)
            Debug.Log("Swipe down");
        if (Vector2.Dot(Vector2.left, dir) >= distThreshold)
            Debug.Log("Swipe left");
        if (Vector2.Dot(Vector2.right, dir) >= distThreshold)
            Debug.Log("Swipe right");
    }
   
       // Update is called once per frame
    void Update()
    {
        
    }
}
