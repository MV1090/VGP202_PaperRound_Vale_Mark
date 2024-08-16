using UnityEngine;

public class HouseGen : ObjectGen
{
    [SerializeField] Transform spawnPoint;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();                

        GameObject firstPiecePivot = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab, spawnPoint.position, spawnPoint.rotation);
        currentLevelPieces.Add(firstPiecePivot);

        for (int i = 0; i <= startingPieces; i++)
        {
            SpawnLevelPiece();
        }
    }

    public override void SpawnLevelPiece()
    {
        base.SpawnLevelPiece();
        
        Vector2 spawnPosition = new Vector2(spawnPoint.position.x, currentLevelPieces[currentLevelPieces.Count - 1].GetComponent<ObjectScroll>().GetNextSpawnPoint(GetExtents(newPiece.tag)).y);
       
        newPiece.transform.SetPositionAndRotation(spawnPosition, Quaternion.identity);
        newPiece.SetActive(true);

        currentLevelPieces.Add(newPiece);
    }

    public override void ResetAllPieces()
    {
        if (currentLevelPieces.Count > 0)
            return;

        base.ResetAllPieces();

        GameObject firstPiecePivot = ObjectPooler.Instance.SpawnFromPool(pieces[randNum].prefab, spawnPoint.position, spawnPoint.rotation);
        currentLevelPieces.Add(firstPiecePivot);

        for (int i = 0; i <= startingPieces; i++)
        {
            SpawnLevelPiece();
        }
    }

  

}
