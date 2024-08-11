using UnityEngine;


public class PlayerSwipe : SwipeDetection
{
    [SerializeField] Transform[] nextMovePos;
    int positionIndex;
    public float speed;
    public float angle;

    private enum PlayerDirection
    {
        MovingLeft,
        MovingRight,
        Stationary   
    }

    [SerializeField] private PlayerDirection playerMove;

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
        }
           
        if (Vector2.Dot(Vector2.right, dir) >= distThreshold)
        {
            if (positionIndex == nextMovePos.Length -1)
                return;
            playerMove = PlayerDirection.MovingRight;
            positionIndex++;           
        }                
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == nextMovePos[positionIndex].position)
            playerMove = PlayerDirection.Stationary;

        transform.position = Vector3.MoveTowards(transform.position, nextMovePos[positionIndex].position, speed * Time.deltaTime);
    }
}
