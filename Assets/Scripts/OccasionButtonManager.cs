using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OccasionButtonManager : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    private string activeButtonOutput;

    // 인덱스 0일 때 참조할 TMP_InputField
    public TMP_InputField specialInputField;

    private string[] predefinedStrings = {
        "사랑",
        "응원",
        "축하",
        "건강",
        "감사"
    };

    private int activeButtonIndex = -1;

    public void OnButtonClick(Button clickedButton)
    {
        foreach (Button button in buttons)
        {
            Outline outline = button.GetComponentInChildren<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }

        Outline clickedOutline = clickedButton.GetComponentInChildren<Outline>();
        if (clickedOutline != null)
        {
            clickedOutline.enabled = true;
            activeButtonIndex = buttons.IndexOf(clickedButton);
            if (activeButtonIndex != -1)
            {
                Debug.Log("Activated Occasion Button Index: " + activeButtonIndex);
                UpdateActiveButtonOutput();
                Debug.Log("Active Occasion Output: " + activeButtonOutput);
            }

            Text buttonText = clickedButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Selected";
                Debug.Log("Occasion Button Text Changed: " + buttonText.text);
            }
        }
    }

    // 인덱스에 따라 activeButtonOutput 설정
    public void UpdateActiveButtonOutput()
    {
        if (activeButtonIndex == 0 && specialInputField != null)
        {
            // 인덱스가 0일 때는 특정 InputField의 텍스트 사용, 비어 있으면 "랜덤 추천"
            if (string.IsNullOrWhiteSpace(specialInputField.text))
            {
                activeButtonOutput = "랜덤 추천";
            }
            else
            {
                activeButtonOutput = specialInputField.text;
            }
        }
        else if (activeButtonIndex >= 1 && activeButtonIndex <= predefinedStrings.Length)
        {
            activeButtonOutput = predefinedStrings[activeButtonIndex - 1];
        }
        else
        {
            activeButtonOutput = "No valid output";
        }
    }

    // 현재 활성화된 버튼의 출력 값을 반환하는 메서드
    public string GetActiveButtonOutput()
    {
        return activeButtonOutput;
    }
}
