using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

//[RequireComponent(typeof(ObjectPooler))]
public class ObjectGen : MonoBehaviour 
{
   // public Transform spawnPoint;
    public int startingPieces;

    public List<ObjectPooler.PoolType> pieces;
    public List<GameObject> currentLevelPieces = new List<GameObject>();

    public int randNum;
    public GameObject newPiece;
    // Start is called before the first frame update
    virtual public void Start()
    {
        foreach (ObjectPooler.PoolType curPieces in pieces)
        {
            ObjectPooler.Instance.AddToPool(curPieces);
        }

        randNum = Random.Range(0, pieces.Count);
       
    }

    virtual public void SpawnLevelPiece()
    {
       randNum = Random.Range(0, pieces.Count);
       newPiece = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab);
        
    }

    virtual public void RemoveActiveLevelPiece()
    {
        if(currentLevelPieces.Count <= 0)
            return;

        SpawnLevelPiece();
        currentLevelPieces.RemoveAt(0);
    }    
    
    public float GetExtents(string tag)
    {
        if (tag.Contains("One"))
            return 0.5f;
        else if (tag.Contains("Two"))
            return 1f;
        else if (tag.Contains("Three"))
            return 1.5f;
        else
            return 0;
    }
        
}