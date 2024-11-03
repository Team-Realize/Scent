using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoFocusOnEnable : MonoBehaviour
{
    public TMP_InputField inputField; // Input Field 참조
    public Button targetButton;       // 눌러야 하는 버튼 참조

    // 오브젝트가 활성화될 때 자동으로 호출되는 메서드
    void OnEnable()
    {
        // Input Field에 포커스를 설정
        inputField.Select();
        inputField.ActivateInputField();
    }

    void Start()
    {
        // Input Field의 onSubmit 이벤트에 함수 연결
        inputField.onSubmit.AddListener(OnSubmit);
    }

    // 사용자가 Enter 키를 누르면 호출되는 메서드
    private void OnSubmit(string text)
    {
        // 지정된 버튼 클릭 시뮬레이션
        targetButton.onClick.Invoke();
    }

    // 오브젝트가 비활성화되면 이벤트 연결 해제
    private void OnDestroy()
    {
        inputField.onSubmit.RemoveListener(OnSubmit);
    }
}