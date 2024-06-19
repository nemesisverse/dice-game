using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stone : MonoBehaviour
{
    public int StoneID;
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
    int donesteps;

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            Steps = Random.Range(1, 7);
            if(donesteps + Steps < FullRoute.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                Debug.Log("Number is too high");
            }
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
            donesteps++;
            
        }
       isMoving = false;
       CurrentNode.stone = null; 
       CurrentNode.isTaken = false;
       
       GoalNode.stone = this;   
       GoalNode.isTaken = true;

        GameManager.instanse.state = GameManager.states.Roll_Dice;

        isMoving = false;
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
           //RoutePosition++;

            Vector3 nextPos = FullRoute[RoutePosition].gameObject.transform.position;

            while (MoveToNextNode(nextPos, 8f))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            Steps--;
            donesteps++;
        }

        GoalNode = FullRoute[RoutePosition];
        if(GoalNode.isTaken)
        {
          //Return to start base node
        }



        //CurrentNode.stone = null;
        //CurrentNode.isTaken = false;

        GoalNode.stone = this;
        GoalNode.isTaken = true;

        CurrentNode = GoalNode;
        GoalNode = null;

        GameManager.instanse.state = GameManager.states.Roll_Dice;
        isMoving = false;
    }

    public bool CheckPossibleMove(int diceNumber)
    {
        int tempPos= RoutePosition  + diceNumber;
        if(tempPos >= FullRoute.Count)
        {
            return false;

        }

        return !FullRoute[tempPos].isTaken;
    }

    public bool CheckPossibleKick(int StoneID , int diceNumber)
    {
        int tempPos = RoutePosition + diceNumber;
        if (tempPos >= FullRoute.Count)
        {
            return false;
        }

        if (FullRoute[tempPos].isTaken)
        {
            if(StoneID == FullRoute[tempPos].stone.StoneID)
            {
                return false;
            }

            return true;
        }

        return false;
    }

    public void StartTheMove(int diceNumber)
    {
        Steps = diceNumber;
        StartCoroutine(Move());
    }

    





}
