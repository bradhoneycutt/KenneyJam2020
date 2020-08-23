using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGoal : MonoBehaviour
{
    public string NextScene;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Player")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().LoadNextSecne(NextScene); 
        }
    }
}
