using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*==============================
    全体管理
===============================*/
public class MainManager : MonoBehaviour
{
    float total;                        //確率合計値格納
    Transform cameraPos;                //カメラの位置
    GameObject stage;                   //リソース抽出用
    GameObject instance;                //インスタンス生成用
    List<GameObject> list = new List<GameObject>();//ステージ管理用
    Dictionary<int,string>stageInfo;    //ステージ辞書
    Dictionary<int,float>sponeInfo;     //確率辞書

    CameraController camera_con;
    
    //初期状態の宣言＆リスト化
    void Start()
    {
        total = 0;
        cameraPos            = GameObject.Find("Main Camera").GetComponent<Transform>();
        //初期配置のステージ
        GameObject [] stages =
        {GameObject.Find("StageBase"),GameObject.Find("StageBase1"),
        GameObject.Find("StageBase2"),GameObject.Find("StageBase3"),
        GameObject.Find("StageBase4"),GameObject.Find("StageBase5")};
        list.AddRange(stages);

        camera_con = GameObject.Find("Main Camera").GetComponent<CameraController>();
        InitializeDicts();
    }

    //プレイヤーより後ろのステージ削除
    //動的ステージ生成
    void Update()
    {
        if(list[0].transform.position.z <= cameraPos.position.z){
            Destroy(list[0]);
            int nextStage = Respone();
            stage = (GameObject)Resources.Load(stageInfo[nextStage]);
            instance = Instantiate(stage,new Vector3(0,0,cameraPos.position.z+35),Quaternion.identity);
            list.Add(instance);
            list.Remove(list[0]);
        }
    }
    //各ギミック生成（重み付き確立）
    int Respone(){
        float random = Random.value * total;
        //randomに該当するキーを返す
        foreach(KeyValuePair<int,float> elem in sponeInfo){
            if(random < elem.Value){
                return elem.Key;
            }else{
                random -= elem.Value;
            }
        }
        return 0;
    }
    //辞書初期化
    void InitializeDicts(){
        stageInfo = new Dictionary<int,string>();
        stageInfo.Add(0,"StageBase");  stageInfo.Add(1,"Stage1");
        stageInfo.Add(2,"Stage2");     stageInfo.Add(3,"Stage3");
        stageInfo.Add(4,"Stage4");     stageInfo.Add(5,"Stage5");
        stageInfo.Add(6,"Stage6");     stageInfo.Add(7,"Stage7");
        stageInfo.Add(8,"Stage8");     stageInfo.Add(9,"Stage9");
        stageInfo.Add(10,"Stage10");   stageInfo.Add(11,"Stage11");
        stageInfo.Add(12,"Stage12");   stageInfo.Add(13,"Stage13");
        stageInfo.Add(14,"Stage14");   stageInfo.Add(15,"Stage15");
        stageInfo.Add(16,"blindStage");stageInfo.Add(17,"itemStage");

        sponeInfo = new Dictionary<int,float>();
        sponeInfo.Add(0,49.5f); sponeInfo.Add(1,3.0f);
        sponeInfo.Add(2,3.0f); sponeInfo.Add(3,3.0f);
        sponeInfo.Add(4,3.0f); sponeInfo.Add(5,3.0f);
        sponeInfo.Add(6,3.0f); sponeInfo.Add(7,3.0f);
        sponeInfo.Add(8,3.0f); sponeInfo.Add(9,3.0f);
        sponeInfo.Add(10,3.0f);sponeInfo.Add(11,3.0f);
        sponeInfo.Add(12,3.0f);sponeInfo.Add(13,3.0f);
        sponeInfo.Add(14,3.0f);sponeInfo.Add(15,3.0f);
        sponeInfo.Add(16,2.5f);sponeInfo.Add(17,3.0f);

        foreach(KeyValuePair<int,float> elem in sponeInfo){
            total += elem.Value;
        }
        Debug.Log(total);
    }
    
    //ギミック生成（旧式3）
    /*int random = (int)Random.Range(0,(3*comera_con.difficulty-1)+7);
        Debug.Log(comera_con.difficulty);
        if(random >= (3*comera_con.difficulty-1)){
            stage = (GameObject)Resources.Load("StageBase");
            random = (int)Random.Range(1,100);
            if(random%20 == 0){
               stage = (GameObject)Resources.Load(randomStage[17].name);
            }
        }else{
            stage = (GameObject)Resources.Load(randomStage[random].name);
        }
        instance = Instantiate(stage,new Vector3(0,0,cameraPos.position.z+35),Quaternion.identity);
        list.Add(instance);*/
    //各ギミック生成(旧式２)
    //ステージ１７番（Blinder）のみ別途処理
    /*public void Respone(){
        int random = (int)Random.Range(0,randomStage.Length+10);
        if(random < randomStage.Length - 1){
            stage = (GameObject)Resources.Load(randomStage[random].name);
            instance = Instantiate(stage,new Vector3(0,0,cameraPos.position.z+35),Quaternion.identity);
            list.Add(instance);
        }else if(random == 17){
            random = (int)Random.Range(1,10);
            if(random%2 == 0){
               stage = (GameObject)Resources.Load(randomStage[17].name);
                instance = Instantiate(stage,new Vector3(0,0,cameraPos.position.z+35),Quaternion.identity);
                list.Add(instance); 
            }else{
               stage = (GameObject)Resources.Load("StageBase");
                instance = Instantiate(stage,new Vector3(0,0,cameraPos.position.z+35),Quaternion.identity);
                list.Add(instance); 
            }
        }else{
            stage = (GameObject)Resources.Load("StageBase");
            instance = Instantiate(stage,new Vector3(0,0,cameraPos.position.z+35),Quaternion.identity);
            list.Add(instance);
        }
    }

    //各ギミック生成(旧式)
    void Respone(){
        int random = (int)Random.Range(0,20.0f);
        switch(random){
            case 0:
                stage = (GameObject)Resources.Load("StageBase");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 1:
                stage = (GameObject)Resources.Load("Stage1");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 2:
                stage = (GameObject)Resources.Load("Stage2");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 3:
                stage = (GameObject)Resources.Load("Stage3");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 4:
                stage = (GameObject)Resources.Load("Stage4");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 5:
                stage = (GameObject)Resources.Load("Stage5");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 6:
                stage = (GameObject)Resources.Load("Stage6");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 7:
                stage = (GameObject)Resources.Load("Stage7");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 8:
                stage = (GameObject)Resources.Load("Stage8");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 9:
                stage = (GameObject)Resources.Load("Stage9");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 10:
                stage = (GameObject)Resources.Load("Stage10");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            case 11:
                stage = (GameObject)Resources.Load("Stage11");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
            default:
                stage = (GameObject)Resources.Load("StageBase");
                instance = Instantiate(stage,new Vector3(0,0,camera.position.z+35),Quaternion.identity);
                list.Add(instance);
                break;
        }
    }*/
}
