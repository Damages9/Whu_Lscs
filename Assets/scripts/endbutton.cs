using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endbutton : MonoBehaviour {

    private UILabel label;

    void Awake()
    {
        label = transform.Find("Label").GetComponent<UILabel>();
    }

    public void onEndButtonClick()
    {
        label.text = "对方回合";
    }
}
