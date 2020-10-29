using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        SceneManager.sceneLoaded += LoadState;
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //Refrences
    public Player player;

    // Logic
    public int pesos;
    public int experience;
    
    public void SaveState()
    {
        Debug.Log("Save State");
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Load State");   
    }
}
