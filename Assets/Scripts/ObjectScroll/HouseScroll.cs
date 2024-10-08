using UnityEngine;

public class HouseScroll : ObjectScroll
{
    public bool hasBeenHit;
    
    int colorIndex;
    int randSprite;
    [SerializeField] Sprite[] objectSprite;
    [SerializeField] Sprite[] colorSprite;
    public override void Start()
    {
        speed = 2.0f;
        base.Start();
        hasBeenHit = false;
        if (sr)
        {
            randSprite = Random.Range(0, objectSprite.Length);
            colorIndex = randSprite;
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
            colorIndex = randSprite;
            sr.sprite = objectSprite[randSprite];
        }
    }

    public void SetColor()
    {
        sr.sprite = colorSprite[colorIndex];
        ParticleManager.Instance.PlayHitParticle(transform);

        if(GameManager.Instance.activeBonus == GameManager.ActiveBonus.DoubleScore)
            ParticleManager.Instance.PlayBonusHitParticle(transform);
    }
    
}
