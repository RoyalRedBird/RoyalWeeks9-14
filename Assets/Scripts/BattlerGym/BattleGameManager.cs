using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleGameManager : MonoBehaviour
{

    public BattleScript p1Script;
    public BattleScript p2Script;

    bool firstPlayerGo = true;

    // Start is called before the first frame update
    void Start()
    {

        p1Script.myGo = true;


    }

    // Update is called once per frame
    void Update()
    {

        SwapPlayer();

    }

    public void ClockOut()
    {

        firstPlayerGo = !firstPlayerGo;
        
    }

    void SwapPlayer()
    {

        if(firstPlayerGo)
        {

            p1Script.myGo = true;
            p2Script.myGo = false;

        }
        else
        {

            p1Script.myGo = false;
            p2Script.myGo = true;

        }

    }

}
