using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    // 헤더 추가함 -> 자동차는 개별적으로 설정할 + inspector에서 만질 수 있는 설정들이 많아서 헷갈리는데 이때 header로 한 번 더 표기해주면 제목처럼 떠서 구분하기 좋음
    [Header("후진 및 회전 속도 설정")] // 기본값이라서 inspector에서 바꿀 수 있음 inspector 우선순위가 더 높음
    public float reverseSpeed = 6f;
    public float rotateSpeed = 150f;

    [Header("전진 속도 설정")] // 기본값이라서 inspector에서 바꿀 수 있음 inspector 우선순위가 더 높음
    public float minSpeed = 1.0f; // 최소 속도(처음 출발 속도)
    public float maxSpeed = 10.0f; // 최대 속도(car ani가 불꽃으로 변하는 속도)
    public float speedStep = 1.0f; // 속도 증가량
    public float timePerStep = 1f; // 키보드 입력 누적 1초마다 속도 증가하게 설정함

    Rigidbody2D rigid2D; // Physics(물체에 작용하는 중력이나 마찰 등의 힘 계산 가능 -> 물리적 동작 자연스러움)
    float currentSpeed; // 현재 속도(디버깅용)
    float holdTime = 0f; // 위쪽 화살표 방향키 누른 시간

    // Car 애니메이션 설정용 변수(스프라이트 애니메이션)
    public Sprite straightSprites;
    public Sprite speedySprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    SpriteRenderer spriteRenderer; // 스프라이트 애니메이션 클래스?

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        this.currentSpeed = minSpeed;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float v = 0f; // 직진, 후진 제어 변수
        float h = 0f; // 회전 제어 변수

        // 키보드 입력에 따른 자동차 움직임 제어 -> down으로 제어하면 움직임이 자연스럽지 않아 getkey로 하는게 더 자연스럽다고 하여 변경함
        if (Input.GetKey(KeyCode.UpArrow))    v = 1f; // 직진 시 양수
        if (Input.GetKey(KeyCode.DownArrow))  v = -1f; // 후진 시 음수
        if (Input.GetKey(KeyCode.LeftArrow))  h = -1f; // 왼쪽 회전 시 음수
        if (Input.GetKey(KeyCode.RightArrow)) h = 1f; // 오른쪽 회전

        if (v > 0) // 위쪽 화살표 방향키를 눌렀을 때
        {
            holdTime += Time.deltaTime; // 위쪽 화살표 방향키 누르고 있는 시간 누적

            int step = (int)(holdTime / timePerStep); // 속도를 얼마나 눌러야 하는지 계속 계산(step 계산))
            float newSpeed = Mathf.Min(minSpeed + step * speedStep, maxSpeed);
            // 최소 속도부터 2씩 증가 1->2->3 ... (최대 maxSpeed까지 증가하게끔 설정)

            /*if (newSpeed != this.this.currentSpeed)
            {
                Debug.Log("현재 속도: " + this.this.currentSpeed);
            } 디버깅용 코드*/
            this.currentSpeed = newSpeed;
        }
        else // 위쪽 방향키에서 손 뗐을 때
        {
            holdTime = 0f;  // 누적시간 초기화
            this.currentSpeed = minSpeed; // 자동차 속도 최소 속도로 변경
        }

        // 차량 이동 및 회전 
        float speed = v > 0 ? this.currentSpeed : reverseSpeed;
        // 전진이면(v>0) 현재 스피드 사용하고 아니면(v<=0) 후진 스피드(고정값) 사용하도록 설정

        rigid2D.linearVelocity = transform.up * v * speed;
        // 차가 바라보는 방향 기준으로 속도값)

        if (Mathf.Abs(v) > 0.1f)
            rigid2D.angularVelocity = -h * rotateSpeed * Mathf.Sign(v);
            // 후진 시 핸들 방향 반전되게 해서 뒤로 가게끔(바라보는 방향)
        else
            rigid2D.angularVelocity = 0f;


        // Car 애니메이션 관련 설정(일반, 회전, 속력)
        if(this.currentSpeed == 10)
        {
            this.spriteRenderer.sprite = this.speedySprite;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            this.spriteRenderer.sprite = this.leftSprite;
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            this.spriteRenderer.sprite = this.rightSprite;
        }
        else
        {
            this.spriteRenderer.sprite = this.straightSprites;
        }
    }

    // 아이템과 차 충돌 시 trigger 
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlusItem"))
        {
            Destroy(other.gameObject);
            this.holdTime = timePerStep * ((maxSpeed - minSpeed) / speedStep); // holdTime을 최대속도에 해당하는 값으로 설정
            // 처음에는 this.currentSpeed = maxSpeed; 와 같이 코드 작성하였는데 이렇게 작성하면
            // 매 프레임마다 update 문에서 덮어 버려서 holdTime(화살표 누적시간을 변경하는 코드로 바꿈)

        }

        else if(other.CompareTag("MinusItem"))
        {
            Destroy(other.gameObject);
            this.holdTime = 0f;// minus item 먹으면 최소 스피드로 변경됨
        }
    }
}
// Gravity Scale 0으로 설정해야 중력 영향 안 받음(1이 기본값인 듯 함)