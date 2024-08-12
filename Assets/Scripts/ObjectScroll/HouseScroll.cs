using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class HouseScroll : ObjectScroll
{
    public bool hasBeenHit;

    public override void Start()
    {
        speed = 2.0f;
        base.Start();
        hasBeenHit = false;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        hasBeenHit = false;
    }
    
}
