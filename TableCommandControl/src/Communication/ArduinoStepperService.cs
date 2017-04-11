using System;
using System.IO.Ports;

using TableCommandControl.Domain;

namespace TableCommandControl.Communication {
    public class ArduinoStepperService {
        private SerialPort _serialPort = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);

        /// <summary>
        ///     Wird gefeuert wenn ein Acknowledge vom ComPort empfangen wurde.
        /// </summary>
        public event EventHandler DataAcknowledgeReceived;

        public void SendpolarCoordinate(PolarCoordinate polarCoordinate) {
        }

        protected virtual void OnDataAcknowledgeReceived() {
            DataAcknowledgeReceived?.Invoke(this, EventArgs.Empty);
        }
    }
}