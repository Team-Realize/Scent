using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel; // 다이얼로그 패널 (Panel)

    void Start()
    {
        // 시작할 때 다이얼로그를 비활성화
        dialogPanel.SetActive(false);
    }

    // 다이얼로그 열기 함수
    public void OpenDialog()
    {
        dialogPanel.SetActive(true);
        Debug.Log("dssd");
    }

    // 다이얼로그 닫기 함수
    public void CloseDialog()
    {
        dialogPanel.SetActive(false);
    }
}