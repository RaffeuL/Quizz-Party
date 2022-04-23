using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
   [SerializeField] private Route currentRoute;

    public static bool canMove = false;

    public int myIndex;

    int routePosition = -1;

    public int steps;

    bool isMoving;

    public bool onQuizz = true;

    [SerializeField] private DiceScript dice;

    void  Update()
    {
        if(myIndex == GameSystem.Instance.playerIndexTurn)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(routePosition != -1) CheckTile();  
                dice.RollTheDice();
            }

            if(canMove)
            {
                if(onQuizz)
                {
                    steps = DiceNumberTextScript.diceNumber;
                    canMove = false;
                    StartCoroutine(Move());
                }
                
            }
        }

    }

    public IEnumerator Move()
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
        //GameSystem.Instance.NextPlayer();
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
            GameSystem.Instance.StartQuizz("Fácil");
            onQuizz = true;
        }

        //Pergunta Média
        if(tileColor == Color.yellow)
        {
            GameSystem.Instance.StartQuizz("Média");
            onQuizz = true;
        }

        //Pergunta Dificil
        if(tileColor == Color.red)
        {
            GameSystem.Instance.StartQuizz("Difícil");
            onQuizz = true;
        }
        
    }
}
