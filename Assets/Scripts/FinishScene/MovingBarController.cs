using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragToTransition : MonoBehaviour
{
    public string nextSceneName = "IntroScene";
    public Image fillImage; // Fill 오브젝트 연결

    Vector2 startPos;
    public float barWidth = 3000f; // 실제 길이보다 크게 해서 천천히 채워지게

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            fillImage.fillAmount = 0f; // 시작 시 초기화
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 currentPos = Input.mousePosition;
            float swipeLength = currentPos.x - startPos.x; // 스와이프 x 길이 계산
            fillImage.fillAmount = Mathf.Clamp01(swipeLength / barWidth); // 0~1로 변환

            if (fillImage.fillAmount >= 1f)
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            fillImage.fillAmount = 0.05f; // 손 떼면 리셋 -> 0이면 슬라이드 위치도 안 보여서 최소로 설정
        }
    }
}