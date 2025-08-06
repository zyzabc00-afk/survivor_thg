public interface ICSVParsable
{
    string GetID();
    void ParseCSVRow(string[] row);
}