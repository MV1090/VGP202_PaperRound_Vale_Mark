using UnityEngine;

public class CarScroll : ObjectScroll
{
    //[SerializeField] float minSpeed = 2.0f;
    //[SerializeField] float maxSpeed = 7.0f;

    //[SerializeField] float duration = 30.0f;
    //float startTime;

    public override void Start()
    {
        speed = 4.0f; 
        //startTime = Time.time;

        base.Start();
    }

    public override void Update()
    {
    //    float t = (Time.time - startTime) / duration;
    //    speed = Mathf.SmoothStep(minSpeed, maxSpeed t);
    //    rb.velocity = Vector2.down * speed;// * Time.deltaTime;

        base.Update();
    }
       
}
