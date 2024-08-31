using UnityEngine;

public class CarScroll : ObjectScroll
{
    int randSprite;
    [SerializeField] Sprite[] objectSprite;

    public override void Start()
    {        
        base.Start();

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

    private void FixedUpdate()
    {
        speed = GameManager.Instance.carSpeed;
    }


    public override void OnEnable()
    {
        speed = GameManager.Instance.carSpeed;

        if(sr)
        {
            randSprite = Random.Range(0, objectSprite.Length);
            sr.sprite = objectSprite[randSprite];
        } 
        
        base.OnEnable();
    }

}
