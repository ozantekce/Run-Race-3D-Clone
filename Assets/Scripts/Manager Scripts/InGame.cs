using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame : MonoBehaviour
{

    public Text[] namesText;
    public Image thirdPlaceImg, secondPlaceImg, firstPlaceImg;
    public string a, b, c;


    void Update()
    {
        namesText[0].text = a;
        namesText[1].text = b;
        namesText[2].text = c;
    }

}
