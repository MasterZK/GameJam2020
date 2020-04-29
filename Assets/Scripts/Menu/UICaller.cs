using UnityEngine;

public class UICaller : MonoBehaviour
{
    [Header("Canvas objects")]
    [SerializeField] private GameObject ingameUI;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameObject pauseMenuCanvas;

    [SerializeField] private bool paused = false;

    private void Update()
    {
        CallUiByButton();
    }

    private void CallUiByButton()
    {
        CallInventory();
        CallPauseMenu();
    }
    private void CallInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && !pauseMenuCanvas.activeSelf)
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            GameStateChange();
        }
    }
    private void CallPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !inventoryCanvas.activeSelf)
        {
            pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
            GameStateChange();
        }
    }
    public void GameStateChange()
    {
        ingameUI.SetActive(!ingameUI.activeSelf);
        Time.timeScale = paused ? 1 : 0;
        paused = !paused;
    }
}
