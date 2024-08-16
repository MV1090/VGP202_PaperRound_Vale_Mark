using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject ObjectGen;
    private SpriteRenderer sr;

    [SerializeField] int bonusLength;
    enum PickUpType
    {
        DoubleScore, CowCatcher
    }

    [SerializeField] PickUpType currentPickUp;

    // Start is called before the first frame update
    void Start()
    {
        if (ObjectGen == null)
            ObjectGen = FindTag(gameObject);

        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (currentPickUp)
            {
                case PickUpType.DoubleScore:
                    StartCoroutine(setBonus(GameManager.ActiveBonus.DoubleScore, bonusLength));
                    break;
                case PickUpType.CowCatcher:
                    StartCoroutine(setBonus(GameManager.ActiveBonus.CowCatcher, bonusLength));
                    break;
            }            
        }     
    }

    public GameObject FindTag(GameObject obj)
    {
          if (obj.tag.Contains("PickUp"))
            obj = GameObject.Find("PickUpGen");

        return obj;
    }

    IEnumerator setBonus(GameManager.ActiveBonus bonus, float secondsActive)
    {
        sr.enabled = false;
        GameManager.Instance.activeBonus = bonus;
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(secondsActive);
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.Instance.activeBonus = GameManager.ActiveBonus.Normal;
        sr.enabled = true;
        ObjectPooler.Instance.ReturnToPool(gameObject);
        ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();
        
    }
}