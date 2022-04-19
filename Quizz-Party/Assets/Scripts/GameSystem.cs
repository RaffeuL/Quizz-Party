using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Route currentRoute;

    public List<Renderer> coloredTiles = new List<Renderer>();

    public PlayerPiece[] playersPieces;
    public int playerIndexTurn;
    private int maxGreen = 5;
    private int maxYellow = 5;
    private int maxRed = 5;

    void Start()
    {
        CreateQuizzTiles();
        
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
        if(playerIndexTurn > 6) playerIndexTurn = 0;
    }
}
