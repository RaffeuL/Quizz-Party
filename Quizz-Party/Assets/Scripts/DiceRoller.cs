using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public SpriteRenderer rend;
    public Image img;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        img = GetComponent<Image>();
    }
}
