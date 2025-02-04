using System;
using System.IO;
using System.Threading;
using Gurux.DLMS;
using Gurux.DLMS.Objects;
using Gurux.DLMS.Plc;
using Gurux.DLMS.Enums;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Ports;
using InTheHand.Net.Sockets;

namespace EnergoConector
{
    internal class TradeClass
    {
        string btDeviceAddress = "XX:XX:XX:XX:XX:XX";  // Укажите MAC-адрес вашего Bluetooth-устройства
        string btPortName = "COMx";  // Укажите порт Bluetooth, который используется (например, COM3)

        //public void SerachBLE() {
        //    BluetoothDeviceInfo device = null;
        //    foreach (BluetoothDeviceInfo dev in )
        //    {
        //        if (dev.DeviceAddress.ToString() == btDeviceAddress)
        //        {
        //            device = dev;
        //            break;
        //        }
        //        if (device == null)
        //        {
        //            Console.WriteLine("Устройство не найдено.");
        //            return;
        //        }

        //    }
        //}

    }
          
    
    
}
