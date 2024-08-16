using UnityEngine;

public class SpawnDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("CanSpawn");
    }
}
