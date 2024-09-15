using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{    
    public BoxCollider2D bc;
    public SpriteRenderer sr;

    // Start is called before the first frame update
    public virtual void Start()
    {          
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();    
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void setProjectilePos()
    {

    }

    public virtual void OnEnable()
    {

    }

    public virtual void setProjectileRotation()
    {

    }

}
