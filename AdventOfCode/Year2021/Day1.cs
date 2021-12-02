﻿namespace AdventOfCode.Year2021
{
    public class Day1 : PuzzleSolution
    {
        private int[] Input => GetInputAsIntArray(@"Year2021\Inputs\Day1.txt");

        public override int Day => 1;

        public override int ResultPartOne()
        {
            int largerCount = 0;
            for (int i = 1; i < Input.Length; i++)
            {
                if (Input[i] > Input[i - 1])
                {
                    largerCount++;
                }
            }
            return largerCount;
        }

        public override int ResultPartTwo()
        {
            int largerCount = 0;
            for (int i = 3; i < Input.Length; i++)
            {
                var previousDepth = Input[i - 3] + Input[i - 2] + Input[i - 1];
                var depth = Input[i - 2] + Input[i - 1] + Input[i];
                if (depth > previousDepth)
                {
                    largerCount++;
                }
            }
            return largerCount;
        }
    }
}
