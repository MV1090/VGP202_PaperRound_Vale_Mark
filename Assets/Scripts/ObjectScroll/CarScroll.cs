using UnityEngine;

public class CarScroll : ObjectScroll
{

    public override void Start()
    {        
        base.Start();
    }

    public override void Update()
    {        
        base.Update();
    }

    private void FixedUpdate()
    {
        speed = GameManager.Instance.carSpeed;
    }


    public override void OnEnable()
    {
        speed = GameManager.Instance.carSpeed;
        base.OnEnable();
    }

}
