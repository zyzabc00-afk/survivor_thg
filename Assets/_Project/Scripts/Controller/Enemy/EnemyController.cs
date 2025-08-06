using System;
using Microlight.MicroBar;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyData enemy;
    private EnemyService enemyService;
    [SerializeField] private MicroBar hpBar;
    public Action<EnemyController> OnEnemyDie;

    private void Start()
    {
        var enemyComponent = GetComponent<EnemyAttackController>();
        enemy = enemyComponent.enemy;

        enemy.Reset();
        enemyService = new EnemyService(enemy, this);

        float hpEnemy = enemy.maxHP;
        hpBar.Initialize(hpEnemy);
        hpBar.UpdateBar(hpEnemy);
    }

    public void ReceiveDamage(float damage)
    {
        enemyService.TakeDamage(damage);
        hpBar.UpdateBar(enemy.currentHP);
    }

    public void OnDie()
    {
        gameObject.SetActive(false);
        ScoreManager.Instance.AddScore(1);

        OnEnemyDie?.Invoke(this);
    }

    public bool IsDead => enemyService.IsDead;
}