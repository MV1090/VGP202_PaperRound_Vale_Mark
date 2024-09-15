using UnityEngine;


public class PlayerMovement : SwipeDetection
{
    AudioSource audioSource;

    [SerializeField] Transform[] nextMovePos;
    [SerializeField] GameState gameState;
    [SerializeField] CarGen CarGen;

    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip destroyCar;
    [SerializeField] AudioClip moveSound;

    int positionIndex;
    public float speed;
    [Range(0f, 10f)]
    public float angleSpeed;

    public float angle;


    private enum PlayerDirection
    {
        MovingLeft,
        MovingRight,
        Stationary   
    }

    [SerializeField] private PlayerDirection playerMove;
    private PlayerDirection lastPosition;

    void Start()
    {
        positionIndex = 1;
        transform.position = nextMovePos[positionIndex].position;
        playerMove = PlayerDirection.Stationary;
        audioSource = GetComponent<AudioSource>();
        angle = 0;
    }

    public void ResetPlayer()
    {
        positionIndex = 1;
        transform.position = nextMovePos[positionIndex].position;
        playerMove = PlayerDirection.Stationary;
        gameObject.SetActive(true);
    }

    public override void SwipeDirection(Vector2 dir)
    {
        if (Time.timeScale == 0)
            return;

        base.SwipeDirection(dir);        

        if (Vector2.Dot(Vector2.left, dir) >= distThreshold)
        {
            if(positionIndex == 0) 
                return;
            playerMove = PlayerDirection.MovingLeft;
            positionIndex--;
            lastPosition = PlayerDirection.MovingLeft;
        }
           
        if (Vector2.Dot(Vector2.right, dir) >= distThreshold)
        {
            if (positionIndex == nextMovePos.Length -1)
                return;
            playerMove = PlayerDirection.MovingRight;
            positionIndex++;
            lastPosition = PlayerDirection.MovingRight;
        }

        AudioClipManager.Instance.audioSource.PlayOneShot(moveSound);
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        if (transform.position == nextMovePos[positionIndex].position)
            playerMove = PlayerDirection.Stationary;

        if (playerMove == PlayerDirection.MovingLeft)
            angle += angleSpeed;

        if (playerMove == PlayerDirection.MovingRight)
            angle -= angleSpeed;

        if(playerMove == PlayerDirection.Stationary)
        {
                           
            if(lastPosition == PlayerDirection.MovingLeft)
              angle -= angleSpeed;
            
                
            if(lastPosition == PlayerDirection.MovingRight)
                angle += angleSpeed;

            if (angle <= 10 && angle > 0 || angle >= -10 && angle < 0 )
                angle = 0;
        }

        transform. rotation = Quaternion.Euler(0,0, angle);
        transform.position = Vector3.MoveTowards(transform.position, nextMovePos[positionIndex].position, speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneCar"))
        {
            if (GameManager.Instance.activeBonus == GameManager.ActiveBonus.CowCatcher)
            {
                //GameManager.Instance.score += 3;
                ParticleManager.Instance.PlaySmoke(collision.gameObject.transform);
                ObjectPooler.Instance.ReturnToPool(collision.gameObject);
                CarGen.RemoveActiveLevelPiece();
                AudioClipManager.Instance.audioSource.PlayOneShot(destroyCar);
            }
            else
            {
                AudioClipManager.Instance.audioSource.PlayOneShot(deathSound);
                GameManager.Instance.activeBonus = GameManager.ActiveBonus.Normal;
                GameManager.Instance.gameOver = true;
                gameState.JumpToGameOver();
                gameObject.SetActive(false);                
            }
           
        }
    }


}
