using System.Collections.Generic;
using UnityEngine;

public class GridPanelUI : MonoBehaviour
{
//    public Transform[] grids;
    public List<Transform> gridList;
    
    public Transform GetEmptyGrid()
    {
        for (int i = 0; i < gridList.Count; i++)
        {
            if (gridList[i].childCount == 0)
            {
                return gridList[i];
            }
        }

        return null;
    }

    public void MoveGridToLast(GameObject gridToBeMoved)
    {
        gridList.Remove(gridToBeMoved.transform);
        gridList.Add(gridToBeMoved.transform);
    }
}
