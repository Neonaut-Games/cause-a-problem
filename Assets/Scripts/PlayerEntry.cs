using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerEntry
{

    public readonly int Number;
    public int Points;
    public int Skips;
    public int Plays;

    public PlayerEntry(int number)
    {
        this.Number = number;
    }

    public void AddPoints(int amount)
    {
        Points += amount;
    }
    
    public static PlayerEntry GetPlayerWithMostPoints(PlayerEntry[] list)
    {
        if (list.Length == 0)
        {
            throw new InvalidOperationException("Empty list");
        }

        var winner = new PlayerEntry(-1);
        foreach (PlayerEntry player in list)
        {
            if (player.Points > winner.Points) winner = player;
        }
        return winner;
    }
}