using UnityEngine;

public class ResponseDataManager : MonoBehaviour
{
    public static ResponseDataManager Instance { get; private set; }
    public string apiResponseContent;

    private void Awake()
    {
        // 싱글톤 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 오브젝트 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // API 응답을 저장하는 메서드
    public void SetApiResponseContent(string content)
    {
        apiResponseContent = content;
    }

    // API 응답을 반환하는 메서드
    public string GetApiResponseContent()
    {
        return apiResponseContent;
    }
}