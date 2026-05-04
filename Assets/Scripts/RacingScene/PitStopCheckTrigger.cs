using UnityEngine;

public class PitStopCheckTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Trigger 판정만 하고 labs 같은건 Director 에서 관장
        GameDirector.Instance.OnPitStopPointPassed();
    }
}
