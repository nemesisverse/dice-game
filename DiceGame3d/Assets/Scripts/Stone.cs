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

    int StartNodeIndex;
    bool hasTurn;

    public GameObject Selector;

    int Steps; //dice rolled number
    int downSteps;

    private void Start()
    {
        StartNodeIndex = CommonPath.RequestPosition(StartNode.gameObject.transform);
        CreateFullRoute();
    }

    void CreateFullRoute()
    {
        for(int i = 0; i< CommonPath.ChildNodeList.Count; i++)
        {
            int tempPos = StartNodeIndex + i;
            tempPos %= CommonPath.ChildNodeList.Count; // this will stop the overflow 1,2,3,0,1,2,3 like this

            FullRoute.Add(CommonPath.ChildNodeList[tempPos].GetComponent<Node>());

        }

        for(int i = 0; i < FinalPath.ChildNodeList.Count; i++)
        {
        
            FullRoute.Add(FinalPath.ChildNodeList[i].GetComponent<Node>());
        }
    }

    IEnumerator Move()
    {
        if(isMoving)
        { 
            yield break;
        }

        isMoving = true;

        while(Steps > 0)
        {
            RoutePosition++;

            Vector3 nextPos = FullRoute[RoutePosition].gameObject.transform.position;

            while(MoveToNextNode(nextPos , 8f))
            {
                yield return null;  
            }
            yield return new WaitForSeconds(0.1f);
            Steps--;
            downSteps++;
        }
    }

    bool MoveToNextNode(Vector3 goalpos , float speed)
    {
        return goalpos != (transform.position = Vector3.MoveTowards(transform.position , goalpos , speed*Time.deltaTime));
    }


    public bool ReturnIsOut()
    {
        return isOut;
    }


    public void LeaveBase()
    {
        Steps = 1;
        isOut = true;
        RoutePosition = 0;
        StartCoroutine(MoveOut());
        //start coroutine
    }


    IEnumerator MoveOut()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        while (Steps > 0)
        {
           // RoutePosition++;

            Vector3 nextPos = FullRoute[RoutePosition].gameObject.transform.position;

            while (MoveToNextNode(nextPos, 8f))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            Steps--;
            downSteps++;
        }
    }

}
