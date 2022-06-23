using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItensManagement : MonoBehaviour
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
            PlayerPiece.me.hasCursedDice = true;
            cursedDice.itemQuantity--;
            cursedDice.itemQuantityText.text = cursedDice.itemQuantity.ToString();
            PlayerPiece.me.CallInventory();
            GameSystem.Instance.UseItemWarning(cursedDice.itemName);
        }
    }

    
}
