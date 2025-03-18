using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchScript : MonoBehaviour
{

    public UnityEvent onSwitchToggle;

    SpriteRenderer switchRenderer;

    // Start is called before the first frame update
    void Start()
    {
        
        switchRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButtonDown(0) && switchRenderer.bounds.Contains(mousePos))
        {

            onSwitchToggle.Invoke();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            onSwitchToggle.Invoke();

        }
        
    }

}
