using System;
using System.Windows;

namespace TableCommandControl.Domain {
    /// <summary>
    ///     Diese Klasse stellt eine Polarkoordinate dar.
    /// </summary>
    public class PolarCoordinate {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="T:System.Object" />-Klasse.
        /// </summary>
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
        ///     Liefert den Winkel im Bogenmaß
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
        ///     Liefert oder setzt den Radius in Millimeter
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Liefert eine Polarkoordinate anhand eines Punktes in kartesischen Koordinaten
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static PolarCoordinate FromPoint(Point point) {
            double radius = Math.Sqrt(Math.Pow(point.X, 2) + Math.Pow(point.Y, 2));
            double atan = Math.Atan(point.Y / point.X);
            double angle = Trigonometry.RadianToDegree(atan);
            if (point.X < 0 && point.Y > 0) {
                angle = 180 + Math.Abs(angle);
            }
            if (point.X < 0 && point.Y < 0) {
                angle = 180 - Math.Abs(angle);
            }
            if (point.X > 0 && point.Y < 0) {
                angle = Math.Abs(angle);
            }
            if (point.X > 0 && point.Y > 0) {
                angle = 360 - Math.Abs(angle);
            }
            return new PolarCoordinate(angle, radius);
        }

        /// <summary>
        ///     Liefert die Polarkoordinate als string im Format Winkel;Radius
        /// </summary>
        /// <param name="angleFactor"></param>
        /// <param name="polarRadiusFactor"></param>
        /// <returns></returns>
        public string AsTableCommand(double angleFactor, double polarRadiusFactor) {
            return $"{(int)(Angle * angleFactor)};{(int)(Radius * polarRadiusFactor)}";
        }

        /// <summary>Gibt eine Zeichenfolge zurück, die das aktuelle Objekt darstellt.</summary>
        /// <returns>Eine Zeichenfolge, die das aktuelle Objekt darstellt.</returns>
        public override string ToString() {
            return $"[{Angle:N2}°;{Radius:N2}mm]";
        }
    }
}