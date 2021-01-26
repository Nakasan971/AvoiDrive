using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*==============================
    プレイヤー用スクリプト
===============================*/
public class CameraController_Dev : MonoBehaviour
{
    Transform tf;               //カメラの位置
    float targetpos;            //移動先の位置
    public string direction;    //移動方向判定用
    public string itemName;     //アイテム所持判定用
    public float speed;         //進行速度
    public float prespeed;      //進行速度一時保持
    public float time;          //加速判定用計測時間
    public float effectTime;    //アイテム効果時間計測用
    public bool start;          //ゲームを始めたか？
    public bool isUse;          //アイテム（能力）発動時
    public bool protect;        //Shield発動時
    public bool protectBreak;   //Shield終了時
    public bool brake;          //Brake発動時  
    public bool warp;           //Warp発動時
    public bool blind;          //Blinder発動時

    AudioManager_Dev Audio_Man;
    PanelController_Dev Panel_Con;
    void Start()
    {
        tf           = this.gameObject.transform;
        direction    = "null";
        itemName     = "null";
        time         = 10;
        effectTime   = 1;

        Audio_Man   = gameObject.GetComponent<AudioManager_Dev>();
        Panel_Con   = GameObject.Find("PanelSystem").GetComponent<PanelController_Dev>();
    }

    /*非同期化予定*/
    void Update()
    {
        basicSystem();
    }
    
    // 基本的な流れの作業
    public void basicSystem(){
        //プレイ中
        if(start){
            //前進
            tf.Translate(new Vector3(0,0,speed));
             //移動制御
            if(direction == "null"){
                direction = moveKey();
            }
            move(direction);
            //アイテム（能力）発動
            UseAbility();
           
            //経過時間で加速を表現
            time -= Time.deltaTime;
            if(time <= 0 && speed < 0.4f){
                speed += 0.05f;
                time = 10;
            }
        }
    }

    //移動機能
    public void move(string dir){
        switch(dir){
            case "up":      //上昇
                tf.Translate(new Vector3(0,0.1f,0));
                if(tf.position.y >= targetpos){
                    direction = "null";
                }
                break;
            case "down":    //降下
                tf.Translate(new Vector3(0,-0.1f,0));
                if(tf.position.y <= targetpos){
                    direction = "null";
                }
                break;
            case "left":    //車線変更（左）
                tf.Translate(new Vector3(-0.1f,0,0));
                if(tf.position.x <= targetpos){
                    direction = "null";
                }
                break;
            case "right":   //車線変更(右) 
                tf.Translate(new Vector3(0.1f,0,0));
                if(tf.position.x >= targetpos){
                    direction = "null";
                }
                break;
            default:
                break;
        }
    }

    //入力キー取得
    public string moveKey(){
        switch(Input.inputString){
            case "w":
                if(tf.position.y < 3.3){
                    direction = "up";
                    targetpos = tf.position.y + 1;
                }
                break;
            case "s":
                if(tf.position.y > 1.6){
                    direction = "down";
                    targetpos = tf.position.y - 1;
                }
                break;
            case "a":
                if(tf.position.x > -1){
                    direction = "left";
                    targetpos = tf.position.x - 1f;
                }
                break;
            case "d":
                if(tf.position.x < 1){
                    direction = "right";
                    targetpos = tf.position.x + 1;
                }
                break;
            case " ":
                if(itemName != "null"){
                    UseItem(itemName);
                    isUse = true;
                }
                break;
            default :
                break;
        }
        return direction;
    }
    //何のアイテムが使用されるか
    public void UseItem(string item){
        switch(item){
            case "Shield_Dev(Clone)":
                protect = true;
                Audio_Man.ActiveShield();
                break;
            case "Brake_Dev(Clone)":
                prespeed = speed;
                effectTime = 3;
                brake = true;
                Audio_Man.ActiveBrake();
                break;
            case "Warp_Dev(Clone)":
                GetComponent<SphereCollider>().enabled = false;
                effectTime = 3;
                warp = true;
                Audio_Man.ActiveWarp();
                break;
        }
        itemName = "null";
    }
    //能力実装部分
    public void UseAbility(){
        //Shieldの場合
        if(protectBreak){
            effectTime -= Time.deltaTime;
            if(effectTime < 0){
                effectTime = 1;
                GetComponent<SphereCollider>().enabled = true;
                protectBreak = false;
            }
        }
        //Brakeの場合
        if(brake){
            effectTime -= Time.deltaTime;
            speed = 0.1f;
            if(effectTime < 0){
                effectTime = 1;
                brake = false;
                speed = prespeed;
            }
        }
        //Warpの場合
        if(warp){
            tf.Translate(new Vector3(0,0,speed*5));
            effectTime -= Time.deltaTime;
            if(effectTime < 0){
                effectTime = 1;
                GetComponent<SphereCollider>().enabled = true;
                warp = false;
            }
        }
        //Blindの場合
        if(blind){
            effectTime -= Time.deltaTime;
            if(effectTime < 0){
                effectTime = 1;
                blind = false;
            }
        }
        //何の能力も発動していないなら...
        if(!protect && !brake && !warp && !blind){
            isUse = false;
        }
    }

    //ゲームオーバー&アイテム取得判定
    void OnCollisionEnter(Collision collision){
        //当たったものが...
        switch(collision.gameObject.tag){
            //アイテムの場合
            case "Item":
                break;
            //Blinderの場合
            case "Blinder":
                blind = true;
                isUse = true;
                Audio_Man.ActiveBlinder();
                effectTime = 3;
                break;
            //それ以外の場合
            default:
                //Shieldが発動しているなら...
                if(protect){
                    protect = false;
                    protectBreak = true;
                    GetComponent<SphereCollider>().enabled = false;
                    break;
                }
                Audio_Man.Crash();
                StartCoroutine(Panel_Con.GameOver());
                start = false;
                StopCoroutine(Panel_Con.GameOver());
                break;
        }
    }
}

/*
    移動機能（旧式）
    if(direction == "up"){              //上昇
        tf.Translate(new Vector3(0,0.1f,0));
        if(tf.position.y >= targetpos){
            direction = "null";
        }
    }else if(direction == "down"){      //下降
        tf.Translate(new Vector3(0,-0.1f,0));
        if(tf.position.y <= targetpos){
            direction = "null";
        }
    }else if(direction == "left"){　    //左移動
        tf.Translate(new Vector3(-0.1f,0,0));
        if(tf.position.x <= targetpos){
            direction = "null";
        }
    }else if(direction == "right"){     //右移動
        tf.Translate(new Vector3(0.1f,0,0));
        if(tf.position.x >= targetpos){
            direction = "null";
        }
    }

    入力キー取得(旧式)
    if(Input.GetKeyDown("w")){          //上昇
        if(tf.position.y < 3.3){
            direction = "up";
            targetpos = tf.position.y + 1;
            }
    }else if(Input.GetKeyDown("s")){    //下降
        if(tf.position.y > 1.6){
            direction = "down";
            targetpos = tf.position.y - 1;
        }
    }else if(Input.GetKeyDown("a")){    //左移動
        if(tf.position.x > -1){
            direction = "left";
            targetpos = tf.position.x - 1f;
        }
    }else if(Input.GetKeyDown("d")){    //右移動
        if(tf.position.x < 1){
            direction = "right";
            targetpos = tf.position.x + 1;
        }
    }else if(Input.GetKeyDown(KeyCode.Space)){  //アイテム使用
        if(itemName != "null"){
            UseItem(itemName);
            isUse = true;
        } 
    }*/