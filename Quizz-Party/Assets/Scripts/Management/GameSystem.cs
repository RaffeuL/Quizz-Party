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
    //[SerializeField] private DiceScript _dice;
    [SerializeField] private Button _startGame;
    [SerializeField] private QuizzManagement _quizzManagement;
    [SerializeField] private DiceRoller _dice;
    [SerializeField] private GameObject _winnerWarning;
    [SerializeField] private Text _winnerName;

    #endregion

    #region Lists
    private List<PlayerPiece> _players;   
    public List<PlayerPiece> Players { get => _players; private set => _players = value; }
    public Transform[] Spawns { get => _spawns; private set => _spawns = value; }
    [SerializeField] public Color[] playerColors;
    
    [SerializeField] protected Sprite[] DiceSprites;
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
        _winnerWarning.gameObject.SetActive(false);
        playerInventory.SetActive(false);
        _startGame.interactable = NetworkManager.Instance.MasterClient();
    }

    private void Update()
    {
        if(activePlayer != null)
        {
            playerTurnText.text = activePlayer.NickName;
        }
        
    }

    #region Start Game
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
    #endregion

    #region UI Update
    [PunRPC]
    public void UpdadeDiceUI(int number)
    {
        _dice.img.sprite = DiceSprites[number];
    }

    
    public void UseItemWarning(string itemName)
    {
        photonView.RPC("UseItemWarningMultiplayer", RpcTarget.All, itemName);  
    }

    [PunRPC]
    private void UseItemWarningMultiplayer(string itemName)
    {
        Debug.LogError("O player " + activePlayer.NickName + " usou o " + itemName);
    }
    #endregion

    #region Player Management
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

    [PunRPC]
    public void NextPlayer()
    {
        activePlayer = null;
        playerIndexTurn++;
        if(playerIndexTurn >= PhotonNetwork.PlayerList.Length) playerIndexTurn = 0;
        activePlayer = PhotonNetwork.PlayerList[playerIndexTurn];
    }
    
    #endregion

    public void StartDice()
    {
        //_dice.RollTheDice();
        //_dice.photonView.RPC("RollTheDice", RpcTarget.All);
    }

    #region TileEventsFunctions
    public void CreateQuizzTiles()
    {   
        coloredTiles.Clear();
        int tilesCount = 0;
        int tileIndex;
        int maxTiles =  currentRoute.childTileColorList.Count;
        int quizzTiles = Mathf.RoundToInt((maxTiles/2)/3);

        int maxGreen = quizzTiles;
        int maxYellow = quizzTiles;
        int maxRed = quizzTiles;
        int maxEvents = Mathf.RoundToInt((maxTiles)/4);
        
        //Preenche as casas Verdes
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
        //Preenche as casas Amarelas
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

        //Preenche as casas Vermelhas
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

        //Preenche as casas Com Eventos
        tilesCount = 0;
        while(tilesCount < maxEvents)
        {
            tileIndex = Random.Range(0, maxTiles);
            if(!coloredTiles.Contains(currentRoute.childTileColorList[tileIndex]))
            {
                currentRoute.childTileColorList[tileIndex].material.color = Color.cyan;
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
        _quizzManagement.BuildQuizz();
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

    [PunRPC]
    void EndGame(string playerName)
    {
        _winnerWarning.gameObject.SetActive(true);
        _winnerName.text = "Ganhador(a): " + playerName;
    }
}