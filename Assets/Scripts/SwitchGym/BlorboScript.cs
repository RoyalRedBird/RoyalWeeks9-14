using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlorboScript : MonoBehaviour
{

    Color offColor;
    Color onColor;

    [SerializeField] AnimationCurve switchCurve;
    float t = 0;
    float maxT = 1;

    SpriteRenderer mySprite;

    bool isOnline = false;

    // Start is called before the first frame update
    void Start()
    {

        mySprite = this.GetComponent<SpriteRenderer>();

        offColor = Color.gray;
        onColor = Random.ColorHSV();

    }

    // Update is called once per frame
    void Update()
    {

        float scaling = 1 + switchCurve.Evaluate(t);
        mySprite.transform.localScale = new Vector3(scaling, scaling, scaling);

        if (isOnline)
        {

            t += Time.deltaTime;
            mySprite.color = onColor;


        }
        else
        {

            t -= Time.deltaTime;
            mySprite.color = offColor;

        }

        if(t >= maxT)
        {

            t = maxT;

        }

        if(t <= 0)
        {

            t = 0;

        }

    }

    public void toggleOnOff()
    {

        isOnline = !isOnline;

    }

}
