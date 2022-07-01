using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EventsManagement : MonoBehaviourPunCallbacks
{
    public static EventsManagement Instance {get; private set;}
    [SerializeField] public GameObject MenuPlayerList;
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

    public void StartEvent(int eventId)
    {
        
        switch (eventId)
        {
            case 0: //Voltar casas
                Debug.Log("Dado Reverso");
                PlayerPiece.me.StartReverseMove();
            break;

            case 1:
                Debug.Log("Perdeu a Vez");
                PlayerPiece.me.isMoving = false;
                PlayerPiece.me.ResetItensProps();
                GameSystem.Instance.photonView.RPC("NextPlayer", RpcTarget.All);
            break;

            case 2:
                Debug.Log("Não sei");
            break;
        }
    }

    [PunRPC]
    void CursePlayer(string playerName)
    {
        Debug.LogError("Dado amaldiçoado no player: " + playerName);
        if(PlayerPiece.me.playerName == playerName) PlayerPiece.me.hasCursedDice = true;
    }


}

