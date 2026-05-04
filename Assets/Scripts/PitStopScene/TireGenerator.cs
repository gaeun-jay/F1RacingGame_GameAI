using UnityEngine;

public class TireGenerator : MonoBehaviour
{
    public GameObject tirePrefab; // Inspector에서 prefab 연결

    // 앞바퀴, 뒷바퀴 위치
    Vector3 frontWheelPos = new Vector3(-4.64f, -2.7f, -1f);
    Vector3 rearWheelPos = new Vector3(6.3f, -2.7f, -1f);

    public void SpawnTire(PitStopSceneDirector director)
    {
        if (!director.isFrontDone && director.frontTire == null)
        {
            // 첫 번째 클릭 -> 앞바퀴 위치에 생성
            director.frontTire = Instantiate(tirePrefab, frontWheelPos, Quaternion.identity);
        }
        else if (director.isFrontDone && director.rearTire == null)
        {
            // 두 번째 클릭 -> 뒷바퀴 위치에 생성
            director.rearTire = Instantiate(tirePrefab, rearWheelPos, Quaternion.identity);
        }
    }
}