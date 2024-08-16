using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGen : ObjectGen
{
    [SerializeField] Transform[] spawnPoint;

    public List<int> pieceTracker = new List<int>();

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();        

       // InitialLevelPieceSpawn();
    }

    void InitialLevelPieceSpawn()
    {
        randNum = Random.Range(0, 3);

        int randSpawn = Random.Range(0, spawnPoint.Length -1);


        GameObject firstPiecePivot = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab, spawnPoint[randSpawn].position, spawnPoint[randSpawn].rotation);
        currentLevelPieces.Add(firstPiecePivot);

        for (int i = 0; i <= startingPieces; i++)
        {
            randNum = Random.Range(0, 3);
            newPiece = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab);

            randSpawn = Random.Range(0, spawnPoint.Length);

            Vector2 spawnPosition = new Vector2(spawnPoint[randSpawn].position.x, currentLevelPieces[currentLevelPieces.Count - 1].GetComponent<ObjectScroll>().GetNextSpawnPoint(GetExtents(newPiece.tag)).y);
            newPiece.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
            newPiece.SetActive(true);

            currentLevelPieces.Add(newPiece);
        }
    }

    public override void SpawnLevelPiece()
    {
        base.SpawnLevelPiece();

        randNum = Random.Range(0, pieces.Count);        

        if(pieces[randNum].prefab.tag.Contains("PickUp"))
        {
            if (pieceTracker.Contains(randNum))
            {
                randNum = Random.Range(0, 2);                
            }
            else
              pieceTracker.Add(randNum);
        }
        
        newPiece = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab);

        int randSpawn = Random.Range(0, spawnPoint.Length);

        Vector2 spawnPosition = new Vector2(spawnPoint[randSpawn].position.x, currentLevelPieces[currentLevelPieces.Count - 1].GetComponent<ObjectScroll>().GetNextSpawnPoint(GetExtents(newPiece.tag)).y);
        newPiece.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        newPiece.SetActive(true);

        currentLevelPieces.Add(newPiece);
    }

    public override void Update()
    {
        base.Update();
        if (GameManager.Instance.gameOver == true)
        {
            RemoveAllLevelPieces();
        }
    }

    public override void ResetAllPieces()
    {        
        InitialLevelPieceSpawn();
    }

    public override void RemoveAllLevelPieces()
    {
        base.RemoveAllLevelPieces();
        StopCoroutine(ResetPickUp());
        pieceTracker.Clear();
    }

    public override void RemoveActiveLevelPiece()
    {
        if (currentLevelPieces[0].gameObject.tag.Contains("PickUp"))
        {
            if (pieceTracker.Count <= 0)
                return;
            StartCoroutine(ResetPickUp());
        }           

        base.RemoveActiveLevelPiece();        
    }

    private IEnumerator ResetPickUp()
    {
        
        Debug.Log("coroutine Started");
        yield return new WaitForSeconds(30);
        if (pieceTracker.Count != 0)
        pieceTracker.RemoveAt(0);        
    }
    
}
