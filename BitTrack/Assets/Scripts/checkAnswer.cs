using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkAnswer : MonoBehaviour
{

    private List<GameObject> answerSlotObjects = new List<GameObject>();

    private string[] answersArray;

    public GameObject tickImage;

    public GameObject crossImage;

    List<GameObject> createdStages = new List<GameObject>();

    public GameObject submitButton;

    private int explainCounter;

    int clickedHelp;


    void Start()
    {

        clickedHelp = 0;
        //Debug.Log("Begin Submit Section Setup");

        enableTickImage(false);
        enableCrossImage(false);

        createdStages = GameObject.Find("loadedStages").GetComponent<stagesScript>().getCreatedStages();

        explainCounter = 0;

        //Debug.Log("createdStages.Count is " + createdStages.Count);
        //Debug.Log("Completed Submit Section Setup");



    }


    public void enableTickImage(bool value)
    {
        tickImage.GetComponent<Image>().enabled = value;

    }
    public void enableCrossImage(bool value)
    {
        crossImage.GetComponent<Image>().enabled = value;

    }






    public bool preCheck(){
        //Debug.Log("createdStages.Count " + createdStages.Count );
        for (int x = 0; x < createdStages.Count; x++)
            { //checks each of the stages 
                answerSlotObjects = createdStages[x].GetComponent<stageSetUp>().getAnswerSlotObjects();
                //Debug.Log("answerSlotObjects.Count " + answerSlotObjects.Count );

                if (answerSlotObjects.Count > 0){
                     //Debug.Log("checkA" );
                    for (int y = 0; y < answerSlotObjects.Count; y++){
                        //Debug.Log("checkB" );
                        if (!(answerSlotObjects[y].GetComponent<answerSlot>().isTileAssignedToAnswerBox())){
                            //Debug.Log("checkC is " + answerSlotObjects[y].GetComponent<answerSlot>().isTileAssignedToAnswerBox() );
                            //Debug.Log("checkD is " + answerSlotObjects[y].GetComponent<answerSlot>().getTileInAnswerBox());
                            return false;
                        }    
                    }
 
                }
                else{
                    return false;
                }
            }
        return true;
        
    }


    public void buttonClicked(){
        if( submitButton.GetComponent<submitButton>().getGoToNextLevel()){
            if((GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() == 1)){
                //Debug.Log("Going to LEVEL 2 TEACHING CONCEPTS");
                GameObject.Find("navigation").GetComponent<navigation>().goToExplainConceptsLevel2();

            }
            else if((GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() == 2)){
                //Debug.Log("Going to LEVEL 3 TEST SCREEN");
                GameObject.Find("navigation").GetComponent<navigation>().goToLevel3test();

            
        }
        }
        else{
            checkAnswers();
        }
    }

    public void helpButtonClicked(){
        GameObject.Find("feedbackBox").GetComponent<feedbackBox>().goingBackTeachingConcepts();
        clickedHelp++;
        if(clickedHelp >= 2){
            if (GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() == 1){
                GameObject.Find("navigation").GetComponent<navigation>().goToExplainConceptsLevel1();

            }
            else if (GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() == 2)
            GameObject.Find("navigation").GetComponent<navigation>().goToExplainConceptsLevel2();


        }

         
        


    }

    public void checkAnswers()
    {
        bool allAnswersCorrect = true;
        enableCrossImage(false);
        enableTickImage(false);
        Button[] stageButtons = GameObject.Find("loadedStages").GetComponent<stagesScript>().getStageButtons();

        if(preCheck()){
            //Debug.Log("PRECHECK()" + preCheck() );
            for (int x = 0; x < createdStages.Count; x++) { 
                //checks each of the stages which exists 
                answerSlotObjects = createdStages[x].GetComponent<stageSetUp>().getAnswerSlotObjects();
                //Debug.Log("AnswerSlotObjects number : " + answerSlotObjects.Count);
                for (int i = 0; i < answerSlotObjects.Count; i++)
                { // do this for every answerSlot object in the stage
                    bool stageAnswersCorrect = true;
                    if (answerSlotObjects[i].GetComponent<answerSlot>().isTileAssignedToAnswerBox())
                    { // ensure answerSlot contains a tile
                        string correctAnswer = answerSlotObjects[i].GetComponent<answerSlot>().getCorrectAnswer(); //gets text of correctAnswer for the slot
                        GameObject currentTile = answerSlotObjects[i].GetComponent<answerSlot>().getTileInAnswerBox(); //gets tile in the slot
                        string currentTileText = currentTile.GetComponent<answerTile>().tileText.text; //get text of tile in the slot 

                        //Debug.Log("Correct Answer is : " + correctAnswer);
                        //Debug.Log("Answer assigned to Tile box is : " + currentTileText);

                        //set the stage icons to be false so the old icons don't cover the new icons
                        stageButtons[x].GetComponent<stageButton>().tickIcon.enabled = false;
                        stageButtons[x].GetComponent<stageButton>().crossIcon.enabled = false;
                        enableTickImage(false);
                        enableCrossImage(false);

                        if (currentTileText == correctAnswer)
                        { //checks if answer is correct
                            answerSlotObjects[i].GetComponent<Image>().color = new Color32(0, 255, 0, 255); //set tileOutline colour to green
                            stageButtons[x].GetComponent<stageButton>().tickIcon.enabled = true;
                            //Debug.Log("Correct answer detected at AnswerSlot" + (i + 1) + "On Stage" + (x + 1));
                            

                        }
                        else
                        {
                            stageAnswersCorrect = false;
                            answerSlotObjects[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255); //set tileOutline colour to red
                            stageButtons[x].GetComponent<stageButton>().crossIcon.enabled = true;
                            //Debug.Log("Wrong answer detected at AnswerSlot" + (i + 1) + "On Stage" + (x + 1));
                            allAnswersCorrect = false;
                            enableCrossImage(true);
                            GameObject.Find("feedbackBox").GetComponent<feedbackBox>().incorrectAnswerDetected();
                            GameObject.Find("score").GetComponent<score>().reduceScore();
                            GameObject.Find("score").GetComponent<score>().updateScoreText();
                            
                        }

                    }
                
                    if (stageAnswersCorrect == false){
                        stageButtons[x].GetComponent<stageButton>().tickIcon.enabled = false;
                        stageButtons[x].GetComponent<stageButton>().crossIcon.enabled = true;

                    }
                    else if (stageAnswersCorrect == true) {
                        stageButtons[x].GetComponent<stageButton>().tickIcon.enabled = true;
                        stageButtons[x].GetComponent<stageButton>().crossIcon.enabled = false;

                    }

                }
            }
        }
        else{
            enableCrossImage(true);
            allAnswersCorrect = false;
            //Debug.Log("PLEASE ANSWER ALL QUESTIONS ON ALL THE STAGES FIRST");
            GameObject.Find("feedbackBox").GetComponent<feedbackBox>().notCompletedAllAnswers();
            for (int x = 0; x < createdStages.Count; x++) { 
                stageButtons[x].GetComponent<stageButton>().tickIcon.enabled = false;
                stageButtons[x].GetComponent<stageButton>().crossIcon.enabled = false;
            }



        }
        if(allAnswersCorrect){
            enableTickImage(true);
            GameObject.Find("feedbackBox").GetComponent<feedbackBox>().allCorrectAnswersDetected();
            submitButton.GetComponent<submitButton>().allAnswersCorrectText();

             if( GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() == 2){
                GameObject.Find("feedbackBox").GetComponent<feedbackBox>().explainLedger(explainCounter);
                explainCounter++;
                
             }
             else{
                GameObject.Find("submitButton").GetComponent<submitButton>().setGoToNextLevel(true);
             }
            
            

            


        }
            
    }
}



