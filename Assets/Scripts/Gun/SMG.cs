public class SMG : Gun
{
    // Update is called once per frame
    public override void Update()
    {
        if (fire)
        {
            FireOneShot();
        }
        base.Update();
    }
}
