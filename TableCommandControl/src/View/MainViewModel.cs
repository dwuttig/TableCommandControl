using System;
using System.Collections.ObjectModel;
using System.Linq;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Communication;
using TableCommandControl.Domain;

namespace TableCommandControl.View {
    public class MainViewModel : WindowViewModelBase, IMainViewModel {
        private readonly ArduinoProtocolLayer _arduinoProtocolLayer = new ArduinoProtocolLayer();
        private int _circleRadius = 1;

        private string _communicationProtocolText = string.Empty;

        private PolarCoordinate _currentPolarCoordinate;

        private string _errorMessage;

        private RelayCommand _generateCircleCommand;

        private RelayCommand _generateHelixCommand;

        private int _helixEndRadius = 1;

        private int _helixStartRadius = 2;

        private double _helixWhorls = 1;

        private string _infoMessage;

        private ObservableCollection<PolarCoordinate> _polarCoordinates = new ObservableCollection<PolarCoordinate>();

        private RelayCommand _startSendingCommand;

        private int _steps = 200;

        private RelayCommand _stopSendingCommand;

        /// <summary>
        ///     Liefert oder setzt den Radius des Kreises der generiert werden soll.
        /// </summary>
        public int CircleRadius {
            get { return _circleRadius; }
            set { SetProperty(ref _circleRadius, value); }
        }

        /// <summary>
        ///     Liefert oder setzt den Text der Protokollkommunikation.
        /// </summary>
        public string CommunicationProtocolText {
            get { return _communicationProtocolText; }
            set { SetProperty(ref _communicationProtocolText, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die aktuelle Polarkoordinate
        /// </summary>
        public PolarCoordinate CurrentPolarCoordinate {
            get { return _currentPolarCoordinate; }
            set { SetProperty(ref _currentPolarCoordinate, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Fehlernachricht. Diese wird benutzt um eine Fehlerbenachrichtigung für den Nutzer
        ///     anzuzeigen.
        /// </summary>
        public string ErrorMessage {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        /// <summary>
        ///     Liefert den Command zum Generieren eines Kreises anhand des Radius
        /// </summary>
        public RelayCommand GenerateCircleCommand {
            get {
                if (_generateCircleCommand == null) {
                    _generateCircleCommand = new RelayCommand(GenerateCircle, CanGenerateCircle);
                }

                return _generateCircleCommand;
            }
        }

        /// <summary>
        ///     Liefert den Command zum Erzeugen der Helix
        /// </summary>
        public RelayCommand GenerateHelixCommand {
            get {
                if (_generateHelixCommand == null) {
                    _generateHelixCommand = new RelayCommand(GenerateHelix, CanGenerateHelix);
                }

                return _generateHelixCommand;
            }
        }

        /// <summary>
        ///     Liefert oder setzt den Endradius der Helix
        /// </summary>
        public int HelixEndRadius {
            get { return _helixEndRadius; }
            set { SetProperty(ref _helixEndRadius, value); }
        }

        /// <summary>
        ///     Liefert oder setzt Startradius der Spirale
        /// </summary>
        public int HelixStartRadius {
            get { return _helixStartRadius; }
            set { SetProperty(ref _helixStartRadius, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Anzahl der Windungen der Helix
        /// </summary>
        public double HelixWhorls {
            get { return _helixWhorls; }
            set { SetProperty(ref _helixWhorls, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Info-Nachricht. Dies kann benutzt werden um dem Nutzer Hinweise zu geben.
        /// </summary>
        public string InfoMessage {
            get { return _infoMessage; }
            set { SetProperty(ref _infoMessage, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die zu senden Polarkoordinaten.
        /// </summary>
        public ObservableCollection<PolarCoordinate> PolarCoordinates {
            get { return _polarCoordinates; }
            set { SetProperty(ref _polarCoordinates, value); }
        }

        /// <summary>
        ///     Liefert den Command zum Start des Sendens der Koordinaten
        /// </summary>
        public RelayCommand StartSendingCommand {
            get {
                if (_startSendingCommand == null) {
                    _startSendingCommand = new RelayCommand(StartSending);
                }

                return _startSendingCommand;
            }
        }

        /// <summary>
        ///     Liefert oder setzt die Schrittanzahl beim Generieren der Pfade.
        /// </summary>
        public int Steps {
            get { return _steps; }
            set { SetProperty(ref _steps, value); }
        }

        /// <summary>
        ///     Liefert den Command zum Stoppen des Sendens der Koordinaten
        /// </summary>
        public RelayCommand StopSendingCommand {
            get {
                if (_stopSendingCommand == null) {
                    _stopSendingCommand = new RelayCommand(StopSending);
                }

                return _stopSendingCommand;
            }
        }

        /// <summary>
        ///     Setzt die Fehlernachricht
        /// </summary>
        /// <param name="errorMessage"></param>
        public void SetErrorMessage(string errorMessage) {
            ErrorMessage = errorMessage;
            ErrorMessage = null;
        }

        /// <summary>
        ///     Setzt die Fehlernachricht
        /// </summary>
        /// <param name="infoMessage"></param>
        public void SetInfoMessage(string infoMessage) {
            InfoMessage = infoMessage;
            InfoMessage = null;
        }

        private bool CanGenerateCircle() {
            return CircleRadius > 0;
        }

        private bool CanGenerateHelix() {
            return HelixStartRadius >= HelixEndRadius && HelixWhorls > 0;
        }

        private void GenerateCircle() {
            PolarCoordinates.Clear();
            double angleSteps = 360 / (double)Steps;
            double currentAngle = 0;
            for (int i = 0; i < Steps + 1; i++) {
                PolarCoordinates.Add(new PolarCoordinate(currentAngle, _circleRadius));
                currentAngle += angleSteps;
            }
        }

        private void GenerateHelix() {
            double radiusSteps = (HelixStartRadius - HelixEndRadius) / (double)Steps;
            PolarCoordinates.Clear();
            double angleSteps = 360 * HelixWhorls / Steps;
            double currentAngle = 0;
            double currentRadius = HelixStartRadius;
            for (int i = 0; i < Steps + 1; i++) {
                PolarCoordinates.Add(new PolarCoordinate(currentAngle, currentRadius));
                currentRadius -= radiusSteps;
                currentAngle += angleSteps;
            }
        }

        private void HandleCommunicationError(object sender, Exception e) {
            SetErrorMessage("Bei der Kommunikation mit dem Arduino ist ein Fehler aufgetreten.");
            CommunicationProtocolText += e.Message;
            CommunicationProtocolText += Environment.NewLine;
        }

        private void HandleDataAcknowledge(object sender, EventArgs e) {
            if (CurrentPolarCoordinate != null) {
                int currentIndex = PolarCoordinates.IndexOf(CurrentPolarCoordinate);
                if (currentIndex < PolarCoordinates.Count) {
                    CurrentPolarCoordinate = PolarCoordinates[currentIndex + 1];
                } else {
                    CurrentPolarCoordinate = PolarCoordinates.FirstOrDefault();
                }
            }
            if (CurrentPolarCoordinate != null) {
                _arduinoProtocolLayer.SendpolarCoordinate(CurrentPolarCoordinate);
                CommunicationProtocolText += $"Send: {CurrentPolarCoordinate}";
                CommunicationProtocolText += Environment.NewLine;
            }
        }

        private void StartSending() {
            _arduinoProtocolLayer.DataAcknowledgeReceived += HandleDataAcknowledge;
            _arduinoProtocolLayer.CommunicationErrorOccured += HandleCommunicationError;
        }

        private void StopSending() {
            _arduinoProtocolLayer.DataAcknowledgeReceived -= HandleDataAcknowledge;
        }
    }
}