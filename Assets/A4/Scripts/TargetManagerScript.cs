using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetManagerScript : MonoBehaviour
{

    int testQuantityToSpawn = 6;
    int targetsSpawned = 0;

    [SerializeField] GameObject targetObject;
    [SerializeField] GameObject weaponHandler;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(targetsSpawned <= testQuantityToSpawn)
        {

            GameObject newTgt = Instantiate(targetObject, Random.insideUnitCircle * 4, transform.rotation);
            TargetScript tgtScript = newTgt.GetComponent<TargetScript>();

            UnityEvent shootEvent = weaponHandler.GetComponent<WeaponManager>().onShotFired;

            shootEvent.AddListener(tgtScript.DidIGetHit);

            targetsSpawned++;

        }
        
    }
}
