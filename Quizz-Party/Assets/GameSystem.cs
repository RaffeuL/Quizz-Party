using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Route route;
    [SerializeField] private Renderer myObject;
    Renderer[] tilesRender;

    public List<Renderer> childTileList = new List<Renderer>();

    private int tileIndex;
    public List<Renderer> coloredTiles = new List<Renderer>();

    void Start()
    {
        childTileList.Clear();
        tilesRender = route.GetComponentsInChildren<Renderer>();
        
        foreach (Renderer child in tilesRender)
        {
            childTileList.Add(child);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateQuizzTiles()
    {
        int tilesGreen = 0;
        while(tilesGreen < 5)
        {
            tileIndex = Random.Range(0,childTileList.Count);
            if(!coloredTiles.Contains(childTileList[tileIndex]))
            {
                childTileList[tileIndex].material.color = Color.green;
                coloredTiles.Add(childTileList[tileIndex]);
                tilesGreen++;
            }
        }
    }
}
