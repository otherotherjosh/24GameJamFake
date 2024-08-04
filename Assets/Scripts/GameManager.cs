using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private Player player;
    public Player Player { get => player; }

    private int points = 0;
    public int Points { get => points; set => points = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        } 

        Instance = this;
    }

    private void Start()
    {
        player.OnDie.AddListener(EndGame);
    }

    public void EndGame()
    {
        player.OnDeath();
        Debug.Log("womp womp");
    }
}
