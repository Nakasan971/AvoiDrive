using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*==============================
    UIボタン用スクリプト
===============================*/
public class Button_Dev : MonoBehaviour
{
    string sceneName;               //シーン名格納

    CameraController_Dev camera_Con;//ポーズ用
    PanelController_Dev panel_Con;  //UIパネル制御用
    AudioManager_Dev audio_Man;     //カウントダウン音用
    AudioSource audio_Src;          //BGM制御用

    void Start()
    {
        sceneName  = SceneManager.GetActiveScene().name;
        panel_Con  = GameObject.Find("PanelSystem").GetComponent<PanelController_Dev>();
        camera_Con = GameObject.Find("Main Camera").GetComponent<CameraController_Dev>();
        audio_Man  = GameObject.Find("Main Camera").GetComponent<AudioManager_Dev>();
        audio_Src  = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }
    //始める
    public void toStart(){
        panel_Con.topPanal.SetActive(false);
        panel_Con.countdownPanel.SetActive(true);
        panel_Con.countdown = true;
    }
    //操作説明
    public void howTo(){
        panel_Con.topPanal.SetActive(false);
        panel_Con.howtoPanel.SetActive(true);
    }
    //操作説明ー＞タイトルへ
    public void Back(){
        panel_Con.howtoPanel.SetActive(false);
        panel_Con.topPanal.SetActive(true);
    }
    //一時停止
    public void Pause(){
        panel_Con.playingPanel.SetActive(false);
        panel_Con.pausePanel.SetActive(true);
        camera_Con.start = false;
        audio_Src.Pause();
    }
    //再開
    public void Resume(){
        panel_Con.countdownPanel.SetActive(true);
        panel_Con.pausePanel.SetActive(false);
        panel_Con.countdown =true;
        audio_Man.Countdown();
        audio_Src.UnPause();
    }
    //各種ー＞タイトルへ
    public void toTop(){
        SceneManager.LoadScene(sceneName);
    }
    //ゲーム終了
    public void Exit(){
        #if UNITY_EDITOR    //エディタに依存
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
