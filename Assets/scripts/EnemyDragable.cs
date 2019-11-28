using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragable : UIDragDropItem
{
    private EnemyCardInHand Enemy;
    private Arena enemy;
    private GameObject me;
    //public Gamecontroller gc;

    protected override void OnDragDropRelease(GameObject surface)
    {
        //int needCrystal = this.GetComponent<EnemyCardInHand>().needCrystal;
        Enemy = this.transform.parent.GetComponent<EnemyCardInHand>();
        base.OnDragDropRelease(surface);

        //Gamecontroller gc = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        //bool isSuccess = GameObject.Find("GameController").GetComponent<gamecontroller>().consumeCrystal(needCrystal);

        //判断是否是可拖拽区域
        if (surface != null)
        {
            if (Enemy!=null && surface.tag == "EnemyArena")
            {
                //还得判断水晶！！！！！！！！！！！！！！
                this.transform.parent.GetComponent<EnemyCardInHand>().removeCard(this.gameObject);
                surface.GetComponent<Arena>().pushCard(this.gameObject);
                enemy = this.transform.parent.GetComponent<Arena>();
            }
            else if (Enemy != null)           //从手牌上打到空气
            {
                Enemy.UpdateShow();
            }

            else if (enemy != null)            //从场上打到空气
            {
                enemy.updateshow();
            }

            if (enemy != null && surface.tag == "mycard")         //敌方卡牌攻击我方卡牌
            {
                me = surface;
                if (me == null)
                    Debug.Log("me为空");
                this.transform.GetComponent<cardInHand>().Resetshow(me);
                enemy.updateshow();
            }
        }
    }
}

