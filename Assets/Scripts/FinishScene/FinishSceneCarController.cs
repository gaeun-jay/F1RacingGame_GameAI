using UnityEngine;

public class FinishSceneCarController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // 들어오는 차 표현
        if(transform.position.x > 0.0f) // 피트스탑보다 좀 더 빠르게
        {
            transform.Translate(-70 * Time.deltaTime, 0, 0);
        }
    }
}
