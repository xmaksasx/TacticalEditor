using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using TacticalEditor.Models;

namespace TacticalEditor.Helpers
{
    class UdpHelper
    {

        private UdpClient _receiveClient;
        private UdpClient _sendClient;

        public UdpHelper()
        {
            _receiveClient = new UdpClient(20060);
            _sendClient = new UdpClient();
        }

        public byte[] Receive()
        {
            if (IsAvailable) return new byte[0];
            IPEndPoint ipendpoint = null;
            return _receiveClient.Receive(ref ipendpoint);
        }

        private bool IsAvailable
        {
            get
            {
                if (_receiveClient.Available == 0)
                {
                    Thread.Sleep(10);
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