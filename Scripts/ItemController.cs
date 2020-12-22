using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*==============================
    アイテム用スクリプト
===============================*/

public class ItemController : MonoBehaviour
{
    CameraController camera_con;
    void Start(){
        camera_con = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    void OnCollisionEnter(Collision collision){
        camera_con.itemName = this.gameObject.name;
        Destroy(this.gameObject);
    }
}
