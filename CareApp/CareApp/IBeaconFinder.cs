using System;
using System.Collections.Generic;
using CareApp.Models;

namespace CareApp
{
    public interface IBeaconFinder
    {
        void startScanning();
        void stopScanning();
        IEnumerable<Beacon> getBeacons();
    }
}
