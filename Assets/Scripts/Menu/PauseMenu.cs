using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UICaller uiCaller;
    [SerializeField] private GameObject pauseMenuCanvas;

    private void Update()
    {
        ResumeByButton();
    }

    public void Resume()
    {
        uiCaller.GameStateChange();
        pauseMenuCanvas.SetActive(false);
    }
    private void ResumeByButton()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Resume();
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
