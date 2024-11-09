using UnityEngine;
using System.Collections.Generic;
using System;

public class FlowerDisplayManager : MonoBehaviour
{
    public GameObject[] flowerObjects; // 각 인덱스에 해당하는 꽃 오브젝트를 설정합니다.

    // 색상 대응 사전 설정
    private Dictionary<string, Color> colorDictionary = new Dictionary<string, Color>
    {
        { "코랄레드", HexToColor("FF8B8B") },
        { "스칼렛레드", HexToColor("FF4040") },
        { "오렌지", HexToColor("FF9046") },
        { "노랑", HexToColor("FFE931") },
        { "파스텔그린", HexToColor("B4FF31") },
        { "하늘색", HexToColor("31CDFF") },
        { "라벤더색", HexToColor("CF86FF") },
        { "연핑크", HexToColor("FF7BB8") },
        { "흰색", HexToColor("FFFFFF") },
        { "민트", HexToColor("96FFF3") }
    };

    private void Start()
    {
        // ResponseDataManager에서 API 응답을 가져와 꽃 인덱스와 색상을 추출하고 해당 꽃을 표시 및 색상 변경
        string apiResponse = ResponseDataManager.Instance.GetApiResponseContent();
        int flowerIndex = ExtractFlowerIndex(apiResponse);
        string flowerColor = ExtractFlowerColor(apiResponse);
        
        DisplayFlowerObject(flowerIndex, flowerColor);
    }

    // 꽃 오브젝트를 활성화하고 색상을 적용하는 메서드
    public void DisplayFlowerObject(int index, string colorName)
    {
        // 모든 꽃 오브젝트를 비활성화
        foreach (var flower in flowerObjects)
        {
            flower.SetActive(false);
        }

        // 인덱스에 해당하는 꽃 오브젝트만 활성화
        if (index >= 0 && index < flowerObjects.Length)
        {
            flowerObjects[index].SetActive(true);

            // Petal 태그가 붙은 자식 오브젝트의 색상 변경
            Color petalColor = colorDictionary.ContainsKey(colorName) ? colorDictionary[colorName] : HexToColor("FFFFFF");
            ApplyColorToPetals(flowerObjects[index], petalColor);
        }
        else
        {
            Debug.LogWarning("Invalid flower index: " + index);
        }
    }

    // Petal 태그가 달린 자식 오브젝트의 색상을 변경하는 메서드
    private void ApplyColorToPetals(GameObject flowerObject, Color color)
    {
        foreach (Transform child in flowerObject.transform)
        {
            if (child.CompareTag("Petal"))
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = color;
                }
            }
        }
    }

    // API 응답에서 꽃 인덱스를 추출하는 메서드
    private int ExtractFlowerIndex(string response)
    {
        int index = -1;

        int indexStart = response.IndexOf("꽃 인덱스:");
        if (indexStart != -1)
        {
            int endIndex = response.IndexOf("\n", indexStart);
            string indexStr = response.Substring(indexStart + 6, endIndex - indexStart - 6).Trim();
            int.TryParse(indexStr, out index);
        }

        return index;
    }

    // API 응답에서 꽃 색상을 추출하는 메서드
    private string ExtractFlowerColor(string response)
    {
        int colorStart = response.IndexOf("꽃 색상:");
        if (colorStart == -1)
        {
            Debug.LogWarning("꽃 색상 정보가 응답에 없습니다.");
            return "흰색"; // 기본값으로 흰색 반환
        }

        // 줄바꿈 문자 확인 - "\r\n", "\n", 또는 Environment.NewLine 중 하나 찾기
        int endIndex = response.IndexOf("\r\n", colorStart);
        if (endIndex == -1) endIndex = response.IndexOf("\n", colorStart);
        if (endIndex == -1) endIndex = response.IndexOf(Environment.NewLine, colorStart);
        if (endIndex == -1) endIndex = response.Length; // 줄바꿈이 없으면 끝까지

        // 안전하게 색상 부분을 추출
        string colorStr = response.Substring(colorStart + 6, endIndex - (colorStart + 6)).Trim();

        if (string.IsNullOrEmpty(colorStr))
        {
            Debug.LogWarning("꽃 색상이 추출되지 않았습니다. 기본값으로 흰색을 반환합니다.");
            return "흰색"; // 기본값으로 흰색 반환
        }

        return colorStr;
    }


    // Hex 코드 문자열을 Unity Color로 변환하는 메서드
    private static Color HexToColor(string hex)
    {
        if (ColorUtility.TryParseHtmlString("#" + hex, out Color color))
        {
            return color;
        }
        return Color.white; // 기본값으로 흰색
    }
}
