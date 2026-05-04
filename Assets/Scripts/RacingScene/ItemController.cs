using UnityEngine;

public class ItemController : MonoBehaviour
{
    Vector3 startPos;
    float scaleMin = 0.1f;
    float scaleMax = 0.24f; // 스케일(아이템 커지는 최대값) 고정값 사용
    float moveSpeed = 2f;
    float rotateSpeed = 180f;
    float scaleSpeed = 2f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        // x, y 왔다갔다 설정하였는데 너무 부자연스러워서 sin파로 하면 자연스럽다 하여 추가함
        float offset = Mathf.Sin(Time.time * moveSpeed) * 0.2f; // 0.2f가 이동범위임
        transform.position = startPos + new Vector3(offset, -offset, 0);

        // startScale 대신 고정값 scaleMax 사용
        float scaleValue = Mathf.Abs(Mathf.Sin(Time.time * scaleSpeed));
        float s = Mathf.Lerp(scaleMin, scaleMax, scaleValue);
        transform.localScale = new Vector3(s, s, s);
    }
}