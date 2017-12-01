using System.Collections.Generic;

namespace Demo.Backend.Api
{
    internal static class Catalog
    {
        public static readonly IReadOnlyDictionary<int, decimal> Prices = new Dictionary<int, decimal>
        {
            {1, 25.00m},
            {2, 35.00m},
            {3, 45.50m},
            {4, 99.99m},
        };

        public static readonly IReadOnlyDictionary<int, string> Names = new Dictionary<int, string>
        {
            {1, "Broomstick"},
            {2, "Chessboard"},
            {3, "Sunglasses"},
            {4, "DVD player"},
        };

        public static readonly IReadOnlyDictionary<int, string> Images = new Dictionary<int, string>
        {
            {1, "https://www.iconfinder.com/data/icons/scarycons/140/broom-128.png"},
            {2, "https://www.iconfinder.com/data/icons/Futurosoft%20Icons%200.5.2/128x128/apps/kchess.png"},
            {3, "https://www.iconfinder.com/data/icons/bnw/128x128/apps/gwenview.png"},
            {4, "https://www.iconfinder.com/data/icons/Milkanodised-png/128/DVD-Player.png"},
        };
    }
}