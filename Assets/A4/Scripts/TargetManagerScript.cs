using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TargetManagerScript : MonoBehaviour
{

    [SerializeField] int quantityToSpawn = 5; //The number of targets to spawn in a wave.
    [SerializeField] int targetsSpawned = 0; //The number of targets currently spawned.

    public bool spawnLock = false; //Locks the spawner when all the targets are initially spawned.

    [SerializeField] GameObject targetObject; //The target prefab spawned by this script.
    [SerializeField] GameObject weaponHandler; //The weapon handler.
    TargetManagerScript tmScript; //This target manager.

    // Start is called before the first frame update
    void Start()
    {

        tmScript = GetComponent<TargetManagerScript>(); //Grabs itself for the target manager.
        
    }

    // Update is called once per frame
    void Update()
    {

        if(targetsSpawned <= quantityToSpawn) //If the number of targets spawned is less than the spawn cap.
        {

            if(!spawnLock) //And the spawn lock is disengaged.
            {

                GameObject newTgt = Instantiate(targetObject, Random.insideUnitCircle * 4, transform.rotation); //Spawn a new target prefab.
                TargetScript tgtScript = newTgt.GetComponent<TargetScript>(); //Grab its target script.

                UnityEvent destroyEvent = tgtScript.onTargetDestroyed; //Get the onTargetDestroyed event from the spawned target.
                UnityEvent shootEvent = weaponHandler.GetComponent<WeaponManager>().onShotFired; //Get the onShotFired event from the weapon handler.

                shootEvent.AddListener(tgtScript.DidIGetHit); //Subscribe the new target's DidIGetHit function to the onShotFired event on the weapon handler.
                destroyEvent.AddListener(tmScript.StartRefreshTargetCount); //Subscribe the Target Manager's StartRefreshTargetCount function to the target's onTargetDestroyed event.

                targetsSpawned++; //Increment the number of spawned targets by one.

            }           

        }

        if(targetsSpawned == quantityToSpawn) //If the number of targets spawned reaches the cap.
        {

            spawnLock = true; //Engage the spawn lock and stop the spawning of targets.

        }

        if (targetsSpawned == 0) //If there are no more targets left.
        {

            spawnLock = false; //Disengage the spawn lock.

        }

    }

    public void StartRefreshTargetCount() //This method exists so that the target can fire off the RefreshTargetCount coroutine when it is destroyed.
    {

        StartCoroutine(RefreshTargetCount());     

    }

    public IEnumerator RefreshTargetCount()
    {

        yield return new WaitForSeconds(0.01f); //Waits a short moment before checking for targets. This is done to ensure the game doesn't softlock itself.
        //If the final two targets in a game were destroyed in one shot, the refresh wouldn't work correctly and targets wouldn't spawn anymore.
        //The delay exists to ensure the targets have time do delete themselves before the function checks for remaining targets.

        targetsSpawned = GameObject.FindGameObjectsWithTag("Target").Length; //Gets the number of remaining targets on the board. Each target has the "Target" tag and this looks for every object with said tag.

    }

}
