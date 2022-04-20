using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private MenuEntrance _menuEntrance;
    [SerializeField] private MenuLobby _menuLobby;

    void Start()
    {
        _menuEntrance.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        _menuEntrance.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        ChangeMenu(_menuLobby.gameObject);
        _menuLobby.UpdateListPlayer();
    }

    public void ChangeMenu(GameObject menu)
    {
        _menuEntrance.gameObject.SetActive(false);
        _menuLobby.gameObject.SetActive(false);

        menu.SetActive(true);
    }
}
