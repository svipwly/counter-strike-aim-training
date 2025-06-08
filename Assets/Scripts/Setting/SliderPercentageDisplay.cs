using UnityEngine;
using UnityEngine.UI;
using TMPro; // ������õ���TextMeshPro

public class SliderPercentageDisplay : MonoBehaviour
{
    public Slider slider;            // ��קSlider����
    public TMP_Text percentageText;  // ��קTextMeshPro-Text����

    void Start()
    {
        slider.onValueChanged.AddListener(UpdatePercentage);
        UpdatePercentage(slider.value); // ��ʼ����ʾ
    }

    void UpdatePercentage(float value)
    {
        // ����Slider��Min=0��Max=1����ʾΪ�ٷֱ�
        int percent = Mathf.RoundToInt(value * 100);
        percentageText.text = percent + "%";
    }
}