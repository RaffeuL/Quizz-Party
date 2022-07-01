using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ItensManagement : MonoBehaviourPunCallbacks
{    
    [SerializeField] ItemButton doubleDice;
    [SerializeField] ItemButton cursedDice;

    public void UseDoubleDice()
    {
        if(doubleDice.itemQuantity > 0)
        {
            PlayerPiece.me.hasDoubleDice = true;
            doubleDice.itemQuantity--;
            doubleDice.itemQuantityText.text = doubleDice.itemQuantity.ToString();
            PlayerPiece.me.CallInventory();
            GameSystem.Instance.UseItemWarning(doubleDice.itemName);
        }
    }

    public void UseCursedDice()
    {
        if(cursedDice.itemQuantity > 0)
        {
            var playerIndex = Random.Range(1, PhotonNetwork.PlayerList.Length);
            var playerName = GameSystem.Instance.Players[playerIndex].playerName;
            Debug.LogError("Jogador escolhido: " + playerName);
            EventsManagement.Instance.photonView.RPC("CursePlayer", RpcTarget.All, playerName);
            cursedDice.itemQuantity--;
            cursedDice.itemQuantityText.text = cursedDice.itemQuantity.ToString();
            PlayerPiece.me.CallInventory();
        }
    }
    
}
