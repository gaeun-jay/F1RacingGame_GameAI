using UnityEngine;

public class CheckpointTrigger : MonoBehaviour // 피니시 라인만 설정하면 역주행시에도 lap update 돼서 체크포인트 하나 더 설정함
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameDirector.Instance.OnCheckpointPassed();
    }
}