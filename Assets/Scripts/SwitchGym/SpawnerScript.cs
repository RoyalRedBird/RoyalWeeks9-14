using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnerScript : MonoBehaviour
{

    float timer;
    float spawnTime = 3;

    [SerializeField] GameObject toSpawn;
    [SerializeField] GameObject theSwitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;

        if(timer >= spawnTime)
        {

            timer = 0;

            GameObject newBlorbo = Instantiate(toSpawn, Random.insideUnitCircle * 4, transform.rotation);
            BlorboScript blorbScript = newBlorbo.GetComponent<BlorboScript>();

            UnityEvent switchEvent = theSwitch.GetComponent<SwitchScript>().onSwitchToggle;

            switchEvent.AddListener(blorbScript.toggleOnOff);

        }

    }
}
