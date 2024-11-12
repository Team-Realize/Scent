using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel; // 다이얼로그 패널 (Panel)
    public GameObject object1;     // 첫 번째 오브젝트
    public GameObject object2;     // 두 번째 오브젝트

    void Start()
    {
        // 시작할 때 다이얼로그를 비활성화
        dialogPanel.SetActive(false);
    }

    // 다이얼로그 열기 함수
    public void OpenDialog()
    {
        dialogPanel.SetActive(true);
        Debug.Log("다이얼로그 열림");
    }

    // 다이얼로그 닫기 함수
    public void CloseDialog()
    {
        // 다이얼로그 비활성화
        dialogPanel.SetActive(false);

        // 두 오브젝트를 활성화
        object1.SetActive(true);
        object2.SetActive(true);

        Debug.Log("다이얼로그 닫힘, 두 오브젝트 활성화");
    }
}