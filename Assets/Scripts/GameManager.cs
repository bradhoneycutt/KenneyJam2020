using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
  //todo  we can manage som things about the current level here, how do we do level handler passing variables to the next scene
    
    public Text GameOverText;
    private bool _playerAlive = true;
    public Text PlayerDialogText;
    public Image DialogModal; 


    public float DialogDelay = .5f ; 

    private void Update()
    {
        DialogDelay -= Time.deltaTime;
        if(DialogDelay <= 0)
        {
            DialogModal.gameObject.SetActive(false);
            PlayerDialogText.gameObject.SetActive(false);

        }

        if (!_playerAlive && Input.GetKeyDown(KeyCode.R)) { 
            LoadScene2();
        }
        
    }

    public void PlayGame()
    {
        GameOverText.gameObject.SetActive(false);
        
        //todo how can we pass in scene name to reset player in current level
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadScene2() 
    {
        GameOverText.gameObject.SetActive(false);
        
        //todo how can we pass in scene name to reset player in current level
        SceneManager.LoadScene("Level2");   
    }

    public void SetPlayerStatus(bool status)
    {
        _playerAlive = status;
    }

    public void PlayerDialog(string message, float dialogDelay = 1.5f)
    {
        DialogModal.gameObject.SetActive(true);
        PlayerDialogText.text = message; 
        PlayerDialogText.gameObject.SetActive(true);
        DialogDelay = dialogDelay;
    }

}
