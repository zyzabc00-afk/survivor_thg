using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyService
{
    public List<string> GetPrefabs()
    {
        List<string> prefabs = CsvReader.LoadCSVFlexible<string>("CSV/enemy_spawn_config");
        return prefabs;
    }

    public Vector2 GetPoisitionRandom(Rect rect)
    {
        int side = Random.Range(0, 4);
        float x, y;
        switch (side)
        {
            case 0:
                x = Random.Range(rect.xMin, rect.xMax);
                y = rect.yMax;
                break;
            case 1:
                x = rect.xMax;
                y = Random.Range(rect.yMin, rect.yMax);
                break;
            case 2:
                x = Random.Range(rect.xMin, rect.xMax);
                y = rect.yMin;
                break;
            case 3:
                x = rect.xMin;
                y = Random.Range(rect.yMin, rect.yMax);
                break;
            default:
                x = y = 0;
                break;
        }

        return new Vector2(x, y);
    }
}