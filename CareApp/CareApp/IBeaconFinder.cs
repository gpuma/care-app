using System;
using System.Collections.Generic;

namespace CareApp
{
    public interface IBeaconFinder
    {
        void startScanning();
        void stopScanning();
        IEnumerable<String> getBeacons();
    }
}
