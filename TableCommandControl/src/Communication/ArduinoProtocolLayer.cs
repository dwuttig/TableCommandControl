using System;
using System.IO.Ports;
using System.Windows;

using TableCommandControl.Domain;

namespace TableCommandControl.Communication {
    /// <summary>
    ///     Diese Klasse stellt den ProtocolLayer zum Arduino dar.
    /// </summary>
    public class ArduinoProtocolLayer {
        private const string ACK = "A";
        private const string ERROR = "E";
        private readonly SerialPort _serialPort = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);

        /// <summary>
        /// Wird gefeurt wenn eine Exception aufgetreten ist
        /// </summary>
        public event EventHandler<Exception> CommunicationErrorOccured;

        /// <summary>
        ///     Wird gefeuert wenn ein Acknowledge vom ComPort empfangen wurde.
        /// </summary>
        public event EventHandler DataAcknowledgeReceived;

        /// <summary>
        ///     Initialisiert den ProtocolLayer und öffnet den SerialPort;
        /// </summary>
        public void Initialize() {
            _serialPort.Open();
            _serialPort.DataReceived += HandleDataReceived;
        }

        public void SendpolarCoordinate(PolarCoordinate polarCoordinate) {
            if (!_serialPort.IsOpen) {
                _serialPort.Open();
            }
            _serialPort.Write(polarCoordinate.AsCommand());
        }

        protected virtual void OnCommunicationErrorOccured(Exception e) {
            CommunicationErrorOccured?.Invoke(this, e);
        }

        protected virtual void OnDataAcknowledgeReceived() {
            DataAcknowledgeReceived?.Invoke(this, EventArgs.Empty);
        }

        private void HandleDataReceived(object sender, SerialDataReceivedEventArgs e) {
            try {
                SerialPort serialPort = sender as SerialPort;
                if (serialPort != null) {
                    string data = serialPort.ReadExisting();
                    if (data == ACK) {
                        Application.Current.Dispatcher.InvokeAsync(OnDataAcknowledgeReceived);
                    }
                    if (data==ERROR) {
                        Application.Current.Dispatcher.InvokeAsync(()=>OnCommunicationErrorOccured(new Exception()));
                    }
                }
            } catch (Exception ex) {
                OnCommunicationErrorOccured(ex);
            }
        }
    }
}