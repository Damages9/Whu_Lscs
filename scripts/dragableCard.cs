using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragableCard : UIDragDropItem
{

    public handcard Me;
    public Arena me;
    private GameObject enemy;
    private int player;
    private bool isSuccess;


    protected override void OnDragDropRelease(GameObject surface)
    {
        int needCrystal = this.GetComponent<cardInHand>().needCrystal;
        Me = this.transform.parent.GetComponent<handcard>();
        me = this.gameObject.transform.parent.parent.GetComponentInChildren<Arena>();
        player = this.gameObject.transform.parent.parent.GetComponentInChildren<GameCtrl>().player;

        base.OnDragDropRelease(surface);


            if (me == null)
                Debug.Log("me empty!!!!!!!!!!!!!!!!!!!!!!");
            if (Me == null)
                Debug.Log("Me empty??????????????????????");

        //改过===============================================================================================================================
        //判断是否是可拖拽区域
        if (surface.tag!="Untagged" && player == 0)
        {
            if (this.gameObject.tag=="myhand" && surface.tag == "Arena")           //从手牌打到己方场上
            {
                isSuccess = GameObject.Find("GameCtrl").GetComponent<GameCtrl>().consumeCrystal(needCrystal);

                if (isSuccess)                                                      //水晶够
                {
                    Me.removeCard(this.gameObject);
                    //surface.GetComponent<Arena>().pushCard(this.gameObject);
                    me.pushCard(this.gameObject);
                    this.gameObject.tag = "mycard";
                    //surface.GetComponent<Arena>().updateshow();
                    me.updateshow();
                    Me.UpdateShow();
                    this.interactable = false;                                      //刚上场不能拖拽
                }
                else                                                                //水晶不够
                {
                    Me.UpdateShow();                                                //手牌重整
                }
            }
            else if (this.gameObject.tag=="mycard" && surface.tag == "enemycard")         //己方卡牌攻击对方卡牌
            {
                enemy = surface;
                if (enemy == null)
                    Debug.Log("enemy为空");
                this.transform.GetComponent<cardInHand>().Resetshow(enemy);
                me.updateshow();
                this.interactable = false;                                               //攻击后不能再次攻击
                this.gameObject.GetComponent<cardInHand>().changeMaterial();             //绿色描边去除
            }
            else if (this.gameObject.tag == "mycard" && surface.tag == "enemyhero")             //从我发卡牌攻击敌方英雄
            {
                Debug.Log("attttttttttttack");
                int Harm;
                UILabel harm = this.transform.Find("harm").GetComponent<UILabel>();
                if (harm.text.Length > 1)
                {
                    Harm = (harm.text[0] - '0') * 10 + harm.text[1] - '0';
                }
                else
                {
                    Harm = harm.text[0] - '0';
                }
                surface.GetComponent<enemyhero>().takedamage(Harm);
                me.updateshow();
                this.interactable = false;                                              //同理不能再次攻击
                this.gameObject.GetComponent<cardInHand>().changeMaterial();             //同理绿色描边去除
            }
            else //if (me != null)
            {
                me.updateshow();                    //从场上打到空气
                Me.UpdateShow();
            }
        }
        else
        {
            me.updateshow();
            Me.UpdateShow();
        }
    }
}
