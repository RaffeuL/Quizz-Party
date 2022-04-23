using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance {get; private set;}
    [SerializeField] private Route currentRoute;
    public List<Renderer> coloredTiles = new List<Renderer>();
    [SerializeField] private PlayerPiece[] playersPieces;
    [SerializeField] private QuizzManagement _quizzManagement;
    public int playerIndexTurn;
    private int maxGreen = 5;
    private int maxYellow = 5;
    private int maxRed = 5;

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
    void Start()
    {
        CreateQuizzTiles();
        _quizzManagement.gameObject.SetActive(false);
        
    }
    // Update is called once per frame
    void Update()
    {
        
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
}
