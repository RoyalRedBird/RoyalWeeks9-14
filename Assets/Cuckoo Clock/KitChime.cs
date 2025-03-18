using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitChime : MonoBehaviour
{

    public KitClock clockScript;

    private void Start()
    {

        clockScript.OnTheHour.AddListener(Chime);

    }

    public void Chime(int currentHour)
    {
        Debug.Log("Chiming! It is currently " + currentHour + " o'clock");
    }

    public void ChimeNoArg()
    {
        Debug.Log("Chiming!");
    }

}
