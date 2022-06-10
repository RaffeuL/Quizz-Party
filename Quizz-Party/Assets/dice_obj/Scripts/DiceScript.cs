using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DiceScript : MonoBehaviourPunCallbacks
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
    }

    public void RollTheDice()
    {
            DiceCheckZoneScript.isRolling = true;
            //DiceNumberTextScript.diceNumber = 0;
            float dirX = Random.Range(0, 500);
            float dirY = Random.Range(0, 500);
            float dirZ = Random.Range(0, 500);
            
            transform.position = new Vector3 (2.85f, 5f, 3.15f);
            transform.rotation = Quaternion.identity;
            rb.AddForce( transform.up*500 );
            rb.AddTorque(dirX, dirY, dirZ);
    }
}
