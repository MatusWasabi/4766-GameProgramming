using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private int playerHealth = 3;
    [SerializeField] private HealthCheck healthCheck;

    /*public int PlayerHealth
    {
        get => playerHealth;
        set { playerHealth = value; }
    }*/

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            healthCheck.UpdateHealth(playerHealth);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel(int levelIndex)
    { 
        SceneManager.LoadScene(levelIndex);
        DOTween.KillAll();
    }



    public void LoadNextLevel()
    {
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if(nextSceneBuildIndex == SceneManager.sceneCountInBuildSettings)
        {
            LoadLevel(0);
        }
        else
        {
            LoadLevel(nextSceneBuildIndex);
        }
        
    }

    public void PlayerDeath()
    {
        playerHealth--;
        healthCheck.UpdateHealth(playerHealth);
        if (playerHealth <= 0) { LoadLevel(0); Destroy(gameObject); }
        else
        {
            LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public int GetIndexLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    private void OnDestroy()
    {
        //Debug.Log("This has been destoryed");
    }

    public void LoadMainMenu()
    {
        LoadLevel(0);
        Destroy(gameObject);
    }



}
