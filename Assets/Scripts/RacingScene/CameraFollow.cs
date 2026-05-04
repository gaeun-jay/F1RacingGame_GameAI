using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f; // 카메라 따라가는거 스무스하게 하기 위함

    void Start()
    {
        string driver = GameData.selectedDriver;

        if (driver == "Lewis" || driver == "Charles")
            target = GameObject.Find("Ferrari").transform;
        else if (driver == "Max")
            target = GameObject.Find("Red Bull").transform;
        else if (driver == "Lando")
            target = GameObject.Find("McLaren").transform;
        else if (driver == "Carlos")
            target = GameObject.Find("Williams").transform;
    }

    void LateUpdate()
    {
        if (target == null) return;
        Vector3 desired = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
    }
}