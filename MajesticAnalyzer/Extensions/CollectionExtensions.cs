using System;
using System.Collections.Generic;
using System.Linq;

namespace MajesticAnalyzer.Extensions
{
    public static class CollectionExtensions
    {
        public static IList<T> Shuffle<T>(this IList<T> collection, int maxCount)
        {
            if(maxCount < 0) throw new ArgumentException("MaxCount of elements should not be negative", nameof(maxCount));
            
            if(maxCount == 0) return new List<T>();
            
            var r = new Random();

            for (var i = 0; i < collection.Count - 1; i++)
            {
                var randomIdx = r.Next(i, collection.Count);
                var tmp = collection[randomIdx];
                collection[randomIdx] = collection[i];
                collection[i] = tmp;
            }

            return collection.Take(maxCount).ToList();
        }
    }
}