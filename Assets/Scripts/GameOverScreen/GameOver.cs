using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject gameOverCanvas;
    public NextbotAI nextBotAI;

    private void Start() {
        Cursor.lockState = CursorLockMode.None;
    }

    void Update() {
        // Check if the player is alive
        if (nextBotAI != null && !nextBotAI.playerAlive) {
            // Player is dead, show game over canvas
            ShowGameOverCanvas();
        }
    }

    void ShowGameOverCanvas() {
        // Activate the game over canvas
        if (gameOverCanvas != null) {
            gameOverCanvas.SetActive(true);
        }
    }

    // Method to handle player death
    public void PlayerDies() {
        // Set playerAlive in NextBotAI to false
        if (nextBotAI != null) {
            nextBotAI.playerAlive = false;
        }
        // Do whatever you need to do when the player dies
        Debug.Log("Player died");
    }

    // Method to handle player respawn (if needed)
    public void PlayerRespawn() {
        // Reset player's state and revive if necessary
        if (nextBotAI != null) {
            nextBotAI.playerAlive = true;
        }
        // Hide game over canvas if it's active
        if (gameOverCanvas != null && gameOverCanvas.activeSelf) {
            gameOverCanvas.SetActive(false);
        }
        // Load the "world" scene
        SceneManager.LoadScene("world");
    }
}
