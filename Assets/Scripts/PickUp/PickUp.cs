using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject ObjectGen;
    private SpriteRenderer sr;

    [SerializeField] int bonusLength;
    [SerializeField] AudioClip pickUpSound;
    enum PickUpType
    {
        DoubleScore, CowCatcher
    }

    [SerializeField] PickUpType currentPickUp;
    [SerializeField] Color color;

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
                    CoroutineManager.Instance.StartCoroutine(setBonus(GameManager.ActiveBonus.DoubleScore, bonusLength, color));
                    break;
                case PickUpType.CowCatcher:
                    CoroutineManager.Instance.StartCoroutine(setBonus(GameManager.ActiveBonus.CowCatcher, bonusLength, color));
                    break;
            }
            AudioClipManager.Instance.audioSource.PlayOneShot(pickUpSound);
        }     
    }

    public GameObject FindTag(GameObject obj)
    {
          if (obj.tag.Contains("PickUp"))
            obj = GameObject.Find("PickUpGen");

        return obj;
    }

    IEnumerator setBonus(GameManager.ActiveBonus bonus, float secondsActive, Color color)
    {
        sr.enabled = false;
        GameManager.Instance.activeBonus = bonus;
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(secondsActive);
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.Instance.activeBonus = GameManager.ActiveBonus.Normal;
        sr.enabled = true;
        ObjectPooler.Instance.ReturnToPool(gameObject);
        ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();        
    }
}
