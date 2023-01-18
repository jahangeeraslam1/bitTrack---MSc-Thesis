using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public  TextMeshProUGUI scoreText;

    private static int userScore = 200;


public int getScore(){
    return userScore;
}

public void setScore( int value){
    userScore = value;

}

public void reduceScore(){
    userScore = userScore - 10;
}

public void updateScoreText(){
    scoreText.text = "Score : " + userScore;
 
}

public void showFinalScore(){
    scoreText.text = " " + userScore +  " "; 


}



}







    

