using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childObjects;
    public List<Transform> childTileList = new List<Transform>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        FillNodes();

        for (int i = 0; i < childTileList.Count; i++)
        {
             
            Vector3 currentPos = childTileList[i].position;
            if(i > 0)
            {
                Vector3 prevPos = childTileList[i - 1].position;
                Gizmos.DrawLine(prevPos, currentPos);
            }
        }
    }

    void FillNodes()
    {
        childTileList.Clear();

        childObjects = GetComponentsInChildren<Transform>();

        foreach (Transform child in childObjects)
        {
            if(child != this.transform)
            {
                childTileList.Add(child);
            }
        }
    }
}
