using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntrance : MonoBehaviour
{
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _roomName;

    public void CreateRoom()
    {
        NetworkManager.Instance.CreateRoom(_roomName.text);
        NetworkManager.Instance.ChangeNick(_playerName.text);
    }

    public void JoinRoom()
    {
        NetworkManager.Instance.JoinRoom(_roomName.text);
        NetworkManager.Instance.ChangeNick(_playerName.text);
    }

}
