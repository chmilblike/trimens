using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour
{
    static int nbRulesToApply = 0;
    static int remainingRulesToApply = 0;

    // Flags    
    static bool _nextPlayerEvent = false;
    static bool _nextRuleEvent = false;
    static bool _replayEvent = false;

    // Text
    static GameObject titleText;
    static GameObject informationText;
    static GameObject currentPlayerText;

    // Other Game Object
    static GameObject informationFrame;

    void Start()
    {
        // Retrieve and intitialize texts
        titleText = GameObject.Find("Title");
        titleText.GetComponent<TMPro.TextMeshProUGUI>().text = "Lancez les des";

        informationText = GameObject.Find("InfomationText");
        informationText.GetComponent<TMPro.TextMeshProUGUI>().text = "";

        currentPlayerText = GameObject.Find("CurrentPlayerText");
        int playerNumber = GameController.getCurrentPlayer() + 1;
        currentPlayerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber;

        // Retrieve and disable information frame
        informationFrame = GameObject.Find("InformationFrame");
        informationFrame.SetActive(false);        
    }

    void Update()
    {
        
    }

    static public void RulesToApply(int nbRules)
    {
        nbRulesToApply = nbRules;
        remainingRulesToApply = nbRules - 1;

        // Set title text
        SetText(titleText, "Appliquez les regles");

        // Set information Text
        informationFrame.SetActive(true);
        SetText(informationText, GameController._rulesToApply[0]);

        _nextRuleEvent = true;

        DiceRoll.DisableDicerRolling();

    }

    

    
    static public void NothingHappen()
    {        
        if (GameController.getCurrentPlayer() != GameController.getTrimen())
        {
            // Set title text
            SetText(titleText, "Rien ne se passe ...");

            // Set information Text
            informationFrame.SetActive(true);
            SetText(informationText, "Au joueur suivant");

            _nextPlayerEvent = true;
        }        
        else
        {
            // Set title text
            SetText(titleText, "Partie Terminee");

            // Set information Text
            informationFrame.SetActive(true);
            SetText(informationText, "Nouvelle partie ?");

            _replayEvent = true;
        }

        DiceRoll.DisableDicerRolling();
    }

    

    static public void GameButtonClic()
    {
        if (_nextPlayerEvent)
            NextPlayerEvent();

        if (_nextRuleEvent)
            NextRuleEvent();

        if (_replayEvent)
            ReplayEvent();
    }

    static public void NextRuleEvent()
    {
        if (_nextRuleEvent)
        {
            if (0 == remainingRulesToApply)
            {
                // Set title text
                SetText(titleText, "Nouveau tirage");

                // Set information Text
                informationFrame.SetActive(false);

                DiceRoll.EnableDicerRolling();
                _nextRuleEvent = false;
            }
            else
            {
                // Set information Text
                SetText(informationText, GameController._rulesToApply[nbRulesToApply - remainingRulesToApply]);

                remainingRulesToApply--;
            }            
        }
    }

    static public void NextPlayerEvent()
    {
        if (_nextPlayerEvent)
        {
            // Switch to next player
            GameController.SwitchToNextPlayer();

            // Set title text
            SetText(titleText, "Lancez les des");

            // Set current player text
            currentPlayerText = GameObject.Find("CurrentPlayerText");
            int playerNumber = GameController.getCurrentPlayer() + 1;
            currentPlayerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber;

            // Check if the current player is the Trimen
            if (GameController.getCurrentPlayer() == GameController.getTrimen())
                currentPlayerText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber + " [Trimen]";

            // Hide information frame
            informationFrame.SetActive(false);

            // Place Dice in their initial position

            _nextPlayerEvent = false;

            // Enable dice rolling
            DiceRoll.EnableDicerRolling();
        }
    }

    static public void ReplayEvent()
    {
        _replayEvent = false;
        GameController.SwitchToNextPlayer();
        GameController.setFirstGame(false);
        GameController.setLookingForTrimen(true);
        GameController.GoBackToTrimenSelectionScene();        
    }

    static public void SetText(GameObject iTextGameObject, string iText)
    {
        iTextGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = iText;
    }
}
