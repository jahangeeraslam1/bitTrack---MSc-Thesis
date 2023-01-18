using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class submitButton : MonoBehaviour
{

    public TextMeshProUGUI submitButtonText;



    private bool goToNextLevel = false;



    
    public void allAnswersCorrectText(){
        submitButtonText.text = "Continue";

    }

    public void setGoToNextLevel(bool value){
        goToNextLevel = value; 

    }

    public bool getGoToNextLevel(){
        return goToNextLevel;
    }


}
