using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class slotOrder : MonoBehaviour
{


public TextMeshProUGUI [] numbers;




    public void enableOrderNumber(int i){
        numbers[i].enabled = true;
        //Debug.Log(" NUMBER for slot" + i + "enabled");
    }
}
