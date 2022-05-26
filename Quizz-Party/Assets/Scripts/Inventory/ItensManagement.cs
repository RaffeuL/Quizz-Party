using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItensManagement : MonoBehaviour
{    [SerializeField] ItemButton doubleDice;

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

    
}
