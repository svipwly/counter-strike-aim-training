using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button trainingButton;
    public Button taskCustomizationButton;

    void Start()
    {
        trainingButton.onClick.AddListener(OnTrainingClicked);
        taskCustomizationButton.onClick.AddListener(OnTaskCustomizationClicked);
    }

    void OnTrainingClicked()
    {
        // ����ѵ������
        SceneManager.LoadScene("TrainingScene");
    }

    void OnTaskCustomizationClicked()
    {
        // ���������Զ��峡��
        SceneManager.LoadScene("ChallengeScene");
    }

}
