using System.Collections;
using UnityEngine;
using TMPro;

public class StartSceneDirector : MonoBehaviour // 초반에 차 안에 앉아있는 시선에서 경기장 전체 둘러볼 수 있는 효과를 넣고 싶었음
{
    // 나레이션에 맞춰서 text UI 작성 및 화면 전환을 하려면 
    public GameObject racingStartBg; 
    public TMP_Text introText;

    public GameObject map;

    public float scaleDuration = 10.0f; // public로 설정해 놓으면 inspector에서 변경 가능함(코드는 초기값이라서 변경할 때 inspector에 적용해 줘야 함)

    public string fullText = "Formula 1 fans around the world welcome to the 75th anniversary of the Formula 1 World Championship";
    public float typingSpeed = 0.1f;

    Vector3 mapStartScale = Vector3.one;
    Vector3 mapEndScale = new Vector3(0.24f, 0.24f, 0.24f);

    void Start()
    {
        racingStartBg.SetActive(true); // 배경 활성화
        introText.text = ""; // 텍스트 초기화

        map.SetActive(false); // 맵 숨기기
        map.transform.localScale = mapStartScale; // 맵 크기 원래대로

        StartCoroutine(IntroSequence()); // 인트로 시작
    }

    IEnumerator IntroSequence() // 인트로 권장
    {
        StartCoroutine(TypeText(introText, fullText)); // 텍스트 타이핑 시작
        yield return new WaitForSeconds(12f); // 12초 동안 기다리기

        racingStartBg.SetActive(false); // 배경 숨기기
        introText.gameObject.SetActive(false); // 텍ㄱ스트 숨기기

        map.SetActive(true); // 맵 보이게 하기
        yield return StartCoroutine(ScaleDown(map, mapStartScale, mapEndScale, scaleDuration)); // 맵 축소시까지 기다리기

        // Debug.Log("맵 전환"); 디버깅용 코드
    }

    IEnumerator TypeText(TMP_Text textComp, string text)
    {
        textComp.text = "";
        foreach (char c in text) // typing 한 글자씩 되도록 순회해서 사용
        {
            textComp.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator ScaleDown(GameObject target, Vector3 from, Vector3 to, float duration)
    {
        // 맵 크기 서서히 줄이기 위한 함수
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float smooth = Mathf.SmoothStep(0f, 1f, t);
            target.transform.localScale = Vector3.Lerp(from, to, smooth);
            yield return null;
        }
        target.transform.localScale = to;
    }
}

// 원하는 효과를 내기 위해서는 corutineㅇ라는 기능?을 사용해야해서 AI의 도움을 받아 만듦
// 일반 함수의 경우 한 프레임에 작업을 다 실행하는데 coroutine을 쓰면 n초 기다렸다가 작업을 실행 가능함
// 따라서 이번 씬을 위해 해당 기능은 꼭 필요했기 때문에 이번 cs에서는 안 배운 기능을 사용할 수 밖에 없었음 