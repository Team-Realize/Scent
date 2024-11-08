using UnityEngine;

public class FlowerDisplayManager : MonoBehaviour
{
    public GameObject[] flowerObjects; // 각 인덱스에 해당하는 꽃 오브젝트를 설정합니다.

    private void Start()
    {
        // ResponseDataManager에서 API 응답을 가져와 꽃 인덱스를 추출하고 해당 꽃을 표시
        string apiResponse = ResponseDataManager.Instance.GetApiResponseContent();
        int flowerIndex = ExtractFlowerIndex(apiResponse);
        DisplayFlowerObject(flowerIndex);
    }

    // 꽃 오브젝트를 활성화하는 메서드
    public void DisplayFlowerObject(int index)
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
        }
        else
        {
            Debug.LogWarning("Invalid flower index: " + index);
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
}