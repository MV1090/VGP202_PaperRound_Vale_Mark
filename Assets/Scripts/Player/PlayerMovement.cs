using UnityEngine;


public class PlayerMovement : SwipeDetection
{
    [SerializeField] Transform[] nextMovePos;
    [SerializeField] GameState gameState;
    [SerializeField] CarGen CarGen;
    int positionIndex;
    public float speed;
    [Range(0f, 1f)]
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
    }

    void Update()
    {
        if(transform.position == nextMovePos[positionIndex].position)
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

            if (angle <= 1 && angle > 0 || angle >= -1 && angle < 0 )
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
                GameManager.Instance.score += 10;
                ObjectPooler.Instance.ReturnToPool(collision.gameObject);
                CarGen.RemoveActiveLevelPiece();
            }
            else
            {
                GameManager.Instance.activeBonus = GameManager.ActiveBonus.Normal;
                GameManager.Instance.gameOver = true;
                gameState.JumpToGameOver();
                gameObject.SetActive(false);

            }
           
        }
    }


}
