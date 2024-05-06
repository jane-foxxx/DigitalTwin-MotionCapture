using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Votanic.vXR.vGear.Networking;

public class vGearNetworkTemp : MonoBehaviour
{
    public vGear_Networking networking;
    public InputField ip;
    public InputField port;

    // Start is called before the first frame update
    void Start()
    {
        if (!networking) networking = FindObjectOfType<vGear_Networking>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Host()
    {
        if (!networking) return;
        int po = !port || string.IsNullOrEmpty(port.text) ? 7777 : int.Parse(port.text);
        networking.port = po;
        networking.uNetManager.networkPort = po;
        networking.Host();
    }

    public void Join()
    {
        if (!networking) return;
        string address = !ip || string.IsNullOrEmpty(ip.text) ? "localhost" : ip.text;
        int po = !port || string.IsNullOrEmpty(port.text) ? 7777 : int.Parse(port.text);
        networking.Connect(address, po);
    }
}
