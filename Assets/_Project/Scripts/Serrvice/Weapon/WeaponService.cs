using System.Collections.Generic;
using UnityEngine;

public class WeaponService : MonoBehaviour
{
    public Transform center;
    private List<WeaponOrbit> weapons = new();

    void Update()
    {
        foreach (var weapon in weapons)
        {
            weapon.UpdateOrbit(center);
        }
    }

    public Dictionary<string, Weapon> GetStat()
    {
        Dictionary<string, Weapon> weapons = CsvReader.LoadCSV<Weapon>("Csv/weapon_stat_config");

        return weapons;
    }

    public void RegisterWeapon(WeaponOrbit weapon)
    {
        if (!weapons.Contains(weapon))
        {
            weapons.Add(weapon);
        }
    }

    public void UnregisterWeapon(WeaponOrbit weapon)
    {
        if (weapons.Contains(weapon))
        {
            weapons.Remove(weapon);
        }
    }
}