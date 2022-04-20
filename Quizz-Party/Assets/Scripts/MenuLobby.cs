using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLobby : MonoBehaviour
{
    [SerializeField] private Text _playerlist;
    [SerializeField] private Button _startGame;

    public void UpdateListPlayer()
    {
        _playerlist.text = NetworkManager.Instance.GetPlayerList();
        _startGame.interactable = NetworkManager.Instance.MasterClient();
    }

}
