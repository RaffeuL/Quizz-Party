using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DiceCheckZoneScript : MonoBehaviourPunCallbacks
{

    Vector3 diceVelocity;

    void FixedUpdate()
    {
        diceVelocity = DiceScript.diceVelocity;
    }

    public static bool isRolling = false;
    void OnTriggerStay(Collider col)
    {

        if(diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch(col.gameObject.name)
            {
                case "Side1":
                    DiceNumberTextScript.diceNumber = 6;

                    break;
                case "Side2":
                    DiceNumberTextScript.diceNumber = 5;

                    break;
                case "Side3":
                    DiceNumberTextScript.diceNumber = 4;

                    break;
                case "Side4":
                    DiceNumberTextScript.diceNumber = 3;

                    break;
                case "Side5":
                    DiceNumberTextScript.diceNumber = 2;

                    break;
                case "Side6":
                    DiceNumberTextScript.diceNumber = 1;
                    
                    break;
            }
            if(isRolling)
            {
                isRolling = false;
                GameSystem.Instance.photonView.RPC("StartPlayerMove", RpcTarget.All);
            }
        }
        
    }
    
}
