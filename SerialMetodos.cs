/*Classe Serial
 * Classe para processar todos os metodos serial para a comunicação com o Esp32
 */

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace TwoCamerasVision
{
    public class SerialMetodos
    {
        public SerialPort _serialPort { get; set; }
        private string NomeText { get; set; }
        private int BaudRate { get; set; }

        //Metodo para inicializar a Porta Serial
        public SerialMetodos(string nomeText , int baudRate)
        {
            _serialPort = new SerialPort(nomeText, baudRate);
            NomeText = nomeText;
            BaudRate = baudRate;
        }


        public void Serial()
        {
            _serialPort.PortName = NomeText;
            _serialPort.BaudRate = BaudRate;
            try
            {
                if (!_serialPort.IsOpen)
                    _serialPort.Open();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
