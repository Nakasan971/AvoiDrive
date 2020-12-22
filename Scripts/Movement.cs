using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*==============================
    ギミック用スクリプト
===============================*/
public class Movement : MonoBehaviour
{
    public float width = 2f;//振り幅
    public float speed = 1.5f;//移動速度
    public bool move_upDown;//上下運動
    public bool move_leftRight;//左右運動
    Vector3 pos;

    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        if(move_upDown){
            transform.position = new Vector3(pos.x,Mathf.Sin(Time.time*speed)*width+pos.y,pos.z);
        }else if(move_leftRight){
            transform.position = new Vector3(Mathf.Sin(Time.time*speed)*width+pos.x,pos.y,pos.z);
        }
    }
}
