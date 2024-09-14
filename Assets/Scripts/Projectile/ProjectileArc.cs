using UnityEngine;

public class ProjectileArc : MonoBehaviour
{
    
    RaycastHit2D hit;
    // Start is called before the first frame update
    void Start()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.up, 10f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hit)
        {
            Debug.Log("Hit something: " + hit.collider.gameObject.name);
        }
    }

    void CheckForColliders()
    {
       
    }
}
