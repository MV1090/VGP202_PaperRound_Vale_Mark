using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{    
    public BoxCollider2D bc;

    // Start is called before the first frame update
    public virtual void Start()
    {          
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void setProjectilePos()
    {

    }

    public virtual void setProjectileRotation()
    {

    }

}
