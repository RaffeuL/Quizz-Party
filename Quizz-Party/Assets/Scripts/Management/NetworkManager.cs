using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance {get; private set;}

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangeNick(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }

    public string GetPlayerList()
    {
        var playerList = "";
        foreach (var player in PhotonNetwork.PlayerList)
        {
            playerList += player.NickName + "\n";
        }

        return playerList;
    }

    public bool MasterClient()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexão bem sucedida");
    }

    public void LeftLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    [PunRPC]
    public void StartGame(string  sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
