using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponService weaponService;
    public GameObject weaponPrefab;
    public Transform parentObject;
    private Dictionary<string, Weapon> weaponData;

    void Start()
    {
        weaponData = weaponService.GetStat();
        SpawnWeapon("2001");
    }

    private void SpawnWeapon(string id, int count = 0)
    {
        if (!weaponData.ContainsKey(id))
        {
            return;
        }

        Weapon weapon = weaponData[id];
        if (count == 0) count = weapon.countBase;

        for (int i = 0; i < count; i++)
        {
            float angle = i * 360f / count;

            GameObject obj = Instantiate(weaponPrefab, parentObject);
            WeaponOrbit weaponOrbit = obj.GetComponent<WeaponOrbit>();
            weaponOrbit.Setup(weapon, angle);

            weaponService.RegisterWeapon(weaponOrbit);
        }
    }
}
