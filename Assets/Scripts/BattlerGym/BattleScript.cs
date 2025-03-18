using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScript : MonoBehaviour
{

    [SerializeField] Button attackButton;
    [SerializeField] BattleGameManager gameManager;

    float homieRotate = 0;
    public Transform homieTransform;

    public bool myGo = false;
    bool attackInProgress = false;

    float t;
    float maxT = 1;

    // Start is called before the first frame update
    void Start()
    {

        homieTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {

        if (myGo && !attackInProgress)
        {

            attackButton.interactable = true;

        }
        else{

            attackButton.interactable = false;
        
        }

    }

    public void KILL()
    {

        StartCoroutine(startAttack());

    }

    IEnumerator startAttack()
    {

        attackInProgress = true;
        yield return StartCoroutine(AttackExecute());
        attackInProgress = false;
        gameManager.ClockOut();

    }

    IEnumerator AttackExecute()
    {

        t = 0;
        while (t <= maxT)
        {

            t += Time.deltaTime;

            homieTransform.Rotate(0, 0, -(360 / maxT) * Time.deltaTime);
            yield return null;

        }        

    }

}
