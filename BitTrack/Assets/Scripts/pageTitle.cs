using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pageTitle : MonoBehaviour
{
        public TextMeshProUGUI tileText;



public void changePageTitle(int value){
    tileText.text = "Level " + value;

}

}