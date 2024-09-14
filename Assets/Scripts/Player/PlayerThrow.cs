using UnityEngine;

public class PlayerThrow : Singleton<PlayerThrow>
{
    public float minDist = 0.3f;
    public float maxTime = 0.8f;

    Vector2 startPos;
    Vector2 endPos;
    float startTime;
    float endTime;
       
    [SerializeField] Transform spawnLeft;
    [SerializeField] Transform spawnRight;  
    [SerializeField] GameObject newspaperPrefab;
    [SerializeField] GameObject BonusNewspaperPrefab;
    //[SerializeField] NewsPaperGen newsPaper;
    [SerializeField] AudioClip throwSound;

    public Vector2 touchPos;
      

    private void Start()
    {
       
    }


    private void OnEnable()
    {
        InputManager.OnStartTouch += TapStart;
        InputManager.OnEndTouch += TapEnd;                
    }

    private void OnDisable()
    {
        InputManager.OnStartTouch -= TapStart;
        InputManager.OnEndTouch -= TapEnd;
    }

    private void TapStart(Vector2 pos, float time)
    {
        startPos = pos;
        startTime = time;
    }
    private void TapEnd(Vector2 pos, float time)
    {
        endPos = pos;
        endTime = time;
        Throw();
        touchPos = startPos;
    }

    void Throw()
    {
        if (Time.timeScale == 0)
            return;
              
        if (Vector2.Distance(startPos, endPos) <= minDist)
        {
            if (startPos.x > transform.position.x)
            {                
                //newsPaper.spawnPoint = spawnRight;
                //newsPaper.SpawnLevelPiece();
                if(GameManager.Instance.activeBonus == GameManager.ActiveBonus.DoubleScore)
                    Instantiate(BonusNewspaperPrefab, spawnRight.position, Quaternion.identity);

                else
                    Instantiate(newspaperPrefab, spawnRight.position, Quaternion.identity);                
            }                

            if (startPos.x < transform.position.x)
            {
                //newsPaper.spawnPoint = spawnLeft;
                //newsPaper.SpawnLevelPiece();
                if (GameManager.Instance.activeBonus == GameManager.ActiveBonus.DoubleScore)
                    Instantiate(BonusNewspaperPrefab, spawnLeft.position, Quaternion.identity);

                else
                    Instantiate(newspaperPrefab, spawnLeft.position, Quaternion.identity);                
            }
            AudioClipManager.Instance.audioSource.PlayOneShot(throwSound);
        }
    }   

    // Update is called once per frame
    void Update()
    {
  
    }
}
