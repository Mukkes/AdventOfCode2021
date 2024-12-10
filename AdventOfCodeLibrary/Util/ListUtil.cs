﻿namespace AdventOfCodeLibrary.Util;
public static class ListUtil
{
    [Obsolete("To ListExtension class.")]
    public static T GetNextOrFirst<T>(this List<T> list, T item)
    {
        var index = list.IndexOf(item);
        if (index + 1 < list.Count)
        {
            return list[index + 1];
        }
        return list.First();
    }
}