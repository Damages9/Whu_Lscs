using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myhero : MonoBehaviour {

    private int max = 30, min = 0;
    private UILabel hp;
    private int hpcount = 30;

    public void takedamage(int damage)
    {
        hpcount -= damage;
        hp.text = hpcount + "";

        if (hpcount <= min)
        {
            //游戏结束
        }
    }

    public void addhp(int cure)
    {
        hpcount += cure;

        if (hpcount >= max)
            hpcount = max;

        hp.text = hpcount + "";
    }

    private void Awake()
    {
        hp = this.transform.Find("HP").GetComponent<UILabel>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            takedamage(Random.Range(1, 5));

        if (Input.GetKeyDown(KeyCode.R))
            addhp(Random.Range(1, 5));
    }
}
