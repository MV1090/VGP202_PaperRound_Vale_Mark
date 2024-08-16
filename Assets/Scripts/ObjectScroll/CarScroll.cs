using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScroll : ObjectScroll
{
    public override void Start()
    {
        speed = 4.0f;
        base.Start();
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.Find("").GetComponent<Collider2D>());
    }

    public override void Update()
    {
        base.Update();
    }
       
}
