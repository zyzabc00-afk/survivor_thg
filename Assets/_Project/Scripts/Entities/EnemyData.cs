using System;

[Serializable]
public class EnemyData : ICSVParsable
{
    public string Id;
    public float damage;
    public float maxHP;
    public float currentHP;
    public bool IsDead;
    public bool isMove = true;
    public float delayBeforeDealDamage = 2f;

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP < 0) currentHP = 0;
    }

    public void ParseCSVRow(string[] row)
    {
        Id = row[0];
        maxHP = float.Parse(row[1]);
        currentHP = maxHP;
        damage = float.Parse(row[2]);
    }

    public string GetID()
    {
        return Id;
    }

    public void Reset()
    {
        currentHP = maxHP;
        IsDead = false;
    }
}