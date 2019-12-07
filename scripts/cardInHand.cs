using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cardInHand : MonoBehaviour {

    //============================================================================================现在费用不为1辽
    public int needCrystal;

    public int Hp = 0, Harm = 0;

    private UILabel hp, harm;

    //private UISprite displaycard;
    private UISprite sprite;
    private bool isHover;

    public Material outlineMaterial;


    void Awake()
    {
        this.gameObject.GetComponent<UISprite>().material = null;
        //myMaterial = this.gameObject.GetComponentInParent<UISprite>().material;
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

        //===============================================================================================================================
        if (spritName[6] == '_')
        {
            needCrystal = spritName[5] - '0';
        }
        else
        {
            needCrystal = (spritName[5] - '0') * 10 + spritName[6] - '0';
        }
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

    //====================================================================================================  增加显示卡牌血量攻击力
    public void enter()
    {
        displaycard._instance.show(cardName,harm.text,hp.text);
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
        //================================================================================================================    改过
        if (Hp <= 0)           //我方卡牌血量低于0
            //this.transform.parent.GetComponent<Arena>().removecard(this.gameObject);             //从场上移除
            this.gameObject.transform.parent.parent.GetComponentInChildren<Arena>().removecard(this.gameObject);
        if (enemyHp - Harm <= 0)
            //enemy.transform.parent.GetComponent<Arena>().removecard(enemy);
            this.gameObject.transform.parent.parent.GetComponentInChildren<Arena>().removecard(enemy);
    }

    //=======================================================================================================      外部接口以改变卡牌是否带有描边
    public void changeMaterial()
    {
        Debug.Log("henshin");
        if (this.gameObject.GetComponent<UISprite>().material != outlineMaterial)
            this.gameObject.GetComponent<UISprite>().material = outlineMaterial;
        else
            this.gameObject.GetComponent<UISprite>().material = null;
    }

    public void lockCard()
    {
        this.gameObject.GetComponent<UISprite>().material = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.gameObject.GetComponent<UISprite>().material = null;
            Debug.Log(this.gameObject.GetComponent<UISprite>().material);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.gameObject.GetComponent<UISprite>().material = outlineMaterial;
            Debug.Log(this.gameObject.GetComponent<UISprite>().material);
        }
    }

}
