using System.Collections.Generic;
using System.Threading;
using TacticalEditor.Models;

namespace TacticalEditor.Helpers
{
    class ProcessingLoop
    {
        private Route _route;
        private Thread _threadProcessing;
        private UdpHelper _udpHelper;
        private bool IsLooping;

        public ProcessingLoop()
        {
            IsLooping = true;
            _udpHelper = new UdpHelper();
            _route = new Route();
            _threadProcessing = new Thread(MainLoop);
            _threadProcessing.Start();
        }

 
        private void MainLoop()
        {
            while (IsLooping)
            {
                _udpHelper.Receive();
                _udpHelper.Send(_route.GetByte(), "255.255.255.255",20020);
                Thread.Sleep(20);
            }
        }

        public void Destroy()
        {
            IsLooping = false;
        }
    }
}
