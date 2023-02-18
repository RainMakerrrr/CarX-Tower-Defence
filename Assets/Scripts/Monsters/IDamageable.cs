namespace Monsters
{
    public interface IDamageable
    {
        bool IsDead { get; }
        void TakeDamage(int damage);
    }
}