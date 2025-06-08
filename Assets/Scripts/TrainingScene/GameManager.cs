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

    public bool isSettingsOpen = false; // 设置面板是否打开

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

    // 新增方法：重置分数和命中
    public void ResetScore()
    {
        score = 0;
        totalShots = 0;
        hitCount = 0;
        UpdateScoreText();
        UpdateAccuracyText();
    }

    // 不再需要AddScoreWithoutHit方法（自动消失不加分可直接删掉）
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

//    public bool isSettingsOpen = false; // 设置面板是否打开

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

//    // 新增方法：重置分数和命中
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
//        // 不加hitCount，不更新准确率
//    }


//    // 你也可以加上 isSettingsOpen 的设置方法
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

//    public bool isSettingsOpen = false; // 设置面板是否打开

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

//    // 推荐：统一控制设置面板开关和暂停
//    public void SetSettingsOpen(bool open)
//    {
//        isSettingsOpen = open;
//        Time.timeScale = open ? 0 : 1;
//    }
//}

//using UnityEngine;
//using TMPro;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;  // 引用UI命名空间

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
