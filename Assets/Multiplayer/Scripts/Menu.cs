using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using ExitGames.Client.Photon;

public class Menu : MonoBehaviourPunCallbacks
{
    private int _maxPlayersPerRoom = 2;

    [SerializeField]private Text LogText;

    public void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonPeer.RegisterType(typeof(int),242, SerializeInt, DeserializeInt);
    }
    public override void OnConnectedToMaster()
    {
        MyPrint("ConnectedToMaster");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        MyPrint($"No clients are waiting for an opponent, create a new room");
    }

    public override void OnJoinedRoom()
    {
        MyPrint("Client succesfully joined a room");
        PhotonNetwork.LoadLevel(1);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = (byte)_maxPlayersPerRoom });

    }
    public void Exit()
    {
        Application.Quit();
    }

    void MyPrint(string text)
    {
        Debug.Log(text);
        LogText.text += "\n\n";
        LogText.text += text;
    }

    public static object DeserializeInt(byte[] data)
    {
        int result = new int();

        result = BitConverter.ToInt32(data, 0);

        return result;
    }

    public static byte[] SerializeInt(object obj)
    {
        int integer = (int)obj;
        byte[] result = new byte[4];

        BitConverter.GetBytes(integer).CopyTo(result, 0);

        return result;
    }
}
