using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject ObjectGen;

    // Start is called before the first frame update
    void Start()
    {
        if (ObjectGen == null)
            ObjectGen = FindTag(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
            ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();
        }
        
    }

    public GameObject FindTag(GameObject obj)
    {
          if (obj.tag.Contains("PickUp"))
            obj = GameObject.Find("CarGen");

        return obj;
    }
}
