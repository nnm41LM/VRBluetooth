using System.Collections;
using System.Collections.Generic;
using UnityEngine.Android;
using UnityEngine;

// MUST: アタッチするゲームオブジェクトの名前を"BluetoothManager"にすること
public class BlueUnityManager : MonoBehaviour
{
    private string _receivedData = "";
    public string ReceivedData
    {
        get { return _receivedData; }
    }

    private string _deviceAddress = "";

    private bool _isConnected;

    private static AndroidJavaClass unity3dbluetoothplugin;
    private static AndroidJavaObject BluetoothConnector;
    void Start()
    {
        InitBluetooth();
        _isConnected = false;
    }

    private void OnDisable()
    {
        StopConnection();
    }

    // creating an instance of the bluetooth class from the plugin 
    private void InitBluetooth()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        // Check BT and location permissions
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)
            || !Permission.HasUserAuthorizedPermission(Permission.FineLocation)
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADMIN")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_ADVERTISE")
            || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT"))
        {

            Permission.RequestUserPermissions(new string[] {
                        Permission.CoarseLocation,
                            Permission.FineLocation,
                            "android.permission.BLUETOOTH_ADMIN",
                            "android.permission.BLUETOOTH",
                            "android.permission.BLUETOOTH_SCAN",
                            "android.permission.BLUETOOTH_ADVERTISE",
                             "android.permission.BLUETOOTH_CONNECT"
                    });

        }

        unity3dbluetoothplugin = new AndroidJavaClass("com.example.unity3dbluetoothplugin.BluetoothConnector");
        BluetoothConnector = unity3dbluetoothplugin.CallStatic<AndroidJavaObject>("getInstance");
    }

    // Start device scan
    public void StartScanDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        BluetoothConnector.CallStatic("StartScanDevices");
    }

    // Stop device scan
    public void StopScanDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        BluetoothConnector.CallStatic("StopScanDevices");
    }



    public string[] GetPairedDevices()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            Debug.LogWarning("Platform is not Android");
            return null;
        }

        string[] data = BluetoothConnector.CallStatic<string[]>("GetPairedDevices");
        return data;
    }

    public void StartConnection(string addressValue)
    {
        _deviceAddress = addressValue;
        StartConnection();
    }

    // Start BT connect using device MAC address "deviceAdd"
    public void StartConnection()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        BluetoothConnector.CallStatic("StartConnection", _deviceAddress.ToUpper());
    }

    // Stop BT connetion
    public void StopConnection()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        if (_isConnected)
            BluetoothConnector.CallStatic("StopConnection");
    }



    public void WriteData(string inputText)
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        if (_isConnected)
            BluetoothConnector.CallStatic("WriteData", inputText);
    }

    // Function to display an Android Toast message
    public void Toast(string data)
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

        BluetoothConnector.CallStatic("Toast", data);
    }

    // This function will be called by Java class to update the scan status,
    // DO NOT CHANGE ITS NAME OR IT WILL NOT BE FOUND BY THE JAVA CLASS
    public void ScanStatus(string status)
    {
        Toast("Scan Status: " + status);
    }

    // This function will be called by Java class whenever a new device is found,
    // and delivers the new devices as a string data="MAC+NAME"
    // DO NOT CHANGE ITS NAME OR IT WILL NOT BE FOUND BY THE JAVA CLASS
    public void NewDeviceFound(string data)
    {
        Debug.Log($"NEW!: {data}");
    }

    // This function will be called by Java class to send Log messages,
    // DO NOT CHANGE ITS NAME OR IT WILL NOT BE FOUND BY THE JAVA CLASS
    public void ReadLog(string data)
    {
        Debug.Log(data);
    }

    // This function will be called by Java class to update BT connection status,
    // DO NOT CHANGE ITS NAME OR IT WILL NOT BE FOUND BY THE JAVA CLASS
    public void ConnectionStatus(string status)
    {
        Toast("Connection Status: " + status);
        _isConnected = status == "connected";
    }

    // This function will be called by Java class whenever BT data is received,
    // DO NOT CHANGE ITS NAME OR IT WILL NOT BE FOUND BY THE JAVA CLASS
    public void ReadData(string data)
    {
        Debug.Log("BT Stream: " + data);
        _receivedData = data;
    }

}
