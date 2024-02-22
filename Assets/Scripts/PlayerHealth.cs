using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int numOflives = 6;
    public Image[] imageLivesArray;

    void Update()
    {
        numOflives = Mathf.Clamp(numOflives, 0, 6);

        for(int i = 0; i < imageLivesArray.Length; i++) 
        {
            if(i < numOflives) 
            {
                imageLivesArray[i].enabled = true;
            }
            else imageLivesArray[i].enabled = false;
        }
    }

    public void TakeDamage()
    {
        numOflives--;
    }
}
