using System;
using System.Collections.Generic;

namespace TableCommandControl.Utils {
    /// <summary>
    ///     Enthält Methoden für den allgemeinen Umgang mit Objekten.
    /// </summary>
    public static class Require {
        /// <summary>
        ///     Überprüft, ob ein <see cref="TValue" /> größer oder gleich dem geforderten <see cref="TValue" /> ist.
        /// </summary>
        public static void Ge<TValue>(TValue number, TValue atLeast, string propertyName) {
            if (Comparer<TValue>.Default.Compare(number, atLeast) < 0) {
                throw new ArgumentOutOfRangeException(propertyName,
                    number,
                    string.Format("Der Parameter {0} muss größer als oder gleich {1} sein. Er war aber nur {2}",
                        propertyName,
                        atLeast,
                        number));
            }
        }

        /// <summary>
        ///     Überprüft ob ein <see cref="TValue" /> größer dem geforderten Wert ist.
        /// </summary>
        public static void Gt<TValue>(TValue number, TValue atLeast, string propertyName) {
            if (Comparer<TValue>.Default.Compare(number, atLeast) <= 0) {
                throw new ArgumentOutOfRangeException(propertyName,
                    number,
                    string.Format("Der Parameter {0} muss größer als {1} sein. Er war aber nur {2}", propertyName, atLeast, number));
            }
        }

        /// <summary>
        ///     Überprüft, ob ein <see cref="TValue" /> kleiner oder gleich dem geforderten <see cref="TValue" /> ist.
        /// </summary>
        public static void Le<TValue>(TValue number, TValue atLeast, string propertyName) {
            if (Comparer<TValue>.Default.Compare(number, atLeast) > 0) {
                throw new ArgumentOutOfRangeException(propertyName,
                    number,
                    string.Format("Der Parameter {0} muss kleiner als oder gleich {1} sein. Er war aber nur {2}",
                        propertyName,
                        atLeast,
                        number));
            }
        }

        /// <summary>
        ///     Überprüft ob ein <see cref="TValue" /> kleiner dem geforderten Wert ist.
        /// </summary>
        public static void Lt<TValue>(TValue number, TValue atLeast, string propertyName) {
            if (Comparer<TValue>.Default.Compare(number, atLeast) >= 0) {
                throw new ArgumentOutOfRangeException(propertyName,
                    number,
                    string.Format("Der Parameter {0} muss kleiner als {1} sein. Er war aber nur {2}", propertyName, atLeast, number));
            }
        }

        /// <summary>
        ///     Überprüft, dass die übergebene Objektreferenz nicht <code>null</code> ist.
        /// </summary>
        /// <remarks>
        ///     Die Methode ist hauptsächlich für den Einsatz bei der Parametervalidierung in
        ///     Methoden und Konstruktoren gedacht. Entsprechend dem folgenden Beispiel:
        ///     public void Foo(Bar bar) {
        ///     this.bar = Require.RequireNonNull(bar);
        ///     }
        /// </remarks>
        /// <typeparam name="T">Der Typ der Referenz</typeparam>
        /// <param name="obj">Die Objektreferenz die auf <code>null</code> zu prüfen ist.</param>
        /// <returns>Das obj wenn es nicht null ist.</returns>
        public static T NotNull<T>(T obj) where T : class {
            return NotNull(obj, "");
        }

        /// <summary>
        ///     Überprüft, dass die übergebene Objektreferenz nicht <code>null</code> ist.
        /// </summary>
        /// <remarks>
        ///     Die Methode ist hauptsächlich für den Einsatz bei der Parametervalidierung in
        ///     Methoden und Konstruktoren gedacht. Entsprechend dem folgenden Beispiel:
        ///     <code>
        ///     public void Foo(Bar bar) {
        ///         this.bar = Require.RequireNonNull(bar, "bar");
        ///     }
        /// </code>
        /// </remarks>
        /// <typeparam name="T">Der Typ der Referenz</typeparam>
        /// <param name="obj">Die Objektreferenz die auf <code>null</code> zu prüfen ist.</param>
        /// <param name="parameterName">Der Name des überprüften Parameters</param>
        /// <returns>Das obj wenn es nicht null ist.</returns>
        public static T NotNull<T>(T obj, string parameterName) {
            if (obj == null) {
                throw new ArgumentNullException(parameterName);
            }
            return obj;
        }

        /// <summary>
        ///     Überprüft, dass eine Zeichenfolge nicht null und nicht leer ist.
        /// </summary>
        /// <param name="name">Die zu prüfende Zeichenfolge</param>
        /// <param name="propertyName">Der Name des Properties bzw. Parameters als welches der zu prüfende Wert übergeben wird.</param>
        public static void NotNullOrEmpty(string name, string propertyName) {
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentNullException(propertyName);
            }
        }

        /// <summary>
        ///     Überprüft, dass eine Zeichenfolge nicht null und nicht leer ist, sowie nicht nur Leerzeichen enthält.
        /// </summary>
        /// <param name="name">Die zu prüfende Zeichenfolge</param>
        /// <param name="propertyName">Der Name des Properties bzw. Parameters als welches der zu prüfende Wert übergeben wird.</param>
        public static void NotNullOrWhiteSpace(string name, string propertyName) {
            if (string.IsNullOrWhiteSpace(name)) {
                throw new ArgumentNullException(propertyName);
            }
        }
    }
}