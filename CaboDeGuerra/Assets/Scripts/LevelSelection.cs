using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    private void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1);
        
        for(int j = 0; j < lvlButtons.Length; j++)
        {
            if (j + 1 > levelAt)
            {
                lvlButtons[j].interactable = false;
            }
        }
    }
}
