using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public int[] DiceValues;
    public int DiceTotal;

    public int DoubleDice;

    public bool isrolling = false;

    public Sprite[] spriteArray;

    public int Chances = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollTheDice()
    {
        int value;
        DiceTotal = 0;
        for(int i = 0; i < Chances; i++)
        {
            value = Random.Range(1, 7);
            transform.GetComponent<Image>().sprite = spriteArray[value-1];
            DiceTotal += value; 
        }

        //Debug.Log("Valor para andar:" + DiceTotal);
        
    }
}
