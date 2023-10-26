using UnityEngine;
using WebSocketSharp;
using System.Net.NetworkInformation;
using System.Diagnostics;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Text;
using System.Numerics;
public class SocketManager : MonoBehaviour
{
    public static SocketManager instance;
    private WebSocket ws;
    private const string IpifyUrl = "https://api.ipify.org?format=json";

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ConnectToServer()
    {
        ws = new WebSocket("ws://<websocketadress>");
        ws.OnMessage += async (sender, e) =>
        {
            await HandlerClient.HandShake(e.Data);
        };
        ws.OnOpen += (sender, e) =>
        {
            StartCoroutine(GetPublicIPAddress(ipAddress =>
            {
                string macAddress = GetMacAddress();
                SendDataFromJson(JsonUtility.ToJson(HandlerClient.CreateLoginPack(ipAddress, macAddress, false, false)));
            }));
            UnityEngine.Debug.Log("WebSocket connection open");
            string gotowebsiteurl = "";  
            Process.Start(gotowebsiteurl);
        };
        ws.OnClose += (sender, e) =>
        {
            UnityEngine.Debug.Log("WebSocket connection close.");
        };
        ws.Connect();
    }

    public void DisconnectFromServer()
    {
        if (ws != null && ws.ReadyState == WebSocketState.Open)
        {
            ws.Close();
            UnityEngine.Debug.Log("WebSocket connection manuel close.");
        }
    }

    public void OdemeyeYolla(BigInteger amount, string senderadress,string receiptadres)
    {
        SendDataFromJson(JsonUtility.ToJson(HandlerClient.CreateTransactionPack(amount, receiptadres, senderadress))); 
    }

    public void BalanceEth(string walletadress)
    {
        StartCoroutine(GetPublicIPAddress(ipAddress =>
        {
            SendDataFromJson(JsonUtility.ToJson(HandlerClient.CreateBalancePack(ipAddress, walletadress)));
        }));
    }

    public void SendDataFromJson(string sendjson)
    {
        if (ws.ReadyState == WebSocketState.Open)
        {
            byte[] Data = Encoding.UTF8.GetBytes(sendjson);
            ws.Send(Data);
        }
    }

    private void OnDestroy()
    {
        if (ws != null) ws.Close();
    }

    private string GetMacAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        string macAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (!string.IsNullOrEmpty(adapter.GetPhysicalAddress().ToString()))
            {
                macAddress = adapter.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddress;
    }
    private IEnumerator GetPublicIPAddress(Action<string> callback)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(IpifyUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = webRequest.downloadHandler.text;
                IPResponse ipResponse = JsonUtility.FromJson<IPResponse>(jsonResponse);
                callback?.Invoke(ipResponse.ip);
            }
            else
            {
                UnityEngine.Debug.LogError("GetPublicIpAdress Error => " + webRequest.error);
            }
        }
    }
}

[System.Serializable]
public class IPResponse
{
    public string ip;
}