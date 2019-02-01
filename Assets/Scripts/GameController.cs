using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public  static GameController instance;

    [Header("UI")]
    public ProgressBar healthBar;
    public Image gameOverPanel;
    public Text scoretxt;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Time.timeScale = 1f;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning("cannot have more than one GameControllers per scene");
        }
    }


    bool isGameOver = false;
    public bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            if(value)
            {
                gameOverPanel.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                gameOverPanel.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            isGameOver = value;
        }
    }
    public void Wasted()
    {
        IsGameOver = true;
    }
    public void Restore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }



    private int score;
    private HealthCare playerHealth;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }



    private void Update()
    {
        if (IsGameOver)
        {
            ;//...
        }
        else
        {
            if (scoretxt != null)
                scoretxt.text = score.ToString();
            
            if(playerHealth!=null&&healthBar!=null)
            {
                healthBar.value = playerHealth.health0To1();
            }
        }

    }
    private void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
        var hcm = gameObject.GetComponentsInChildren<HealthCare>();
        playerHealth = hcm.Length>0?hcm[0]:null;
    }
    //private void StartGame()
    //{
    //    UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    //}



}
