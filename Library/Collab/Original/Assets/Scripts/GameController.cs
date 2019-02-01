using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public  static GameController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            Debug.LogWarning("cannot have more than one GameControllers per scene");
        }
    }
    
    public bool IsGameOver=false;
    public Image gameOverPanel;
    public Text scoretxt;
    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    private void Update()
    {
        if(scoretxt!=null)
        scoretxt.text = score.ToString();
        if (IsGameOver)
        {
            gameOverPanel.gameObject.SetActive(true);
        }

    }
    private void Start()
    {
        gameOverPanel.gameObject.SetActive(false);
    }
    private void StartGame()
    {
        
        Application.LoadLevel(0);
    }


}
