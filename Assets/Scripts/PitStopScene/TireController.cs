using UnityEngine;

public class TireController : MonoBehaviour
{
    void OnMouseDown()
    {
        PitStopSceneDirector director = FindObjectOfType<PitStopSceneDirector>(); // director 에서 정의한 변수들 불러오기 위함
        TireGenerator generator = GetComponent<TireGenerator>(); // 같은 오브젝트에 붙어있으니까

        // 다른 종류 타이어 이미 선택했으면 무시
        if (director.selectedTireType != "" && director.selectedTireType != gameObject.name) return;

        // 앞바퀴 완료 전에 이미 타이어 선택했으면 무시
        if (!director.isFrontDone && director.frontTire != null) return;
        // 뒷바퀴 완료 전에 이미 타이어 선택했으면 무시
        if (director.isFrontDone && director.rearTire != null) return;

        // 첫 클릭 시 타이어 종류 저장
        if (director.selectedTireType == "")
            director.selectedTireType = gameObject.name;

        generator.SpawnTire(director); // 생성은 TireGenerator에 넘김
    }
}