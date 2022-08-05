using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] checkPoints;

    [HideInInspector]
    public int currentCheckPoint = 1;


    void Awake()
    {
        checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        currentCheckPoint = 1;


    }


    void Start()
    {

        foreach (GameObject checkPoint in checkPoints)
        {
            //checkPoint.AddComponent<CurrentCheckPoint>();
            checkPoint.GetComponent<CurrentCheckPoint>().currentCheckNumber = currentCheckPoint;
            checkPoint.name = "CheckPoint" + currentCheckPoint;
            currentCheckPoint++;
        }

    }
}
