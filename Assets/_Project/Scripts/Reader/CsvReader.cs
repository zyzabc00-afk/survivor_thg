using System.Collections.Generic;
using UnityEngine;

public static class CsvReader
{
    public static Dictionary<string, T> LoadCSV<T>(string filePath) where T : ICSVParsable, new()
    {
        Dictionary<string, T> data = new();

        TextAsset csvData = Resources.Load<TextAsset>(filePath);
        if (csvData == null)
        {
            return null;
        }

        string[] lines = csvData.text.Split("\n");

        T item = default;

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] elements = lines[i].Split(",");

            if (!string.IsNullOrEmpty(elements[0]))
            {
                item = new T();
                item.ParseCSVRow(elements);
                data[item.GetID()] = item;
            }
            else
            {
                item?.ParseCSVRow(elements);
            }
        }

        return data;
    }

    public static List<T> LoadCSVFlexible<T>(string filePath)
    {
        List<T> data = new();

        TextAsset csvData = Resources.Load<TextAsset>(filePath);
        if (csvData == null)
        {
            return null;
        }

        string[] lines = csvData.text.Split("\n");

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line)) continue;

            try
            {
                T value = (T)System.Convert.ChangeType(line, typeof(T));
                data.Add(value);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error convert type: " + ex);
            }
        }

        return data;
    }
}
