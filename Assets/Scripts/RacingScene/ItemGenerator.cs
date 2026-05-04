using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGenerator : MonoBehaviour
{
    public GameObject item_plus_prefab;
    public GameObject item_minus_prefab;

    float span = 5.0f; // 2초당 한 세트 생성
    float delta = 0;

    Vector3[][] spawnSets = new Vector3[][] // 정해놓은 위치 중에 random으로 item 생성하기 위해 다차원 벡터 생성
    {
        new Vector3[] { new Vector3(-0.96f, 23.17f, 0.01f),   new Vector3(-0.86f, -24.57f, 0.07f) },
        new Vector3[] { new Vector3(-19.5f, 24.16f, -1.089f), new Vector3(-17.9f, 24.24f, -1.017f) },
        new Vector3[] { new Vector3(2.62f, 33.6f, 0.35f),     new Vector3(1.53f, 32.66f, 0.35f) },
        new Vector3[] { new Vector3(16.4f, 1.6f, 0.71f),      new Vector3(17.9f, 1.7f, 0.814f) },
        new Vector3[] { new Vector3(-0.79f, 24.63f, 0.07f),   new Vector3(-0.96f, 23.17f, 0.01f) },
        new Vector3[] { new Vector3(-18.88f, 19.35f, 0.73f),  new Vector3(-17.76f, 20.29f, 0.87f) },
        new Vector3[] { new Vector3(2.51f, 12.37f, 4.76f),    new Vector3(3.9f, 12.01f, 5.04f) }
    };

    void Start()
    {
    }

    void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;

            int setIndex = Random.Range(0, spawnSets.Length);
            Vector3[] selectedSet = spawnSets[setIndex];

            GameObject plus = Instantiate(item_plus_prefab) as GameObject;
            plus.transform.position = selectedSet[0];

            GameObject minus = Instantiate(item_minus_prefab) as GameObject;
            minus.transform.position = selectedSet[1];
        }
    }
}