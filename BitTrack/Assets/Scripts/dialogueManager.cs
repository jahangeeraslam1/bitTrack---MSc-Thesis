using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;


public class dialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI labelComponent;

    public GameObject buttons;

    public GameObject teachingImage;

    private List<string> data;
    public float textSpeed;
    public string label;

    private int index;

    private string teachingContentPath;
    private string teachingImagesPath;


    public List<string> ExplainConceptsImageNames;

    private int imageIndex;

    public GameObject extraImage;



    void Start(){
        textComponent.text = string.Empty; //set text in dialogue to be empty
        labelComponent.text = label; //set the page title to the label 
        teachingContentPath =   Application.streamingAssetsPath + "/Levels/"  + GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() + "/TeachingContent.txt"; 
        teachingImagesPath =   Application.streamingAssetsPath + "/Levels/"  + GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() + "/Teaching-Images/";
        data = readDataFile(teachingContentPath);
        index = 0;
        imageIndex = 0;
        setUpStageImage(ExplainConceptsImageNames[imageIndex]);
        extraImage.GetComponent<Image>().enabled = false;
        startDialogue();
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            if (!(buttons.activeSelf == true)){
                StopAllCoroutines();

                
                textComponent.text = "";
                foreach (char c in data[index].ToCharArray())
                {
                    if (c == '§')
                    {
                        textComponent.text += '\n';
                        textComponent.text += '\n';
                    }
                    else if (c == '±')
                    {
                        textComponent.text += '\n';
    
                    }
                    else{
                        textComponent.text += c;
                    }
                }
                buttons.SetActive(true);
            }
        }
    }

    private List<string> readDataFile(string filePath){
        List<string> lines = File.ReadAllLines(filePath).ToList();
        foreach (string line in lines){
            Console.WriteLine(line);
        }
        return lines;
    }


    // Start is called before the first frame update
 

    void startDialogue(){
        StartCoroutine(typeLine()); //method we created below
    }
    IEnumerator typeLine()
    {
        buttons.SetActive(false);
        foreach(char c in data[index].ToCharArray()){ //takes the string and breaks it down into a char array
            if(c == '§'){
                textComponent.text += '\n';
                textComponent.text += '\n';
            }
            else if ( c == '±'){
                  textComponent.text += '\n';
            }
            else{
                textComponent.text += c;
            }

            yield return new WaitForSeconds(textSpeed);

        }
        buttons.SetActive(true);
    }

    public void nextLine(){
        deSelectButton();
        if(buttons.activeSelf == true){
            print("STAGE a");
            if (index < data.Count - 1 ){
                print("STAGE b");
                index++;
                textComponent.text = string.Empty;
                getNextImage();
                StartCoroutine(typeLine());
                print("STAGE C");
                
            }
            else{
                print("STAGE d");
                 print("You have reached the end, you will now go to the beginning of Level" + GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() + "gameplay.");
                 if (GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() == 1){
                    GameObject.Find("navigation").GetComponent<navigation>().goToLevel1Gameplay();
                 }
                 else if(GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() == 2){
                    GameObject.Find("navigation").GetComponent<navigation>().goToLevel2Gameplay();

                 }
                 
            }
        }
    }

     public void previousLine(){
        deSelectButton();
        if( buttons.activeSelf == true){
            if (index > 0){
                index--;
                textComponent.text = string.Empty;
                getPreviousImage();
;
                StartCoroutine(typeLine());
            }
            else{
                print("You have reached the beginning, use the home button to return home");
            }

        }
    }

    private void deSelectButton(){
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);

    }


    public void getPreviousImage(){
        if (imageIndex > 0){
            imageIndex = imageIndex - 1;
            setUpStageImage(ExplainConceptsImageNames[imageIndex]);
        }
        else{
            //Debug.Log("You are viewing the the first image!");
        }
        int currentLevel = GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel();
        if(index == 4 && (currentLevel == 1)){
            extraImage.GetComponent<Image>().enabled = true;
        }
        else{
            extraImage.GetComponent<Image>().enabled = false;
        }
    


    }

    public void getNextImage(){
        if (imageIndex < (ExplainConceptsImageNames.Count - 1)){
            imageIndex++;
            setUpStageImage(ExplainConceptsImageNames[imageIndex]);
        }
        else{
            //Debug.Log("You are viewing the last image");
        }

        int currentLevel = GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel();
        if(index == 4 && (currentLevel == 1)){
            extraImage.GetComponent<Image>().enabled = true;
        }
        else{
            extraImage.GetComponent<Image>().enabled = false;
        }
    
    


    }


    public void setUpStageImage(string nameOfImage){
        //Debug.Log("Begin Setup Stage Image...");
        //Debug.Log("Name of image to load is:" + nameOfImage);
        Sprite imageToShow2 = Resources.Load<Sprite>("Levels/" + GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel() + "/Teaching-Images/" + nameOfImage);
        if (!(imageToShow2 == null)){
            //Debug.Log("Setting stage image to" + nameOfImage );  
            teachingImage.GetComponent<Image>().sprite = imageToShow2;
        }
        else{
        //Debug.Log("The image can not be found");  
        }
        //Debug.Log("Image Setup Completed");


    }
}
