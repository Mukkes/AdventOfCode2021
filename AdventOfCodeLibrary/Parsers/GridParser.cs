﻿using AdventOfCodeLibrary.Models;

namespace AdventOfCodeLibrary.Parsers;
public class GridParser : StringArrayParser, IInputParser<Grid<char>>
{
    public new Grid<char> Parse(string input)
    {
        var dictionary = new Grid<char>();
        var parsedInput = base.Parse(input);
        for (int y = 0; y < parsedInput.Length; y++)
        {
            for (int x = 0; x < parsedInput[y].Length; x++)
            {
                dictionary.Add(new Point2D(x, y), parsedInput[y][x]);
            }
        }
        return dictionary;
    }
}
