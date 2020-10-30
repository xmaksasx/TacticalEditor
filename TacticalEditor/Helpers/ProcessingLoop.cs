using System;
using System.Threading;
using TacticalEditor.Calculate;
using TacticalEditor.Models;
using TacticalEditor.Send;

namespace TacticalEditor.Helpers
{
    class ProcessingLoop
    {
        private ListOfNavigationPoint _listOfNavigationPoint;
        private ListOfAirBases _listOfAirBases;
        private Thread _threadSend;
        private Thread _threadReceive;
        private UdpHelper _udpHelper;
        private SendLandingStruct _sendLandingStruct;
        private AircraftPosition _aircraftPosition;
        private SendAircraftStruct _sendAircraftStruct;
        private SendRouteToIup _sendRouteToIup;
        private CalculatePpmPoints _calculatePpmPoints;
        private CalculateAirBases _calculateAirBases;
        private bool IsLooping;

        public ProcessingLoop()
        {
             IsLooping = true;

            _udpHelper = new UdpHelper();
            _aircraftPosition = new AircraftPosition();

            _listOfNavigationPoint = new ListOfNavigationPoint();
            _listOfAirBases = new ListOfAirBases();
            _sendLandingStruct = new SendLandingStruct();
            _sendAircraftStruct = new SendAircraftStruct();
            _sendRouteToIup = new SendRouteToIup();
            _calculatePpmPoints = new CalculatePpmPoints();
            _calculateAirBases = new CalculateAirBases();

            _threadSend = new Thread(SendingLoop);
            _threadSend.Start();
            _threadReceive = new Thread(ReceivingLoop);
            _threadReceive.Start();
        }


        private void SendingLoop()
        {
            while (IsLooping)
            {

                _udpHelper.Send(_listOfNavigationPoint.GetByte(), "255.255.255.255", 20041);
                _udpHelper.Send(_listOfAirBases.GetByte(), "255.255.255.255", 20020);
                _udpHelper.Send(_sendLandingStruct.GetByte(), "255.255.255.255", 20020);
                _udpHelper.Send(_sendAircraftStruct.GetByte(_aircraftPosition), "255.255.255.255", 20020);
                _udpHelper.Send(_sendRouteToIup.GetByte(), "192.168.1.56", 30042);
                Thread.Sleep(20);
            }
        }

        private void ReceivingLoop()
        {
            while (IsLooping)
            {
                var receivedBytes = _udpHelper.Receive();
                if (receivedBytes.Length == 0) continue;
                string header = System.Text.Encoding.UTF8.GetString(receivedBytes, 0, 30).Trim('\0');
                ProcessingPackage(header, receivedBytes);
            }
        }
        

        private void ProcessingPackage(string header, byte[] receivedBytes)
        {
            switch (header)
            {
                case "Aircraft_Position":
                    ConvertHelper.ByteToObject(receivedBytes, _aircraftPosition);
                 //   EventsHelper.OnChangeAircraftCoordinateEvent(_aircraftPosition);
                    break;
            }
        }

        public void Destroy()
        {
            IsLooping = false;
        }

    }
}