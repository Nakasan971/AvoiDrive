using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============================
    BGM＆SE用スクリプト
===============================*/
public class AudioManager : MonoBehaviour
{
    //連続再生制御用
    bool once;
    bool acShield;
    bool acBrake;
    bool acWarp;
    bool acBlind;
    bool moved;
    //各BGM＆SE格納
    public AudioClip[] sound;
    AudioSource audio_sour;
    
    PanelController panel_con;
    CameraController camera_con;
    void Start()
    {
        once        = false;
        acShield    = false;
        acBrake     = false;
        acWarp      = false;
        acBlind     = false;
        moved       = false;
        audio_sour  = GetComponent<AudioSource>();
        panel_con   = GameObject.Find("PanelSystem").GetComponent<PanelController>();
        camera_con  = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void Update()
    {
        if(panel_con.countdown){
            audio_sour.PlayOneShot(sound[0]);
        }
        //プレイ中
        if(!camera_con.end){
            //BGMループ再生
            audio_sour.loop = true;
            //移動中
            if(camera_con.direction != "null" && !moved){
                audio_sour.PlayOneShot(sound[1]);
                moved = true;
            }else if(camera_con.direction == "null" && moved){
                moved = false;
            }
            if(camera_con.protect && !acShield){
                audio_sour.PlayOneShot(sound[2]);
                acShield = true;
            }else if(camera_con.protectBreak){
                acShield = false;
            }
            if(camera_con.brake && !acBrake){
                audio_sour.PlayOneShot(sound[3]);
                acBrake = true;
            }else if(!camera_con.brake){
                acBrake = false;
            }
            if(camera_con.warp && !acWarp){
                audio_sour.PlayOneShot(sound[4]);
                acWarp = true;
            }else if(!camera_con.warp){
                acWarp = false;
            }
            if(camera_con.blind && !acBlind){
                audio_sour.PlayOneShot(sound[5]);
                acBlind = true;
            }else if(!camera_con.blind){
                acBlind = false;
            }
            //ポーズ画面中
            if(panel_con.pause.activeSelf){
                audio_sour.Pause();
            }else{
                audio_sour.UnPause();
            }
        }
        //ゲームオーバー時
        if(camera_con.end){
            Crush();
        }
    }
    //ゲームオーバー音
    void Crush(){
        if(!once){
            audio_sour.Stop();
            audio_sour.PlayOneShot(sound[6]);
            once = true;
        }
    }
}
