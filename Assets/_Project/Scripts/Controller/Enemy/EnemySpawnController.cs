using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public BoxCollider2D spawnBoundary;
    [SerializeField]
    private EnemyPrefab enemyPrefab;
    private SpawnEnemyService spawnService;
    private List<EnemyController> aliveEnemies = new();
    private int maxEnemy = 5;

    void Awake()
    {
        spawnService = new();
    }

    void Start()
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Rect rect = new Rect(
            spawnBoundary.bounds.min.x,
            spawnBoundary.bounds.min.y,
            spawnBoundary.bounds.size.x,
            spawnBoundary.bounds.size.y
        );

        List<string> prefabs = spawnService.GetPrefabs();

        if (prefabs.Count == 0) return;

        int randomIndex = Random.Range(0, prefabs.Count);

        Vector2 spawnPos = spawnService.GetPoisitionRandom(rect);

        var gameObject = enemyPrefab.GetPrefab(prefabs[randomIndex]);
        var enemyGO = Instantiate(gameObject, spawnPos, Quaternion.identity);

        EnemyController enemyCtrl = enemyGO.GetComponent<EnemyController>();
        if (enemyCtrl != null)
        {
            enemyCtrl.OnEnemyDie += OnEnemyDie;
        }
    }

    void OnEnemyDie(EnemyController enemy) {
        if (aliveEnemies.Contains(enemy))
        {
            aliveEnemies.Remove(enemy);
        }

        if (aliveEnemies.Count < maxEnemy)
        {
            SpawnEnemy();
        }
    }
}