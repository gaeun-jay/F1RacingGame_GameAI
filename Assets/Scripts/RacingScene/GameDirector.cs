using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public static GameDirector Instance;
    public int totalLaps = 3;       // 총 바퀴 수
    int currentLap = 0;             // 현재 바퀴 수
    bool checkpointPassed = false;  // 체크포인트 통과 여부 (역주행 방지)
    bool isFromPitStop = false;     // 피트스톱에서 복귀했는지 여부
    Vector3 pitStopExitPos = new Vector3(-0.96f, -2.63f, 0f); // 피트스톱 출구 위치
    GameObject LapTime;             // 랩타임 UI
    public float gameTime = 0;
    GameObject Ferrari;
    GameObject McLaren;
    GameObject RedBull;
    GameObject Williams;
    public string selectedTireType = ""; // 선택된 타이어 종류 저장

    // 드라이버별 BGM
    public AudioClip lewisBGM;   // Inspector에서 연결
    public AudioClip maxBGM;
    public AudioClip carlosBGM;
    public AudioClip charlesBGM;
    public AudioClip landoBGM;

    private AudioSource audioSource; // BGM 재생용

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환해도 유지
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
        SceneManager.sceneLoaded += OnSceneLoaded;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // BGM 반복 재생
    }

    void Start()
    {
        // 비워둠 - 차량 관련 로직은 OnSceneLoaded에서 처리
    }

    void Update()
    {
        gameTime += Time.deltaTime;
        if (this.LapTime != null)
        {
            this.LapTime.GetComponent<TMPro.TextMeshProUGUI>().text =
                currentLap + " Lap(s)\n" + "Time : " + gameTime.ToString("F2") + "s";
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.LapTime = GameObject.Find("LapTime"); // UI 재연결

        if (scene.name == "RacingScene")
        {
            // 차량 찾기 및 제어는 RacingScene 로드될 때만 실행
            string driver = GameData.selectedDriver; // player select 씬에서 선택된 플레이어
            // Lewis, Max, Carlos, Charles, Lando 있음

            this.Ferrari = GameObject.Find("Ferrari");
            this.McLaren = GameObject.Find("McLaren");
            this.RedBull = GameObject.Find("Red Bull");
            this.Williams = GameObject.Find("Williams");

            if (driver == "Lewis")
            {
                Destroy(this.McLaren);
                Destroy(this.RedBull);
                Destroy(this.Williams);
            }
            else if (driver == "Max")
            {
                Destroy(this.McLaren);
                Destroy(this.Ferrari);
                Destroy(this.Williams);
            }
            else if (driver == "Carlos")
            {
                Destroy(this.McLaren);
                Destroy(this.RedBull);
                Destroy(this.Ferrari);
            }
            else if (driver == "Charles")
            {
                Destroy(this.McLaren);
                Destroy(this.RedBull);
                Destroy(this.Williams);
            }
            else if (driver == "Lando")
            {
                Destroy(this.Ferrari);
                Destroy(this.RedBull);
                Destroy(this.Williams);
            }

            // 드라이버별 BGM 재생
            AudioClip clip = driver switch
            {
                "Lewis"   => lewisBGM,
                "Max"     => maxBGM,
                "Carlos"  => carlosBGM,
                "Charles" => charlesBGM,
                "Lando"   => landoBGM,
                _ => null
            };

            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play(); // BGM 재생
            }

            if (isFromPitStop) // 피트스톱에서 복귀한 경우에만 출구 위치로 이동
            {
                isFromPitStop = false; // 플래그 초기화
                StartCoroutine(SetPitStopExitPosition()); // ← 코루틴으로 변경 (한 프레임 대기 후 위치 설정)
            }
        }
        else if (scene.name == "PitStopScene")
        {
            audioSource.Pause(); // 피트스톱 씬에서 BGM 일시정지
        }
        else if (scene.name == "FinishScene")
        {
            audioSource.Stop(); // 게임종료 시 BGM 완전 정지
        }
    }

    // 피트스톱 복귀 시 출구 위치로 이동하는 코루틴
    // OnSceneLoaded에서 바로 위치를 설정하면 차량 Start()가 그 다음 프레임에 실행되면서
    // Inspector에 설정된 원래 위치로 덮어씌워지는 문제가 있어서 한 프레임 대기 후 설정함
    IEnumerator SetPitStopExitPosition()
    {
        yield return null; // 한 프레임 대기 → 차량 Start() 완료 후 위치 덮어쓰기
        GameObject car = GameObject.FindWithTag("Player");
        if (car != null)
        {
            car.transform.position = pitStopExitPos;
            // Rigidbody2D 속도 초기화 → 피트스톱 진입 전 속도가 그대로 남아있는 현상 방지
            Rigidbody2D rb = car.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }

    public void OnCheckpointPassed()
    {
        checkpointPassed = true;
        //Debug.Log("체크포인트 통과함"); 디버깅용 코드
    }

    public void OnFinishLinePassed()
    {
        if (!checkpointPassed) return; // 체크포인트 안 통과하면 무시
        checkpointPassed = false;
        currentLap++;
        Debug.Log($"Lap: {currentLap} / {totalLaps}");
        if (currentLap >= totalLaps)
        {
            SceneManager.LoadScene("FinishScene");
        }
    }

    public void OnPitStopPointPassed()
    {
        isFromPitStop = true; // 피트스톱 진입 플래그 설정
        SceneManager.LoadScene("PitStopScene");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 메모리 누수 방지(원래 안 넣었었는데 이상한 경고 떠서? AI에게 질문 후 추가)
    }
}