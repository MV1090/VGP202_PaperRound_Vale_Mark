using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{

    public float speed;
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mr.material.mainTextureOffset = new Vector2(Time.time * speed, mr.material.mainTextureOffset.y);
    }


}
