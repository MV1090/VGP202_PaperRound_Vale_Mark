using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(ObjectPooler))]
public class LevelGen : Singleton<LevelGen>
{
    public Transform rightLane;
    public int startingPieces;

    public List<ObjectPooler.PoolType> pieces;
    List<GameObject> currentLevelPieces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (ObjectPooler.PoolType curPieces in pieces)
        {
            ObjectPooler.Instance.AddToPool(curPieces);
        }

        int randNum = Random.Range(0, pieces.Count);

        GameObject firstPiecePivot = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab, rightLane.position, rightLane.rotation);   
        currentLevelPieces.Add(firstPiecePivot);

        for(int i = 0; i <= startingPieces; i++)
        {
            SpawnLevelPiece();
        }
    }

    void SpawnLevelPiece()
    {
        int randNum = Random.Range(0, pieces.Count);

        GameObject newPiece = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab);

        float newExtents = 1;

        if (newPiece.tag.Contains("One"))
            newExtents = 0.5f;
        else if (newPiece.tag.Contains("Two"))
            newExtents = 1f;
        else if (newPiece.tag.Contains("Three"))
            newExtents = 1.5f;

        Vector2 spawnPosition = currentLevelPieces[currentLevelPieces.Count - 1].GetComponent<ObjectScroll>().GetNextSpawnPoint(newExtents); 
        newPiece.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        newPiece.SetActive(true);

        currentLevelPieces.Add(newPiece);
    }

    public void RemoveActiveLevelPiece()
    {
        if(currentLevelPieces.Count <= 0)
            return;

        SpawnLevelPiece();
        currentLevelPieces.RemoveAt(0);
    }      
}
