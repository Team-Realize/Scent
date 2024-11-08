using UnityEngine;
using TMPro;
using System.Collections;

public class DisplayResponseText : MonoBehaviour
{
    public TMP_Text responseText; // 텍스트를 표시할 TMP_Text 컴포넌트
    public float typingSpeed = 0.05f; // 한 글자씩 나타나는 속도 (초 단위)

    private void Start()
    {
        // ResponseDataManager에서 API 응답 내용을 가져와서 구성과 설명 부분만 추출
        string apiResponse = ResponseDataManager.Instance.GetApiResponseContent();
        string parsedResponse = ExtractCompositionAndDescription(apiResponse);
        
        Debug.Log(parsedResponse);
        
        // 코루틴 시작 - 한 글자씩 텍스트를 출력
        StartCoroutine(TypeText(parsedResponse));
    }

    // 텍스트를 한 글자씩 나타내는 코루틴
    private IEnumerator TypeText(string textToType)
    {
        responseText.text = ""; // 초기 텍스트를 빈 상태로 설정

        foreach (char letter in textToType)
        {
            responseText.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(typingSpeed); // 지정된 속도만큼 대기
        }
    }

    // 구성과 설명을 추출하는 메서드
    private string ExtractCompositionAndDescription(string response)
    {
        string composition = "";
        string description = "";

        int compositionIndex = response.IndexOf("구성:");
        int descriptionIndex = response.IndexOf("설명:");

        if (compositionIndex != -1)
        {
            int endIndex = response.IndexOf("\n", compositionIndex);
            composition = response.Substring(compositionIndex, endIndex - compositionIndex).Trim();
        }

        if (descriptionIndex != -1)
        {
            int endIndex = response.IndexOf("\n", descriptionIndex);
            description = response.Substring(descriptionIndex, endIndex - descriptionIndex).Trim();
        }

        return $"{composition}\n{description}";
    }
}