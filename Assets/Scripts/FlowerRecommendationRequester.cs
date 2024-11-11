using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FlowerRecommendationRequester : MonoBehaviour
{
    private string apiKey = "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"; // OpenAI API 키 (변경 필요)
    private string modelId = "ft:gpt-3.5-turbo-0125:personal::AR5slKZW"; // Fine-tuned 모델 ID (변경 필요)

    public ColorButtonManager colorButtonManager; // ColorButtonManager 인스턴스
    public OccasionButtonManager occasionButtonManager; // OccasionButtonManager 인스턴스
    public SceneChanger sceneChanger; // SceneChanger 인스턴스

    // ResponseDataManager 인스턴스
    public ResponseDataManager responseDataManager;

    public void RequestFlowerRecommendation()
    {
        // colorButtonManager와 occasionButtonManager에서 색상과 용도를 가져옴
        string color = colorButtonManager.GetActiveButtonOutput();
        string occasion = occasionButtonManager.GetActiveButtonOutput();

        // 제한 조건과 형식을 포함한 시스템 메시지
        string systemMessage =
            "You are a helpful assistant that recommends flower arrangements strictly based on specific flowers and colors. \n" +
            "You must follow the exact format below in each response, with no deviations or additional information:\n\n" +
            "구성: [색상(하나의 색상만 사용)] [꽃 이름]\n" +
            "설명: [사용자가 감동을 느낄 수 있도록, 꽃의 특성과 아름다움을 표현하는 긴 설명 작성]\n" +
            "꽃 인덱스: [꽃의 인덱스 번호]\n" +
            "꽃 이름: [꽃 이름 (색상 정보는 포함하지 마세요)]\n" +
            "꽃 색상: [하나의 색상만 사용]\n\n" +
            "Strictly adhere to the format above and do not deviate. Only use the allowed six flowers and colors provided below:\n" +
            "Allowed flowers:\n" +
            "  - Index 1: 작약\n" +
            "  - Index 2: 아이리스\n" +
            "  - Index 3: 장미+튤립\n" +
            "  - Index 4: 장미+카네이션\n" +
            "  - Index 5: 수국\n" +
            "  - Index 6: 국화\n\n" +
            "Do not repeatedly recommend the same flower, especially Index 1 (작약). Make sure to provide variety by using different flowers from Index 1 to Index 6, depending on the user's input." +
            "Allowed colors:\n" +
            "  - 코랄레드 (FF8B8B)\n" +
            "  - 스칼렛레드 (FF4040)\n" +
            "  - 오렌지 (FF9046)\n" +
            "  - 노랑 (FFE931)\n" +
            "  - 파스텔그린 (B4FF31)\n" +
            "  - 하늘색 (31CDFF)\n" +
            "  - 라벤더색 (CF86FF)\n" +
            "  - 연핑크 (FF7BB8)\n" +
            "  - 흰색 (FFFFFF)\n" +
            "  - 민트 (96FFF3)\n\n" +
            "Each response must strictly follow this format without changes and must contain a detailed, moving description that will evoke strong emotions in the user. Use only one color per flower.\n" +
            "Don't contain Flower Color output in FlowerName output";


        string prompt = $"색상/색감: {color}\n \n용도: {occasion}";

        // API 요청을 보내는 코루틴 시작
        StartCoroutine(SendOpenAIRequest(prompt, systemMessage));
    }

    private IEnumerator SendOpenAIRequest(string prompt, string systemMessage)
    {
        string url = "https://api.openai.com/v1/chat/completions";
        string jsonData = $"{{\"model\": \"{modelId}\", \"messages\": [" +
                          $"{{\"role\": \"system\", \"content\": \"{EscapeJson(systemMessage)}\"}}," +
                          $"{{\"role\": \"user\", \"content\": \"{EscapeJson(prompt)}\"}}]}}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

        float requestStartTime = Time.time; // 요청 시작 시간 기록

        yield return request.SendWebRequest();

        float requestDuration = Time.time - requestStartTime; // 요청 시간 계산

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            string recommendedContent = ExtractContentFromResponse(response);
            Debug.Log("API Response: " + recommendedContent);

            // ResponseDataManager를 통해 응답 내용 저장
            if (responseDataManager != null)
            {
                responseDataManager.SetApiResponseContent(recommendedContent);
            }

            // 씬 전환을 위한 대기 시간 계산 (4초 이상일 경우 즉시 전환)
            if (sceneChanger != null)
            {
                float waitTime = requestDuration >= 4f ? 0f : (4f - requestDuration);
                sceneChanger.ChangeSceneAfterDelay(waitTime);
            }
        }
        else
        {
            Debug.LogError("Request failed: " + request.error);
        }
    }

    private string EscapeJson(string text)
    {
        return text.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
    }

    private string ExtractContentFromResponse(string json)
    {
        try
        {
            int contentIndex = json.IndexOf("\"content\":");
            if (contentIndex == -1) return "Content not found.";

            int startIndex = json.IndexOf("\"", contentIndex + 10) + 1;
            int endIndex = json.IndexOf("\"", startIndex);

            if (startIndex == -1 || endIndex == -1) return "Content extraction failed.";

            string extracted = json.Substring(startIndex, endIndex - startIndex).Trim();
            return extracted.Replace("\\n", "\n").Replace("\\\"", "\"");
        }
        catch
        {
            return "Failed to parse response.";
        }
    }
}
