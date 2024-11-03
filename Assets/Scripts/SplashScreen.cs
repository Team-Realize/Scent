using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager 사용을 위한 네임스페이스
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    public float waitTime = 2f; // 기다릴 시간 (2초)
    public string nextSceneName = "MainScene"; // 이동할 씬 이름

    void Start()
    {
        // 스플래시 화면이 뜨고 지정된 시간 후에 다음 씬으로 이동
        StartCoroutine(WaitAndLoadScene());
    }

    // 대기 후 씬 전환을 위한 Coroutine
    IEnumerator WaitAndLoadScene()
    {
        // 지정한 시간 동안 대기
        yield return new WaitForSeconds(waitTime);

        // 다음 씬으로 전환
        SceneManager.LoadScene(nextSceneName);
    }
}
