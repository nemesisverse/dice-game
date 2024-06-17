using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instanse;
    [System.Serializable]
    public class Entity
    {
        public string playername;
        public Stone[] mystones;
        public bool hasturn;
        public enum playertypes
        {
            human,cpu,noplayer
        }

        public playertypes playertype;

        public bool haswon;

    }

    public List<Entity> playerlist = new List<Entity>();

    public enum states
    {
        Waiting,
        Roll_Dice,
        Switch_Player
    }



    public states state;


    public int active_player;
    bool switch_player;


    private void Awake()
    {
        instanse = this;
    }


    private void Update()
    {

        if (playerlist[active_player].playertype == Entity.playertypes.cpu)
        {
            switch (state)
            {
                case states.Roll_Dice:
                    {
                        StartCoroutine(RollDiceDelay());
                        state = states.Waiting;
                    }
                    break;

                case states.Waiting:
                    {

                    }
                    break;

                case states.Switch_Player:
                    {

                    }
                    break;
            }


           
        }
        
    }


    void RollDice()
    {
        int diceNumber = 6;//Random.Range(1, 7);
        if(diceNumber == 6)
        {
            CheckStartNode(diceNumber);
;            //check the  start node
        }

        if(diceNumber < 6)
        {
            //check for kick
        }


        Debug.Log("dicenumber" + diceNumber);



    }


    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(2);
        RollDice();
    }


    void CheckStartNode(int diceNumber)
    {
        bool startNodeFull = false;
        for(int i = 0; i < playerlist[active_player].mystones.Length; i++)
        {
            if (playerlist[active_player].mystones[i].CurrentNode == playerlist[active_player].mystones[i].StartNode)
            {
                startNodeFull = true;
                break;

            }
        }

        if (startNodeFull)
        {
            //Move a stone
        }
        else
        {
            for(int i = 0; i < playerlist[active_player].mystones.Length; i++)
            {
                if (!playerlist[active_player].mystones[i].ReturnIsOut())
                {
                    playerlist[active_player].mystones[i].LeaveBase();
                    state = states.Waiting;
                    return;
                }
            }


            //Move A Stone
            
            
        }
    }


}
