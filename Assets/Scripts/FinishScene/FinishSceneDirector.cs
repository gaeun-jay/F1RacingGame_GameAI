using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class FinishSceneDirector : MonoBehaviour // 게임 종료 Laps, Time 시각화
{
    GameObject gameResult_lap;
    GameObject gameResult_time;

    GameObject FCar; // 페라리
    GameObject MCar; // 맥라렌
    GameObject RCar; // 레드불
    GameObject WCar; // 윌리엄스

    void Start()
    {
        this.gameResult_lap = GameObject.Find("GameResult_Lap");
        this.gameResult_time = GameObject.Find("GameResult_Time");
        float totalTime = GameDirector.Instance.gameTime;
        int totalLaps = GameDirector.Instance.totalLaps;
        this.gameResult_lap.GetComponent<TMPro.TextMeshProUGUI>().text = "Total Lap(s)" + "\n" + totalLaps + " Lap(s)";
        this.gameResult_time.GetComponent<TMPro.TextMeshProUGUI>().text = "Total Game Time" + "\n" + totalTime.ToString("F2") + "s";

        string driver = GameData.selectedDriver; // player select 씬에서 선택된 플레이어
        
        this.FCar = GameObject.Find("FCar");
        this.MCar = GameObject.Find("MCar");
        this.RCar = GameObject.Find("RCar");
        this.WCar = GameObject.Find("WCar");
        
        if (driver == "Lewis") 
        {
            Destroy(this.MCar);
            Destroy(this.RCar);
            Destroy(this.WCar);
        }
        else if (driver == "Max") 
        {
            Destroy(this.MCar);
            Destroy(this.FCar);
            Destroy(this.WCar);
        }
        else if (driver == "Carlos") 
        {
            Destroy(this.MCar);
            Destroy(this.RCar);
            Destroy(this.FCar);
        }
        else if (driver == "Charles") 
        {
            Destroy(this.MCar);
            Destroy(this.RCar);
            Destroy(this.WCar);
        }
        else if (driver == "Lando") 
        {
            Destroy(this.FCar);
            Destroy(this.RCar);
            Destroy(this.WCar); 
        }
    }

    void Update()
    {
    }
}