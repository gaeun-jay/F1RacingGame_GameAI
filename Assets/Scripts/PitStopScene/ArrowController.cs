using UnityEngine;
public class ArrowController : MonoBehaviour // right arrow 선택시 피트스탑 안내 화면 및 차량 사라짐
{
    GameObject pitStop1; 
    GameObject car;

    void Start()
    {
        this.pitStop1 = GameObject.Find("PitStop_1"); 

        string driver = GameData.selectedDriver;
        // 드라이버에 따른 obj 선택
        if (driver == "Lewis" || driver == "Charles")
            this.car = GameObject.Find("FCar");
        else if (driver == "Max")
            this.car = GameObject.Find("RCar");
        else if (driver == "Lando")
            this.car = GameObject.Find("MCar");
        else if (driver == "Carlos")
            this.car = GameObject.Find("WCar");
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Destroy(this.pitStop1);
            Destroy(gameObject); // right arrow
            Destroy(this.car);
        }
    }
}