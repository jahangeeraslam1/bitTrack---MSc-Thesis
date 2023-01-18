using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockGameObjects : MonoBehaviour
{
    public GameObject lockedBlockchainPanel;

    public GameObject circleLedgerText;


    public void unlockBlockchainPanel(){
        lockedBlockchainPanel.SetActive(false);
        

    }


    public void showCircleLedgerText(){
        circleLedgerText.SetActive(true);

    }

    public void hideCircleLedgerText(){
        circleLedgerText.SetActive(false);

    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
