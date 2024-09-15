using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Newspaper : Projectile
{
    Vector2 targetPos;
    Vector2 targetDir;
    Vector2 throwVector = Vector2.zero;
    Vector2 startingPos = Vector2.zero;
    Vector2 distanceVector;
    Vector2 currentPos;

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationAmount;    
    [SerializeField] AudioClip hitSound;
           
    RaycastHit2D hit;
    LayerMask mask;


    float distance;
    float originalDistance;
    float distanceThisFrame;

    public override void Start()
    {
        base.Start();               

        startingPos = new Vector2(transform.position.x, transform.position.y);

        targetPos = PlayerThrow.Instance.touchPos;
        throwVector = (targetPos - startingPos).normalized * speed;

        SetUpRay();
        CheckForCollision();        

        Destroy(gameObject, 1);
               
    }  

    public override void Update()
    {
        currentPos = new Vector2(transform.position.x, transform.position.y);
        CheckDistance();        
        setProjectilePos();
        setProjectileRotation();
        SetScale();

        if (GameManager.Instance.gameOver == true)
        {
            Destroy(gameObject);
        }
    }    

    public override void setProjectilePos()
    {
       transform.position += new Vector3(throwVector.x, throwVector.y, 0) * Time.deltaTime;
    }

    public override void setProjectileRotation()
    {
        if (startingPos.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationSpeed -= rotationAmount);
        }

        if (startingPos.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationSpeed += rotationAmount);
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "OneLeftHouse" || collision.gameObject.tag == "OneRightHouse")
        {
            if (collision.gameObject.GetComponent<HouseScroll>().hasBeenHit == true) 
            {            
                Destroy(gameObject);                
                return;
            }           

            collision.gameObject.GetComponent<HouseScroll>().hasBeenHit = true;
            collision.gameObject.GetComponent<HouseScroll>().SetColor();

            GameModeManager.Instance.GameModeScoreSet();
            AudioClipManager.Instance.audioSource.PlayOneShot(hitSound);            
            
            Destroy(gameObject);        
            
        }
    }

    void CheckForCollision()
    {
        if (hit)
        {
            distance = hit.distance;
        }
        else
            distance = 7;

        originalDistance = distance;
    }

    void SetUpRay()
    {
        mask = LayerMask.GetMask("House");
        targetDir = (targetPos - startingPos).normalized;

        hit = Physics2D.Raycast(transform.position, targetDir, 10000f, mask);

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
        if(distance > originalDistance/2)
        {
            gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.0f);
        }
        else if (distance < originalDistance/2)
        {
            gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.0f);

        }
    }

}
