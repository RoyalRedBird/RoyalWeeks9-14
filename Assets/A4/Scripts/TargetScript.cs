using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetScript : MonoBehaviour
{

    public GameManager GameManager;

    public UnityEvent onTargetDestroyed;

    [SerializeField] SpriteRenderer oneCircle;
    [SerializeField] SpriteRenderer twoCircle;
    [SerializeField] SpriteRenderer threeCircle;

    [SerializeField] WeaponManager wpnMgr;
    [SerializeField] Vector2 aimPnt;

    // Start is called before the first frame update
    void Start()
    {

        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        wpnMgr = GameObject.Find("AimPoint").GetComponentInParent<WeaponManager>();
        
    }

    public void DidIGetHit()
    {

        aimPnt = wpnMgr.aimpointPosition;

        if(threeCircle.bounds.Contains(aimPnt))
        {

            GameManager.increaseScore(3);
            onTargetDestroyed.Invoke();
            Destroy(gameObject);

        }
        else if (twoCircle.bounds.Contains(aimPnt))
        {

            GameManager.increaseScore(2);
            onTargetDestroyed.Invoke();
            Destroy(gameObject);

        }
        else if (oneCircle.bounds.Contains(aimPnt))
        {

            GameManager.increaseScore(1);
            onTargetDestroyed.Invoke();
            Destroy(gameObject);

        }

    }

}
