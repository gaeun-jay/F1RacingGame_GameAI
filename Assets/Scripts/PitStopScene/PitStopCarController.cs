using UnityEngine;

public class PitStopCarController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // pit stop 들어오는 차 표현
        if(transform.position.x > -0.05f)
        {
            transform.Translate(-50 * Time.deltaTime, 0, 0);
        }
    }
}
