using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviourPunCallbacks
{
    public static GameSystem Instance {get; private set;}

    [SerializeField] public Route currentRoute;
    [SerializeField] private DiceScript _dice;
    public List<Renderer> coloredTiles = new List<Renderer>();
    //[SerializeField] private PlayerPiece[] playersPieces;
    [SerializeField] private QuizzManagement _quizzManagement;
    
    private int maxGreen = 5;
    private int maxYellow = 5;
    private int maxRed = 5;

    #region Player Stuff
    private int _playerColor = 0;
    private int _playersInGame = 0;
    private List<PlayerPiece> _players;   
    public List<PlayerPiece> Players { get => _players; private set => _players = value; }
    [SerializeField] private string[] _prefabLocation;
    [SerializeField] private Transform[] _spawns;
    public int playerIndexTurn;
        
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
        var playerObject = PhotonNetwork.Instantiate(_prefabLocation[0], _spawns[0].position, Quaternion.identity);
        var player = playerObject.GetComponent<PlayerPiece>();

        player.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
        
    }
    public void CreateQuizzTiles()
    {   
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

    [PunRPC]
    public void NextPlayer()
    {
        playerIndexTurn++;
        if(playerIndexTurn >= 2) playerIndexTurn = 0;
        Debug.Log("Next player: " + playerIndexTurn);
    }

    public void StartQuizz(string dificult)
    {
        Debug.Log("Começo o quizz fml");
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

    public void StartDice()
    {
        Debug.Log("Dado");
    }
}
