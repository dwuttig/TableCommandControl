using System.Collections.ObjectModel;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;
using TableCommandControl.View.PatternGenerators;

namespace TableCommandControl.View {
    public interface IMainViewModel : IWindowViewModelBase {
        /// <summary>
        ///     Liefert oder setzt den Text der Protokollkommunikation.
        /// </summary>
        string CommunicationProtocolText { get; set; }

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
        ///    Liefert oder setzt die Liste der aktuellen Punkte
        /// </summary>
        ObservableCollection<PolarCoordinate> CurrentPoints { get; set; }

        /// <summary>
        ///    Liefert oder setzt die Tischgröße in Millimeter
        /// </summary>
        int TableRadiusInMillimeters { get; set; }
    }
}