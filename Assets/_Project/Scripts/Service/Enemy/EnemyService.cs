public class EnemyService : IEnemy
{
    private EnemyData enemy;
    private EnemyController enemyController;
    public bool IsDead => enemy.IsDead;

    public EnemyService(EnemyData enemy, EnemyController enemyController)
    {
        this.enemy = enemy;
        this.enemyController = enemyController;
    }

    public void Die()
    {
        if (enemy.IsDead) return;

        enemy.IsDead = true;
        enemyController.OnDie();
    }

    public void TakeDamage(float damage)
    {
        if (enemy.IsDead) return;

        enemy.currentHP -= damage;

        if (enemy.currentHP <= 0)
        {
            Die();
        }
    }
}