using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MenuLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text _playerlist;
    [SerializeField] private Button _startGame;

    [PunRPC]
    public void UpdatePlayerList()
    {
        _playerlist.text = NetworkManager.Instance.GetPlayerList();
        _startGame.interactable = NetworkManager.Instance.MasterClient();
    }

}
