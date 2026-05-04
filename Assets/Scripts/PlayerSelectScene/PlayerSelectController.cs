using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Scene 넘기는 역할 하기 위해 import

public class PlayerSelectController : MonoBehaviour
{
    GameObject playerCardGroup;

    void Start()
    {
        this.playerCardGroup = GameObject.Find("PlayerCardGroup");

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(2)) // 원래 down으로 썼는데 자연스러움이 좀 없어서 누르는 동안으로 수정함
        {
            if(this.playerCardGroup.transform.position.x < 0.2f) // 좌표 가늠해서 카드 덱 길이 만큼 이동하게 설정
            {
                this.playerCardGroup.transform.Translate(5 * Time.deltaTime, 0, 0);
            }
            // print(this.playerCardGroup.transform.position.x);
        }
        if(Input.GetMouseButton(1))
        {
            if(this.playerCardGroup.transform.position.x > -18f)
            {
                this.playerCardGroup.transform.Translate(-5 * Time.deltaTime, 0, 0);
            }
            // print(this.playerCardGroup.transform.position.x);
        }
    }
}
