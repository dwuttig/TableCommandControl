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

        private SerialPort _serialPort = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One);

        /// <summary>
        ///     Wird gefeuert wenn eine Exception aufgetreten ist
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
            try {
                _serialPort.DataReceived += HandleDataReceived;
                _serialPort.Open();
            } catch (Exception e) {
                OnCommunicationErrorOccured(e);
            }
        }

        /// <summary>
        ///     Sendet die Polarkoordinate als Command über die Serielle Schnittstelle zum Arduino.
        ///     Multipliziert den Winkel und den Radius mit den entsprechenden Faktoren.
        /// </summary>
        /// <param name="polarCoordinate"></param>
        /// <param name="angleFactor"></param>
        /// <param name="polarRadiusFactor"></param>
        public void SendpolarCoordinate(PolarCoordinate polarCoordinate, double angleFactor, double polarRadiusFactor) {
            try {
                if (!_serialPort.IsOpen) {
                    _serialPort.Open();
                }
                string coordinateAsTableCommand = polarCoordinate.AsTableCommand(angleFactor, polarRadiusFactor);
                _serialPort.Write(coordinateAsTableCommand);
            } catch (Exception e) {
                OnCommunicationErrorOccured(e);
            }
        }

        /// <summary>
        ///     Setzt den Port neu
        /// </summary>
        /// <param name="port"></param>
        public void SetPort(string port) {
            if (!port.StartsWith("COM")) {
                throw new ArgumentException("Der Port mus mit COM beginnen.");
            }
     
                _serialPort = new SerialPort(port, 115200, Parity.None, 8, StopBits.One);
        
        }

        private void HandleDataReceived(object sender, SerialDataReceivedEventArgs e) {
            try {
                SerialPort serialPort = sender as SerialPort;
                if (serialPort != null) {
                    string data = serialPort.ReadExisting();
                    if (data == ACK) {
                        Application.Current.Dispatcher.InvokeAsync(OnDataAcknowledgeReceived);
                    }
                    if (data == ERROR) {
                        Application.Current.Dispatcher.InvokeAsync(() => OnCommunicationErrorOccured(new Exception()));
                    }
                }
            } catch (Exception ex) {
                OnCommunicationErrorOccured(ex);
            }
        }

        private void OnCommunicationErrorOccured(Exception e) {
            CommunicationErrorOccured?.Invoke(this, e);
        }

        private void OnDataAcknowledgeReceived() {
            DataAcknowledgeReceived?.Invoke(this, EventArgs.Empty);
        }
    }
}