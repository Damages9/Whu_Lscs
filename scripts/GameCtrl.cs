using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    start,
    play,
    end,
    enemy
}

public class GameCtrl : MonoBehaviour
{
    //回合数
    private int turns;

    //区别敌我回合
    public int player;

    //回合提示
    public UISprite myturn;

    //水晶数

    public UILabel mylabel;
    public UILabel yourlabel;

    //水晶
    private int usableNumber;
    private int totalNumber;
    private int maxNumber;

    public UISprite[] crystals;

    //卡片
    public string[] cardNames;

    public float transTime = 2.0f;
    public int transformSpeed = 20;//一秒变换多少次

    private bool readyToChange = false;
    private float timer = 0;
    private UISprite nowCard;

    private int index;

    //绳子
    private GameState gameState;
    public float cycleTime = 30f;
    private float timer2 = 0;
    private UISprite rope;
    private float ropelength;

    //我方手牌组件
    private handcard hCard;

    //我方场上卡牌组件
    private Arena arena;

    void Awake()
    {
        usableNumber = 1;
        totalNumber = 1;
        maxNumber = crystals.Length;
        rope = this.transform.Find("rope").GetComponent<UISprite>();
        ropelength = rope.width;
        rope.width = 0;
        //=============================================================================================   初始化回合数为1
        turns = 1;
        //=============================================================================================   游戏状态初始化为start
        gameState = GameState.start;
        //=============================================================================================   player 为0表示我的回合，1是电脑的
        player = 0;
        //=============================================================================================   获取手牌组件
        hCard = this.transform.parent.GetComponentInChildren<handcard>();
        //=============================================================================================   获取场上卡牌组件
        arena = this.transform.parent.GetComponentInChildren<Arena>();
    }

    void updateCrystal()
    {
        for (int i = 0; i < maxNumber; i++)
        {
            if (i >= totalNumber)
                crystals[i].gameObject.SetActive(false);
            else if (i >= usableNumber)
            {
                crystals[i].gameObject.SetActive(true);
                crystals[i].spriteName = "darkcrystal";
            }
            else
            {
                crystals[i].gameObject.SetActive(true);
                crystals[i].spriteName = "crystal";
            }
        }
    }

    //每回合增加和复原水晶
    public void refreshCrystal()
    {
        if (totalNumber < maxNumber)
            totalNumber++;

        usableNumber = totalNumber;
    }

    public bool consumeCrystal(int number)
    {
        if (usableNumber >= number)
        {
            Debug.Log(usableNumber);
            usableNumber -= number;
            return true;
        }
        else
        {
            //水晶不足
            return false;
        }

    }

    void Update()
    {
        //水晶数
        mylabel.text = usableNumber + "/" + totalNumber;

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (totalNumber < maxNumber)
                totalNumber += 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (usableNumber > 0)
                usableNumber -= 1;
        }
        

        //水晶
        updateCrystal();


        //绳子
        if (gameState == GameState.play||gameState == GameState.end)
        {
            timer2 += Time.deltaTime;
            if (timer2 > cycleTime)
            {
                changePlayer();
            }
            else if (cycleTime - timer2 <= 15)
            {
                gameState = GameState.end;
                rope.width = (int)((cycleTime - timer2) / 15f * ropelength);
            }
        }
    }

    //================================================================================================== 留给外界的换人接口
    public void changePlayer()
    {
        if (player == 0)
        {
            player = 1;
            gameState = GameState.enemy;
        }
        else
        {
            player = 0;
            gameState = GameState.start;
            refreshCrystal();
            hCard.addCard();                                                                            //新回合发牌
            gameState = GameState.play;
            print("wryyyyyyyyy");
            arena.activateArena();
        }
    }

    //================================================================================================== 整局游戏初始发牌

    public void initHandCard()
    {
        Debug.Log("start");
        for(int i = 0;i<4;i++)
        {
            hCard.addCard();
        }
        gameState = GameState.play;
    }

 
}
