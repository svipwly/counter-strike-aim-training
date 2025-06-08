using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool show = !settingsPanel.activeSelf;
            settingsPanel.SetActive(show);

            if (GameManager.Instance != null)
                GameManager.Instance.SetSettingsOpen(show);

            // 鼠标锁定与显示设置
            if (show)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}