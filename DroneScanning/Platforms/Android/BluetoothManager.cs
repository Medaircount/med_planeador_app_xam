using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Bluetooth;
using DroneScanning.Interface;
using Java.Util;
using DroneScanning.Interface;

namespace DroneScanning.Platforms.Android
{
    public class BluetoothManager : IBluetoothManager
    {
        private BluetoothAdapter _bluetoothAdapter;
        private BluetoothSocket _bluetoothSocket;

        public BluetoothManager()
        {
            _bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
        }

        public void ConnectToDevice(BluetoothDevice device)
        {
            _bluetoothSocket = device?.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));
            _bluetoothSocket?.Connect();
        }

        public async Task<string> ReceiveTextAsync()
        {
            try
            {
                var adapter = BluetoothAdapter.DefaultAdapter;
                if (adapter == null)
                    throw new Exception("Bluetooth is not supported on this device.");

                var serverSocket = adapter.ListenUsingRfcommWithServiceRecord("BluetoothApp", UUID.FromString("00001101-0000-1000-8000-00805F9B34FB"));
                var clientSocket = await serverSocket.AcceptAsync();

                byte[] buffer = new byte[1024];
                var bytes = await clientSocket.InputStream.ReadAsync(buffer, 0, buffer.Length);
                return System.Text.Encoding.UTF8.GetString(buffer, 0, bytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving text: {ex.Message}");
                return null;
            }
        }
    }
}
