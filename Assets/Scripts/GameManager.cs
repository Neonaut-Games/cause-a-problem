using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    [Header("Game - Ongoing")]
    public GameObject ongoingPanel;
    public TextMeshProUGUI playerHeader;
    public TextMeshProUGUI ask;
    public Animator accept;
    public Animator decline;

    [Header("Game - Completed")]
    public GameObject completedPanel;
    public TextMeshProUGUI textWinner;
    public TextMeshProUGUI textPoints;
    public TextMeshProUGUI textStats;

    [HideInInspector] public PlayerEntry[] players;
    [HideInInspector] public int totalCardsRead;
    [HideInInspector] public int totalCardsSkipped;
    [HideInInspector] public int currentPlayer;
    [HideInInspector] public int numPlayers;
    private static readonly int Start = Animator.StringToHash("Start");

    public void InitializeGame()
    {
        if (Instance != null) return;
        
        numPlayers = GameSettings.Instance.numPlayers;
        
        players = new PlayerEntry[numPlayers];
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) players[i] = new PlayerEntry(i + 1);
        }
        
        Instance = this;
    }
    
    public void SkipCard()
    {
        totalCardsSkipped++;
        Continue(true);
    }

    public void NextCard()
    {
        int amt = 100;
        int index = currentPlayer - 1;
        
        if (index >= 0) players[index].AddPoints(amt);
        Continue(false);
    }
    
    private void Continue(bool wasSkipped)
    {
        
        totalCardsRead++;
        
        string challenge =
            GameSettings.Instance.Challenges.Get();

        // Play transition animation
        if (wasSkipped) decline.SetTrigger(Start);
        else accept.SetTrigger(Start);

        int next = NextPlayer();
        StartCoroutine(ShowNextCard(challenge, next));
    }

    private IEnumerator ShowNextCard(string challenge, int next)
    {
        /* Wait until the transition banner is
         above the text to create a seamless effect. */
        yield return new WaitForSeconds(0.4f);
        
        // Display next card if applicable
        if (challenge != null)
        {
            playerHeader.SetText("Player " + next);
            ask.SetText(challenge);
            yield break;
        }
        
        EndGame();
        
    }

    private int NextPlayer()
    {
        currentPlayer++;
        if (currentPlayer > numPlayers) currentPlayer = 1;
        return currentPlayer;
    }

    private void EndGame()
    {
        ongoingPanel.SetActive(false);
        completedPanel.SetActive(true);

        StringBuilder stringBuilder = new StringBuilder();
        foreach (var player in players)
        {
            stringBuilder.AppendLine("Player " + player.Number + ": " + player.Points + " points");
        }


        var winner = PlayerEntry.GetPlayerWithMostPoints(players);
        if (winner.Points == 0)
        {
                textWinner.SetText("YOU ARE ALL VERY BAD AT THIS GAME");
        }
        else
        {
            textWinner.SetText("PLAYER " + winner.Number + " WINS");
        }
        
        textPoints.SetText(stringBuilder.ToString());

        textStats.SetText(
            "Total Cards Played: " + totalCardsRead + "\n" +
            "Total Cards Accepted: " + (totalCardsRead - totalCardsSkipped )  + "\n" +
            "Total Cards Declined: " + totalCardsSkipped);
    }

    public void MainMenu()
    {
        foreach (var component in GetDontDestroyOnLoadObjects())
        {
            Destroy(component);
        }
        SceneSwitcher.Instance.LoadScene("MainMenu");
    }

    private static IEnumerable<GameObject> GetDontDestroyOnLoadObjects()
    {
        GameObject temp = null;
        try
        {
            temp = new GameObject();
            DontDestroyOnLoad( temp );
            var dontDestroyOnLoad = temp.scene;
            DestroyImmediate( temp );
            temp = null;
 
            return dontDestroyOnLoad.GetRootGameObjects();
        }
        finally
        {
            if (temp != null) DestroyImmediate( temp );
        }
    }
    

}
