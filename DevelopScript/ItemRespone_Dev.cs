using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*==============================
    アイテム生成スクリプト（itemStage）
===============================*/
public class ItemRespone_Dev : MonoBehaviour
{
    //出現アイテムのランダム化を図る
    public GameObject[] randomItem; //アイテム格納
    GameObject item;                //アイテムリソース抽出
    GameObject instance;            //インスタンス生成用
    void Start(){
        int random = (int)Random.Range(0,randomItem.Length);
        item       = (GameObject)Resources.Load(randomItem[random].name);
        instance   = Instantiate(item,new Vector3(0,2.5f,transform.position.z),Quaternion.identity);

        instance.transform.parent   = transform;//ItemStageの子オブジェクトにする
    }
}
