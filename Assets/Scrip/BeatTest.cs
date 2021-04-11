using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using System;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class BeatTest : MonoBehaviour
{
    [Header("track的名字")]  
    public string eventID1;//打节拍 123 +猛男对应嘶吼
    public string eventID2;//相应玩家的打击

    [Header("动画控制")] 
    public Animator playAni;

    [Header("响应的按键")]
    public KeyCode theButton;

    [Header("音乐")]
    public AudioSource theMusic;
    public AudioSource beatHit;
    public GameObject fireMusicOBJ;

    [Header("分数输出")]
    public Text scoreText;

    [Header("体力条")]
    public float maxHealth;
    public float currentHealth;
    public float numHealth;

    [Header("鬼畜条")]
    public float maxFire;
    public float currentFire;
    public float numFire;
    public float speed;
    public bool ifFire;

    [Header("金钱")]
    public int moneyLevel;
    public int resultMoney;
    public Text moneyText;

    static int ifTest = 0;

    [Header("判定界面")]
    public GameObject gameover;
    public Text FinalMoney;
    public Text FinalScore;
    public Text result;
    public Text OKText,GOODText,PERFECTText,MISSText,AWHText;
    public int OKhit,GOODhit,PERFECThit,MISShit,AWHhit;

    [Header("道具相关的检测debug")]//最后去掉就行
    public bool ifImmune ;
    public bool ifpre3;
    public int pre3Num = 0;

    // Start is called before the first frame update
    void Start()
    {
        Koreographer.Instance.RegisterForEvents(eventID1,Beat);
        Koreographer.Instance.RegisterForEvents(eventID2,PlayerBeat);
        playAni.GetComponent<Animator>();
        scoreText.text = null;
        currentHealth = maxHealth;
        currentFire = 0;
        ifFire = false;
        moneyText.text = "Money: 0 ";
        ifImmune = false;
        ifpre3 = false;
    }

    private void PlayerBeat(KoreographyEvent koreoEvent)
    {
        string ScoreJudge = koreoEvent.GetTextValue();

        if(ScoreJudge == "Perfect")
        {
            if(Input.GetMouseButtonDown(0) && ifTest != 1)
            {
                
                testButton("Perfect");
                ifTest = 1;
                currentFire += maxFire * (1 / numFire);//增加鬼畜值
                PERFECThit += 1;
                PERFECTText.text = "PERFECT HIT: " + PERFECThit.ToString();
                if(ifpre3 ==true)
                {
                    pre3Num += 1;
                }
            }
        }
        else if (ScoreJudge == "Good")
        {
            if (Input.GetMouseButtonDown(0) && ifTest != 1)
            {
                
                if(ifpre3 == true)
                {
                    pre3Num += 1;

                    testButton("Perfect");
                    ifTest = 1;
                    currentFire += maxFire * (1 / numFire);//增加鬼畜值
                    PERFECThit += 1;
                    PERFECTText.text = "PERFECT HIT: " + PERFECThit.ToString();
                }
                else
                {
                    testButton("Good");
                    ifTest = 1;
                    currentHealth -= maxHealth * (2 / numHealth);
                    GOODhit += 1;
                    GOODText.text = "GOOD HIT: " + GOODhit.ToString();
                }
            }
        }
        else if (ScoreJudge == "OK")
        {
            if (Input.GetMouseButtonDown(0) && ifTest != 1)
            {
                if (ifpre3 == true)
                {
                    pre3Num += 1;

                    testButton("Perfect");
                    ifTest = 1;
                    currentFire += maxFire * (1 / numFire);//增加鬼畜值
                    PERFECThit += 1;
                    PERFECTText.text = "PERFECT HIT: " + PERFECThit.ToString();
                }
                else
                {
                    testButton("OK");
                    ifTest = 1;
                    currentHealth -= maxHealth * (3 / numHealth);
                    OKhit += 1;
                    OKText.text = "OK HIT: " + OKhit.ToString();
                }
            }
        }
        else if (ScoreJudge == "Awh")
        {
            if (Input.GetMouseButtonDown(0) && ifTest != 1)
            {
                if (ifpre3 == true)
                {
                    pre3Num += 1;

                    testButton("Perfect");
                    ifTest = 1;
                    currentFire += maxFire * (1 / numFire);//增加鬼畜值
                    PERFECThit += 1;
                    PERFECTText.text = "PERFECT HIT: " + PERFECThit.ToString();
                }
                else
                {
                    if (ifImmune == false)
                    {
                        testButton("Awh");//AWH还需要一个特殊的动画，之后有图了再修改触发器
                        ifTest = 1;
                        currentHealth -= maxHealth * (5 / numHealth);
                        AWHhit += 1;
                        AWHText.text = "AWH HIT: " + AWHhit.ToString();
                    }
                    else if (ifImmune == true)
                    {
                        testButton("免疫");////还需要一个特殊的动画，之后有图了再修改触发器
                        ifTest = 1;
                        ifImmune = false;
                    }
                }
               
            }
        }
        else if (ScoreJudge == "Miss")
        {
                if (ifTest == 0)
                {
                    scoreText.text = "Miss";
                    playAni.SetTrigger("auto");
                    playAni.ResetTrigger("clap01");
                    playAni.ResetTrigger("clap02");
                    playAni.ResetTrigger("clap03");
                    currentHealth -= maxHealth * (5 / numHealth);
                    MISShit += 1;
                    MISSText.text = "MISS HIT: " + MISShit.ToString();
                }
        }
        else if (ScoreJudge =="zero")
        {
            ifTest = 0;
            currentHealth -= 0f;
        }
        else if(ScoreJudge =="END")
        {
            Debug.Log("!!!!!!!!1");
            gameover.SetActive(true);
            //Time.timeScale = 0f;
            theMusic.Pause();
            result.text = "WIN";
        }

    }

    private void Beat(KoreographyEvent koreoEvent)
    {
        int beatNum = koreoEvent.GetIntValue();
        if(beatNum ==1)
        {
            playAni.SetTrigger("clap01");
            playAni.ResetTrigger("clap02");
            playAni.ResetTrigger("clap03");
            playAni.ResetTrigger("auto");
            beatHit.Play();
        }
        else if(beatNum ==2)
        {
            playAni.SetTrigger("clap02");
            playAni.ResetTrigger("clap01");
            playAni.ResetTrigger("clap03");
            playAni.ResetTrigger("auto");
            beatHit.Play();
        }
        else if(beatNum ==3)
        {
            beatHit.Play();
        }
    }
   

    void Update()
    {
        
        if (currentFire >= maxFire)
        {
            currentFire = maxFire;
            ifFire = true;
            fireMusicOBJ.SetActive(true);

            StartCoroutine(addMoney());
        }

        if(ifFire)
        {
            theMusic.Pause();
            currentFire -= speed * Time.deltaTime;
            if(currentFire<=0)
            {
                currentFire = 0;
                ifFire = false;
                theMusic.Play();
                fireMusicOBJ.SetActive(false);
            }
        }
        if(currentHealth<=0)
        {
            gameover.SetActive(true);
            //Time.timeScale = 0f;
            theMusic.Pause();
            result.text = "GAME OVER";
            FinalScore.text = "FINAL SCORE:F";
        }

        FinalMoney.text = "MONEY:"+ resultMoney.ToString();

        if(pre3Num>=3)
        {
            pre3Num = 0;
            ifpre3 = false;
        }
    }

    public void testButton(string outText)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(outText);
            scoreText.text = outText;

            playAni.SetTrigger("clap03");
            playAni.ResetTrigger("clap01");
            playAni.ResetTrigger("clap02");
            playAni.ResetTrigger("auto");
        }
        
    }

    public void GuiChu()
    {
        currentFire = maxFire+1;
    }

    public void addHealthOnce(float addnum)
    {
        addnum *= maxHealth;
        currentHealth += addnum;
    }

    public void downHarm()
    {
        numHealth *= 2;
    }

    public void cheerUp(float upLevel)
    {
        StartCoroutine(toCheerUp(upLevel));
    }

    public void immuneOneHarm()
    {
        ifImmune = true;
    }

    public void preTrible()
    {
        ifpre3 = true;
    }

    IEnumerator addMoney()
    {
        for (int i = 0; i < moneyLevel; i++)
        {
            resultMoney += 1;
            moneyText.text = "Money: "+resultMoney.ToString();
            yield return new WaitForSeconds(0.1f);     //每 0.1s 加一次
        }
        StopCoroutine(addMoney());
    }

    IEnumerator toCheerUp(float upLevel)
    {
        for (int i = 0; i < upLevel; i++)
        {
            currentHealth += 1;
            yield return new WaitForSeconds(0.1f);     //每 0.1s 加一次
        }
        StopCoroutine(toCheerUp(upLevel));
    }

}
