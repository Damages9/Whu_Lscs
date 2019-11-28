using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    start,
    play,
    end
}

public class gamecontroller : MonoBehaviour {

    //水晶数

    public UILabel mylabel;
    public UILabel yourlabel;

    //水晶
    public int usableNumber = 2;
    public int totalNumber = 5;
    public int maxNumber = 10;

    public UISprite[] crystals;

    //卡片
    public GameObject cardPrefab;
    public Transform fromcard;
    public Transform tocard;
    public string[] cardNames;

    public float transTime = 2.0f;
    public int transformSpeed = 20;//一秒变换多少次

    private bool readyToChange = false;
    private float timer = 0;
    private UISprite nowCard;

    private int index;

    //绳子
    public GameState gameState = GameState.start;
    public float cycleTime = 60f;
    private float timer2 = 0;
    private UISprite rope;
    private float ropelength;

    void Awake()
    {
        maxNumber = crystals.Length;
        rope = this.transform.Find("rope").GetComponent<UISprite>();
        ropelength = rope.width;
        rope.width = 0;
    }

    void updateCrystal()
    {
        for(int i = 0;i<maxNumber; i++)
        {
            if(i>=totalNumber)
                crystals[i].gameObject.SetActive(false);
            else if(i>=usableNumber)
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
                if(totalNumber<maxNumber)
                    totalNumber += 1;
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            if (usableNumber > 0)
                usableNumber -= 1;
        }

        //水晶
        updateCrystal();

        //卡牌
        if (Input.GetKeyDown(KeyCode.Space))
        {
            randomGenerateCard();
        }

        if(readyToChange)
        {
            //timer += Time.deltaTime;
            //index = (int)(timer/(1f/transformSpeed));
            //index %= cardNames.Length;
            index = Random.Range(1,cardNames.Length);
            nowCard.spriteName = cardNames[index];

            //if(timer>transTime)
            //{
            //    timer = 0;
            //    readyToChange = false;
            //}
        }

        //绳子
        if(gameState==GameState.start)
        {
            timer2 += Time.deltaTime;
            if(timer2>cycleTime)
            {
                //changePlayer();
            }
            else if(cycleTime-timer2<=15)
            {
                rope.width = (int)((cycleTime - timer2) / 15f * ropelength);
            }
        }
    }

    //随机生成卡牌
    public void randomGenerateCard()
    {
       GameObject go = NGUITools.AddChild(this.gameObject,cardPrefab);
       go.transform.position = fromcard.position;
        nowCard = go.GetComponent<UISprite>();
        iTween.MoveTo(go, tocard.position, 1.0f);
        readyToChange = true;
    }
}
