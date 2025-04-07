using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBarScript : MonoBehaviour
{

    RectTransform sliderTransform;

    // Start is called before the first frame update
    void Start()
    {
        
        sliderTransform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePosUI = Input.mousePosition;

        Vector2 newSliderPos = sliderTransform.position;

        newSliderPos.x = mousePosUI.x;

        newSliderPos.y = mousePosUI.y - 50;

        sliderTransform.position = newSliderPos;
        
    }
}
