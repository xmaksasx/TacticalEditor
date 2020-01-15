using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using TacticalEditor.Models;

namespace TacticalEditor.Helpers
{
    class ProcessingLoop
    {
        private Route _route;
        private List<AirPoint> _airPoints = new List<AirPoint>();
        private Thread _threadProcessing;
        private UdpHelper _udpHelper;

        public ProcessingLoop()
        {
            EventsHelper.PpmCollectionEvent += PpmCollection;
            _udpHelper = new UdpHelper();
            _route = new Route();
            _threadProcessing = new Thread(MainLoop);
            _threadProcessing.Start();
        }

        private void PpmCollection(List<AirPoint> airPoints)
        {
            _airPoints = airPoints;
            _route.AirPoints = airPoints;
        }

        private void MainLoop()
        {
            while (true)
            {
                _udpHelper.Receive();
                _udpHelper.Send(_route.GetByte(), "255.255.255.255",20020);
                Thread.Sleep(20);
            }
        }
    }
}
