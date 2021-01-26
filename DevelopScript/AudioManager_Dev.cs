using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============================
    BGM＆SE用スクリプト
===============================*/
public class AudioManager_Dev : MonoBehaviour
{
    bool isMove;                    //移動中か？
    public AudioClip[] sound;       //各BGM＆SE格納

    CameraController_Dev camera_con;//カメラから呼び出し
    AudioSource audio_sour;         //ループ再生制御

    void Start(){
        camera_con  = GetComponent<CameraController_Dev>();
        audio_sour  = GetComponent<AudioSource>();

        //BGMループ再生
        audio_sour.loop = true;
    }

    void Update(){
        //移動音
        if(camera_con.direction != "null" && !isMove){
            audio_sour.PlayOneShot(sound[1]);
            isMove = true;
        }else if(camera_con.direction == "null" && isMove){
            isMove = false;
        }
    }
    //カウントダウン音
    public void Countdown(){
        audio_sour.PlayOneShot(sound[0]);
    }
    //シールド音
    public void ActiveShield(){
        audio_sour.PlayOneShot(sound[2]);
    }
    //ブレーキ音
    public void ActiveBrake(){
        audio_sour.PlayOneShot(sound[3]);
    }
    //ワープ音
    public void ActiveWarp(){
        audio_sour.PlayOneShot(sound[4]);
    }
    //ブラインダー音
    public void ActiveBlinder(){
        audio_sour.PlayOneShot(sound[5]);
    }
    //ゲームオーバー音
    public void Crash(){
        audio_sour.Stop();//BGM停止
        audio_sour.PlayOneShot(sound[6]);
    }
}
