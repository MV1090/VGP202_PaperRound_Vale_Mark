using UnityEngine;

public class HouseScroll : ObjectScroll
{
    public bool hasBeenHit;

    int randSprite;
    [SerializeField] Sprite[] objectSprite;
    public override void Start()
    {
        speed = 2.0f;
        base.Start();
        hasBeenHit = false;
        if (sr)
        {
            randSprite = Random.Range(0, objectSprite.Length);
            sr.sprite = objectSprite[randSprite];
        }

    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        hasBeenHit = false;
        if (sr)
        {
            randSprite = Random.Range(0, objectSprite.Length);
            sr.sprite = objectSprite[randSprite];
        }

    }
    
}
