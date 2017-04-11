using System.Collections.ObjectModel;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;

namespace TableCommandControl.View {
    public interface IMainViewModel : IWindowViewModelBase {
        /// <summary>
        ///     Liefert oder setzt den Radius des Kreises der generiert werden soll.
        /// </summary>
        int CircleRadius { get; set; }

        /// <summary>
        ///     Liefert den Command zum Generieren eines Kreises anhand des Radius
        /// </summary>
        RelayCommand GenerateCircleCommand { get; }

        /// <summary>
        ///     Liefert oder setzt die zu senden Polarkoordinaten.
        /// </summary>
        ObservableCollection<PolarCoordinate> PolarCoordinates { get; set; }

        /// <summary>
        ///    Liefert oder setzt die Anzahl der Windungen der Helix
        /// </summary>
        double HelixWhorls { get; set; }

        /// <summary>
        ///    Liefert oder setzt Startradius der Spirale
        /// </summary>
        int HelixStartRadius { get; set; }

        /// <summary>
        ///    Liefert oder setzt den Endradius der Helix
        /// </summary>
        int HelixEndRadius { get; set; }

        /// <summary>
        /// Liefert den Command zum Erzeugen der Helix
        /// </summary>
        RelayCommand GenerateHelixCommand { get; }

        /// <summary>
        ///     Liefert oder setzt die Schrittanzahl beim Generieren der Pfade.
        /// </summary>
        int Steps { get; set; }

        /// <summary>
        /// Liefert den Command zum Start des Sendens der Koordinaten
        /// </summary>
        RelayCommand StartSendingCommand { get; }

        /// <summary>
        ///    Liefert oder setzt die aktuelle Polarkoordinate
        /// </summary>
        PolarCoordinate CurrentPolarCoordinate { get; set; }

        /// <summary>
        /// Liefert den Command zum Stoppen des Sendens der Koordinaten
        /// </summary>
        RelayCommand StopSendingCommand { get; }

        /// <summary>
        ///     Liefert oder setzt die Info-Nachricht. Dies kann benutzt werden um dem Nutzer Hinweise zu geben.
        /// </summary>
        string InfoMessage { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Fehlernachricht. Diese wird benutzt um eine Fehlerbenachrichtigung für den Nutzer
        ///     anzuzeigen.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        ///    Liefert oder setzt den Text der Protokollkommunikation.
        /// </summary>
        string CommunicationProtocolText { get; set; }
    }
}