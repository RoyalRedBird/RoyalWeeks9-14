using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventsDemo : MonoBehaviour
{

    public Image foodImage;

    public UnityEvent onTimeUp;

    [SerializeField] float timerLength = 2;
    [SerializeField] float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if(timer >= timerLength)
        {

            onTimeUp.Invoke();
            timer = 0;

        }

    }

    public void ButtonMashed()
    {

        Debug.Log("I JUST BOUGHT MORE LAND IN THE METAVERSE.");

    }

    public void SliderSlid(float slideValue)
    {

        Debug.Log("Slider has been slid to " + slideValue);

    }

    public void MouseInTheThing()
    {

        Debug.Log("O_O");
        foodImage.transform.localScale = Vector3.one * 1.2f;


    }

    public void MouseLeftTheThing()
    {

        Debug.Log("-_-");
        foodImage.transform.localScale = Vector3.one;

    }

    public void MouseClickedTheThing()
    {

        Debug.Log(">m<");

    }

}
