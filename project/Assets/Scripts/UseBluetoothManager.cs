using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UseBluetoothManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _reternedText;

    private BlueUnityManager _blueUnityManager;
    private string _m5StackDeviceAddress = "";


    private void Awake()
    {
        _blueUnityManager = GetComponent<BlueUnityManager>();
    }

    private void Start()
    {
        GetBluetoothDevices(_blueUnityManager);
        ConnectM5Stack(_m5StackDeviceAddress);
    }

    private void Update()
    {
        _reternedText.text = _blueUnityManager.ReceivedData;
    }

    private void ConnectM5Stack(string m5StackDeviceAddress)
    {
        if (m5StackDeviceAddress.Length == 0) return;

        _blueUnityManager.StartConnection(m5StackDeviceAddress);
    }

    private void GetBluetoothDevices(BlueUnityManager blueUnityManager)
    {
        var adresses = blueUnityManager.GetPairedDevices();
        if (adresses is null) return;

        var stringBuilder = new StringBuilder();
        foreach (var ad in adresses)
        {
            stringBuilder.AppendLine(ad.ToString());
        }

        SearchM5StackDevice("Sample", adresses, ref _m5StackDeviceAddress);
    }

    private void SearchM5StackDevice(string searchName, string[] pairedDevices, ref string m5StackAddress)
    {
        foreach (var device in pairedDevices)
        {
            if (device.Contains(searchName))
            {
                var deviceNameData = device.Split("+");
                m5StackAddress = deviceNameData[0];
            }
        }
    }
}
