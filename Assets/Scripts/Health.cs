using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int PlayerHealth;
    public int NumberHearts;

    public Image[] Hearts;
    public Sprite HalfHeart;
    public Sprite FullHeart;
    public Sprite EmptyHeart;

    private void Update()
    {

        Debug.Log("PlayerHealth" + PlayerHealth);

        for (int i = 0; i < Hearts.Length; i++)
        {

            if(i< PlayerHealth)
            {
                Hearts[i].sprite = FullHeart;
            }
            else
            {
                Hearts[i].sprite = EmptyHeart;
            }

            if(i < NumberHearts)
            {
                Hearts[i].enabled = true;
            } else
            {
                Hearts[i].enabled = false;
            }
        }
    }
}
