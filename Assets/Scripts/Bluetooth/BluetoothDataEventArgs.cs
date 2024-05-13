using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothDataEventArgs : EventArgs
{
    public string Data { get; }

    public BluetoothDataEventArgs(string data)
    {
        Data = data;
    }
}
