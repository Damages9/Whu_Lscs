using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endbutton : MonoBehaviour {

    private UILabel label;
    public UISprite tip;
    private GameCtrl game;
    private Arena arena;

    void Awake()
    {
        label = transform.Find("Label").GetComponent<UILabel>();
        game = this.transform.parent.GetComponentInChildren<GameCtrl>();
        arena = this.transform.parent.GetComponentInChildren<Arena>();
    }

    public void onEndButtonClick()
    {
        if (label.text == "结束回合")
        {
            label.text = "对方回合";
            game.changePlayer();
            //========================================================================================================             锁定场上卡牌不能动
            arena.lockArena();
        }
        else
        {
            label.text = "结束回合";
            tip.GetComponent<TweenScale>().ResetToBeginning();
            tip.GetComponent<TweenScale>().PlayForward();
            game.changePlayer();
        }
    }
}
