using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{


    public int currentCheckPoint, lapCount;

    public float distance;
    private Vector3 checkPoint;

    public float counter;
    public int rank;

    void Start()
    {
        currentCheckPoint = 1;
        checkPoint = GameObject.Find("CheckPoint1").transform.position;
    }

    void Update()
    {
        CalculateDistance();

    }


    private void CalculateDistance()
    {

        distance = Vector3.Distance(transform.position,checkPoint);
        counter = lapCount * 1000 + currentCheckPoint * 100 + distance;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("CheckPoint"))
        {
            currentCheckPoint = other.GetComponent<CurrentCheckPoint>().currentCheckNumber;
            checkPoint = other.gameObject.transform.position;
        }

        if(other.tag == "Finish")
        {
            lapCount++;
            GameManager.instance.pass += 1;
        }

    }



}
