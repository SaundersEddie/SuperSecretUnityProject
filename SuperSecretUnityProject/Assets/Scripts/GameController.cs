using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public TMP_Text turnText; // Turn display
    public TMP_Text winText; // UI text for displaying winner (assign in Inspector)
    public Button[] cells; // Tic-Tac-Toe cells
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
        if (gameOver || !string.IsNullOrEmpty(board[index])) return; // Prevent moves after win

        board[index] = isXTurn ? "X" : "O";
        cells[index].GetComponentInChildren<TMP_Text>().text = board[index];

        if (CheckWin())
        {
            winText.text = board[index] + " Wins!";
            gameOver = true;
            DisableButtons();
        }
        else if (CheckTie())
        {
            winText.text = "It's a Tie!";
            gameOver = true;
        }
        else
        {
            isXTurn = !isXTurn;
            turnText.text = isXTurn ? "X's Turn" : "O's Turn";
        }
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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
