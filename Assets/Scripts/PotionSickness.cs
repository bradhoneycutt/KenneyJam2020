using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionSickness : MonoBehaviour
{
        public int Potions;
        public int NumberOfPotions;


        public Image[] Skulls;
        public Sprite Skull;


        private void Update()
        {
            for (int i = 0; i < Skulls.Length; i++)
            {

                if (i < Potions)
                {
                    Skulls[i].sprite = Skull;
                }
                else
                {
                    Skulls[i].enabled = false;
                }

                if (i < NumberOfPotions)
                {
                    Skulls[i].enabled = true;
                }
                else
                {
                    Skulls[i].enabled = false;
                }

            }
        }
    
}
