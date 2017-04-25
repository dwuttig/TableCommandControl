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
        private double _angleFactor = 1;
        private double _polarRadiusFactor = 1;
        private SerialPort _serialPort = new SerialPort("COM6", 115200, Parity.None, 8, StopBits.One);

        /// <summary>
        ///     Setzt den Port neu
        /// </summary>
        /// <param name="port"></param>
        public void SetPort(string port) {
            _serialPort = new SerialPort(port, 115200, Parity.None, 8, StopBits.One);
        }

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
            }
            catch (Exception e) {

                OnCommunicationErrorOccured(e);

            }
        }

        public void SendpolarCoordinate(PolarCoordinate polarCoordinate) {
            try {
                if (!_serialPort.IsOpen) {
                    _serialPort.Open();
                }
                string asCommand = polarCoordinate.AsCommand(_angleFactor, _polarRadiusFactor);

                _serialPort.Write(asCommand);
            }
            catch (Exception e) {

                OnCommunicationErrorOccured(e);
            }


        }

        public void SetPolarRadiusFactor(double radiusFactor) {
            _polarRadiusFactor = radiusFactor;
        }

        public void SetAngleFactor(double angleFactor) {
            _angleFactor = angleFactor;
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
                    if (data == ERROR) {
                        Application.Current.Dispatcher.InvokeAsync(() => OnCommunicationErrorOccured(new Exception()));
                    }
                }
            }
            catch (Exception ex) {
                OnCommunicationErrorOccured(ex);
            }
        }
    }
}