using UnityEngine;

//public class ProjectileArc : MonoBehaviour
//{
//    Vector2 targetPos;
//    Vector2 targetDir;
//    Vector2 startPos;
//    RaycastHit2D hit;
//    LayerMask mask;

//    float distance;

//    // Start is called before the first frame update
//    void Start()
//    {
        

//        CheckForCollision();
//    }

//    // Update is called once per frame
//    void Update()
//    {
      
//    }

//    void CheckForCollision()
//    {
//        if (hit)
//        {
//            distance = hit.distance;
//            Debug.Log("Hit something: " + hit.distance);
//        }
//        else
//            distance = 7;
//    }

//    void SetUpRay()
//    {        
//        mask = LayerMask.GetMask("House");
//        targetDir = (targetPos - startPos).normalized;

//        hit = Physics2D.Raycast(transform.position, targetDir, 10000f, mask);

//        Debug.DrawRay(transform.position, targetDir, Color.red, 1000f);
//    }
//}
