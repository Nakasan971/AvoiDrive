using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============================
    アイテム用スクリプト
===============================*/

public class ItemController_Dev : MonoBehaviour
{
    CameraController_Dev camera_con;    //アイテム名引き渡し用
    void Start(){
        camera_con = GameObject.Find("Main Camera").GetComponent<CameraController_Dev>();
    }
    //カメラが触れたら
    void OnCollisionEnter(Collision collision){
        camera_con.itemName = this.gameObject.name;
        Destroy(this.gameObject);
    }
}
