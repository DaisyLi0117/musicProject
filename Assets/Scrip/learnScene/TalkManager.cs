using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    [Header("对话教学")]
    public Animator conAnimator;
    public GameObject talkPanel;
    public Text textLable;
    //public TextAsset textFile;
    public TextAsset[] textFiles;
    public int textNum = 0;
    public int index;
    public int LevelLearn = 0;
    public bool learnChange = false;
    
    public bool talkToPlay = false;

    [Header("实践教学")]
    public GameObject[] PlayOBJ;
    public AudioSource theMusic;
    public GameObject fire;

    private BeatTest beat;

    // Start is called before the first frame update
    List<string> textList = new List<string>();

    private void Awake()
    {
        conAnimator = GetComponent<Animator>();
        GetTextFromFile(textFiles[textNum]);
        textLable.text = textList[index];
        index = 1;

        
    }

    // Update is called once per frame
    void Update()
    {
        //beat = GameObject.FindGameObjectWithTag("Beat").GetComponent<BeatTest>();
        if (Input.GetMouseButtonDown(0)&&index==textList.Count&&!theMusic.isPlaying&&fire.activeSelf ==false)
        {
            talkPanel.SetActive(false);
            index = 1;

            PlayOBJ[0].SetActive(true);
            PlayOBJ[1].SetActive(true);
            
            theMusic.Play();
            conAnimator.SetTrigger("teachPlay");
            textNum++;

            if (textNum <= textFiles.Length-1)
            {

                conAnimator.SetTrigger("hammerEnd");
                GetTextFromFile(textFiles[textNum]);
                //textLable.text = textList[index];
                
            }
            else
            {
                conAnimator.SetTrigger("hammerEnd");
                textLable.text =null;
            }

            return;
        }
        if(Input.GetMouseButtonDown(0) && !theMusic.isPlaying && fire.activeSelf==false)
        {
            textLable.text = textList[index];
            index++;
        }

        if (Input.GetMouseButtonDown(0) && theMusic.isPlaying)
        {
            LevelLearn += 1;
        }
        if(LevelLearn>=4&&learnChange ==false)
        {
            LevelLearn = 0;
            conAnimator.SetTrigger("teachTalk");
            conAnimator.SetTrigger("hammerTalk");
            theMusic.Pause();
            learnChange = true;
        }
    }
    public void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');

        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    public void showPanel()
    {
        talkPanel.SetActive(true);
    }

    public void closePanel()
    {
        talkPanel.SetActive(false);
    }

    
}
