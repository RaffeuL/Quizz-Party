using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public Route currentRoute;

    public static bool canMove = false;

    int routePosition;

    public int steps;

    bool isMoving;

    public DiceScript dice;

    void  Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dice.RollTheDice();
        }

        if(canMove)
        {
            steps = DiceNumberTextScript.diceNumber;
            canMove = false;
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        if(isMoving)
        {
            yield break;
        }
        isMoving = true;
        Debug.Log("olha quantos passos tem mano: " + steps);
        while(steps > 0)
        {
            Vector3 nextPos = currentRoute.childTileList[routePosition + 1].position;
            while(MoveToNextTile(nextPos)){yield return null;}

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePosition++;
        }
        Debug.Log("Cabo de andar");
        
        isMoving = false;
        //DiceCheckZoneScript.isFrozzen = false;
    }

    bool MoveToNextTile(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }
    
}
