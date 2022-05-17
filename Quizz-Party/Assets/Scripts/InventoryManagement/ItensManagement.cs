using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItensManagement : MonoBehaviour
{
    #region ItensButtons
    [SerializeField] protected ItemButton doubleDice;
    #endregion

    public void UseItem(ItemButton item)
    {
        if(item.itemQuantity > 0)
        {
            Debug.LogError("O jogador " + GameSystem.Instance.activePlayer.NickName + "Usou " + item.itemName.text);
        }

        item.itemQuantity--;
        item.itemQuantityText.text = item.itemQuantity.ToString();
    }
    

}
