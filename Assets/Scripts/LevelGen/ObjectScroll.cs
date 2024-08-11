using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class ObjectScroll : MonoBehaviour
{
    public float speed;
    public float minYPos = -10;

    Rigidbody2D rb;
    BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        speed = 2;

        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.velocity = Vector2.down * speed;
    }

    private void OnEnable()
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

    public Vector2 GetNextSpawnPoint(float nextPieceExtent)
    {
        return new Vector2(transform.position.x, bc.bounds.max.y + nextPieceExtent);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minYPos)
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
            LevelGen.Instance.RemoveActiveLevelPiece();
        }
    }
}
