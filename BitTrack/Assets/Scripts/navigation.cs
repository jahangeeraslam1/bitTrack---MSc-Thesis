using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navigation : MonoBehaviour
{

   public int level;



   public void goToHome(){
      SceneManager.LoadScene(0);
   }

   public void goToLeaderBoard(){
      SceneManager.LoadScene(1);
   }

   public void goToExplainConceptsLevel1()
   {
    SceneManager.LoadScene(2);
   }


   public void goToLevel1Gameplay(){
          SceneManager.LoadScene(3);

   }
   public void goToExplainConceptsLevel2(){
      
      SceneManager.LoadScene(4);



   }


   public void goToLevel2Gameplay(){
       SceneManager.LoadScene(5);
       //Debug.Log("LEVEL 2 LOADED");

   }

   public void goToLevel3test(){
       SceneManager.LoadScene(6);

   }

   public void QuitGame()
   {
      #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
   }

   public int getCurrentLevel(){
      return level;

   }

   public void setCurrentLevel(int value){
      level = value;
   }

 


}

