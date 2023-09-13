using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Availiable,
    Current,
    Completed
}

public class MazeNode : MonoBehaviour
{
    [SerializeField] GameObject[] walls;
    [SerializeField] MeshRenderer floor;

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
    }
    public void setState(NodeState state)
    {
        switch(state)
        {
            case NodeState.Availiable:
                floor.material.color = Color.white;
                break;
            case NodeState.Current:
                floor.material.color = Color.yellow;
                break;  
            case NodeState.Completed:
                floor.material.color = Color.blue;
                break;
        }
    }
}