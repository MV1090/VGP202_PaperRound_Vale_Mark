using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newspaper : Projectile
{
    Vector2 targetPos;
    private Vector2 throwVector = Vector2.zero;
    private Vector2 startingPos = Vector2.zero;

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationAmount;

   

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        startingPos = new Vector2(transform.position.x, transform.position.y);
        
        targetPos = PlayerThrow.Instance.touchPos;
        throwVector = (targetPos - startingPos).normalized * speed;
        Destroy(gameObject, 1);
                
    }
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
       setProjectilePos();
       setProjectileRotation();
    }

    public override void setProjectilePos()
    {
        base.setProjectilePos();
        transform.position += new Vector3(throwVector.x, throwVector.y, 0) * Time.deltaTime;
    }

    public override void setProjectileRotation()
    {
        base.setProjectileRotation();
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
                Debug.Log("House has been hit");
                Destroy(gameObject);
                return;
            }               

            collision.gameObject.GetComponent<HouseScroll>().hasBeenHit = true;
            if(GameManager.Instance.activeBonus == GameManager.ActiveBonus.DoubleScore)
            {
                GameManager.Instance.score ++;
            }
            GameManager.Instance.score ++;
            Destroy(gameObject);
            Debug.Log("Hit House");
        }
    }
    
    

}
