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
            Debug.LogError("O player " + GameSystem.Instance.activePlayer.NickName + " usou o " + doubleDice.itemName);
            doubleDice.itemQuantity--;
            doubleDice.itemQuantityText.text = doubleDice.itemQuantity.ToString();
        }
    }
    

}
