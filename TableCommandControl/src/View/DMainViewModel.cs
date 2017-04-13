using System.Collections.ObjectModel;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Domain;
using TableCommandControl.View.PatternGenerators;

namespace TableCommandControl.View {
    public class DMainViewModel : WindowViewModelBase, IMainViewModel {
        public DMainViewModel() {
            PolarCoordinates = new ObservableCollection<PolarCoordinate>();
            PolarCoordinates.Add(new PolarCoordinate(45, 100));
            PolarCoordinates.Add(new PolarCoordinate(135, 100));
            PolarCoordinates.Add(new PolarCoordinate(225, 100));
            PolarCoordinates.Add(new PolarCoordinate(315, 100));
            PatternGenerators = new ObservableCollection<IPatternGenerator>();
            PatternGenerators.Add(new CircleGenerator(this));
            PatternGenerators.Add(new HelixGenerator(this));
                PatternGenerators.Add(new RectangleGenerator(this));
            CurrentPoints = new ObservableCollection<PolarCoordinate>();
            CurrentPoints.Add(new PolarCoordinate(45, 100));
        }

        /// <summary>
        ///     Liefert oder setzt den Text der Protokollkommunikation.
        /// </summary>
        public string CommunicationProtocolText { get; set; }

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
        ///     Liefert oder setzt die Info-Nachricht. Dies kann benutzt werden um dem Nutzer Hinweise zu geben.
        /// </summary>
        public string InfoMessage { get; set; }

        /// <summary>
        ///     Liefert oder setzt die Liste der Mustergeneratoren
        /// </summary>
        public ObservableCollection<IPatternGenerator> PatternGenerators { get; set; }

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

        /// <summary>
        ///    Liefert oder setzt die Liste der aktuellen Punkte
        /// </summary>
        public ObservableCollection<PolarCoordinate> CurrentPoints { get; set; }

        /// <summary>
        ///    Liefert oder setzt die Tischgröße in Millimeter
        /// </summary>
        public int TableRadiusInMillimeters { get; set; } = 300;
    }
}