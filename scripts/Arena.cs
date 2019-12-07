using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour {            //己方战斗区域
    private List<GameObject> cardList = new List<GameObject>();
   // public GameObject cardPrefab;
    public Transform card1;
    public Transform card2;
    private float xOffset = 0;

    void Start()
    {
        xOffset = card2.position.x - card1.position.x;
    }

    //添加卡到场中卡片数组
    public void pushCard(GameObject go)
    {

        cardList.Add(go);

        Vector3 pos = calcuPos();
        iTween.MoveTo(go, pos, 0.5f);
    }

    //计算当前卡牌的位置
    Vector3 calcuPos()
    {
        int index = cardList.Count;
        float newOffset;

        if(index%2==0)
        {
            newOffset = (index / 2) * xOffset;
            Vector3 pos = new Vector3(card1.position.x - newOffset, card1.position.y, card1.position.z);
            return pos;
        }
        else
        {
            newOffset = (index / 2) * xOffset;
            Vector3 pos = new Vector3(card1.position.x + newOffset, card1.position.y, card1.position.z);
            return pos;
        }
    }

    public void removecard(GameObject go)        //从场中移除卡牌
    {
        cardList.Remove(go);
        Destroy(go);
        updateshow();
    }

    public void updateshow()
    {
        Debug.Log("updateshow");
        float newOffset;
        Vector3 pos;
        for (int i = 1; i <= cardList.Count; i++)
        {
            if (i % 2 == 0)
            {
                newOffset = (i / 2) * xOffset;
                pos = new Vector3(card1.position.x - newOffset, card1.position.y, card1.position.z);
            }
            else
            {
                newOffset = (i / 2) * xOffset;
                pos = new Vector3(card1.position.x + newOffset, card1.position.y, card1.position.z);
            }
            iTween.MoveTo(cardList[i-1], pos, 0.5f);
            //====================================================================================================        上场的牌深度统一为3
            cardList[i - 1].gameObject.GetComponent<cardInHand>().setdepth(3);
        }
    }

    //=============================================================================================================       由gameCtrl调用激活场上卡牌拖拽功能且加描边
    public void activateArena()
    {
        for (int i = 1; i <= cardList.Count; i++)
        {
            cardList[i - 1].GetComponent<dragableCard>().interactable = true;
            cardList[i - 1].GetComponent<cardInHand>().changeMaterial();
        }
    }

    public void lockArena()
    {
        for (int i = 1; i <= cardList.Count; i++)
        {
            cardList[i - 1].GetComponent<dragableCard>().interactable = false;
            cardList[i - 1].GetComponent<cardInHand>().lockCard();
        }
    }
}
