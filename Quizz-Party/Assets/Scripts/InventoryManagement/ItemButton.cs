using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    [SerializeField] public Button item;
    [SerializeField] public Text itemName;
    [SerializeField] public Text itemQuantityText;

    public int itemQuantity = 2;

    
}
