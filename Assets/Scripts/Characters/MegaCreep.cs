public class MegaCreep : BaseEnemy
{
    protected override void Death()
    {
        ReturnToPool();
    }
}
