using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolShop : MonoBehaviour
{
    [Header("道具：直接鬼畜")]
    public Button tool01;
    public Text shop01;
    public int[] price01;
    public int num01 = 0;

    [Header("道具：体力立刻")]
    public Button tool02;
    public Text shop02;
    public int[] price02;
    public int num02 = 0;

    [Header("道具：伤害减半")]
    public Button tool03;
    public Text shop03;
    public int[] price03;
    public int num03 = 0;

    [Header("道具：免疫一次伤害")]
    public Button tool04;
    public Text shop04;
    public int[] price04;
    public int num04 = 0;

    [Header("道具：perfect三连")]
    public Button tool05;
    public Text shop05;
    public int[] price05;
    public int num05 = 0;

    [Header("道具：观众欢呼")]
    public Button tool06;
    public Text shop06;
    public int[] price06;
    public int num06 = 0;

    private BeatTest beat;
    private void Awake()
    {
        beat = GameObject.FindGameObjectWithTag("Beat").GetComponent<BeatTest>();
        shop01.text = (price01[num01]).ToString();
        shop02.text = (price02[num01]).ToString();
        shop03.text = (price03[num03]).ToString();
        shop04.text = (price04[num04]).ToString();
        shop05.text = (price05[num05]).ToString();
        shop06.text = (price06[num06]).ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        judgeTheLimit(tool01, price01, num01);
        judgeTheLimit(tool02, price02, num02);
        judgeTheLimit(tool03, price03, num03);
        judgeTheLimit(tool04, price04, num04);
        judgeTheLimit(tool05, price05, num05);
        judgeTheLimit(tool06, price06, num06);
    }

    public void clickPriceUp(string name)
    {
        switch(name)
        {
            case "tool01":
                priceUp(num01, price01, shop01);
                break;
            case "tool02":
                priceUp(num02, price02, shop02);
                break;
            case "tool03":
                priceUp(num03, price03, shop03);
                break;
            case "tool04":
                priceUp(num04, price04, shop04);
                break;
            case "tool05":
                priceUp(num05, price05, shop05);
                break;
            case "tool06":
                priceUp(num06, price06, shop06);
                break;
            default:
                break;
        }
        
    }

    public void priceUp(int num,int[] price,Text shop)
    {
        beat.resultMoney = beat.resultMoney - price[num];
        beat.moneyText.text = "Money: " + beat.resultMoney.ToString();
        num++;
        if (num >= price.Length)
        {
            num = price.Length - 1;
        }
        shop.text = (price[num]).ToString();
    }
    
    public void judgeTheLimit(Button tool,int[] price,int num)
    {
        if (num >= price.Length)
        {
            num = price.Length - 1;
        }
        int ifcanBUY = beat.resultMoney - price[num];
        if (ifcanBUY < 0)
        {
            tool.enabled = false;
        }
        else
        {
            tool.enabled = true;
        }
    }
}
