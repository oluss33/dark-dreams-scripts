namespace Combat.Boss
{
    public interface IBoss
    {
        public abstract void PhaseSound();
        public abstract void StunSound();
        public abstract void ResetBoss();
    }
}