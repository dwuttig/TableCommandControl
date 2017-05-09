
﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;




using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Collections;
using TableCommandControl.Domain;
using TableCommandControl.View.PatternGenerators;

namespace TableCommandControl.View {
    public interface IMainViewModel : IWindowViewModelBase {
        /// <summary>
        ///     Liefert oder setzt den AngleFactor
        /// </summary>
        double AngleFactor { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Queue für das Sendeprotokoll
        /// </summary>
        ObservableQueue<string> CommandHistoryQueue { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Liste der aktuellen Punkte
        /// </summary>
        ObservableCollection<PolarCoordinate> CurrentPoints { get; set; }

        /// <summary>
        ///     Liefert oder setzt die aktuelle Polarkoordinate
        /// </summary>
        PolarCoordinate CurrentPolarCoordinate { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Fehlernachricht. Diese wird benutzt um eine Fehlerbenachrichtigung für den Nutzer
        ///     anzuzeigen.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Info-Nachricht. Dies kann benutzt werden um dem Nutzer Hinweise zu geben.
        /// </summary>
        string InfoMessage { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Liste der Mustergeneratoren
        /// </summary>
        ObservableCollection<IPatternGenerator> PatternGenerators { get; set; }

        /// <summary>
        ///     Liefert oder setzt die zu senden Polarkoordinaten.
        /// </summary>
        ObservableCollection<PolarCoordinate> PolarCoordinates { get; set; }

        /// <summary>
        ///     Liefert oder setzt den RadiusFactor
        /// </summary>
        double RadiusFactor { get; set; }

        /// <summary>
        ///     Liefert den Command zum Start des Sendens der Koordinaten
        /// </summary>
        RelayCommand StartSendingCommand { get; }

        /// <summary>
        ///     Liefert oder setzt die Schrittanzahl beim Generieren der Pfade.
        /// </summary>
        int Steps { get; set; }

        /// <summary>
        ///     Liefert den Command zum Stoppen des Sendens der Koordinaten
        /// </summary>
        RelayCommand StopSendingCommand { get; }

        /// <summary>
        ///     Liefert oder setzt die Tischgröße in Millimeter
        /// </summary>
        int TableRadiusInMillimeters { get; set; }


        /// <summary>
        /// Setzt den Port
        /// </summary>
        string Port { get; set; }

        /// <summary>
        /// Setzt die Liste der Ports
        /// </summary>
        IList<string> Ports { get; set; }

        /// <summary>
        ///     Liefert den Command zum Stoppen des Sendens der Koordinaten
        /// </summary>
        RelayCommand SetZeroCommand { get; }
    }
}