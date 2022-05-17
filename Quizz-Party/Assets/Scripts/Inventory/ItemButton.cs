using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{   [SerializeField] public string itemName;
    [SerializeField] public int itemQuantity; 
    [SerializeField] public Text itemNameText;
    [SerializeField] public Text itemQuantityText;

    void Awake()
    {
        itemNameText.text = itemName;
        itemQuantityText.text = itemQuantity.ToString();
    }
}
