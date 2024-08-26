using UnityEngine;

public class CarScroll : ObjectScroll
{

    public override void Start()
    {
        speed = GameManager.Instance.carSpeed;
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
