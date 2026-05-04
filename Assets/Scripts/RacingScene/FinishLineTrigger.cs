using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // 디버깅용 코드 (코드 상엔 문제 없었고 collider를 3d로 설정해서 인식이 안되는 것이었음)
        //Debug.Log("트리거 감지됨 ->  태그: " + other.tag); 
        // Player 태그가 아니면 무시 -> 혹시 다른 오브젝트 감지될 수도 있어서 해당 조건으로 바꿈
        if (!other.CompareTag("Player")) return; 
    
        // other가 누군지 이미 알고 있음 → CompareTag로 태그만 확인하면 끝나서
        // 굳이 find 쓸 필요 없이 지금 compareTag 쓰는게 맞다 함

        // 충돌 감지만 하고 로직은 GameDirector에 넘김
        GameDirector.Instance.OnFinishLinePassed();
    }
}