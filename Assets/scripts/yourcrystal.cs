using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yourcrystal : MonoBehaviour {

    public int usableNumber = 1;
    public int totalNumber = 1;

    private UILabel yourlabel;

    void Awake()
    {
        yourlabel = this.GetComponent<UILabel>();
    }

	void updateNumber()
    {
        yourlabel.text = usableNumber + "/" + totalNumber;
    }
}
