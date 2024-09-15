using UnityEngine;

public class Newspaper : Projectile
{
    Vector2 targetPos;
   
    Vector2 throwVector = Vector2.zero;
    Vector2 startingPos = Vector2.zero;
    
    bool hasLanded;
    

    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationAmount;    
    [SerializeField] AudioClip hitSound;
           
    

    public override void Start()
    {
        base.Start();               

        startingPos = new Vector2(transform.position.x, transform.position.y);

        targetPos = PlayerThrow.Instance.touchPos;
        throwVector = (targetPos - startingPos).normalized * speed;
              
        Destroy(gameObject, 1);
                
    }  

    public override void Update()
    {      
        if(hasLanded)
        {
            ScrollDownScreen();
            if (GameManager.Instance.gameOver == true)
            {
                Destroy(gameObject);
            }
            return;
        }

        setProjectilePos();
        setProjectileRotation();        

        if (GameManager.Instance.gameOver == true)
        {
            Destroy(gameObject);
        }
    }    

    public override void setProjectilePos()
    {
        if (transform.position.y >= targetPos.y)
            hasLanded = true;

        transform.position += new Vector3(throwVector.x, throwVector.y, 0) * Time.deltaTime;
    }

    void ScrollDownScreen()
    {
        transform.position -= new Vector3(0, 1.9f, 0) * Time.deltaTime;
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
       

}
