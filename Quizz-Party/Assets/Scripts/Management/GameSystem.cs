using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviourPunCallbacks
{
    public static GameSystem Instance {get; private set;} 

    #region Object Management
    [SerializeField] public Route currentRoute;
    [SerializeField] private DiceScript _dice;
    [SerializeField] private Button _startGame;
    [SerializeField] private QuizzManagement _quizzManagement;
    
    #endregion

    #region Lists
    private List<PlayerPiece> _players;   
    public List<PlayerPiece> Players { get => _players; private set => _players = value; }
    public Transform[] Spawns { get => _spawns; private set => _spawns = value; }
    [SerializeField] public Color[] playerColors;
    public List<Renderer> coloredTiles = new List<Renderer>();
    #endregion

    #region Players Stuff
    [SerializeField] private string _prefabLocation;
    [SerializeField] private Transform[] _spawns;
    [SerializeField] public GameObject playerInventory;
    private int _playersInGame = 0;
    public int playerIndexTurn = 0;
    public Player activePlayer = null;
    public Text playerTurnText;
    #endregion
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _players = new List<PlayerPiece>();
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
        
        CreateQuizzTiles();  
        _quizzManagement.gameObject.SetActive(false);
        playerInventory.SetActive(false);
    }

    private void Update()
    {
        if(activePlayer != null)
        {
            playerTurnText.text = activePlayer.NickName;
        }
        
    }

    [PunRPC]
    private void AddPlayer()
    {
        _playersInGame++;
        if( _playersInGame == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }

    private void CreatePlayer()
    {
        var playerObject = PhotonNetwork.Instantiate(_prefabLocation, _spawns[0].position, Quaternion.identity);
        var player = playerObject.GetComponent<PlayerPiece>();

        player.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);  
    }
    

    public void StartGame()
    {
        photonView.RPC("StartGameMultiplayer", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void StartGameMultiplayer()
    {
        playerIndexTurn = 0;
        activePlayer = PhotonNetwork.PlayerList[playerIndexTurn];
        _startGame.gameObject.SetActive(false);
    }

    public void StartDice()
    {
        //_dice.RollTheDice();
        //_dice.photonView.RPC("RollTheDice", RpcTarget.All);
    }

    [PunRPC]
    public void NextPlayer()
    {
        activePlayer = null;
        playerIndexTurn++;
        if(playerIndexTurn >= PhotonNetwork.PlayerList.Length) playerIndexTurn = 0;
        activePlayer = PhotonNetwork.PlayerList[playerIndexTurn];
    }


    [PunRPC]
    public void UpdadeDiceUI(int number)
    {
        DiceNumberTextScript.diceNumber = number;
    }

    #region QuizzFunctions
    public void CreateQuizzTiles()
    {   
        var maxGreen = 5;
        var maxYellow = 5;
        var maxRed = 5;

        coloredTiles.Clear();
        int tilesCount = 0;
        int tileIndex;
        int maxTiles =  currentRoute.childTileColorList.Count;
        
        //Fill Green tiles
        while(tilesCount < maxGreen)
        {
            tileIndex = Random.Range(0, maxTiles);
            if(!coloredTiles.Contains(currentRoute.childTileColorList[tileIndex]))
            {
                currentRoute.childTileColorList[tileIndex].material.color = Color.green;
                coloredTiles.Add(currentRoute.childTileColorList[tileIndex]);
                tilesCount++;
            }
        }
        //Fill Yellow tiles
        tilesCount = 0;
        while(tilesCount < maxYellow)
        {
            tileIndex = Random.Range(0, maxTiles);
            if(!coloredTiles.Contains(currentRoute.childTileColorList[tileIndex]))
            {
                currentRoute.childTileColorList[tileIndex].material.color = Color.yellow;
                coloredTiles.Add(currentRoute.childTileColorList[tileIndex]);
                tilesCount++;
            }
        }

        //Fill Red tiles
        tilesCount = 0;
        while(tilesCount < maxRed)
        {
            tileIndex = Random.Range(0, maxTiles);
            if(!coloredTiles.Contains(currentRoute.childTileColorList[tileIndex]))
            {
                currentRoute.childTileColorList[tileIndex].material.color = Color.red;
                coloredTiles.Add(currentRoute.childTileColorList[tileIndex]);
                tilesCount++;
            }
        }
    }

    public void StartQuizz(string dificult)
    {
        _quizzManagement.gameObject.SetActive(true);
        switch (dificult)
        {
            case "Fácil":
                _quizzManagement.GetEasyRandomQuestion();
            break;
            case "Média":
                _quizzManagement.GetMediumRandomQuestion();
            break;
            case "Difícil":
                _quizzManagement.GetHardRandomQuestion();
            break;
        }
        _quizzManagement.ShowQuizz();
    }

    public void EndQuizz()
    {
        DisableQuizz();
        photonView.RPC("NextPlayer", RpcTarget.All);
    }

    public void DisableQuizz()
    {
        PlayerPiece.me.onQuizz = false;
        PlayerPiece.me.answeredRight = false;
        _quizzManagement.gameObject.SetActive(false);
    }
          
    #endregion
}
