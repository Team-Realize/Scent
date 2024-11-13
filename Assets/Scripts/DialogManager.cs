using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject dialogPanel; // 다이얼로그 패널 (Panel)
    public GameObject object1;     // 첫 번째 오브젝트
    public GameObject object2;     // 두 번째 오브젝트
    
    public Color object1Color;     // 첫 번째 오브젝트에 적용할 색상

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
        
        // 첫 번째 오브젝트의 색상 변경
        Renderer object1Renderer = object1.GetComponent<Renderer>();
        if (object1Renderer != null)
        {
            object1Renderer.material.color = object1Color;
            Debug.Log("첫 번째 오브젝트 색상 변경됨");
        }
        else
        {
            Debug.LogWarning("첫 번째 오브젝트에 Renderer가 없습니다.");
        }


        Debug.Log("다이얼로그 닫힘, 두 오브젝트 활성화");
    }
}