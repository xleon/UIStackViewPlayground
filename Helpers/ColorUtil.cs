using System;
using UIKit;

namespace UIStackViewPlayground.Helpers
{
    public static class ColorUtil
    {
        private static readonly Random Random = new Random(DateTime.Now.Millisecond);

        public static UIColor GetRandomColor(bool dark = false)
            => UIColor.FromHSB((dark ? Random.Next(180, 255) : Random.Next(255)) / 255.0f, 1.0f, 1.0f);
    }
}