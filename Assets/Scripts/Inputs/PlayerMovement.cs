using UnityEngine;


public class PlayerMovement : SwipeDetection
{
    [SerializeField] Transform[] nextMovePos;
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

    // Start is called before the first frame update
    void Start()
    {
        positionIndex = 1;
        transform.position = nextMovePos[positionIndex].position;
        playerMove = PlayerDirection.Stationary;
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

    // Update is called once per frame
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
}
