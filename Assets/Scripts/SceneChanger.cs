using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public Button targetButton; // 클릭할 버튼
    public string sceneName; // 이동할 씬 이름
    public float waitTime = 0f; // 기본 대기 시간 (초)

    void Start()
    {
        // 버튼 클릭 이벤트 리스너 추가
        if (targetButton != null)
        {
            targetButton.onClick.AddListener(OnButtonClick);
        }
    }

    // 버튼 클릭 시 호출될 함수
    void OnButtonClick()
    {
        // 기본 대기 시간으로 씬 전환
        StartCoroutine(WaitAndChangeScene(waitTime));
    }

    // 외부에서 호출할 수 있는 씬 전환 메서드 (대기 시간을 인수로 받음)
    public void ChangeSceneAfterDelay(float waitTime)
    {
        StartCoroutine(WaitAndChangeScene(waitTime));
    }

    // 지정한 시간만큼 대기 후 씬 전환
    IEnumerator WaitAndChangeScene(float delay)
    {
        // delay 초 동안 대기
        yield return new WaitForSeconds(delay);

        // 지정한 씬으로 전환
        SceneManager.LoadScene(sceneName);
    }
}