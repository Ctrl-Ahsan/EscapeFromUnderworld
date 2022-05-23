using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    // Player stats
    public int skulls = 0;
    public int fireSkulls = 0;
    public TextMeshProUGUI skullCounter;
    public TextMeshProUGUI fireSkullCounter;

    public GameObject bootsIndic;
    public GameObject flameIndic;
    public GameObject boltIndic;
    public GameObject message;

    public static UI permanent;

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // Singleton pattern
        if (!permanent)
        {
            permanent = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        if (!Player.hermesBoots)
        {
            bootsIndic.SetActive(false);
        }

        if (!Player.zeusBolt)
        {
            boltIndic.SetActive(false);
        }
        if (!Player.hadesFlame)
        {
            flameIndic.SetActive(false);
        }
        
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                gameIsPaused = false;
            }
            else
            {
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                gameIsPaused = true;
            }
        }
    }
}
