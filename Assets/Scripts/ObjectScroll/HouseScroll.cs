using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class HouseScroll : ObjectScroll
{
    public override void Start()
    {
        speed = 2.0f;
        base.Start();        
    }

    public override void Update()
    {
        base.Update();
    }
    public override void OnEnable()
    {
        speed = 4.0f;
        base.OnEnable();
    }
}
