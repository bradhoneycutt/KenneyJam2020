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
    public float AnimationDelay = .5f;
    private float AnimationCoolDown = 0; 

    private void Update()
    {
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

        AnimationCoolDown -= Time.deltaTime;
        if (AnimationCoolDown <= 0)
        {
            for (int i = 0; i < Hearts.Length; i++)
            {
                var currentPos = Hearts[i].rectTransform.anchoredPosition;


                Hearts[i].rectTransform.anchoredPosition = currentPos + new Vector2(0f, -10.5f);
                Hearts[i].rectTransform.anchoredPosition = currentPos; 
            }
            AnimationCoolDown = AnimationDelay; 
        }
    }
}
