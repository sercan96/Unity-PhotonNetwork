using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI logText;
    void Start()
    {
        #region Simple network systems
        // SERVER(Hostel) - LOBBY - Room
        // PhotonNetwork.ConnectUsingSettings();
        // PhotonNetwork.JoinLobby();
        // PhotonNetwork.JoinRoom("room name");
        // PhotonNetwork.JoinRandomRoom();
        // PhotonNetwork.CreateRoom("room name", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},
        //     TypedLobby.Default);
        // PhotonNetwork.JoinOrCreateRoom("room name", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},
        //     TypedLobby.Default);
        // PhotonNetwork.LeaveLobby();
        // PhotonNetwork.LeaveRoom();
        #endregion
        
        PhotonNetwork.ConnectUsingSettings();
    }
/*
    public override void OnConnected()  // Oyuncunun bağlanıp bağlanmadığını kontrol etme
    {
        Debug.Log("connection made");
    }
    
    */

    public override void OnDisconnected(DisconnectCause cause)
    {
        WriteLogRecord("Connection lost");
        //PhotonNetwork.ReconnectAndRejoin();
    }

public override void OnConnectedToMaster() // Oyuncunun bağlandıktan sonra işleme hazır olup olmadığını kontrol eder.
    {
        WriteLogRecord("Connect to the Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby() // Lobiye bağlandı mı kontrolü(CALL BACK)
    {
        WriteLogRecord("Connect to the Lobby");
        // if connect to room
        //PhotonNetwork.JoinRoom("room name");
        //PhotonNetwork.JoinRandomRoom();
        
        Debug.Log(PhotonNetwork.InLobby ? "LOBBY!!" : "NOT LOBBY!!"); // InLobby?

        // İf ı create room:
        // PhotonNetwork.CreateRoom("room name", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);
        PhotonNetwork.JoinOrCreateRoom("room name", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);
    }

    public override void OnJoinedRoom() // İf Positive
    {
        WriteLogRecord("Entered the room");
        
        Debug.Log(PhotonNetwork.InRoom ? "InRoom!!" : "NOT InRoom!!"); // InRoom?
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Do not enter the room" + message + " - " + returnCode);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Do not enter the random room" + message + " - " + returnCode);
    }

    public override void OnCreateRoomFailed(short returnCode, string message) // İf negative
    {
        Debug.Log("Cannot connect the room" + message + " - " + returnCode);
    }

    public override void OnLeftLobby()
    {
        WriteLogRecord("Left to the lobby");
    }

    public override void OnLeftRoom()
    {
        WriteLogRecord("Left to the room");
        PhotonNetwork.ReconnectAndRejoin();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PhotonNetwork.LeaveRoom();
        }
        else if(Input.GetKeyDown(KeyCode.B))
        {
            PhotonNetwork.LeaveLobby();
        }

        //Debug.Log(PhotonNetwork.IsConnected ? "CONNECT!!" : "NOT CONNECT!!"); // IsConnect?
        //Debug.Log(PhotonNetwork.InLobby ? "LOBBY!!" : "NOT LOBBY!!"); // InLobby?
        //Debug.Log(PhotonNetwork.InRoom ? "InRoom!!" : "NOT InRoom!!"); // InRoom?
        //Debug.Log(PhotonNetwork.IsMasterClient ? "IsMasterClient!!" : "NOT IsMasterClient!!"); // IsMasterClient?
    }

    private void WriteLogRecord(string name)
    {
        logText.text = name;
    }

    public void Disconnected() // Kullanıcı online oynamak istemiyorsa bu şekilde bağlantısını kesebilir.
    {
        PhotonNetwork.Disconnect();
    }
    
    public void Reconnect() // Kullanıcı online oynamak istemiyorsa bu şekilde bağlantısını kesebilir.
    {
        PhotonNetwork.Reconnect();
    }

    public void GetStatistics()
    {
        PhotonNetwork.NetworkStatisticsEnabled = true;
        Debug.Log(PhotonNetwork.NetworkStatisticsToString());
    }
    
    public void ResetStatistics()
    {
        PhotonNetwork.NetworkStatisticsReset();
    }
    
    public void GetPing()
    {
        WriteLogRecord(PhotonNetwork.GetPing().ToString());
    }
}
