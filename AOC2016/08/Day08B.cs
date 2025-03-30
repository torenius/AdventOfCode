using AOC.Common;

namespace AOC2016._08;

public class Day08B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var display = new Grid<char>(50, 6, '.');
        foreach (var row in input)
        {
            //display.Print();
            if (row.StartsWith("rect"))
            {
                var rect = row[4..].Split("x").ToIntArray();
                for (var y = 0; y < rect[1]; y++)
                {
                    for (var x = 0; x < rect[0]; x++)
                    {
                        display[y, x] = '#';
                    }
                }
            }
            else if (row.StartsWith("rotate row"))
            {
                var rotate = row[13..].Split(" by ").ToIntArray();
                var currentRow = display.GetRow(rotate[0]).ToList();
                var newValues = new char[currentRow.Count];
                for (var i = 0; i < currentRow.Count; i++)
                {
                    var newIndex = i + rotate[1];
                    if (newIndex >= currentRow.Count)
                    {
                        newIndex -= currentRow.Count;
                    }
                    
                    newValues[newIndex] = currentRow[i].Value;
                }

                for (var i = 0; i < newValues.Length; i++)
                {
                    currentRow[i].Value = newValues[i];
                }
            }
            else
            {
                var rotate = row[16..].Split(" by ").ToIntArray();
                var currentColumn = display.GetColumn(rotate[0]).ToList();
                var newValues = new char[currentColumn.Count];
                for (var i = 0; i < currentColumn.Count; i++)
                {
                    var newIndex = i + rotate[1];
                    if (newIndex >= currentColumn.Count)
                    {
                        newIndex -= currentColumn.Count;
                    }
                    
                    newValues[newIndex] = currentColumn[i].Value;
                }

                for (var i = 0; i < newValues.Length; i++)
                {
                    currentColumn[i].Value = newValues[i];
                }
            }
        }
        
        return display.Print();
    }
}