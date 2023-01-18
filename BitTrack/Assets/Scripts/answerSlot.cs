using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class answerSlot : MonoBehaviour, IDropHandler
{
private bool intersect = false; 
private GameObject tileInAnswerBox = null;

private string correctAnswer = "NONE";

private bool tileAssignedToAnswerBox = false;

public void OnDrop(PointerEventData eventData){
    //Debug.Log("Tile dropped into answerSlot");
    intersect = true; //if drop within answerSlot set intersect to be true 
    if (eventData.pointerDrag != null){ //ensures the game object is being dragged 
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition; //set the position of the answerTile to the answerSlot 
        setTileInAnswerBox(eventData.pointerDrag); //sets this tile as the tile currently in the answer space - can be used to check answer later
        
        print("Current answer in answerSlot is  " + tileInAnswerBox.GetComponent<answerTile>().tileText.text);
    } 
}

//GETTER METHODS

public bool getIntersect(){ 
    return intersect;
}
public string getCorrectAnswer(){ 
    return correctAnswer;
}


public GameObject getTileInAnswerBox(){ 
    return tileInAnswerBox;
}




public bool isTileAssignedToAnswerBox(){
    return tileAssignedToAnswerBox;
}




//SETTER METHODS

public void setIntersect(bool intersectVal){ 
    intersect = intersectVal;
}

public void setCorrectAnswer(string correctA){ 
    correctAnswer = correctA;
}
public void setTileInAnswerBox(GameObject s){ 
       tileInAnswerBox = s;
       setTileAssignedToAnswerBox(true);
       
     
}
public void setTileAssignedToAnswerBox(bool b){
    tileAssignedToAnswerBox = b;

}



}
