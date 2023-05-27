using System.IO.Ports;

namespace Controlinventarioissn.library
{
    public class SerialPortConnector
    {
        private readonly int _baudRate = 9600; //9600baudios velocidad estandar de la mayoria de los puertos
        private readonly string _portName = "COM7";

        public string Read()
        {
            using (var serialPort = new SerialPort(_portName, _baudRate))
            {
                serialPort.Open();
                return serialPort.ReadLine();
            }

        }
    }
}
