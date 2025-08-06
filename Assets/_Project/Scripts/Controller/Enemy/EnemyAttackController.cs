using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    public Transform target;
    public EnemyData enemy;
    public string Id = "3001";
    private List<Collider2D> colliders = new();
    private float timer = 0f;
    private FlipController flipController;

    void Awake()
    {
        Dictionary<string, EnemyData> enemyStats = CsvReader.LoadCSV<EnemyData>("Csv/enemy_stat_config");
        enemy = enemyStats[Id];

        flipController = GetComponent<FlipController>();
    }

    void Update()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        timer += Time.deltaTime;
        if(timer >= enemy.delayBeforeDealDamage) HandleDealDmg();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        colliders.Add(other);

        DealDamage(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other);
        if (flipController.enabled == false) flipController.enabled = true;
        if (enemy.isMove == false) enemy.isMove = true;
    }

    private void HandleDealDmg()
    {
        foreach (var collider in colliders)
        {
            DealDamage(collider);
        }
    }

    private void DealDamage(Collider2D other)
    {
        IDamageable iTarget = other.GetComponent<IDamageable>();
        if (iTarget == null) return;

        float dealDamage = enemy.damage;

        iTarget.TakeDamage(dealDamage);
        
        flipController.enabled = false;
        enemy.isMove = false;

        // if (iTarget.IsDie()) Debug.Log("Die");

        timer = 0f;
    }
}