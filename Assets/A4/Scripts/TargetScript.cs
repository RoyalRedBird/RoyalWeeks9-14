using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetScript : MonoBehaviour
{

    public GameManager GameManager; //The Game Manager

    public UnityEvent onTargetDestroyed; //Unity event that fires when this target is destroyed.

    [SerializeField] SpriteRenderer oneCircle; //The one point area of the target.
    [SerializeField] SpriteRenderer twoCircle; //The two point area of the target.
    [SerializeField] SpriteRenderer threeCircle; //The three point area of the target.

    [SerializeField] WeaponManager wpnMgr; //The weapon manager.
    [SerializeField] Vector2 aimPnt; //The position of the aim point.

    // Start is called before the first frame update
    void Start()
    {

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>(); //Grabbing the weapon manager from the heirarchy.
        wpnMgr = GameObject.Find("AimPoint").GetComponentInParent<WeaponManager>(); //Grabbing the aim point from the heirarchy.

    }

    //This function checks if the aim point is over one of the three areas of the target.
    //This function is invoked whenever the player fires a weapon. (This function is subscribed to the onShotFired event when initialized.)
    public void DidIGetHit()
    {

        aimPnt = wpnMgr.aimpointPosition; //Gets the aim point.

        if(threeCircle.bounds.Contains(aimPnt)) //The three point area is checked first to ensure the score is logged correctly.
        {

            GameManager.increaseScore(3); //Tell the game manager to increase the player score by the value of the zone.
            onTargetDestroyed.Invoke(); //Invoke the onTargetDestroyed event for the TargetManager to listen to.
            Destroy(gameObject); //Then destroy this target.

        }
        else if (twoCircle.bounds.Contains(aimPnt)) //Then the two point area.
        {

            GameManager.increaseScore(2);
            onTargetDestroyed.Invoke();
            Destroy(gameObject);

        }
        else if (oneCircle.bounds.Contains(aimPnt)) //Then finally the one point area.
        {

            GameManager.increaseScore(1);
            onTargetDestroyed.Invoke();
            Destroy(gameObject);

        }

    }

}
