using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*==============================
    UI用スクリプト
===============================*/
public class PanelController_Dev : MonoBehaviour
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
    public GameObject topPanal;         //タイトル画面
    public GameObject howtoPanel;       //操作説明画面
    public GameObject countdownPanel;   //カウントダウン画面
    public GameObject playingPanel;     //プレイ中の画面
    public GameObject abilityPanel;     //アイテム使用の画面
    public GameObject pausePanel;       //一時停止画面
    public GameObject gameoverPanel;    //ゲームオーバー画面

    //カメラと同期
    CameraController_Dev camera_Con;
    AudioManager_Dev audio_Man;

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
        topPanal        = GameObject.Find("Top");
        howtoPanel      = GameObject.Find("HowTo");
        countdownPanel  = GameObject.Find("CountDown");
        playingPanel    = GameObject.Find("Play");
        abilityPanel    = GameObject.Find("AbilityPanel");
        pausePanel      = GameObject.Find("Pause");
        gameoverPanel   = GameObject.Find("GameOver");
        camera_Con      = GameObject.Find("Main Camera").GetComponent<CameraController_Dev>();
        audio_Man       = GameObject.Find("Main Camera").GetComponent<AudioManager_Dev>();
        //タイトル画面以外非表示
        howtoPanel.SetActive(false);
        countdownPanel.SetActive(false);
        playingPanel.SetActive(false);
        abilityPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }
    void Update(){
        //カウントダウン中
        if(countdown){
            audio_Man.Countdown();
            limit -= Time.deltaTime;
            seconds = (int)limit;
            timer.text = seconds.ToString();
            if(seconds == 0){
                limit = 4.0f;
                countdown = false;
                countdownPanel.SetActive(false);
                camera_Con.start = true;
                playingPanel.SetActive(true);
            }
        }
        //プレイ中
        if(camera_Con.start){
            //スコアは時間換算
            time += Time.deltaTime;
            score = (int)time*100;
            totalScore.text = score.ToString();
            if(camera_Con.warp){
                time += Time.deltaTime * 3;
            }
        }
        //取得アイテム表示
        SetItemFrame(camera_Con.itemName);
        //能力発動
        if(camera_Con.isUse){
            AbilityFrame();
        }else{
            abilityPanel.SetActive(false);
        }
    }
    public IEnumerator GameOver(){
        playingPanel.SetActive(false);
        gameoverPanel.SetActive(true);
        float duration    = 2f;                   //持続時間
		float startTime   = Time.time;            //開始時間
		float endTime     = startTime + duration; //終了時間
        float updateValue = 0;                    //数値加算用
        do {
			// 現在の時間の割合
			float timeRate = (Time.time - startTime) / duration;
			// 数値を更新
			updateValue = (float)((score - slot) * timeRate + slot)*10;
			// テキストの更新
			finalScore.text = updateValue.ToString("f0");
			// 1フレーム待機
			yield return null;
		} while (updateValue < score);
        //最終的なスコア表示
		finalScore.text = score.ToString();
        //ハイスコア比較判定
        if(score > highest){
            highest = score;
            PlayerPrefs.SetInt(key,highest);
            highScore.text = highest.ToString();
        }
    }
    //所持アイテム表示
    public void SetItemFrame(string name){
        switch(name){
            case "null":
                img.sprite = sp[0];
                break;
            case "Shield_Dev(Clone)":
                img.sprite = sp[1];
                break;
            case "Brake_Dev(Clone)":
                img.sprite = sp[2];
                break;
             case "Warp_Dev(Clone)":
                img.sprite = sp[3];
                break;  
        }
    }
    //能力発動状態表示
    public void AbilityFrame(){
        if(camera_Con.protect){
            effecter.sprite = ab[0];
            abilityPanel.SetActive(true);
        }
        if(camera_Con.brake){
            effecter.sprite = ab[1];
            abilityPanel.SetActive(true);
        }
        if(camera_Con.warp){
            effecter.sprite = ab[2];
            abilityPanel.SetActive(true);
        }
        if(camera_Con.blind){
            effecter.sprite = ab[3];
            abilityPanel.SetActive(true);
        }
    }
}
