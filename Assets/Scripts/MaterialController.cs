using UnityEngine;

public class MaterialController : MonoBehaviour
{
    // 변경할 대상 오브젝트의 머티리얼
    public Renderer targetRenderer;

    // 원본 머티리얼 (복사할 기본 머티리얼)
    private Material originalMaterial;

    void Start()
    {
        // 원본 머티리얼 저장 (변경되지 않은 기본 머티리얼)
        originalMaterial = targetRenderer.material;
    }

    // 버튼을 클릭하면 호출되는 함수
   public void ChangeMaterialColor(string colorName)
   {
       Color newColor = Color.white;
       
       switch (colorName)
       {
           case "White":
               newColor = Color.white;
               break;
           case "Red":
               newColor = new Color(1f, 0.3922f, 0.4157f); // 정확한 빨간색(#FF646A)
               break;
           case "Yellow":
               newColor = Color.yellow;
               break;
           case "SkyBlue":
               newColor = new Color(0.4039f, 1f, 0.9686f); // 정확한 하늘색(#67FFF7)
               break;
           case "Purple":
               newColor = new Color(1f, 0.2784f, 0.7373f); // 정확한 퍼플(#FF47BC)
               break;
       }
   
       // 머티리얼에 색상 적용
       targetRenderer.material.SetColor("_BaseColor", newColor);
   }

}
