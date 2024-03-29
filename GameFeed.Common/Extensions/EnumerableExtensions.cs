﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFeed.Common.Extensions {

    public static class EnumerableExtensions {

        /// <summary>
        /// Returns a random object out of a list
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="enumerable">The list from which a random object will be picked</param>
        /// <returns>The randomly picked object</returns>
        public static T TakeRandom<T>(this IEnumerable<T> enumerable) {
            Random r = new Random();
            IList<T> list = enumerable as IList<T> ?? enumerable.ToList();
            return list.ElementAt(r.Next(0, list.Count));
        }

        public static string CommaSeparate<T>(this IEnumerable<T> enumerable) {
            string commaSeparated = enumerable.First().ToString();
            foreach (T element in enumerable.Skip(1)) {
                commaSeparated += $",{element.ToString()}";
            }

            return commaSeparated;
        }
    }
}
