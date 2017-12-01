using System;
using System.Collections.Generic;

namespace Demo.Backend.Api
{
    internal static class Cart
    {
        private static readonly Dictionary<string, List<int>> Counts = new Dictionary<string, List<int>>();

        public static void Register(string cart, int product)
        {
            if (!Counts.TryGetValue(cart, out var c))
            {
                c = new List<int>();
                Counts[cart] = c;
            }
            c.Add(product);
        }

        public static void Remove(string cart, int product)
        {
            if (!Counts.TryGetValue(cart, out var c))
            {
                throw new InvalidOperationException("Cart not registered");
            }
            c.Remove(product);
        }
    }
}