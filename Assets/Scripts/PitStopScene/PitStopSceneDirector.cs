using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PitStopSceneDirector : MonoBehaviour
{
    public GameObject frontTire; // 앞바퀴 prefab 인스턴스
    public GameObject rearTire;  // 뒷바퀴 prefab 인스턴스

    public Image frontGauge; // 앞바퀴 장착률 게이지 바
    public Image rearGauge;  // 앞바퀴 장착률 게이지 바

    public bool isFrontDone = false; // 앞바퀴 장착 완료 여부
    public bool isRearDone = false;  // 뒷바퀴 장착 완료 여부

    float holdTime = 0f;    // 가운데 버튼 누른 시간
    float requiredTime = 5f; // 장착 시간(5초 동안 마우스 가운데 버튼 누르면 장착되도록 설정)
    
    public string selectedTireType = ""; // 선택된 타이어 종류 저장 -> 앞 뒤 서로 다른 타이어 장착하면 안되니까

    GameObject FCar; // 페라리
    GameObject MCar; // 맥라렌
    GameObject RCar; // 레드불
    GameObject WCar; // 윌리엄스
    GameObject FCar_noT; // 페라리 타이어 x
    GameObject MCar_noT; // 맥라렌 타이어 x
    GameObject RCar_noT; // 레드불 타이어 x
    GameObject WCar_noT; // 윌리엄스 타이어 x

    void Start() // obj 다 있는 상태에서 player 선택값에 따라 해당 팀 차량만 남기고 destroy
    {
        frontGauge.fillAmount = 0f;
        rearGauge.fillAmount = 0f;
        string driver = GameData.selectedDriver; // player select 씬에서 선택된 플레이어
        // Lewis, Max, Carlos, Charles, Lando 있음

        this.FCar = GameObject.Find("FCar");
        this.MCar = GameObject.Find("MCar");
        this.RCar = GameObject.Find("RCar");
        this.WCar = GameObject.Find("WCar");

        this.FCar_noT = GameObject.Find("FCar_noT");
        this.MCar_noT = GameObject.Find("MCar_noT");
        this.RCar_noT = GameObject.Find("RCar_noT");
        this.WCar_noT = GameObject.Find("WCar_noT");
        
        if (driver == "Lewis") 
        {
            Destroy(this.MCar);
            Destroy(this.MCar_noT);
            Destroy(this.RCar);
            Destroy(this.RCar_noT);
            Destroy(this.WCar);
            Destroy(this.WCar_noT);
        }
        else if (driver == "Max") 
        {
            Destroy(this.MCar);
            Destroy(this.MCar_noT);
            Destroy(this.FCar);
            Destroy(this.FCar_noT);
            Destroy(this.WCar);
            Destroy(this.WCar_noT); 
        }
        else if (driver == "Carlos") 
        {
            Destroy(this.MCar);
            Destroy(this.MCar_noT);
            Destroy(this.RCar);
            Destroy(this.RCar_noT);
            Destroy(this.FCar);
            Destroy(this.FCar_noT); 
        }
        else if (driver == "Charles") 
        {
            Destroy(this.MCar);
            Destroy(this.MCar_noT);
            Destroy(this.RCar);
            Destroy(this.RCar_noT);
            Destroy(this.WCar);
            Destroy(this.WCar_noT); 
        }
        else if (driver == "Lando") 
        {
            Destroy(this.FCar);
            Destroy(this.FCar_noT);
            Destroy(this.RCar);
            Destroy(this.RCar_noT);
            Destroy(this.WCar);
            Destroy(this.WCar_noT); 
        }

    }

    void Update()
    {
        // 앞바퀴 작업 중
        if (frontTire != null && !isFrontDone)
        {
            HandleTireInstall(frontTire, frontGauge, ref isFrontDone);
        }
        // 뒷바퀴 작업 중
        else if (rearTire != null && !isRearDone)
        {
            HandleTireInstall(rearTire, rearGauge, ref isRearDone);
        }

        // 둘 다 완료 후 스페이스바 누르면 씬 전환
        if (isFrontDone && isRearDone && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("RacingScene"); 
        }
    }

    void HandleTireInstall(GameObject tire, Image gauge, ref bool isDone)
    {
        if (Input.GetMouseButton(2)) // 가운데 버튼 누르는 동안
        {
            holdTime += Time.deltaTime;

            // Z축 회전
            tire.transform.Rotate(0f, 0f, -720f * Time.deltaTime);

            // 게이지 업데이트 (0~1)
            gauge.fillAmount = holdTime / requiredTime;

            if (holdTime >= requiredTime) // 5초 달성
            {
                holdTime = 0f;
                gauge.fillAmount = 1f;
                isDone = true; // 장착 완료
                tire.transform.rotation = Quaternion.identity; // 회전 멈춤(유니티 내부적으로 회전을 표현하는 함수로 identiry -> 최전값 0으로)
                // Debug.Log("타이어 장착"); 디버깅용 코드
            }
        }
        else // 버튼 떼면 회전 멈춤(5초 되기 전에 손 떼면 다시 원래대로 되도록 설정하기 위함)
        {
            tire.transform.rotation = Quaternion.identity;
            holdTime = 0f; 
            gauge.fillAmount = 0f;   // 게이지 바 초기화
        }
    }
}