using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWriter : MonoBehaviour
{
    string End = @"Placing the cursed amulet on the altar the thief is overcome with relief as his maddening fever breaks and his mind is freed. Has the lesson sunk in or is he doomed to be cursed again. The allure of gold and jewels may yet again test his resolve.";
    public float delay = .25f; 
 
    public Text textComp;

    // Use this for initialization
    void Start()
    {
        
       
        textComp.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in End.ToCharArray())
        {
            textComp.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
}
