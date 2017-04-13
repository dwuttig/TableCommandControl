using System;

namespace TableCommandControl.Domain {
    public static class Trigonometry {
        public static double DegreeToRadian(double angle) {
            return Math.PI * angle / 180.0;
        }

        public static double RadianToDegree(double angle) {
            return angle * (180.0 / Math.PI);
        }
    }
}