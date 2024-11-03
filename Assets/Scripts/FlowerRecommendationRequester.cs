using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FlowerRecommendationRequester : MonoBehaviour
{
    [Header("OpenAI API Settings")]
    private string apiKey = "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"; // OpenAI API 키 (변경 필요)
    private string modelId = "ft:gpt-3.5-turbo-0125:personal::AMXdS9Xx"; // Fine-tuned 모델 ID (변경 필요)

    private string[] allowedFlowers = {
        "장미", "튤립", "리시안셔스", "카네이션", "해바라기", "라일락", "수국", "백합", "거베라", "난초",
        "라넌큘러스", "데이지", "피오니", "다알리아", "국화", "알스트로메리아", "히아신스", "프리지아",
        "아네모네", "아이리스"
    };

    private string[] fillingFlowers = {
        "베이비 브레스", "유칼립투스", "라벤더", "안개꽃", "솔리다고"
    };

    public ColorButtonManager colorButtonManager;
    public OccasionButtonManager occasionButtonManager;

    // SceneChanger 인스턴스 추가
    public SceneChanger sceneChanger;

    public void RequestFlowerRecommendation()
    {
        string color = colorButtonManager.GetActiveButtonOutput();
        string occasion = occasionButtonManager.GetActiveButtonOutput();

        string prompt = $"색상/색감: {color}\n특정 꽃: 추천\n용도: {occasion}";

        string systemMessage = 
            "You are a helpful assistant that strictly recommends flower arrangements using only specific flowers. " +
            $"You MUST ONLY use the following primary flowers: {string.Join(", ", allowedFlowers)}. " +
            "Do NOT use any flowers that are not in this list. " +
            $"For filling flowers, you MUST ONLY use the following: {string.Join(", ", fillingFlowers)}. " +
            "Any flower arrangement recommendation MUST follow these rules.";

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
            Debug.Log("Full API Response: " + response);

            string recommendedContent = ExtractContentFromResponse(response);
            Debug.Log("Recommended Content: " + recommendedContent);

            // ResponseDataManager에 저장
            ResponseDataManager.Instance.SetApiResponseContent(recommendedContent);

            // 요청 시간이 4초 이상일 경우 바로 씬 전환
            if (sceneChanger != null)
            {
                float waitTime = requestDuration >= 4f ? 0f : (4f - requestDuration);
                sceneChanger.ChangeSceneAfterDelay(waitTime);
            }
        }
        else
        {
            Debug.LogError("Failed to get response: " + request.error);
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
