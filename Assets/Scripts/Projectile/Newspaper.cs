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

    [SerializeField] AudioClip hitSound;

    //public GameObject ObjectGen;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //if (ObjectGen == null)
        //    ObjectGen = FindTag(gameObject);

        startingPos = new Vector2(transform.position.x, transform.position.y);

        targetPos = PlayerThrow.Instance.touchPos;
        throwVector = (targetPos - startingPos).normalized * speed;
        Destroy(gameObject, 1);
    }

    //public override void OnEnable()
    //{
    //    startingPos = Vector2.zero;
    //    startingPos = new Vector2(transform.position.x, transform.position.y);

    //    targetPos = PlayerThrow.Instance.touchPos;
    //    throwVector = (targetPos - startingPos).normalized * speed;
    //}
    // Update is called once per frame

    public override void Update()
    {
               
       setProjectilePos();
       setProjectileRotation();

        if (GameManager.Instance.gameOver == true)
        {
            //ObjectPooler.Instance.ReturnToPool(gameObject);
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        //if(OutOfRange() == true)
        //{
        //    ObjectPooler.Instance.ReturnToPool(gameObject);
        //    ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();
        //}
    }

    //public bool OutOfRange()
    //{
    //    if(transform.position.y > 5)
    //       return true;

    //    if(transform.position.y < -5)
    //        return true;

    //    if(transform.position.x > 5)
    //        return true;

    //    if(transform.position.x < -5) 
    //        return true;

    //    return false;
    //}

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
                Debug.Log("House has been hit");
                Destroy(gameObject);
                //ObjectPooler.Instance.ReturnToPool(gameObject);
                //ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();
                return;
            }               



            collision.gameObject.GetComponent<HouseScroll>().hasBeenHit = true;

            GameModeManager.Instance.GameModeScoreSet();
            AudioClipManager.Instance.audioSource.PlayOneShot(hitSound);
            //GameManager.Instance.score ++;
            Destroy(gameObject);
            //ObjectPooler.Instance.ReturnToPool(gameObject);
            //ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();
            Debug.Log("Hit House");
        }
    }
    //public GameObject FindTag(GameObject obj)
    //{
    //    if (obj.tag.Contains("News"))
    //        obj = GameObject.Find("NewsPaperGen");

    //    return obj;
    //}

    private void OnDestroy()
    {
        PlayerThrow.Instance.paperTracker --;
    }

}
