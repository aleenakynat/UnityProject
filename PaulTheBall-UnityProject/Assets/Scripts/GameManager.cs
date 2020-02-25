using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    public Text gameText;
    public GameObject playButton;
    public GameObject playerPrefab;
    public CamMovement camMovmnt;
    private GameObject activePlayer;
    public float plheight;
    public Text gameScore;
    //public GameObject canv;
    public int score;
    public GameObject completeLevelUI;
    //private Text[] newText;
    public enum GameState
    {
        Started,
        Playing,
        Over,
        Paused
    }
    private GameState state = GameState.Started;
    
    void Start()
    {
        AudioManager.Instance.PlayAudio("Background");
        completeLevelUI.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex > 1 && SceneManager.GetActiveScene().buildIndex < 6)
        { 
            OnPlayButtonPressed();
            score = GetSavedScore();
        }
        else
        {
            state = GameState.Started;
            camMovmnt.enabled = true;
            score = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Started)
        {
            gameText.text = "Level" + SceneManager.GetActiveScene().buildIndex;
        }
        else if (state == GameState.Playing)
        {
            plheight = activePlayer.gameObject.transform.position.y;
            if (Time.timeScale == 1)
            {
                gameText.text = "Level " + SceneManager.GetActiveScene().buildIndex + ": Run, Paul, Run!";
                score++;
            }
            else
            {
                gameText.text = "Game Paused.";
            }
            gameScore.text = score.ToString();
        }
        else if (state == GameState.Over)
        {
            StartCoroutine(LoadLevelAfterDelay(3));
        }
        else if (state == GameState.Paused)
        {
            gameText.text = "Game Paused";
        }

    }
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CompleteLevel()
    {
        SaveScores();
        completeLevelUI.SetActive(true);
        activePlayer.GetComponent<plMovement>().enabled = false;
        camMovmnt.enabled = false;
        Destroy(activePlayer, 2f);
    }
    public void GameOver()
    {
        state = GameState.Over;
        activePlayer.GetComponent<plMovement>().enabled = false;
        camMovmnt.enabled = false;
        Destroy(activePlayer, 2f);
        AudioManager.Instance.PlayAudio("over");
        gameText.text = "Oops, you got hit! :(";
    }

    public void CollectBonus()
    {
        score += 100;
        AudioManager.Instance.PlayAudio("Bonus");
    }
    public void OnPlayButtonPressed()
    {
        state = GameState.Playing;
        playButton.gameObject.SetActive(false);
        activePlayer = Instantiate(playerPrefab);
        camMovmnt.target = activePlayer.transform;
        camMovmnt.enabled = true;
        activePlayer.GetComponent<plMovement>().enabled = true;
        AudioManager.Instance.PlayAudio("Start");
        //AudioManager.Instance.PlayAudio("Background");
    }

    public void OnPauseButtonPressed()
    {
        if (Time.timeScale == 1)
        {
            state = GameState.Paused;
            Time.timeScale = 0;
        }
        else if(Time.timeScale == 0)
        {
            state = GameState.Playing;
            Time.timeScale = 1;
        }
    }
    public void SaveScores()
    {
        PlayerPrefs.SetInt("Score", score);
    }
    public int GetSavedScore()
    {

        int tempScore = PlayerPrefs.GetInt("Score");
        return tempScore;
    }
    public float increaseSpeed(float speed)
    {
        speed += (SceneManager.GetActiveScene().buildIndex*0.07f);
        return speed;
    }
    public void OnQuit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
