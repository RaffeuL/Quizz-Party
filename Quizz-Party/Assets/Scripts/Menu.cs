using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private EntranceRoom _entranceRoom;

    void Start()
    {
        _entranceRoom.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        _entranceRoom.gameObject.SetActive(true);
    }
}
