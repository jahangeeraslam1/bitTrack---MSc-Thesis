using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;



public class stageSetUp : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject tileOutlinePrefab;
    public GameObject answerSlotParent;

    public GameObject tileParent;

    public GameObject tileOutlineParent;

    public GameObject question;

    public GameObject stageImage;

    private int currentLevel;



    private List<Transform> tileSpawnLocations = new List<Transform>();

    private List<Transform> answerSlotLocations = new List<Transform>();

    private List<GameObject> answerSlotObjects = new List<GameObject>();


    private string stageID;

    public Sprite imageToShow2;

    public void setStageID(string str){
        stageID = str;
    }

    public string getStageID(){
        return stageID;
    }

    private Color32 tileColour;



    public class stageData
    {
        public string stageLetter;
        public string stageQuestion;
        public string[] stageAnswers;
        public string[] correctAnswers;

        public string stageImageName;

        public bool orderEnabled;


    }

    private stageData loadedStage;


    public List<GameObject> getAnswerSlotObjects()
    { //GETTER METHOD
        return answerSlotObjects;
    }


    void Start()
    {
        currentLevel = GameObject.Find("navigation").GetComponent<navigation>().getCurrentLevel();
        setColourOfTile();
        //Debug.Log("BEGIN STAGE " + stageID + " SETUP!");
        if (readJSON(stageID))
        { // if it can read Stage Data from json file 

            setUpStageContents();
            setUpStageImage();
            setUpOrder();
        }
        else
        {
            //Debug.Log("Unable to complete set up for stage " + stageID);
        }

    }


    public void setUpStageContents(){

        //set up number array of answerSlotLocations
            int numberOfAnswerSlots = loadedStage.correctAnswers.Length;
            //Debug.Log("answerSlots Count : " + numberOfAnswerSlots);
            for (int i = 0; i < numberOfAnswerSlots; i++)
            {
                answerSlotLocations.Add(GameObject.Find("answerSlots").transform.GetChild(i).gameObject.transform);

            }

            //set up number of tileSpawnLocations
            int numberOfTiles = loadedStage.stageAnswers.Length;
            //Debug.Log("Tile Count : " + numberOfTiles);
            for (int i = 0; i < numberOfTiles; i++)
            {
                tileSpawnLocations.Add(GameObject.Find("spawnLocations").transform.GetChild(i).gameObject.transform);

            }

            //create new answerSlot for each item in answerSlotLocations and add it to answerSlotObjects array
            for (int i = 0; i < answerSlotLocations.Count; i++)
            {
                GameObject newAnswerSlot = Instantiate(tileOutlinePrefab, answerSlotLocations[i].position, Quaternion.identity, answerSlotParent.transform);
                answerSlotObjects.Add(newAnswerSlot);
            }
            //Debug.Log("Answer Slots Updated");


            //create a new tile and tileOutline for each item in tileSpawnLocations
            for (int i = 0; i < tileSpawnLocations.Count; i++)
            {
                GameObject newTileOutline = Instantiate(tileOutlinePrefab, tileSpawnLocations[i].position, Quaternion.identity, tileOutlineParent.transform);
                GameObject newTile = Instantiate(tilePrefab, tileSpawnLocations[i].position, Quaternion.identity, tileParent.transform);

                newTile.GetComponent<answerTile>().tileText.text = loadedStage.stageAnswers[i]; //change text on tile
                newTile.GetComponent<Image>().color = tileColour; //change color of tile
                newTile.GetComponent<answerTile>().tileText.color = new Color32(255, 255, 255, 255); //change colour of text 

            }
            //Debug.Log("Tiles and Tile Outlines Updated");

            //update the questionText 
            question.GetComponent<TextMeshProUGUI>().text = loadedStage.stageQuestion;
            //Debug.Log("Question Updated");

            //set the correctAnswersToEachAnswerSlot
            for (int i = 0; i < answerSlotObjects.Count; i++)
            { //for each of the answerSlots on the screen
                answerSlotObjects[i].GetComponent<answerSlot>().setCorrectAnswer(loadedStage.correctAnswers[i]);
                //Debug.Log(" Correct Answer Set : " + answerSlotObjects[i].GetComponent<answerSlot>().getCorrectAnswer());
            }

    }



    public bool readJSON(string stageID)
    {
        string json = File.ReadAllText( Application.streamingAssetsPath + "/Levels/" + currentLevel+ "/Gameplay-Content/stage" + stageID + "Data.json");
        loadedStage = JsonUtility.FromJson<stageData>(json);
        //Debug.Log("READ JSON STAGE DATA completed!");
        return true;

    }


    public void setUpStageImage(){
        //Debug.Log("Begin Setup Stage Image...");
        
        string nameOfImage = loadedStage.stageImageName;
        //Debug.Log("Name of image to load is:" + nameOfImage);
        
        imageToShow2 = Resources.Load<Sprite>("Levels/" + currentLevel +"/Gameplay-Images/" + nameOfImage );
        if (!(imageToShow2 == null)){
            //Debug.Log("Setting stage image to" + nameOfImage );  
            stageImage.GetComponent<Image>().sprite = imageToShow2;
        }
        else{
        //Debug.Log("The image can not be found");  
        }

        //Debug.Log("Image Setup Completed");


    }

    public void setColourOfTile(){

        if (currentLevel == 1){
            tileColour = new Color32(234, 163, 50, 255); //orange - level 1 
        }
        else if(currentLevel == 2){
             tileColour = new Color32(153, 112, 176, 255); //purple - level 2
        }
        else{
            tileColour = new Color32(0, 0, 0, 255); //black - error detected
        }

    }

    public void setUpOrder(){
        bool orderEnabled = loadedStage.orderEnabled;
        //Debug.Log("The stage "+ stageID + " has the order enabled set as " + orderEnabled);
        if (orderEnabled){
            for (int i = 0; i < answerSlotObjects.Count; i++){
                GameObject.Find("answerSlots").GetComponent<slotOrder>().enableOrderNumber(i);
            }

        }

    }




}

