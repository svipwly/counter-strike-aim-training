using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score = 0;
    public int totalShots = 0;
    public int hitCount = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI accuracyText;

    public bool isSettingsOpen = false; // ��������Ƿ��

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddShot()
    {
        totalShots++;
        UpdateAccuracyText();
    }

    public void AddScore(int value)
    {
        score += value;
        hitCount++;
        UpdateScoreText();
        UpdateAccuracyText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void UpdateAccuracyText()
    {
        if (accuracyText != null)
        {
            float accuracy = totalShots > 0 ? (float)hitCount / totalShots * 100f : 0f;
            accuracyText.text = $"Accuracy: {accuracy:F1}%";
        }
    }

    // �������������÷���������
    public void ResetScore()
    {
        score = 0;
        totalShots = 0;
        hitCount = 0;
        UpdateScoreText();
        UpdateAccuracyText();
    }

    // ������ҪAddScoreWithoutHit�������Զ���ʧ���ӷֿ�ֱ��ɾ����
    // public void AddScoreWithoutHit(int value) { ... }

    public void SetSettingsOpen(bool open)
    {
        isSettingsOpen = open;
    }
}

//using UnityEngine;
//using TMPro;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public int score = 0;
//    public int totalShots = 0;
//    public int hitCount = 0;

//    public TextMeshProUGUI scoreText;
//    public TextMeshProUGUI accuracyText;

//    public bool isSettingsOpen = false; // ��������Ƿ��

//    void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    public void AddShot()
//    {
//        totalShots++;
//        UpdateAccuracyText();
//    }

//    public void AddScore(int value)
//    {
//        score += value;
//        hitCount++;
//        UpdateScoreText();
//        UpdateAccuracyText();
//    }

//    void UpdateScoreText()
//    {
//        if (scoreText != null)
//            scoreText.text = "Score: " + score;
//    }

//    void UpdateAccuracyText()
//    {
//        if (accuracyText != null)
//        {
//            float accuracy = totalShots > 0 ? (float)hitCount / totalShots * 100f : 0f;
//            accuracyText.text = $"Accuracy: {accuracy:F1}%";
//        }
//    }

//    // �������������÷���������
//    public void ResetScore()
//    {
//        score = 0;
//        totalShots = 0;
//        hitCount = 0;
//        UpdateScoreText();
//        UpdateAccuracyText();
//    }

//    public void AddScoreWithoutHit(int value)
//    {
//        score += value;
//        UpdateScoreText();
//        // ����hitCount��������׼ȷ��
//    }


//    // ��Ҳ���Լ��� isSettingsOpen �����÷���
//    public void SetSettingsOpen(bool open)
//    {
//        isSettingsOpen = open;
//    }
//}

//using UnityEngine;
//using TMPro;

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public int score = 0;
//    public int totalShots = 0;
//    public int hitCount = 0;

//    public TextMeshProUGUI scoreText;
//    public TextMeshProUGUI accuracyText;

//    public bool isSettingsOpen = false; // ��������Ƿ��

//    void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    public void AddShot()
//    {
//        totalShots++;
//        UpdateAccuracyText();
//    }

//    public void AddScore(int value)
//    {
//        score += value;
//        hitCount++;
//        UpdateScoreText();
//        UpdateAccuracyText();
//    }

//    void UpdateScoreText()
//    {
//        if (scoreText != null)
//            scoreText.text = "Score: " + score;
//    }

//    void UpdateAccuracyText()
//    {
//        if (accuracyText != null)
//        {
//            float accuracy = totalShots > 0 ? (float)hitCount / totalShots * 100f : 0f;
//            accuracyText.text = $"Accuracy: {accuracy:F1}%";
//        }
//    }

//    // �Ƽ���ͳһ����������忪�غ���ͣ
//    public void SetSettingsOpen(bool open)
//    {
//        isSettingsOpen = open;
//        Time.timeScale = open ? 0 : 1;
//    }
//}

//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;  // ����UI�����ռ�

//public class GameManager : MonoBehaviour
//{
//    public static GameManager Instance;

//    public int score = 0;
//    public int totalShots = 0;
//    public int hitCount = 0;

//    public TextMeshProUGUI scoreText;
//    public TextMeshProUGUI accuracyText;


//    void Awake()
//    {
//        if (Instance == null)
//            Instance = this;
//        else
//            Destroy(gameObject);
//    }

//    void Start()
//    {

//    }

//    public void AddShot()
//    {
//        totalShots++;
//        UpdateAccuracyText();
//    }

//    public void AddScore(int value)
//    {
//        score += value;
//        hitCount++;
//        UpdateScoreText();
//        UpdateAccuracyText();
//    }

//    void UpdateScoreText()
//    {
//        if (scoreText != null)
//            scoreText.text = "Score: " + score;
//    }

//    void UpdateAccuracyText()
//    {
//        if (accuracyText != null)
//        {
//            float accuracy = totalShots > 0 ? (float)hitCount / totalShots * 100f : 0f;
//            accuracyText.text = $"Accuracy: {accuracy:F1}%";
//        }
//    }

//}
