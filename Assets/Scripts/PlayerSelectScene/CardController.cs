using UnityEngine;
using UnityEngine.SceneManagement;

public class CardController : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    void OnMouseEnter()
    {
        // 마우스 올리면 BGM 재생
        this.audioSource.Play();
        this.transform.localScale = new Vector2(1.1f, 1.1f);

    }

    void OnMouseExit()
    {
        // 마우스 나가면 BGM 정지
        this.audioSource.Stop();
        this.transform.localScale = new Vector2(1f, 1f);
    }

    // CardController.cs 에서 저장
    void OnMouseDown() // GameData public 값이라 같은 프젝 안에 있으면 자동 인식
    {
        // OnMouseDown은 좌측 마우스 버튼(0)에만 반응함 -> 카드 덱 이동 수정   
        GameData.selectedDriver = this.gameObject.name; // GameData는 드라이버 선택값 저장하기 위해 만든 스크립트임
        // Debug.Log("선택된 드라이버: " + GameData.selectedDriver);
        SceneManager.LoadScene("StartScene");

    }
    // Update에서 Input.GetMouseButtonDown(0) 쓰면 화면 어디를 클릭해도 전부 반응해서 카드 5개에 스크립트가 붙어있으면 5개 전부 동시에 실행돼버림
}