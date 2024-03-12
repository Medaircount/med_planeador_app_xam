using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneScanning.Interface
{
    public interface IBluetoothManager
    {
        Task<string> ReceiveTextAsync();
    }
}
