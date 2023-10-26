using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Numerics;

public class HandlerClient : MonoBehaviour
{
    public enum ClientEnum
    {
        Register = 1,
        Login = 2,
        Command = 3,
        WebPack = 4,
        Balance = 5,
        Transaction = 6
    }

    public enum ServerEnum
    {
        Register = 1,
        Login = 2,
        Command = 3,
        WebPack = 4,
        Balance = 5,
        Transaction = 6
    }

    public class Packet
    {
        public string packetid;
        public int id;
        public int type;
        public string status;
        public string message;
        public string wo;
    }

    public static Packet handshakepacket = new Packet();

    public class WebPacket : Packet
    {
        public string IpAdress;
        public string PublicWallet;
    }

    public class DataBalance : Packet
    {
        public string IpAdress;
        public string PublicWalletAdress;
        public string BalanceOfEth;
    }
    public class LoginPacket : Packet
    {
        public string IpAdress;
        public string MacAdress;
        public bool islog;
        public bool auth;
    }

    public class Register : Packet
    {
        public string IpAdress;
        public string MacAdress;
    }

    public class TransactionPacket : Packet
    {
        public string TransMessage;
        public object ContractPack;
    }

    static System.Random random = new System.Random();
    public static async Task HandShake(string datahandjson)
    {
        handshakepacket = JsonUtility.FromJson<Packet>(datahandjson);
        switch (handshakepacket.type)
        {
            case (int)ServerEnum.Login:
                GetLoginPacket(JsonUtility.FromJson<LoginPacket>(datahandjson));
                break;
            case (int)ServerEnum.WebPack:
                Get_WebPack(JsonUtility.FromJson<WebPacket>(datahandjson));
                break;
            case (int)ServerEnum.Balance:
                Get_BalancePack(JsonUtility.FromJson<DataBalance>(datahandjson));
                break;
            case (int)ServerEnum.Transaction:
                GetTranspack(JsonUtility.FromJson<TransactionPacket>(datahandjson));
                break;
            default:
                break;
        }
    }
    public static async Task GetLoginPacket(LoginPacket loginPacket)
    {
        if (loginPacket.islog)
        {
            UnityEngine.Debug.Log("Login Data Received from Server: " + loginPacket.message);
        }
        else if (loginPacket.auth)
        {
            UnityEngine.Debug.Log("User Wallet Connected: " + loginPacket.message);
        }
        else
        {
            UnityEngine.Debug.Log("Login Data Received from Server: " + loginPacket.message);
        }
    }
    public static async Task Get_WebPack(WebPacket _Webpack)
    {
        if (_Webpack.PublicWallet != string.Empty)
        {
            Player.Connect = true;
            Player.UserWalletAdress = _Webpack.PublicWallet;
            Player.Auth = true;
            UnityEngine.Debug.Log("User Wallet adress:" + _Webpack.PublicWallet);
            UnityThread.executeInFixedUpdate(() => {
                Lobby.instance.welcomeGameObject.SetActive(false);
                Lobby.instance.Joingameobject.SetActive(true);
            });

        }
    }
    public static async Task Get_BalancePack(DataBalance _Balancepack)
    {
        if (_Balancepack.BalanceOfEth != string.Empty)
        {
            UnityEngine.Debug.Log("User Balance" + _Balancepack.BalanceOfEth);

            Player.UserWalletAdress = _Balancepack.BalanceOfEth;
        }
    }
    public static async Task GetTranspack(TransactionPacket _transpack)
    {
        if (_transpack.TransMessage != string.Empty)
        {
            UnityEngine.Debug.Log("User Transaction status" + _transpack.TransMessage);

            Player.StatusMessage = _transpack.TransMessage;
        }
    }
    public static DataBalance CreateBalancePack(string ip, string walletad)
    {
        DataBalance packetbalance = new DataBalance();
        packetbalance.packetid = UnityEngine.Random.Range(0, 99999999).ToString();
        packetbalance.type = (int)ClientEnum.Balance;
        packetbalance.PublicWalletAdress = walletad;
        return packetbalance;
    }

    public static LoginPacket CreateLoginPack(string ip, string _MacAdress, bool durum, bool auth)
    {
        LoginPacket packetlogin = new LoginPacket();
        packetlogin.packetid = UnityEngine.Random.Range(0, 99999999).ToString();
        packetlogin.IpAdress = ip;
        packetlogin.type = (int)ClientEnum.Login;
        packetlogin.MacAdress = _MacAdress;
        packetlogin.islog = durum;
        packetlogin.wo = "UnityGame";
        return packetlogin;
    }
    public static TransactionPacket CreateTransactionPack(BigInteger amount, string receiptAddress, string senderAddress)
    {
        TransactionPacket packetTransaction = new TransactionPacket();
        packetTransaction.packetid = random.Next(0, 99999999).ToString();
        packetTransaction.ContractPack = new string[] { receiptAddress, senderAddress };
        packetTransaction.type = (int)ClientEnum.Transaction;
        return packetTransaction;
    }
}