using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    static int nbRulesToApply = 0;
    static int remainingRulesToApply = 0;
    static int currentRuleIndex = 0;

    // Flags    
    static bool _nextPlayerEvent = false;
    static bool _nextRuleEvent = false;
    static bool _replayEvent = false;

    // Text
    static GameObject informationText;
    static GameObject titleText;

    // Rules Game Objects
    static GameObject rulesSection;
    static GameObject ruleText;
    static GameObject rulesTwoTagsSection;
    static GameObject rulesThreeTagsSection;

    static List<GameObject> ruleTagTwo = new List<GameObject>();
    
    //static GameObject ruleTagTwo1;
    //static GameObject ruleTagTwo2;

    static List<GameObject> ruleTagThree = new List<GameObject>();
    

    // Button
    static GameObject gameButton;
    static GameObject gameButtonText;

    void Start()
    {
        // Retrieve and intitialize texts
        informationText = GameObject.Find("InformationText");
        informationText.GetComponent<TMPro.TextMeshProUGUI>().text = "Lancez les des";

        titleText = GameObject.Find("CurrentPlayerText");
        int playerNumber = GameController.getCurrentPlayer() + 1;
        titleText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber;

        // Retrieve rules game objects
        rulesSection = GameObject.Find("RulesSection");        
        ruleText = GameObject.Find("RuleText");        
        rulesTwoTagsSection = GameObject.Find("TwoTags");
        rulesThreeTagsSection = GameObject.Find("ThreeTags");

        ruleTagTwo.Add(GameObject.Find("TwoTag1"));
        ruleTagTwo.Add(GameObject.Find("TwoTag2"));

        ruleTagThree.Add(GameObject.Find("ThreeTag1"));
        ruleTagThree.Add(GameObject.Find("ThreeTag2"));
        ruleTagThree.Add(GameObject.Find("ThreeTag3"));

        rulesSection.SetActive(false);
        rulesTwoTagsSection.SetActive(false);
        rulesThreeTagsSection.SetActive(false);

        // Retrieve button game objects
        gameButton = GameObject.Find("GameButton");
        gameButtonText = GameObject.Find("ButtonText");

        gameButton.SetActive(false);



        // Debug
        GameController._Players = new List<Player>();
        for (int i = 1; i <= 3; i++)
        {
            Player player = new Player();
            player.Name = "Player" + i.ToString();
            GameController._Players.Add(player);
        }
        GameController._Trimen = 2;
        GameController._CurrentPlayer = 0;
        GameController._LookingForTrimen = false;

    }

    void Update()
    {
        
    }

    static public void RulesToApply(int nbRules)
    {
        nbRulesToApply = nbRules;
        remainingRulesToApply = nbRules - 1;
        currentRuleIndex = 0;

        rulesSection.SetActive(true);

        // Activate button text if there is more than one rule
        if (1 != nbRulesToApply)
        {            
            gameButton.SetActive(true);
            SetText(gameButtonText, "");
        }

        if (2 == nbRulesToApply)
        {
            rulesTwoTagsSection.SetActive(true);
            ruleTagTwo[0].GetComponent<Image>().color = new Color32(152, 144, 144, 100);
        }
          

        if (3 == nbRulesToApply)
        {
            rulesThreeTagsSection.SetActive(true);
            ruleTagThree[0].GetComponent<Image>().color = new Color32(152, 144, 144, 100);
        }
            

        // Set rule text
        SetText(ruleText, GameController._rulesToApply[0]);

        // Erase non necessary text
        SetText(informationText, "");
        SetText(gameButtonText, "");

        _nextRuleEvent = true;

        //DiceRoll.DisableDicerRolling();
    }

    

    
    static public void NothingHappen()
    {
        // Disable rule section
        rulesSection.SetActive(false);
        rulesTwoTagsSection.SetActive(false);
        rulesThreeTagsSection.SetActive(false);

        SetText(informationText, "");

        DiceRoll.DisableDicerRolling();

        if (GameController.getCurrentPlayer() != GameController.getTrimen())
        {
            // Set button text
            gameButton.SetActive(true);
            SetText(gameButtonText, "Au joueur suivant");

            _nextPlayerEvent = true;
        }        
        else
        {
            // Set title text
            SetText(titleText, "Partie Terminee");

            // Set button text
            gameButton.SetActive(true);
            SetText(informationText, "Nouvelle partie ?");

            _replayEvent = true;
        }        
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
                // Disable rule section
                rulesSection.SetActive(false);
                rulesTwoTagsSection.SetActive(false);
                rulesThreeTagsSection.SetActive(false);

                // Set information text
                SetText(informationText, "Lance les des");

                DiceRoll.EnableDicerRolling();
                _nextRuleEvent = false;
            }
            else
            {
                // Set information Text
                SetText(ruleText, GameController._rulesToApply[nbRulesToApply - remainingRulesToApply]);
                SwitchRuleTag();

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
            SetText(informationText, "Lancez les des");

            // Set current player text
            titleText = GameObject.Find("CurrentPlayerText");
            int playerNumber = GameController.getCurrentPlayer() + 1;
            titleText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber;

            // Check if the current player is the Trimen
            if (GameController.getCurrentPlayer() == GameController.getTrimen())
                titleText.GetComponent<TMPro.TextMeshProUGUI>().text = "Joueur " + playerNumber + " [Trimen]";

            // Place Dice in their initial position

            _nextPlayerEvent = false;

            // Enable dice rolling
            DiceRoll.EnableDicerRolling();

            gameButton.SetActive(false);
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

    static public void SwitchRuleTag()
    {
        if (2 == nbRulesToApply)
        {
            // Put in grey the previous rule
            ruleTagTwo[currentRuleIndex].GetComponent<Image>().color = new Color32(152, 144, 144, 100);

            // Highlight the new current one
            currentRuleIndex = currentRuleIndex <= nbRulesToApply ? currentRuleIndex++ : 0;
            ruleTagTwo[currentRuleIndex].GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
        if (3 == nbRulesToApply)
        {
            // Put in grey the previous rule
            ruleTagThree[currentRuleIndex].GetComponent<Image>().color = new Color32(152, 144, 144, 100);

            // Highlight the new current one
            currentRuleIndex = currentRuleIndex <= nbRulesToApply ? currentRuleIndex + 1 : 0;
            ruleTagThree[currentRuleIndex].GetComponent<Image>().color = new Color32(255, 255, 255, 100);
        }
    }
}
