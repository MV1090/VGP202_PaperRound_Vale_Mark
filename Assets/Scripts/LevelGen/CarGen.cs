using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGen : ObjectGen
{
    [SerializeField] Transform[] spawnPoint;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        int randSpawn = Random.Range(0, spawnPoint.Length);

        
        GameObject firstPiecePivot = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab, spawnPoint[randSpawn].position, spawnPoint[randSpawn].rotation);
        currentLevelPieces.Add(firstPiecePivot);

        for (int i = 0; i <= startingPieces; i++)
        {
            SpawnLevelPiece();
        }
    }

    public override void SpawnLevelPiece()
    {
        base.SpawnLevelPiece();

        int randSpawn = Random.Range(0, spawnPoint.Length);

        Vector2 spawnPosition = new Vector2(spawnPoint[randSpawn].position.x, currentLevelPieces[currentLevelPieces.Count - 1].GetComponent<ObjectScroll>().GetNextSpawnPoint(GetExtents(newPiece.tag)));
        newPiece.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        newPiece.SetActive(true);

        currentLevelPieces.Add(newPiece);
    }

    
}
