using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardInHand : MonoBehaviour {

    public int needCrystal = 1;//暂时费用全部为1！！！！！！！！！！！！

    public int Hp = 0, Harm = 0;

    private UILabel hp, harm;

    //private UISprite displaycard;
    private UISprite sprite;
    private bool isHover;

    void Awake()
    {
        isHover = false;
        sprite = this.GetComponent<UISprite>();
        hp = transform.Find("hp").GetComponent<UILabel>();
        harm = transform.Find("harm").GetComponent<UILabel>();
    }

    public void setdepth(int depth)
    {
        sprite.depth = depth;
        hp.depth = depth + 1;
        harm.depth = depth + 1;
    }

    public void initproperty()              //初始化血量伤害
    {
        string spritName = sprite.spriteName;
        int[] data = new int[4];
        int j = 0, k = 0;
        //Debug.Log(spritName[7]);

        for (int i = 7; i < spritName.Length - 2; i++)
        {
            if (spritName[i] == '_')
            {
                //Debug.Log("aaa");
                continue;
            }
            else if (spritName[i + 1] == '_')
            {
                k += spritName[i] - '0';
                data[j] = k;            //data[0]为伤害，data[1]为血量
                j++;
                k = 0;
            }
            else
            {
                k += (spritName[i] - '0') * 10;
            }
        }

        Harm = data[0];
        Hp = data[1];

        harm.text = Harm + "";
        hp.text = Hp + "";
    }

    private string cardName
    {
        get{
            //if (sprite == null)
            //{
            //    sprite = this.getcomponent<uisprite>();
            //}
            return sprite.spriteName;
        }
    }


    public void enter()
    {
        displaycard._instance.show(cardName);
    }
    public void exit()
    {
        displaycard._instance.hide();
    }

    public void Resetshow(GameObject enemy)     //更新卡牌血量和伤害
    {
        int enemyHarm, enemyHp;
        UILabel Eharm = enemy.transform.Find("harm").GetComponent<UILabel>();
        UILabel Ehp = enemy.transform.Find("hp").GetComponent<UILabel>();

        if (Eharm.text.Length > 1)
        {
            enemyHarm = (Eharm.text[0] - '0') * 10 + Eharm.text[1] - '0';
        }
        else
        {
            enemyHarm = Eharm.text[0] - '0';
        }

        if (Ehp.text.Length > 1)
        {
            enemyHp = (Ehp.text[0] - '0') * 10 + Ehp.text[1] - '0';
        }
        else
        {
            enemyHp = Ehp.text[0] - '0';
        }

        Hp -= enemyHarm;
        enemy.GetComponent<cardInHand>().Hp = enemyHp - Harm;
        hp.text = Hp + "";
        Ehp.text = enemyHp - Harm + "";
        if (Hp <= 0)           //我方卡牌血量低于0
            this.transform.parent.GetComponent<Arena>().removecard(this.gameObject);             //从场上移除
        if (enemyHp - Harm <= 0)
            enemy.transform.parent.GetComponent<Arena>().removecard(enemy);
    }


}
