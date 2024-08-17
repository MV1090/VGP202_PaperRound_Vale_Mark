using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGen : ObjectGen
{
    [SerializeField] Transform[] spawnPoint;        
    private float spawnTime;

    public override void Start()
    {
        base.Start();
    }
    public override void SpawnLevelPiece()
    {
        base.SpawnLevelPiece();               

        int randSpawn = Random.Range(0, spawnPoint.Length);

        Vector2 spawnPosition = new Vector2(spawnPoint[randSpawn].position.x, spawnPoint[randSpawn].position.y);
        newPiece.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        newPiece.SetActive(true);

        currentLevelPieces.Add(newPiece);
    }

    public override void ResetAllPieces()
    {
        if (currentLevelPieces.Count > 0)
            return;

        StartCoroutine(spawnTimer());
    }

    public override void Update()
    {
        if (GameManager.Instance.gameOver == true)
        {
            StopAllCoroutines();
            RemoveAllLevelPieces();
        }
    }

    public override void RemoveActiveLevelPiece()
    {
        if (currentLevelPieces.Count <= 0)
            return;
        currentLevelPieces.RemoveAt(0);               
    }

    IEnumerator spawnTimer()
    {
        while (true)
        {
            spawnTime = Random.Range(30, 45);

            yield return new WaitForSeconds(spawnTime);

            SpawnLevelPiece();
        }
    }
}
