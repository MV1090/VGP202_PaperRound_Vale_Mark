using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class ObjectScroll : MonoBehaviour
{
    public float speed;
    public float minYPos = -5;

    public Rigidbody2D rb;
    public BoxCollider2D bc;
    public SpriteRenderer sr;

    public GameObject ObjectGen;

    // Start is called before the first frame update
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        
        if (ObjectGen == null)
            ObjectGen = FindTag(gameObject);

        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = Vector2.down * speed; 
    }

    public virtual void OnEnable()
    {
        if (rb)
        {
            rb.velocity = Vector2.down * speed;
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = Vector2.down * speed;
    }

    private Vector2 GetBotLeftCorner()
    {
        return new Vector2(bc.bounds.min.x, bc.bounds.min.y);
    }
    public Vector2 GetTopLeftCorner()
    {
        return new Vector2(bc.bounds.min.x, bc.bounds.max.y);
    }
    public Vector2 GetBotRightCorner()
    {
        return new Vector2(bc.bounds.max.x, bc.bounds.min.y);
    }
    public Vector2 GetTopRightCorner()
    {
        return new Vector2(bc.bounds.max.x, bc.bounds.max.y);
    }

    public virtual Vector2 GetNextSpawnPoint(float nextPieceExtent)
    {
        return new Vector2(transform.position.x , bc.bounds.max.y + nextPieceExtent);
    }
      
    public virtual void Update()
    {
        if (transform.position.y < minYPos)
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
            ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();
        }

        if(GameManager.Instance.gameOver == true)
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
            gameObject.SetActive(false);
        }
    }

    public GameObject FindTag(GameObject obj)
    {
        if (obj.tag.Contains("Car"))
            obj = GameObject.Find("CarGen");
        else if(obj.tag.Contains("Left"))
            obj = GameObject.Find("LeftHouseGen");
        else if (obj.tag.Contains("Right"))
            obj = GameObject.Find("RightHouseGen");
        else if (obj.tag.Contains("PickUp"))
            obj = GameObject.Find("PickUpGen");

        return obj;
    }
}
