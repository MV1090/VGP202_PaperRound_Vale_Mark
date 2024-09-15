using UnityEngine;

public class ProjectileArc : MonoBehaviour
{
    Vector2 targetPos;
    Vector2 targetDir;
    Vector2 startingPos;
    Vector2 distanceVector;
    Vector2 currentPos;

    RaycastHit2D hit;
    LayerMask mask;

    float distance;
    float originalDistance;
    float distanceThisFrame;

    [SerializeField]float widthMultiplier;
    [SerializeField]float heightMultiplier;

    float newWidth;
    float newHeight;
    float originalWidth;
    float originalHeight;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        startingPos = new Vector2(transform.position.x, transform.position.y);
        targetPos = PlayerThrow.Instance.touchPos;

        SetUpRay();
        CheckForCollision();

        originalWidth = sr.size.x;
        originalHeight = sr.size.y;

        newWidth = originalWidth;
        newHeight = originalHeight;

    }

    // Update is called once per frame
    void Update()
    {
        currentPos = new Vector2(transform.position.x, transform.position.y);
        CheckDistance();
        SetScale();      
    }

    void CheckForCollision()
    {
        //if(hit)
        //{
        //    distance = hit.distance;            
        //}
        //else
        //    distance = 10;

        distance = targetPos.magnitude;

        originalDistance = distance;

        
        Debug.Log(originalDistance);
    }

    void SetUpRay()
    {
        mask = LayerMask.GetMask("House");
        targetDir = (targetPos - startingPos).normalized;

        hit = Physics2D.Raycast(transform.position, targetDir, 1000f, mask);

        Debug.DrawRay(transform.position, targetDir, Color.red, 1000f);
    }

    void CheckDistance()
    {
        distanceVector = currentPos - startingPos;
        distanceThisFrame = distanceVector.magnitude;
        distance -= distanceThisFrame;
        startingPos = currentPos;

        Debug.Log(distance);
    }

    void SetScale()
    {
        if(distance > (originalDistance / 3))
        {
            sr.size = new Vector2(newWidth, newHeight);
            newWidth += widthMultiplier;
            newHeight += heightMultiplier;

            Debug.Log("Set Scale increase");
        }
        else if(distance < (originalDistance / 3))
        {
            if (newWidth < originalWidth / 2 || newHeight < originalHeight / 2)
            {
                newWidth = originalWidth / 2;
                newHeight = originalHeight / 2;
            }
            sr.size = new Vector2(newWidth, newHeight);
            newWidth -= widthMultiplier;
            newHeight -= heightMultiplier;
            Debug.Log("Set Scale decrease");
        }     
    }

}
