using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class PlayerPiece : MonoBehaviourPunCallbacks
{
    #region PlayerStuff
    public static PlayerPiece me;
    private Player _photonPlayer;
    private int _id;

    public string playerName;
    private Renderer myRenderer;
    #endregion

    #region QuizzStuff
    public bool answeredRight = false;
    public bool onQuizz = false;
    private int quizzTimer = 0;
    #endregion

    #region Moviment Variables
    private Route currentRoute;
    public bool myTurn = false;
    int routePosition = -1;
    public int steps;
    bool isMoving;

    private int diceTimer;
    private bool isRolling;
    #endregion

    #region InventoryStuff
    public bool canUseItem = true;
    private bool inventoryIsOpen = false;
    public bool hasDoubleDice = false;
    #endregion

    [PunRPC]
    public void Initialize(Player player)
    {
        _photonPlayer = player;
        _id = player.ActorNumber;      
        playerName = player.NickName;
        GameSystem.Instance.Players.Add(this);
        currentRoute = GameSystem.Instance.currentRoute;
        myRenderer = GetComponentInChildren<Renderer>();
        myRenderer.material.color = GameSystem.Instance.playerColors[_id - 1];
        transform.position = GameSystem.Instance.Spawns[_id - 1].position;
        if(_photonPlayer.IsLocal) me = this;
    }
    void  Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        if(onQuizz)
        {
            if(answeredRight)
            {
                GameSystem.Instance.DisableQuizz();
                StartMove(); 
            }
            quizzTimer++;
            //Debug.LogError("Timer : " + quizzTimer);
        }
        if(GameSystem.Instance.activePlayer == _photonPlayer)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(!CheckTile())
                {
                    StartMove();
                }                
            }  

            if(Input.GetKeyDown(KeyCode.S))
            {
                CallInventory();
            }
        }
    }
    public void StartMove()
    {        
        diceTimer = 20;
        StartCoroutine(DiceAnimation());

    }
    public IEnumerator DiceAnimation()
    {
        if(isRolling)
        {
            yield break;
        }

        isRolling = true;
        while(diceTimer > 0)
        {
            steps = Random.Range(1,6);
            GameSystem.Instance.photonView.RPC("UpdadeDiceUI", RpcTarget.All, steps);
            yield return new WaitForSeconds(0.1f);
            diceTimer--;
        }
        isRolling = false;
        if(hasDoubleDice) steps *= 2;
        
        StartCoroutine(Move());  
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
        ResetItensProps();
        GameSystem.Instance.photonView.RPC("NextPlayer", RpcTarget.All);
    }

    bool MoveToNextTile(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 3f * Time.deltaTime));
    }
    
    private bool CheckTile()
    {
        if(routePosition == -1)
        {
            return false;
        }

        Color tileColor = currentRoute.childTileColorList[routePosition].material.color;
        //Verificando casas com Quizz
    
        //Pergunta Fácil
        if(tileColor == Color.green)
        {
            GameSystem.Instance.StartQuizz("Fácil");
            onQuizz = true;
            return true;
        }
        //Pergunta Média
        if(tileColor == Color.yellow)
        {
            GameSystem.Instance.StartQuizz("Média");
            onQuizz = true;
            return true;
        }

        //Pergunta Dificil
        if(tileColor == Color.red)
        {
            GameSystem.Instance.StartQuizz("Difícil");
            onQuizz = true;
            return true;
        }

        //Eventos não-quizz

        return false;    
    }

    public void CallInventory()
    {
        inventoryIsOpen = !inventoryIsOpen;
        GameSystem.Instance.playerInventory.SetActive(inventoryIsOpen);
    }

    public void ResetItensProps()
    {
        canUseItem = true;
        hasDoubleDice = false;
    }

}
