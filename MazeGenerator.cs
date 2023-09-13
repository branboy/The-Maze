using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Xsl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;
    public GameObject Player;
    public GameObject dummy;
    public GameObject ghost;
    public GameObject ghost2;
    public GameObject key;
    float nodeSize = 15f;

    private void Start()
    {
        generateMazeInstant(mazeSize);
        //StartCoroutine(generateMaze(mazeSize));
    }

    IEnumerator generateMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();
        //Create nodes
        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0;y<size.y;y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
                MazeNode node = Instantiate(nodePrefab,nodePos, Quaternion.identity,transform);
                nodes.Add(node);
                yield return null;
            }
        }
        //Choose Starting node
        List<MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPath[0].setState(NodeState.Current);

        while(completedNodes.Count<nodes.Count)
        {
            //Check nodes next to curremt
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if(currentNodeX<size.x-1)
            {
                //check right of the node
                if (!completedNodes.Contains(nodes[currentNodeIndex+size.y]) && !currentPath.Contains(nodes[currentNodeIndex+size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex+size.y);
                }
            }

            if(currentNodeX>0)
            {
                //check left of the node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }

            if(currentNodeY<size.y-1)
            {
                //check above the node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (currentNodeY > 0)
            {
                //check below the node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            if(possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];
                switch(possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }
                currentPath.Add(chosenNode);
                chosenNode.setState(NodeState.Current);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count-1]);
                currentPath[currentPath.Count-1].setState(NodeState.Completed);
                currentPath.RemoveAt(currentPath.Count-1);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    void generateMazeInstant(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();
        //Create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f))*nodeSize;
                MazeNode node = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(node);
            }
        }
        //Instantiate(dummy, new Vector3(nodes[nodes.Count-1].transform.position.x+5, nodes[nodes.Count-1].transform.position.y + 1, nodes[nodes.Count-1].transform.position.z), Quaternion.identity);
        Player.transform.position = new Vector3(nodes[0].transform.position.x, nodes[0].transform.position.y + 5, nodes[0].transform.position.z);
        //Instantiate(Player, new Vector3(nodes[0].transform.position.x, nodes[0].transform.position.y + 1, nodes[0].transform.position.z), Quaternion.identity);
        dummy.transform.position = new Vector3(nodes[Random.Range(0, nodes.Count)].transform.position.x, nodes[Random.Range(0, nodes.Count)].transform.position.y + 5, nodes[Random.Range(0, nodes.Count)].transform.position.z);
        ghost.transform.position = new Vector3(nodes[Random.Range(0,nodes.Count)].transform.position.x, nodes[Random.Range(0,nodes.Count)].transform.position.y + 5, nodes[Random.Range(0,nodes.Count)].transform.position.z);
        ghost2.transform.position = new Vector3(nodes[Random.Range(0, nodes.Count)].transform.position.x, nodes[Random.Range(0, nodes.Count)].transform.position.y + 5, nodes[Random.Range(0, nodes.Count)].transform.position.z);
        key.transform.position = new Vector3(nodes[Random.Range(0, nodes.Count)].transform.position.x, nodes[Random.Range(0, nodes.Count)].transform.position.y + 1, nodes[Random.Range(0, nodes.Count)].transform.position.z);
        for (int i =0;i<7;i++)
        {
            Instantiate(key, new Vector3(nodes[Random.Range(0, nodes.Count)].transform.position.x, nodes[Random.Range(0, nodes.Count)].transform.position.y + 1, nodes[Random.Range(0, nodes.Count)].transform.position.z), Quaternion.identity);
        }

        //Choose Starting node
        List <MazeNode> currentPath = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);

        while (completedNodes.Count < nodes.Count)
        {
            //Check nodes next to curremt
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            if (currentNodeX < size.x - 1)
            {
                //check right of the node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPath.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                }
            }

            if (currentNodeX > 0)
            {
                //check left of the node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPath.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }

            if (currentNodeY < size.y - 1)
            {
                //check above the node
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPath.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                }
            }

            if (currentNodeY > 0)
            {
                //check below the node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPath.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }

            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];
                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }
                currentPath.Add(chosenNode);
            }
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }



    //private void InitializeTreasure()
    //{
     //   int lastIndex = _nodes.Count - 1;
     //   Instantiate(_treasure, _nodes[lastIndex].transform.position, Quaternion.identity);
    //}
}
