public interface IEnemy
{
    void TakeDamage(float damage);
    void Die();
    bool IsDead{ get; }
}