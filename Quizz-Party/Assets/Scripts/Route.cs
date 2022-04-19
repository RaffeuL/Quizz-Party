using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    Renderer[] tilesRender;
    public List<Transform> childTileTransformList = new List<Transform>();

    public List<Renderer> childTileColorList = new List<Renderer>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        FillNodes();

        for (int i = 0; i < childTileTransformList.Count; i++)
        {
             
            Vector3 currentPos = childTileTransformList[i].position;
            if(i > 0)
            {
                Vector3 prevPos = childTileTransformList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        //Fill Nodes with Transform typeof objects
        childTileTransformList.Clear();
        childObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in childObjects)
        {
            if(child != this.transform)
            {
                childTileTransformList.Add(child);
            }
        }

        //Fill Nodes with Renderer typeof objects
        childTileColorList.Clear();
        tilesRender = GetComponentsInChildren<Renderer>();
        
        foreach (Renderer child in tilesRender)
        {
            childTileColorList.Add(child);
        }
    }
}
