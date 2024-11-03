using UnityEngine;

public class RotateYOnly : MonoBehaviour
{
    public float rotationSpeed = 100f; // 회전 속도
    private Vector3 lastMousePosition;
    private bool isDragging = false;

    void Update()
    {
        // 마우스 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }

        // 마우스 버튼을 떼면 드래그 중지
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 마우스 드래그 중일 때
        if (isDragging)
        {
            // 마우스의 X축 움직임을 기반으로 Y축 회전만 적용
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            // 회전 방향 반전 (음수 곱하기)
            float rotationY = -deltaMousePosition.x * rotationSpeed * Time.deltaTime;

            // Y축 회전만 적용 (X, Z는 고정)
            transform.Rotate(0, rotationY, 0, Space.World);

            // 마지막 마우스 위치 갱신
            lastMousePosition = Input.mousePosition;
        }
    }
}