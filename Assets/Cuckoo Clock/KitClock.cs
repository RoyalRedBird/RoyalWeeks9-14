using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KitClock : MonoBehaviour
{

    public Transform hourHandTransform;
    public Transform minuteHandTransform;
    public float timeAnHourTakes = 5;

    public float t;
    public int hour = 0;

    public UnityEvent<int> OnTheHour;

    Coroutine clockOnlineCoroutine;
    IEnumerator clockMovementEnumerator;

    private void Start()
    {

        clockOnlineCoroutine = StartCoroutine(runClock());

    }

    public void CeaseClock()
    {

        if(clockOnlineCoroutine != null)
        {

            StopCoroutine(clockOnlineCoroutine);

        }     

    }

    public void FullStopClock()
    {

        if(clockOnlineCoroutine != null)
        {

            StopCoroutine(clockOnlineCoroutine);

        }

        if(clockMovementEnumerator != null)
        {

            StopCoroutine(clockMovementEnumerator);

        }
       
    }

    IEnumerator runClock()
    {

        while (true)
        {

            clockMovementEnumerator = moveHandsByHour();
            yield return StartCoroutine(clockMovementEnumerator);

        }
        
    }

    IEnumerator moveHandsByHour()
    {

        t = 0;
        while (t < timeAnHourTakes)
        {

            t += Time.deltaTime;
            minuteHandTransform.Rotate(0, 0, -(360/timeAnHourTakes) * Time.deltaTime);
            hourHandTransform.Rotate(0, 0, -(30 / timeAnHourTakes) * Time.deltaTime);
            yield return null;

        }
        hour++;

        if(hour > 12)
        {

            hour = 1;

        }

        OnTheHour.Invoke(hour);

    }

}
