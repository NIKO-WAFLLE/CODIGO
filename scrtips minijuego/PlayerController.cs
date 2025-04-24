using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System; // <-- ESTA LÍNEA FALTABA



public class PlayerController : MonoBehaviour

{
    public event Action PlayerDied;

    [SerializeField] private PlayerController playerController;
    public Canvas GameOverCanvas;
    public TMP_Text TimerText;

    private void Awake()
    {
        if (playerController != null)
        {
            // Subscribe
            playerController.PlayerDied += WhenPlayerDies;
        }

        if (GameOverCanvas.gameObject.activeSelf)
        {
            GameOverCanvas.gameObject.SetActive(false);
        }
    }

    void WhenPlayerDies()
    {
        // Code is called when player dies – activate game over screen!
        GameOverCanvas.gameObject.SetActive(true);
        TimerText.text = "You Lasted: " + Math.Round(Time.timeSinceLevelLoad, 2);

        if (playerController != null)
        {
            // Unsubscribe
            playerController.PlayerDied -= WhenPlayerDies;
        }
    }

    public void RetryClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}