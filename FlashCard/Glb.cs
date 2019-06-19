using System;

namespace FlashCard {
    public class Glb {
        public static int IntRange(int val, int min, int max) {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }
    }
}