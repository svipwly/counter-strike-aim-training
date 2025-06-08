using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button homeButton;
    public Button refreshButton;
    public Button settingButton;
    public Button closeButton;

    void Start()
    {
        if (homeButton != null) homeButton.onClick.AddListener(OnHomeButton);
        if (refreshButton != null) refreshButton.onClick.AddListener(OnRefreshButton);
        if (settingButton != null) settingButton.onClick.AddListener(OnSettingButton);
        if (closeButton != null) closeButton.onClick.AddListener(OnCloseButton);
    }

    // 1. 跳转到主菜单
    void OnHomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // 2. 重置分数（假设GameManager有ResetScore方法）
    void OnRefreshButton()
    {
        GameManager.Instance?.ResetScore();
    }

    // 3. 打开/关闭设置面板
    void OnSettingButton()
    {
        if (settingsPanel != null)
        {
            bool show = !settingsPanel.activeSelf;
            settingsPanel.SetActive(show);

            // 可选：切换鼠标显示状态
            Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = show;
        }
    }

    // 4. 退出游戏
    void OnCloseButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}