﻿using AdventOfCodeLibrary.Attributes;
using AdventOfCodeLibrary.Models;
using AdventOfCodeLibrary.Parsers;
using AdventOfCodeLibrary.Solvers;
using AdventOfCodeLibrary.Util;
using System.Linq;

namespace AdventOfCode.Year2024.Day06.Solvers;

[Solver]
public class Solver : BaseSolver<Grid<char>>
{
    public override int Year => 2024;

    public override int Day => 6;

    public override object? AnswerPartOne => 4789;

    //public override object? AnswerPartTwo => ;

    protected override IInputParser<Grid<char>> InputParser => new GridParser();

    private HashSet<Point2D> _passedPositions = new HashSet<Point2D>();
    private HashSet<(Point2D, Direction)> _passedPositionsWithDirection = new HashSet<(Point2D, Direction)>();
    private HashSet<Point2D> _obstructions = new HashSet<Point2D>();

    public override object SolvePartOne()
    {
        if (_passedPositions.Count == 0)
        {
            ProcessGrid();
        }
        return _passedPositions.Count;
    }

    public override object SolvePartTwo()
    {
        //659 to low.
        if (_obstructions.Count == 0)
        {
            ProcessGrid();
        }
        return _obstructions.Count;
    }

    private void ProcessGrid()
    {
        var startPoint = ParsedInput.First(x => x.Value.Equals('^')).Key;
        var direction = Direction.North;
        var currentPosition = new KeyValuePair<Point2D, char>(startPoint, '.');
        while (true)
        {
            var nextPosition = ParsedInput.GetAdjacentOrDefault(currentPosition.Key, direction);
            if (nextPosition.Value == '#')
            {
                direction = GetNextDirection(direction);
                continue;
            }
            _passedPositions.Add(currentPosition.Key);
            _passedPositionsWithDirection.Add((currentPosition.Key, direction));
            ProcessPartTwo(direction, currentPosition.Key);
            if (nextPosition.Value == default)
            {
                break;
            }
            currentPosition = nextPosition;
        }
    }

    private Direction GetNextDirection(Direction direction)
    {
        return 
            DirectionUtil.GetBasicDirectionsClockWise()
            .GetNextOrFirst(direction);
    }

    private int x = 0;

    private void ProcessPartTwo(Direction direction, Point2D currentPoint)
    {
        var nextDirection = GetNextDirection(direction);
        var currentPosition = new KeyValuePair<Point2D, char>(currentPoint, ParsedInput[currentPoint]);
        while (true)
        {
            if (_passedPositionsWithDirection.Contains((currentPosition.Key, nextDirection)))
            {
                _obstructions.Add(currentPoint.GetAdjacentPoint(direction));
            }
            var nextPosition = ParsedInput.GetAdjacentOrDefault(currentPosition.Key, nextDirection);
            if (nextPosition.Value == '#' && _passedPositionsWithDirection.Contains((currentPosition.Key, GetNextDirection(nextDirection))))
            {
                _obstructions.Add(currentPoint.GetAdjacentPoint(direction));
            }
            else if (nextPosition.Value == '#')
            {
                if (IsLoop(GetNextDirection(nextDirection), currentPosition.Key))
                {
                    _obstructions.Add(currentPoint.GetAdjacentPoint(direction));
                }
                x++;
                //ProcessPartTwo(GetNextDirection(nextDirection), currentPosition.Key);
                break;
            }
            if (nextPosition.Value == default)
            {
                break;
            }
            currentPosition = nextPosition;
        }
    }

    private bool IsLoop(Direction direction, Point2D currentPoint)
    {
        var passedPositions = new HashSet<(Point2D, Direction)>();
        var currentPosition = new KeyValuePair<Point2D, char>(currentPoint, '.');
        while (true)
        {
            if (passedPositions.Contains((currentPosition.Key, direction)))
            {
                return true;
            }
            var nextPosition = ParsedInput.GetAdjacentOrDefault(currentPosition.Key, direction);
            if (nextPosition.Value == '#')
            {
                direction = GetNextDirection(direction);
                continue;
            }
            passedPositions.Add((currentPosition.Key, direction));
            if (nextPosition.Value == default)
            {
                break;
            }
            currentPosition = nextPosition;
        }
        return false;
    }
}
