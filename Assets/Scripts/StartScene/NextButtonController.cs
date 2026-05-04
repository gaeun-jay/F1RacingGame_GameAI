using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButtonController : MonoBehaviour
{

    void Start()
    {

    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("RacingScene"); // Racing Scene으로 전환
        }
    }
}