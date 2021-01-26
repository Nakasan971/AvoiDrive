using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*==============================
    アイテム&ギミック用スクリプト
===============================*/
public class Movement_Dev : MonoBehaviour
{
    public float x;             //回転軸設定
    public float y;             //回転軸設定
    public float width = 2f;    //振り幅
    public float speed = 1.5f;  //移動速度
    public bool move_upDown;    //上下運動
    public bool move_leftRight; //左右運動
    public bool rotate_left;    //左回転
    public bool rotate_right;   //右回転
    Vector3 pos;

    void Start(){
        pos = transform.position;
        if(rotate_left){
            x = 0.5f;
        }else if(rotate_right){
            y = 0.5f;
        }
    }

    void Update(){
        if(move_upDown){
            transform.position = new Vector3(pos.x,Mathf.Sin(Time.time*speed)*width+pos.y,pos.z);
        }else if(move_leftRight){
            transform.position = new Vector3(Mathf.Sin(Time.time*speed)*width+pos.x,pos.y,pos.z);
        }else if(rotate_left || rotate_right){
            transform.Rotate(new Vector3(x,y,transform.position.z));
        }
    }
}
