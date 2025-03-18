using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public TMP_Text turnText; // UI text to show whose turn it is
    public TMP_Text winText;  // UI text to display winner or tie
    public Button[] cells;    // Tic-Tac-Toe grid buttons
    private string[] board = new string[9]; // Tracks board state
    private bool isXTurn = true;
    private bool gameOver = false;

    void Start()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            int index = i; // Capture index for delegate
            cells[i].onClick.AddListener(() => MakeMove(index));
        }
    }

    void MakeMove(int index)
    {
        if (gameOver || !string.IsNullOrEmpty(board[index])) return; // Prevent move if game is over or cell occupied

        // board[index] = isXTurn ? "X" : "O";
        board[index] = "X";
        cells[index].GetComponentInChildren<TMP_Text>().text = "X";
        
        // ✅ Correctly trigger the animation using the Click trigger
        Animator anim = cells[index].GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Click"); 
        }

        SoundManager.instance.PlayMoveSound(); // Play move sound

        if (CheckWinOrTie()) return;

        isXTurn = false;
        turnText.text = "O's Turn";

        Invoke("AI_Move", 1f);
    }

    void AI_Move()
    {
            if (gameOver) return;

            List<int> availableMoves = new List<int>();
            for (int i = 0; i < board.Length; i++)
            {
                if (string.IsNullOrEmpty(board[i])) availableMoves.Add(i);
            }

            if (availableMoves.Count > 0)
            {
                int aiMove = availableMoves[Random.Range(0, availableMoves.Count)];
                board[aiMove] = "O";
                cells[aiMove].GetComponentInChildren<TMP_Text>().text = "O";

                if (CheckWinOrTie()) return;

                isXTurn = true;
                turnText.text = "X's Turn";
            }
    }

    bool CheckWinOrTie()
    {
        if (CheckWin())
        {
            gameOver = true;
            DisableButtons();
            winText.text = (isXTurn ? "X" : "O") + " Wins!";
            PlayerPrefs.SetString("Winner", isXTurn ? "X" : "O");
            PlayerPrefs.Save();
            SoundManager.instance.PlayWinSound(); // Play win sound
            Invoke("LoadGameOverScreen", 1.5f);
            return true;
        }
        if (CheckTie())
        {
            winText.text = "It's a Tie!";
            gameOver = true;
            PlayerPrefs.SetString("Winner", "Tie");
            PlayerPrefs.Save();
            SoundManager.instance.PlayTieSound(); // Play win sound
            Invoke("LoadGameOverScreen", 1.5f);
            return true;
        }
        return false;
    }

    bool CheckWin()
    {
        int[,] winPatterns = new int[,]
        {
            {0, 1, 2}, {3, 4, 5}, {6, 7, 8}, // Rows
            {0, 3, 6}, {1, 4, 7}, {2, 5, 8}, // Columns
            {0, 4, 8}, {2, 4, 6}             // Diagonals
        };

        for (int i = 0; i < winPatterns.GetLength(0); i++)
        {
            int a = winPatterns[i, 0], b = winPatterns[i, 1], c = winPatterns[i, 2];

            if (!string.IsNullOrEmpty(board[a]) &&
                board[a] == board[b] &&
                board[a] == board[c])
            {
                // Stop all input
                gameOver = true;
                DisableButtons();

                // Display winner
                winText.text = board[a] + " Wins!";

                // Transition to Game Over Screen
                Invoke("LoadGameOverScreen", 1.5f);

                return true;
            }
        }
        return false;
    }

    bool CheckTie()
    {
        foreach (string cell in board)
        {
            if (string.IsNullOrEmpty(cell)) return false;
        }
        return true;
    }

    void DisableButtons()
    {
        foreach (Button cell in cells)
        {
            cell.interactable = false;
        }
    }

    void LoadGameOverScreen()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
