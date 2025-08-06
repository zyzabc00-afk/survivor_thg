using System.Collections.Generic;
using Microlight.MicroBar;
using UnityEngine;

public class HeroService : MonoBehaviour, IDamageable
{
    private Hero hero;
    public GameObject objHpBar;
    public string idHero = "1001";
    
    [SerializeField]
    private MicroBar hpBar;

    void Start()
    {
        objHpBar.SetActive(true);
    }

    void Awake()
    {
        Dictionary<string, Hero> heroDataDict = CsvReader.LoadCSV<Hero>("Csv/hero_stat_config");
        hero = heroDataDict[idHero];

        float HpHero = hero.hp;

        hpBar.Initialize(HpHero);
        hpBar.UpdateBar(HpHero);
    }

    public bool IsDie()
    {
        return hero.IsDie();
    }

    public void TakeDamage(float damage)
    {
        hero.TakeDamage(damage);

        hpBar.UpdateBar(hero.hp);
    }
}