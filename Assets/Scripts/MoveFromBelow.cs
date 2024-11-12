using UnityEngine;

public class MoveUIFromBelow : MonoBehaviour
{
    private RectTransform rectTransform; // RectTransform 참조
    private Vector2 initialPosition;     // 원래 위치 저장
    private Vector2 startPosition;       // 시작 위치 (현재 위치에서 -500 아래)
    public float duration = 0.5f;        // 애니메이션 지속 시간

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // 초기 위치와 시작 위치 설정
        initialPosition = rectTransform.anchoredPosition;
        startPosition = initialPosition + new Vector2(0, -500);

        // 시작 위치로 이동시키고 오브젝트 비활성화
        rectTransform.anchoredPosition = startPosition;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        // 활성화될 때 애니메이션 시작
        StartCoroutine(MoveToPosition());
    }

    private System.Collections.IEnumerator MoveToPosition()
    {
        float elapsedTime = 0f;

        // 시작 위치에서 초기 위치로 이동
        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, initialPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종 위치에 정확히 설정
        rectTransform.anchoredPosition = initialPosition;
    }
}
