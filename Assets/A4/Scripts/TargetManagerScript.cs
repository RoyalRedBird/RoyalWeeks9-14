using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetManagerScript : MonoBehaviour
{

    [SerializeField] int quantityToSpawn = 5;
    [SerializeField] int targetsSpawned = 0;

    public UnityEvent onTargetDestroyed;

    public bool spawnLock = false;

    [SerializeField] GameObject targetObject;
    [SerializeField] GameObject weaponHandler;
    TargetManagerScript tmScript;

    // Start is called before the first frame update
    void Start()
    {

        tmScript = GetComponent<TargetManagerScript>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if(targetsSpawned <= quantityToSpawn)
        {

            if(!spawnLock)
            {

                GameObject newTgt = Instantiate(targetObject, Random.insideUnitCircle * 4, transform.rotation);
                TargetScript tgtScript = newTgt.GetComponent<TargetScript>();

                UnityEvent destroyEvent = tgtScript.onTargetDestroyed;
                UnityEvent shootEvent = weaponHandler.GetComponent<WeaponManager>().onShotFired;

                shootEvent.AddListener(tgtScript.DidIGetHit);
                destroyEvent.AddListener(tmScript.StartRefreshTargetCount);

                targetsSpawned++;

            }           

        }

        if(targetsSpawned == quantityToSpawn)
        {

            spawnLock = true;

        }

        if (targetsSpawned == 0)
        {

            spawnLock = false;

        }

    }

    public void StartRefreshTargetCount()
    {

        StartCoroutine(RefreshTargetCount());     

    }

    public IEnumerator RefreshTargetCount()
    {

        yield return new WaitForSeconds(0.01f);
        targetsSpawned = GameObject.FindGameObjectsWithTag("Target").Length;

    }

}
