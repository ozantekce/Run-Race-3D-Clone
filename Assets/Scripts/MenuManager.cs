using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{

    public InputField playerName;
    

    void Start()
    {


    }



    public void StartGame()
    {

        if (playerName.text.Equals(""))
        {
            PlayerPrefs.SetString("PlayerName", "Player");
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", playerName.text);
        }

        SceneManager.LoadScene(PlayerPrefs.GetInt("Level",1));

    }

}
