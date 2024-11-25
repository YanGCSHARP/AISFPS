using IronXL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TechEase.Models;

public class Livenstein
{
    public WorkSheet _sheet { get; set; }
    public int _row { get; set; }
    
    public string _name { get; set; }
    
    public Livenstein(WorkSheet sheet, string _name)
    {
        _sheet = sheet;
        _name = _name;
        _row = GetRow(sheet, _name);
    }
    public int LevenshteinDistance(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            if (string.IsNullOrEmpty(t))
            {
                return 0;
            }
            return t.Length;
        }

        if (string.IsNullOrEmpty(t))
        {
            return s.Length;
        }

        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        for (int i = 0; i <= n; d[i, 0] = i++)
        {
        }

        for (int j = 0; j <= m; d[0, j] = j++)
        {
        }

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                int min1 = d[i - 1, j] + 1;
                int min2 = d[i, j - 1] + 1;
                int min3 = d[i - 1, j - 1] + cost;
                d[i, j] = Math.Min(Math.Min(min1, min2), min3);
            }
        }

        return d[n, m];
    }

    public int GetRow(WorkSheet sheet, string name)
    {
        int row = 1;
        int min = 1000;
        var rowmin = 1;
        while (sheet[$"C{row}"].First().ToString() != "")
        {
            var str = sheet[$"C{row}"].First().ToString();
            str = str.Substring(0, 25);
            int dist = LevenshteinDistance(str, name);
            if (dist < min)
            {
                min = dist;
                rowmin = row;
                if (min == 0)
                {
                    return rowmin;
                }
                
            }
            row++;
        }
        
        return rowmin;
    }

    
}