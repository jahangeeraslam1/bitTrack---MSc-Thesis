using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class stagesScript : MonoBehaviour
{

    private string selectedStage;
    public Button[] stageButtons;

    public GameObject newStagePrefab;

    public GameObject newStageParent;

    private List<GameObject> createdStages = new List<GameObject>();



    void Awake()
    {

        createAllStages();
        stageButtons[0].Select();
        switchStage();
        setStartingScore();
        setPageTitle();

    }
    public Button[] getStageButtons(){
        return stageButtons;
    }

    public List<GameObject> getCreatedStages()
    { //getter
        return createdStages;
    }


     public void switchStage()
    {
        for (int i = 0; i < stageButtons.Length; i++)//change colour of all buttons back to white
        {
            stageButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        selectedStage = EventSystem.current.currentSelectedGameObject.GetComponent<stageButton>().stageText.text; //set  the text of the button selected as the selected stage 
        print("The stage selected is " + selectedStage);
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = new Color32(174, 202, 241, 180); //StencilMaterial colour to be different
        enableStageInScene();


    }

    public void createAllStages()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            GameObject newStage = Instantiate(newStagePrefab, newStageParent.transform);
            newStage.GetComponent<stageSetUp>().setStageID(stageButtons[i].GetComponent<stageButton>().stageText.text);
            //Debug.Log("Stage ID for Stage is  :" + newStage.GetComponent<stageSetUp>().getStageID());
            createdStages.Add(newStage);
            print("New Stage crated with DataToLoad value " + newStage.GetComponent<stageSetUp>().getStageID());

            //hides stage icons on the stage buttons
            stageButtons[i].GetComponent<stageButton>().tickIcon.enabled = false;
            stageButtons[i].GetComponent<stageButton>().crossIcon.enabled = false;
        }
    }
    
    public void enableStageInScene()
    {
        for (int i = 0; i < createdStages.Count; i++)
        {
            if (createdStages[i].GetComponent<stageSetUp>().getStageID() == selectedStage)
            {
                createdStages[i].SetActive(true);
            }
            else
            {
                createdStages[i].SetActive(false);

            }


        }
    }

    public void setStartingScore(){
        int currentLevel = GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel();
        if( currentLevel == 1){
            GameObject.Find("score").GetComponent<score>().setScore(200);
        }
    
        GameObject.Find("score").GetComponent<score>().updateScoreText();

    }

    public void setPageTitle(){
        int currentLevel = GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel();
        GameObject.Find("level").GetComponent<pageTitle>().changePageTitle(currentLevel);
    }


  

  




}
