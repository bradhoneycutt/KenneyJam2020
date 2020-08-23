using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
        //todo  we can manage som things about the current level here, how do we do level handler passing variables to the next scene

    public Text GameOverText;
    public float DialogDelay = 1.25f;
    public Text PlayerDialogText;
    public Image DialogModal;
       
    private Scene _scene;
    private bool _playerAlive = true;
  


    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
       
    }
    private void Start()
    {

    }

    private void Update()
    {
        DialogDelay -= Time.deltaTime;
        if(DialogDelay <= 0)
        {
            DialogModal.gameObject.SetActive(false);
            PlayerDialogText.gameObject.SetActive(false);

        }

        if (!_playerAlive && Input.GetKeyDown(KeyCode.R)) {
            ResetScene();
        }
        
    }

    public void ResetScene()
    {
        GameOverText.gameObject.SetActive(false);
        
        //todo how can we pass in scene name to reset player in current level
        SceneManager.LoadScene(_scene.name);
    }

    public void LoadNextSecne(string nextScene) 
    {
        SceneManager.LoadScene(nextScene);   
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
