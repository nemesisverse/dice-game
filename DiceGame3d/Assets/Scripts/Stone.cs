using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public Path CommonPath;
    public Path FinalPath;

    public List<Node> FullRoute = new List<Node>();

    public Node StartNode;
    public Node BaseNode;
    public Node CurrentNode;
    public Node GoalNode;


    int RoutePosition;
    public bool isOut;
    bool isMoving;


    bool hasTurn;

    public GameObject Selector;

}
