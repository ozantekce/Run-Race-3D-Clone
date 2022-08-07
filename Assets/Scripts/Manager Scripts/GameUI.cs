using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    public static GameUI instance;
    public GameObject inGame, leaderboard;

    private Button nextLevel;

    public Text countText;

    void Awake()
    {
        instance = this;
        StartCoroutine(StartGame());
    }

    void Update()
    {

        if (GameManager.instance.failed)
        {
            if (leaderboard.activeInHierarchy)
            {
                GameManager.instance.failed = false;
                Restart();
            }
        }

    }


    private IEnumerator StartGame()
    {

        Color[] colors = { Color.magenta, Color.yellow, Color.green };
        for (int i = 3; i >= 1; i--)
        {
            
            countText.text = i.ToString();
            yield return new WaitForSeconds(1);
            countText.color = colors[colors.Length - i];
        }
        countText.text = "GO";
        GameManager.instance.start = true;
        yield return new WaitForSeconds(0.5f);
        countText.gameObject.SetActive(false);

    }

    public void OpenLeaderboard()
    {

        inGame.SetActive(false);
        leaderboard.SetActive(true);
        
    }

    private void Restart()
    {

        nextLevel = GameObject.Find("/GameUI/LeaderboardPanel/NextLevel").GetComponent<Button>();
        nextLevel.onClick.RemoveAllListeners();
        nextLevel.onClick.AddListener(()=>Reload());
        nextLevel.transform.GetChild(0).GetComponent<Text>().text = "Again";

    }

    private void Reload()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void NextLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }


    public void Exit()
    {
        SceneManager.LoadScene(0);
    }


}
