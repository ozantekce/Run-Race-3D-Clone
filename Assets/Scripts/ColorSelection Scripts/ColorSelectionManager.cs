using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSelectionManager : MonoBehaviour
{

    private Camera cameraMain;
    private int currentPlayer = 0;

    public float speed = 0.5f;
    public float selectionPos = 15;

    public GameObject charParent;


    public static Color playerColor;

    void Awake()
    {
        cameraMain = Camera.main;



    }

    void Update()
    {
        
    }



    public void Play()
    {
        playerColor = charParent.transform.GetChild(currentPlayer).GetComponentInChildren<SkinnedMeshRenderer>().material.color;
        SceneManager.LoadScene("1");
    }


    public void Next()
    {
        if (waitForNext || waitForPrev)
            return;

        if (currentPlayer < charParent.transform.childCount-1)
        {
            currentPlayer++;
            StartCoroutine(MoveToNext());
        }
    }

    public void Prev()
    {
        if (waitForNext || waitForPrev)
            return;

        if (currentPlayer >0 )
        {
            currentPlayer--;
            StartCoroutine(MoveToPrev());
        }


    }

    private bool waitForNext;
    private IEnumerator MoveToNext()
    {

        waitForNext = true;
        Vector3 tempPos = new Vector3(cameraMain.transform.position.x + selectionPos
            , cameraMain.transform.position.y, cameraMain.transform.position.z);
        while(cameraMain.transform.position.x < tempPos.x)
        {
            cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, tempPos, speed*Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cameraMain.transform.position = tempPos;
        waitForNext = false;
        yield return null;
    }

    private bool waitForPrev;

    private IEnumerator MoveToPrev()
    {

        waitForPrev = true;
        Vector3 tempPos = new Vector3(cameraMain.transform.position.x - selectionPos
            , cameraMain.transform.position.y, cameraMain.transform.position.z);
        while (cameraMain.transform.position.x > tempPos.x)
        {
            cameraMain.transform.position = Vector3.MoveTowards(cameraMain.transform.position, tempPos, speed*Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        cameraMain.transform.position = tempPos;
        waitForPrev = false;
        yield return null;
    }


}
