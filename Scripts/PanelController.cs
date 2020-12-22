using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*==============================
    UI用スクリプト
===============================*/
public class PanelController : MonoBehaviour
{
    //開始時のカウントダウン用
    float limit;
    int seconds;

    //プレイ中のスコア換算用
    float time;
    int score;
    int slot;

    //カウントダウンするか？
    public bool countdown;

    //アイテム関係
    Image img;
    Image effecter;
    public Sprite[] sp;
    public Sprite[] ab;

    //スコア関係のテキスト
    int highest;
    string key = "HIGH SCORE";
    Text timer;
    Text totalScore;
    Text finalScore;
    Text highScore;

    //UIパネル制御用
    public GameObject top;      //タイトル画面
    public GameObject ht;       //操作説明画面
    public GameObject cd;       //プレイ直前のカウントダウン画面
    public GameObject play;     //プレイ中の画面
    public GameObject ability;  //アイテム使用の画面
    public GameObject pause;    //プレイ中の一時停止画面
    public GameObject go;       //ゲームオーバー画面

    //カメラと同期
    CameraController cc;

    void Start()
    {
        limit           = 4.0f;
        time            = 0;
        seconds         = 0;
        score           = 0;
        slot            = 0;
        highest         = PlayerPrefs.GetInt(key,0);
        countdown       = false;
        img             = GameObject.Find("Item").GetComponent<Image>();
        effecter        = GameObject.Find("Effecter").GetComponent<Image>();
        timer           = GameObject.Find("Timer").GetComponent<Text>();
        totalScore      = GameObject.Find("Score").GetComponent<Text>();
        finalScore      = GameObject.Find("FinalScore").GetComponent<Text>();
        highScore       = GameObject.Find("HighScore").GetComponent<Text>();
        highScore.text  = highest.ToString();
        top             = GameObject.Find("Top");
        ht              = GameObject.Find("HowTo");
        cd              = GameObject.Find("CountDown");
        play            = GameObject.Find("Play");
        ability         = GameObject.Find("AbilityPanel");
        pause           = GameObject.Find("Pause");
        go              = GameObject.Find("GameOver");
        cc              = GameObject.Find("Main Camera").GetComponent<CameraController>();
        //タイトル画面以外非表示
        ht.SetActive(false);
        cd.SetActive(false);
        play.SetActive(false);
        ability.SetActive(false);
        pause.SetActive(false);
        go.SetActive(false);
    }
    void Update(){
        //カウントダウン中
        if(countdown){
            limit -= Time.deltaTime;
            seconds = (int)limit;
            timer.text = seconds.ToString();
            if(seconds == 0){
                limit = 4.0f;
                countdown = false;
                cd.SetActive(false);
                cc.start = true;
                play.SetActive(true);
            }
        }
        //プレイ中
        if(cc.start){
            if(!cc.end){
                //スコアは時間換算
                time += Time.deltaTime;
                score = (int)time*100;
                totalScore.text = score.ToString();
            }
            if(cc.warp){
                time += Time.deltaTime * 3;
            }
        }
        //取得アイテム表示
        SetItemFrame(cc.itemName);
        //能力発動
        if(cc.isUse){
            AbilityFrame();
        }else{
            ability.SetActive(false);
        }
        //ゲームオーバー時
        if(cc.end){
            play.SetActive(false);
            go.SetActive(true);
            //動的スコア換算表示
            if(slot != score){
                if(score-slot >= 100){
                    slot = slot + 100;
                }else{
                    slot = slot + 10;
                }
            }
            finalScore.text = slot.ToString();
            //ハイスコア表示
            if(score > highest){
                highest = score;
                PlayerPrefs.SetInt(key,highest);
                highScore.text = highest.ToString();
            }
        }
    }
    //所持アイテム表示
    public void SetItemFrame(string name){
        switch(name){
            case "null":
                img.sprite = sp[0];
                break;
            case "Shield(Clone)":
                img.sprite = sp[1];
                break;
            case "Brake(Clone)":
                img.sprite = sp[2];
                break;
             case "Warp(Clone)":
                img.sprite = sp[3];
                break;  
        }
    }
    //能力発動状態表示
    public void AbilityFrame(){
        if(cc.protect){
            effecter.sprite = ab[0];
            ability.SetActive(true);
        }
        if(cc.brake){
            effecter.sprite = ab[1];
            ability.SetActive(true);
        }
        if(cc.warp){
            effecter.sprite = ab[2];
            ability.SetActive(true);
        }
        if(cc.blind){
            effecter.sprite = ab[3];
            ability.SetActive(true);
        }
    }
}
