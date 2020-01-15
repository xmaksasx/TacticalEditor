using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace TacticalEditor.Helpers
{
    class UdpHelper
    {

        private UdpClient _receiveClient;
        private UdpClient _sendClient;



        public UdpHelper()
        {
            _receiveClient = new UdpClient(20500);
            _sendClient = new UdpClient();
        }



        public void Receive()
        {
            if (IsAvailable) return;
            IPEndPoint ipendpoint = null;
            byte[] packetBytes = _receiveClient.Receive(ref ipendpoint);
        }

        private bool IsAvailable
        {
            get
            {
                if (_receiveClient.Available == 0)
                {
                    return true;
                }

                return false;
            }
        }

        public void Send(byte[] dgram, string hostname, int port)
        {
            try
            {
                _sendClient?.Send(dgram, dgram.Length, hostname, port);
            }
            catch (Exception expSend)
            {
                MessageBox.Show(expSend.ToString());
            }
        }

    }
}
