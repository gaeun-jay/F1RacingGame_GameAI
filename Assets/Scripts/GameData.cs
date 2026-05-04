using UnityEngine;

public class GameData
{
    public static string selectedDriver = ""; // Lewis, Max, Carlos, Charles, Lando만 선택 할 수 있음
}

// 동작을 하는 cs는 아니라서 다른 cs 와 다르게 behavior 없음
// 카드 셀렉부터, 피트스탑, 레이싱 씬까지 드라이버 선택에 따라 차량을 다르게
// 해 주어야 하기 때문에 전체적으로 접근 가능한 public 변수로 드라이버
// 선택값 저장