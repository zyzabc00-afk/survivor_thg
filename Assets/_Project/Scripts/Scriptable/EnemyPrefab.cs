using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPrefabEntry
{
    public string name;
    public GameObject prefab;
}

[CreateAssetMenu(menuName = "Config/EnemyPrefab")]
public class EnemyPrefab : ScriptableObject
{
    public List<EnemyPrefabEntry> entries;

    private Dictionary<string, GameObject> _lookup;

    public GameObject GetPrefab(string name)
    {
        if (_lookup == null)
        {
            _lookup = new();
            foreach (var entry in entries)
            {
                _lookup[entry.name] = entry.prefab;
            }
        }

        return _lookup.TryGetValue(name, out var prefab) ? prefab : null;
    }
}