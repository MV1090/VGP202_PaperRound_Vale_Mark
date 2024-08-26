using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaperGen : ObjectGen
{   
    public Transform spawnPoint;

    public override void Start()
    {
        base.Start();
    }

    public override void SpawnLevelPiece()
    {
        base.SpawnLevelPiece();        

        Vector2 spawnPosition = new Vector2(spawnPoint.position.x, spawnPoint.position.y);
        newPiece.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        newPiece.SetActive(true);

        currentLevelPieces.Add(newPiece);
    }

    public override void RemoveActiveLevelPiece()
    {
        if (currentLevelPieces.Count <= 0)
            return;
        currentLevelPieces.RemoveAt(0);
    }

}
