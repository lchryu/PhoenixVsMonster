using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    [SerializeField] public static int playerScore;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private AudioSource playAddScoreSoundEffect;


    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = "Score: " + playerScore.ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }    
    public void StartGame2Player()
    {
        SceneManager.LoadScene(3);
    }    
    
    public void PlayAgain()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    //public void PlayAddScroreSoundEffect()
    //{
    //    playAddScoreSoundEffect.Play();
    //}
    public void PlayAddScroreSoundEffect()
    {
        if (playAddScoreSoundEffect != null)
        {
            playAddScoreSoundEffect.Play();
        }
    }
}

