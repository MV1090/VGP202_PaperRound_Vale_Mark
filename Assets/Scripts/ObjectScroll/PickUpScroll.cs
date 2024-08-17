using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScroll :ObjectScroll
{
    public override void Start()
    {
        speed = 2.0f;
        base.Start();
    }

    public override void Update()
    {
        if (transform.position.y < minYPos)
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
            ObjectGen.GetComponent<ObjectGen>().RemoveActiveLevelPiece();
        }

        if (GameManager.Instance.gameOver == true)
        {
            ObjectPooler.Instance.ReturnToPool(gameObject);
            gameObject.SetActive(false);
        }
    }
}
