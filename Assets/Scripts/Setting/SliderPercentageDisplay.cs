using UnityEngine;
using UnityEngine.UI;
using TMPro; // 如果你用的是TextMeshPro

public class SliderPercentageDisplay : MonoBehaviour
{
    public Slider slider;            // 拖拽Slider进来
    public TMP_Text percentageText;  // 拖拽TextMeshPro-Text进来

    void Start()
    {
        slider.onValueChanged.AddListener(UpdatePercentage);
        UpdatePercentage(slider.value); // 初始化显示
    }

    void UpdatePercentage(float value)
    {
        // 假设Slider的Min=0，Max=1，显示为百分比
        int percent = Mathf.RoundToInt(value * 100);
        percentageText.text = percent + "%";
    }
}