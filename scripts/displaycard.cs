using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displaycard : MonoBehaviour {

    public static displaycard _instance;
    private UISprite sprite;
    public UILabel harm;
    public UILabel hp;

    void Awake()
    {
        _instance = this;
        sprite = this.GetComponent<UISprite>();
        this.gameObject.SetActive(false);    
    }

    //=============================================================================================    修改函数，传递血量，攻击
    public void show(string cardName,string harmtext,string hptext)
    {
        this.gameObject.SetActive(true);
        sprite.spriteName = cardName;
        harm.text = harmtext;
        hp.text = hptext;
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }
}
