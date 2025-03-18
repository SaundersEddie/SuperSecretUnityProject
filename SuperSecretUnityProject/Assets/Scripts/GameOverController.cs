using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
    public TMP_Text winnerText; // Assign WinnerText in the Inspector

    void Start()
    {
        if (PlayerPrefs.HasKey("Winner"))
        {
            winnerText.text = PlayerPrefs.GetString("Winner") + " Wins!";
        }
        else
        {
            winnerText.text = "It's a Tie!";
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}
