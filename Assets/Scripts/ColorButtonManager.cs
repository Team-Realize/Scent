using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ColorButtonManager : MonoBehaviour
{
    public List<Button> buttons = new List<Button>();
    private string activeButtonOutput;

    // 인덱스 0일 때 참조할 TMP_InputField
    public TMP_InputField specialInputField;

    private string[] predefinedStrings = {
        "붉은색,스칼렛",
        "노란색,옐로우",
        "초록색,그린",
        "보라색,라벤더",
        "분홍색,핑크"
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
                Debug.Log("Activated Color Button Index: " + activeButtonIndex);
                UpdateActiveButtonOutput();
                Debug.Log("Active Color Output: " + activeButtonOutput);
            }

            Text buttonText = clickedButton.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Selected";
                Debug.Log("Color Button Text Changed: " + buttonText.text);
            }
        }
    }

    public void UpdateActiveButtonOutput()
    {
        if (activeButtonIndex == 0 && specialInputField != null)
        {
            if (string.IsNullOrWhiteSpace(specialInputField.text))
            {
                activeButtonOutput = "랜덤 추천";
            }
            else
            {
                activeButtonOutput = specialInputField.text;
            }
        }
        else if (activeButtonIndex >= 1 && activeButtonIndex <= 5)
        {
            activeButtonOutput = predefinedStrings[activeButtonIndex - 1];
        }
        else
        {
            activeButtonOutput = "No valid output";
        }
    }

    public string GetActiveButtonOutput()
    {
        return activeButtonOutput;
    }
}
