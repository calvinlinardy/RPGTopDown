using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject menu = null;
    public static GameManager instance;

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    private void Start()
    {
        menu.SetActive(true);
    }

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

    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) // Max Level
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        GameManager.instance.ShowText("LEVEL UP!", 30, Color.yellow, transform.position, Vector3.up * 40, 1.5f);
        player.OnLevelUp();
    }

    public void SaveState()
    {
        string saveFile = "";

        saveFile += "0" + "|"; // int preferedSkin
        saveFile += pesos.ToString() + "|"; // int pesos
        saveFile += experience.ToString() + "|"; // int experience
        saveFile += weapon.weaponLevel.ToString(); // int weaponLevel

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

        // Experience
        experience = int.Parse(data[2]); // Load saved exp
        if (GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        // Change the weapon level
        weapon.SetWeaponLevel(int.Parse(data[3]));
    }
}
