using System.Collections.ObjectModel;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;

namespace TableCommandControl.View {
    public class DMainViewModel : WindowViewModelBase, IMainViewModel {
        public DMainViewModel() {
            PolarCoordinates = new ObservableCollection<PolarCoordinate>();
            PolarCoordinates.Add(new PolarCoordinate(1, 30));
            PolarCoordinates.Add(new PolarCoordinate(10, 30));
            PolarCoordinates.Add(new PolarCoordinate(20, 30));
            PolarCoordinates.Add(new PolarCoordinate(30, 30));
            PolarCoordinates.Add(new PolarCoordinate(40, 30));
            PolarCoordinates.Add(new PolarCoordinate(45, 30));
        }

        /// <summary>
        ///     Liefert oder setzt den Radius des Kreises der generiert werden soll.
        /// </summary>
        public int CircleRadius { get; set; }

        /// <summary>
        ///     Liefert oder setzt die aktuelle Polarkoordinate
        /// </summary>
        public PolarCoordinate CurrentPolarCoordinate { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Fehlernachricht. Diese wird benutzt um eine Fehlerbenachrichtigung für den Nutzer
        ///     anzuzeigen.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///    Liefert oder setzt den Text der Protokollkommunikation.
        /// </summary>
        public string CommunicationProtocolText { get; set; }

        /// <summary>
        ///     Liefert den Command zum Generieren eines Kreises anhand des Radius
        /// </summary>
        public RelayCommand GenerateCircleCommand { get; }

        /// <summary>
        ///     Liefert den Command zum Erzeugen der Helix
        /// </summary>
        public RelayCommand GenerateHelixCommand { get; }

        /// <summary>
        ///     Liefert oder setzt den Endradius der Helix
        /// </summary>
        public int HelixEndRadius { get; set; }

        /// <summary>
        ///     Liefert oder setzt Startradius der Spirale
        /// </summary>
        public int HelixStartRadius { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Anzahl der Windungen der Helix
        /// </summary>
        public double HelixWhorls { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Info-Nachricht. Dies kann benutzt werden um dem Nutzer Hinweise zu geben.
        /// </summary>
        public string InfoMessage { get; set; }

        /// <summary>
        ///     Liefert oder setzt die zu senden Polarkoordinaten.
        /// </summary>
        public ObservableCollection<PolarCoordinate> PolarCoordinates { get; set; }

        /// <summary>
        ///     Liefert den Command zum Start des Sendens der Koordinaten
        /// </summary>
        public RelayCommand StartSendingCommand { get; }

        /// <summary>
        ///     Liefert oder setzt die Schrittanzahl beim Generieren der Pfade.
        /// </summary>
        public int Steps { get; set; }

        /// <summary>
        ///     Liefert den Command zum Stoppen des Sendens der Koordinaten
        /// </summary>
        public RelayCommand StopSendingCommand { get; }
    }
}