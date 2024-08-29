using UnityEngine;

public class CarScroll : ObjectScroll
{

    public override void Start()
    {        
        base.Start();
    }

    public override void Update()
    {
        speed = GameManager.Instance.carSpeed;
        base.Update();
    }

    public override void OnEnable()
    {
        speed = GameManager.Instance.carSpeed;
        base.OnEnable();
    }

}
