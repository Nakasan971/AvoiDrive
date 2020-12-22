using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*==============================
    UIボタン用スクリプト
===============================*/
public class Button : MonoBehaviour
{
    string SceneName;
    PanelController panel_con;
    CameraController camera_con;

    void Start()
    {
        SceneName  = SceneManager.GetActiveScene().name;
        panel_con  = GameObject.Find("PanelSystem").GetComponent<PanelController>();
        camera_con = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    //始める
    public void ToStart(){
        panel_con.top.SetActive(false);
        panel_con.cd.SetActive(true);
        panel_con.countdown = true;
    }
    //操作説明
    public void HowTo(){
        panel_con.top.SetActive(false);
        panel_con.ht.SetActive(true);
    }
    //説明＞タイトルへ
    public void Back(){
        panel_con.ht.SetActive(false);
        panel_con.top.SetActive(true);
    }
    //プレイ中の一時停止
    public void Pause(){
        panel_con.play.SetActive(false);
        panel_con.pause.SetActive(true);
        camera_con.start = false;
    }
    //再開
    public void Resume(){
        panel_con.cd.SetActive(true);
        panel_con.pause.SetActive(false);
        panel_con.countdown =true;
    }
    //各種＞タイトルへ
    public void ToTop(){
        SceneManager.LoadScene(SceneName);
    }
    //ゲーム終了
    public void Exit(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
