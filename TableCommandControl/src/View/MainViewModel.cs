using System;
using System.Collections.ObjectModel;
using System.Linq;

using Com.QueoFlow.Commons.Mvvm;
using Com.QueoFlow.Commons.Mvvm.Commands;

using TableCommandControl.Collections;
using TableCommandControl.Communication;
using TableCommandControl.Domain;
using TableCommandControl.View.PatternGenerators;

namespace TableCommandControl.View {
    public class MainViewModel : WindowViewModelBase, IMainViewModel {
        private readonly ArduinoProtocolLayer _arduinoProtocolLayer = new ArduinoProtocolLayer();

        private double _angleFactor = 1;

        private ObservableQueue<string> _commandHistoryQueue = new ObservableQueue<string>(10);

        private string _communicationProtocolText = string.Empty;

        private ObservableCollection<PolarCoordinate> _currentPoints = new ObservableCollection<PolarCoordinate>();

        private PolarCoordinate _currentPolarCoordinate;

        private string _errorMessage;

        private string _infoMessage;

        private ObservableCollection<IPatternGenerator> _patternGenerators =
                new ObservableCollection<IPatternGenerator>();

        private ObservableCollection<PolarCoordinate> _polarCoordinates = new ObservableCollection<PolarCoordinate>();

        private double _radiusFactor = 2;

        private RelayCommand _startSendingCommand;

        private int _steps = 200;

        private RelayCommand _stopSendingCommand;

        private int _tableSizeInMillimeters = 300;

        public MainViewModel() {
            try {
                _arduinoProtocolLayer.Initialize();
                PatternGenerators.Add(new CircleGenerator(this));
                PatternGenerators.Add(new HelixGenerator(this));
                PatternGenerators.Add(new RectangleGenerator(this));
                PatternGenerators.Add(new RectangularHelixGenerator(this));
                CommandHistoryQueue.Enqueue("Started...");
            }
            catch (Exception e) {

               CommandHistoryQueue.Enqueue("Init error...");
            }
        }

        /// <summary>
        ///     Liefert oder setzt den AngleFactor
        /// </summary>
        public double AngleFactor {
            get { return _angleFactor; }
            set {
                SetProperty(ref _angleFactor, value);
                _arduinoProtocolLayer.SetAngleFactor(_angleFactor);
            }
        }

        /// <summary>
        ///     Liefert oder setzt die Queue für das Sendeprotokoll
        /// </summary>
        public ObservableQueue<string> CommandHistoryQueue {
            get { return _commandHistoryQueue; }
            set { SetProperty(ref _commandHistoryQueue, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Liste der aktuellen Punkte
        /// </summary>
        public ObservableCollection<PolarCoordinate> CurrentPoints {
            get { return _currentPoints; }
            set { SetProperty(ref _currentPoints, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die aktuelle Polarkoordinate
        /// </summary>
        public PolarCoordinate CurrentPolarCoordinate {
            get { return _currentPolarCoordinate; }
            set {
                SetProperty(ref _currentPolarCoordinate, value);
                CurrentPoints.Clear();
                if (_currentPolarCoordinate != null) {
                    CurrentPoints.Add(_currentPolarCoordinate);
                }
            }
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
        ///     Liefert oder setzt die Info-Nachricht. Dies kann benutzt werden um dem Nutzer Hinweise zu geben.
        /// </summary>
        public string InfoMessage {
            get { return _infoMessage; }
            set { SetProperty(ref _infoMessage, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die Liste der Mustergeneratoren
        /// </summary>
        public ObservableCollection<IPatternGenerator> PatternGenerators {
            get { return _patternGenerators; }
            set { SetProperty(ref _patternGenerators, value); }
        }

        /// <summary>
        ///     Liefert oder setzt die zu senden Polarkoordinaten.
        /// </summary>
        public ObservableCollection<PolarCoordinate> PolarCoordinates {
            get { return _polarCoordinates; }
            set { SetProperty(ref _polarCoordinates, value); }
        }

        /// <summary>
        ///     Liefert oder setzt den RadiusFactor
        /// </summary>
        public double RadiusFactor {
            get { return _radiusFactor; }
            set {
                SetProperty(ref _radiusFactor, value);
                _arduinoProtocolLayer.SetPolarRadiusFactor(_radiusFactor);
            }
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
        ///     Liefert oder setzt die Tischgröße in Millimeter
        /// </summary>
        public int TableRadiusInMillimeters {
            get { return _tableSizeInMillimeters; }
            set { SetProperty(ref _tableSizeInMillimeters, value); }
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

        private void HandleCommunicationError(object sender, Exception e) {
            _arduinoProtocolLayer.DataAcknowledgeReceived -= HandleDataAcknowledge;
            _arduinoProtocolLayer.CommunicationErrorOccured -= HandleCommunicationError;
            CommandHistoryQueue.Enqueue("Bei der Kommunikation mit dem Arduino ist ein Fehler aufgetreten.");
        }

        private void HandleDataAcknowledge(object sender, EventArgs e) {
            if (CurrentPolarCoordinate != null) {
                int currentIndex = PolarCoordinates.IndexOf(CurrentPolarCoordinate);
                if (currentIndex < PolarCoordinates.Count - 1) {
                    CurrentPolarCoordinate = PolarCoordinates[currentIndex + 1];
                } else {
                    CurrentPolarCoordinate = PolarCoordinates.FirstOrDefault();
                }
            }
            if (CurrentPolarCoordinate != null) {
                _arduinoProtocolLayer.SendpolarCoordinate(CurrentPolarCoordinate);
                CommandHistoryQueue.Enqueue($"Sent: {CurrentPolarCoordinate}");
            }
        }

        private void StartSending() {
            _arduinoProtocolLayer.DataAcknowledgeReceived += HandleDataAcknowledge;
            _arduinoProtocolLayer.CommunicationErrorOccured += HandleCommunicationError;
            if (CurrentPolarCoordinate == null) {
                CurrentPolarCoordinate = PolarCoordinates.FirstOrDefault();
            }
            if (CurrentPolarCoordinate != null) {
                _arduinoProtocolLayer.SendpolarCoordinate(CurrentPolarCoordinate);
                CommandHistoryQueue.Enqueue($"Sent: {CurrentPolarCoordinate}");
            }
        }

        private void StopSending() {
            _arduinoProtocolLayer.DataAcknowledgeReceived -= HandleDataAcknowledge;
        }
    }
}