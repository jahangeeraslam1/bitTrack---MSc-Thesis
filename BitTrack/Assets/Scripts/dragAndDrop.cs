using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTrans;
    private CanvasGroup canvasGroup; 
    private Vector2 originalTilePosition;

    private List<GameObject> tileAnswerSlots;

    void Awake(){ 
        //Debug.Log("DRAG AND DROP FILE LOADED");
        rectTrans = GetComponent<RectTransform>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        canvas = GameObject.Find("foregroundCanvas").GetComponent<Canvas>();
        originalTilePosition = rectTrans.anchoredPosition;
        tileAnswerSlots = GetComponentInParent<stageSetUp>().getAnswerSlotObjects();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BEGIN DRAG");
        canvasGroup.blocksRaycasts = false; //disables the draggable item from capturing events
        canvasGroup.alpha = 0.6f; //set transparency when dragging
        
        for( int i = 0; i < tileAnswerSlots.Count ; i++){ //for each of the answerSlots on the screen
            GameObject tile = tileAnswerSlots[i].GetComponent<answerSlot>().getTileInAnswerBox();
            if (tile == eventData.pointerDrag){
                tileAnswerSlots[i].GetComponent<answerSlot>().setIntersect(false); //sets intersect as false allowing us to check for new intersect each time a drag has ocurred
                tileAnswerSlots[i].GetComponent<answerSlot>().setTileAssignedToAnswerBox(false); 
            }
        }



      

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("DRAG ENDED"); 
        canvasGroup.blocksRaycasts = true; //allows item to now capture other events 
        canvasGroup.alpha = 1f; //set transparency back to normal 
       
        bool moveBack = true; //moveBack tile to original position 
         for( int i = 0; i < tileAnswerSlots.Count ; i++){ //for each of the answer slots 
            bool value = tileAnswerSlots[i].GetComponent<answerSlot>().getIntersect(); 
            tileAnswerSlots[i].GetComponent<Image>().color = new Color32(0,0,0,255);
            
            print("The intersect is: " + value);
            if (value != false){ //if even one of the answerSlots detected an intersect then don't move the tile back, otherwise do 
               moveBack = false;

            }
         }
         if (moveBack){ //check if we need to move the tile back and performs the move
             rectTrans.anchoredPosition = originalTilePosition; //returns tile to original position
        }
     
    }

    public void OnDrag(PointerEventData eventData)//called for every frame during dragging
    {
        //Debug.Log("DRAG STARTED");
        rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor; //.delta contains the amount that the mouse moved since the previous frame 
    }


}
