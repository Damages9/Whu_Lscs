using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableCard : UIDragDropItem
{
    //public Gamecontroller gc;
    private handcard Me;
    private Arena me;
    private GameObject enemy;

    protected override void OnDragDropRelease(GameObject surface)
    {

        int needCrystal = this.GetComponent<cardInHand>().needCrystal;
        Me = this.transform.parent.GetComponent<handcard>();
        base.OnDragDropRelease(surface);

        //Gamecontroller gc = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        bool isSuccess = GameObject.Find("GameController").GetComponent<gamecontroller>().consumeCrystal(needCrystal);

        //判断是否是可拖拽区域
        if (surface!=null)
        {
            if (Me != null && surface.tag == "Arena")
            {
                //还得判断水晶！！！！！！！！！！！！！！
                Me.removeCard(this.gameObject);
                surface.GetComponent<Arena>().pushCard(this.gameObject);
                me = this.transform.parent.GetComponent<Arena>();
            }
            else if (Me != null)            //从手牌上打到空气
            {
                Me.UpdateShow();
            }

            else if (me!=null)              //从场上打到空气
            {
                me.updateshow();
            }

            if (me != null && surface.tag == "enemycard")         //己方卡牌攻击对方卡牌
            {
                enemy = surface;
                if (enemy == null)
                    Debug.Log("enemy为空");
                this.transform.GetComponent<cardInHand>().Resetshow(enemy);
                me.updateshow();
            }
        }
    }
}
