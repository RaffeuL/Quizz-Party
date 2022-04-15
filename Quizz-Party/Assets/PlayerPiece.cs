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
            CheckTile();
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
        while(steps > 0)
        {
            Vector3 nextPos = currentRoute.childTileTransformList[routePosition + 1].position;
            while(MoveToNextTile(nextPos)){yield return null;}

            yield return new WaitForSeconds(0.1f);
            steps--;
            routePosition++;
        }
        
        isMoving = false;
    }

    bool MoveToNextTile(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }
    
    void CheckTile()
    {
        Color tileColor = currentRoute.childTileColorList[routePosition].material.color;
    
        //Pergunta Fácil
        if(tileColor == Color.green)
        {
            QuizzManagement.GetEasyRandomQuestion();
            QuizzManagement.ShowQuizz();
        }

        //Pergunta Média
        if(tileColor == Color.yellow)
        {
            QuizzManagement.GetMediumRandomQuestion();
            QuizzManagement.ShowQuizz();
        }

        //Pergunta Dificil
        if(tileColor == Color.red)
        {
            QuizzManagement.GetHardRandomQuestion();
            QuizzManagement.ShowQuizz();
        }
        
    }
}
