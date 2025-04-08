using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBarScript : MonoBehaviour
{

    RectTransform sliderTransform; //Gets the rectTransform for the slider, mainly for positioning.

    // Start is called before the first frame update
    void Start()
    {
        
        sliderTransform = GetComponent<RectTransform>(); //Grabs the slider from its own onject.

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePosUI = Input.mousePosition; //Gets the mouse position.

        Vector2 newSliderPos = sliderTransform.position; //Gets the position of the slider.

        newSliderPos.x = mousePosUI.x; //The X position of the slider is the same as the mouse's X positon.

        newSliderPos.y = mousePosUI.y - 50; //The Y position of the slider is 50 units below the mouse.

        sliderTransform.position = newSliderPos; //Apply the transforms.
        
    }
}
