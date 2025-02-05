using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class PoolType
    {
        public GameObject prefab;
        public LevelPieces pieceType;
        public int size;
    }

    public enum LevelPieces
    {
        LeftHouse,
        RightHouse,
        OneHouseGapLeft,
        OneHouseGapRight,
        TwoHouseGapLeft,
        TwoHouseGapRight,        
        Car,
        OneCarGap,
        TwoCarGap,       
        PickUpBlue,
        PickUpRed,
        PickUpGreen,
        NewsPaper,
        NotFound
    }

    public List<PoolType> pieces;
    public Dictionary<LevelPieces, Queue<GameObject>> poolDictionary;

    protected override void Awake()
    {
        base.Awake();   

        poolDictionary =  new Dictionary<LevelPieces, Queue<GameObject>>();

        foreach (PoolType curPiece in pieces)
        {
           AddToPool(curPiece);
        }
    }

    public void AddToPool(PoolType newPool)
    {
        Queue<GameObject> objectPool = new Queue<GameObject>();
               
        for (int i = 0; i < newPool.size; i++)
        {
            GameObject obj = Instantiate(newPool.prefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
        poolDictionary.Add(newPool.pieceType, objectPool);
    }   
       

    public GameObject SpawnFromPool(GameObject prefab, Vector3 pos, Quaternion rotation)
    {        
        LevelPieces curType = ReturnType(prefab.tag);        
        
        if(poolDictionary.ContainsKey(curType))
        {
            GameObject objToSpawn = poolDictionary[curType].Dequeue();
            objToSpawn.transform.SetPositionAndRotation(pos, rotation); 
            objToSpawn.SetActive(true);
            return objToSpawn;
        }

        Debug.LogWarning("Pool with key " + curType + " does not exist");
        return null;
    }

    public GameObject SpawnFromPool(GameObject prefab)
    {        
        LevelPieces curType = ReturnType(prefab.tag);        

        if (poolDictionary.ContainsKey(curType))
            {
            if (poolDictionary[curType].Count == 0)
             return null;
               
            GameObject objToSpawn = poolDictionary[curType].Dequeue();               
            return objToSpawn;
            }

        Debug.LogWarning("Pool with key " + curType + " does not exist");
        return null;
    }

    public LevelPieces ReturnType(string tag)
    {
        if (tag.Equals("OneLeftHouse")) return LevelPieces.LeftHouse;
        if (tag.Equals("OneRightHouse")) return LevelPieces.RightHouse;
        if (tag.Equals("OneHouseGapLeft")) return LevelPieces.OneHouseGapLeft;
        if (tag.Equals("TwoHouseGapLeft")) return LevelPieces.TwoHouseGapLeft;        
        if (tag.Equals("OneHouseGapRight")) return LevelPieces.OneHouseGapRight;
        if (tag.Equals("TwoHouseGapRight")) return LevelPieces.TwoHouseGapRight;       
        if (tag.Equals("OneCar")) return LevelPieces.Car;
        if (tag.Equals("OneCarGap")) return LevelPieces.OneCarGap;
        if (tag.Equals("TwoCarGap")) return LevelPieces.TwoCarGap;        
        if (tag.Equals("PickUpBlue")) return LevelPieces.PickUpBlue; 
        if (tag.Equals("PickUpRed")) return LevelPieces.PickUpRed; 
        if (tag.Equals("PickUpGreen")) return LevelPieces.PickUpGreen;
        if (tag.Equals("NewsPaper")) return LevelPieces.NewsPaper;

        return LevelPieces.NotFound;
    }

    public void ReturnToPool(GameObject objToReturn)
    {
        LevelPieces curType = ReturnType(objToReturn.tag);
        if (poolDictionary.ContainsKey(curType))
        {
            poolDictionary[curType].Enqueue(objToReturn);
            objToReturn.SetActive(false);
        }
    }
}
