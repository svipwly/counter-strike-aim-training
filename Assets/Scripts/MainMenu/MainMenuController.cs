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
        // 加载训练场景
        SceneManager.LoadScene("TrainingScene");
    }

    void OnTaskCustomizationClicked()
    {
        // 加载任务自定义场景
        SceneManager.LoadScene("ChallengeScene");
    }

}
