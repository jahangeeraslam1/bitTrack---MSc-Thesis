using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class feedbackBox : MonoBehaviour
{

    public TextMeshProUGUI feedbackText;

    
    // Start is called before the first frame update
    void Start()
    {
        feedbackText.text = "Answer the questions by dragging the tiles into the slots for each stage, then click submit!";

        
    }

    public void notCompletedAllAnswers(){
        feedbackText.text = "Error : Please complete every questions for each of the stages on the left hand side of the screen.";
    }


    public void incorrectAnswerDetected(){
        feedbackText.text = "Wrong Answer Detected. Wrong answers will be highlighted in red and correct answers highlighted in green. Fix your wrong answers and try again! ";

    }

     public void allCorrectAnswersDetected(){
        feedbackText.text = "Well Done!. Click continue to progress onto the next level";
     }

    public void goingBackTeachingConcepts(){
        feedbackText.text = "Warning : If you wish to return to the teaching concepts screen your saved answers will be lost. Click the help again to continue";
     }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explainLedger( int count){
        if (count == 0){
            feedbackText.text = "Great Job! Now lets look a bit more into the transactions from Stage C and D.";
            GameObject.Find("lockedUI").GetComponent<unlockGameObjects>().unlockBlockchainPanel();
        }
        else if (count == 1){
            feedbackText.text = "As you can see once Hannah completed the PoW for the Stage C transaction and the transaction details were added to the blockchain.";
            GameObject.Find("lockedUI").GetComponent<unlockGameObjects>().showCircleLedgerText();
        }
        else if (count == 2){
            feedbackText.text = "You will use details within this explorer during the next level to examine when and who performed certain transactions.";
            GameObject.Find("lockedUI").GetComponent<unlockGameObjects>().hideCircleLedgerText();
        }
        else {
            GameObject.Find("submitButton").GetComponent<submitButton>().setGoToNextLevel(true);

        }
        
      

        


    }
}
