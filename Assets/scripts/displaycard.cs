using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displaycard : MonoBehaviour {

    public static displaycard _instance;
    private UISprite sprite;

    void Awake()
    {
        _instance = this;
        sprite = this.GetComponent<UISprite>();
        this.gameObject.SetActive(false);
    }

    public void show(string cardName)
    {
        this.gameObject.SetActive(true);
        sprite.spriteName = cardName;
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }
}
