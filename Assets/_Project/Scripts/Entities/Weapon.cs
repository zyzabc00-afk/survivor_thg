[System.Serializable]
public class Weapon : ICSVParsable
{
    public string Id;
    public float damage;
    public float speed;
    public float radius;
    public int countBase;

    public string GetID() { return Id; }

    public void ParseCSVRow(string[] elements)
    {
        Id = elements[0];
        damage = float.Parse(elements[1]);
        speed = float.Parse(elements[2]);
        radius = float.Parse(elements[3]);
        countBase = int.Parse(elements[4]);
    }
}