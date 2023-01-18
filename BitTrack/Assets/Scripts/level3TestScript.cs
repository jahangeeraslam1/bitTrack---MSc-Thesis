using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level3TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("score").GetComponent<score>().showFinalScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
