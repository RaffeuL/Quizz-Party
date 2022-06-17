using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EventsManagement : MonoBehaviourPun
{

    public static EventsManagement Instance {get; private set;}
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
            PlayerPiece.me.StartReverseMove();
            break;
            case 1:
            Debug.Log("Perder a vez");
            break;
            case 2:
            Debug.Log("Não sei");
            break;
        }
    }

}

