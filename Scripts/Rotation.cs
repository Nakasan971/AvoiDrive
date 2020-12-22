using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============================
    アイテム&ギミック用スクリプト
===============================*/

public class Rotation : MonoBehaviour
{
    public float x;
    public float y;
    public float z;
    public bool rotate_left; //左回転
    public bool rotate_right;//右回転

    void Start(){
        if(rotate_left){
            x = 0.5f;
        }else if(rotate_right){
            y = 0.5f;
        }
    }
    void Update()
    {
        transform.Rotate(new Vector3(x,y,z));
    }
}
