using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrimenSelection : MonoBehaviour
{
    [SerializeField] private CanvasGroup TrimenSelectionCG;
    [SerializeField] private CanvasGroup TrimenSelectedCG;

    static bool displayTrimenMsg = false;

    static GameObject feedBackText;
    static GameObject currentPlayerText;

    private void Start()
    {
        // Retrieve and intitialize the feedback text
        feedBackText = GameObject.Find("FeedBackText");
        feedBackText.GetComponent<TMPro.TextMeshProUGUI>().text = "";

        // Set the current player (who will start), for the first game only
        if(GameController.getFirstGame())
            GameController.setCurrentPlayer(0);

        // Retrieve and intitialize the current player text
        currentPlayerText = GameObject.Find("CurrentPlayerText");
        int playerNumber = GameController.getCurrentPlayer() + 1;
        currentPlayerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber;
    }


    private void Update()
    {
        if (displayTrimenMsg)
            DisplayTrimenMsg();
    }


    public static bool IsTheTrimen(int dice1Val, int dice2Val)
    {
        // There is a 3, current player is the trimen
        if (dice1Val == 3 || dice2Val == 3 || (dice1Val + dice2Val) == 3)
        {
            displayTrimenMsg = true;
            DiceRoll.DisableDicerRolling();

            return true;
        }
        // There is no 3, we update the message
        else
        {            
            if(dice1Val != 0 && dice2Val !=0)
                feedBackText.GetComponent<TMPro.TextMeshProUGUI>().text = "Vous n'etes pas le TRIMENS, au joueur suivant !";

            return false;
        }
    }


    void DisplayTrimenMsg()
    {
        if (TrimenSelectionCG.alpha > 0)
            TrimenSelectionCG.alpha -= Time.deltaTime;

        if (TrimenSelectedCG.alpha < 1)
            TrimenSelectedCG.alpha += Time.deltaTime;

        if (TrimenSelectionCG.alpha == 0 && TrimenSelectedCG.alpha >= 1)
            displayTrimenMsg = false;
    }

    public static void UpdateCurrentPlayerText(int iCurrentPlayer)
    {
        int playerNumber = iCurrentPlayer + 1;
        currentPlayerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber;
    }

    public static void ClearTrimensMessage()
    {
        feedBackText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
    }

    public void SiwtchToGameScene()
    {
        if(!GameController._LookingForTrimen)
            GameController.SwitchToNextScene();
    }
}
