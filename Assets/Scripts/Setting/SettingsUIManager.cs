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

    // 1. ��ת�����˵�
    void OnHomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // 2. ���÷���������GameManager��ResetScore������
    void OnRefreshButton()
    {
        GameManager.Instance?.ResetScore();
    }

    // 3. ��/�ر��������
    void OnSettingButton()
    {
        if (settingsPanel != null)
        {
            bool show = !settingsPanel.activeSelf;
            settingsPanel.SetActive(show);

            // ��ѡ���л������ʾ״̬
            Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = show;
        }
    }

    // 4. �˳���Ϸ
    void OnCloseButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}