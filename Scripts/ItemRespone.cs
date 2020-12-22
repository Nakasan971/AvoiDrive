using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*==============================
    アイテム用スクリプト（itemStage）
===============================*/
public class ItemRespone : MonoBehaviour
{
    //出現アイテムのランダム化を図る
    public GameObject[] randomItem;
    GameObject item;
    GameObject instance;
    void Start()
    {
        int random                  = (int)Random.Range(0,randomItem.Length);
        item                        = (GameObject)Resources.Load(randomItem[random].name);
        instance                    = Instantiate(item,
                                        new Vector3(0,2.5f,transform.position.z),Quaternion.identity);
        instance.transform.parent   = transform;
    }
}
