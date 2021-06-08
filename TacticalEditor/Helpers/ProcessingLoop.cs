using System;
using System.Threading;
using TacticalEditor.Calculate;
using TacticalEditor.Models;
using TacticalEditor.Send;

namespace TacticalEditor.Helpers
{
    class ProcessingLoop
    {
   
        private Thread _threadSend;
        private Thread _threadReceive;
        private UdpHelper _udpHelper;
        private SendLandingStruct _sendLandingStruct;
        private AircraftPosition _aircraftPosition;
        private SendAircraftStruct _sendAircraftStruct;
        private SendRouteToIup _sendRouteToIup;
        private SendToSvvo _sendToSvvo;
        private CalculatePpmPoints _calculatePpmPoints;
        private CalculateAerodromes _calculateAerodromes;
        private ChangeNp _changeNp;
        private bool IsLooping;

        public ProcessingLoop()
        {
             IsLooping = true;
            _udpHelper = new UdpHelper();
            _aircraftPosition = new AircraftPosition();
            _sendLandingStruct = new SendLandingStruct();
            _sendAircraftStruct = new SendAircraftStruct();
            _sendRouteToIup = new SendRouteToIup();
            _calculatePpmPoints = new CalculatePpmPoints();
            _calculateAerodromes = new CalculateAerodromes();
            _sendToSvvo = new SendToSvvo();
            _changeNp = new ChangeNp();
            _threadSend = new Thread(SendingLoop);
            _threadSend.Start();
            _threadReceive = new Thread(ReceivingLoop);
            _threadReceive.Start();
        }


        private void SendingLoop()
        {
            while (IsLooping)
            {
				//_udpHelper.Send(_sendLandingStruct.GetByte(), "255.255.255.255", 20020);
                //_udpHelper.Send(_sendRouteToIup.GetByteNp(), "127.0.0.1", 20020);
               // _udpHelper.Send(_sendRouteToIup.GetByteAerodromes(), "192.168.1.56", 30043);
                Thread.Sleep(20);
                EventsHelper.OnDebugNumberEvent();
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
			        break;
		        case "Reset_Position":
			        _aircraftPosition.GeoCoordinate.X = 0;
			        _aircraftPosition.GeoCoordinate.Z = 0;
			        _aircraftPosition.GeoCoordinate.H = 0;
			        break;
		        case "ChangeRoute":
			        for (int i = 68; i < receivedBytes.Length; i += 8)
				        Array.Reverse(receivedBytes, i, 8);
			        ConvertHelper.ByteToObject(receivedBytes, _changeNp);
                    EventsHelper.OnChangeNpDEvent(_changeNp);
			        break;
	        }
        }


        public void Destroy()
        {
            IsLooping = false;
        }

    }
}