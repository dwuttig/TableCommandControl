using System;

namespace TableCommandControl.Domain {
    public class PolarCoordinate {
        /// <summary>Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.</summary>
        public PolarCoordinate(double angle, double radius) {
            if (radius < 0) {
                throw new ArgumentException("Der radius muss größer gleich 0 sein");
            }
            Angle = angle;
            Radius = radius;
        }

        /// <summary>
        ///     Liefert oder setzt den Winkel in Grad
        /// </summary>
        public double Angle { get; set; }

        /// <summary>
        ///     Liefert den Winkel in Radians
        /// </summary>
        public double AngleAsRadians {
            get { return Angle * Math.PI / 180; }
        }

        /// <summary>
        ///     Liefert den Angle normalisiert auf 0-360 Grad
        /// </summary>
        public double AngleNormalized {
            get { return Angle % 360.0; }
        }

        /// <summary>
        ///     Liefert oder setzt den Radius
        /// </summary>
        public double Radius { get; set; }
    }
}