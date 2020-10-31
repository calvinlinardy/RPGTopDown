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
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // Refrences
    public Player player;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void SaveState()
    {
        string saveFile = "";

        saveFile += "0" + "|"; // int preferedSkin
        saveFile += pesos.ToString() + "|"; // int pesos
        saveFile += experience.ToString() + "|"; // int experience
        saveFile += "0"; // int weaponLevel

        PlayerPrefs.SetString("SaveState", saveFile);
        Debug.Log("Game Saved!");
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        Debug.Log("Game Loaded!");

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change player skin
        pesos = int.Parse(data[1]); // Load saved pesos
        experience = int.Parse(data[2]); // Load saved exp
        // Change the weapon level
    }
}
