public class Hero : ICSVParsable
{
    public string Id;
    public float hp;

    public Hero(){}

    public Hero(string id, float hp)
    {
        Id = id;
        this.hp = hp;
    }

    public bool IsDie()
    {
        return hp <= 0;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp < 0) hp = 0;
    }

    public string GetID()
    {
        return Id;
    }

    public void ParseCSVRow(string[] row)
    {
        Id = row[0];
        hp = float.Parse(row[2]);
    }
}