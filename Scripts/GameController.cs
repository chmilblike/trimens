using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    static public List<Player> _Players;
    static int _Trimen;
    static int _CurrentPlayer;
    static public bool _LookingForTrimen;
    static public List<string> _rulesToApply = new List<string>();
    static public bool _FirstGame = false;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        _LookingForTrimen = true;
    }

    // ------------------------------------------------------------------------
    // DICE ROLLED
    // ------------------------------------------------------------------------

    static public void DiceRolled(int Dice1, int Dice2)
    {
        // Trimen selection
        if(_LookingForTrimen)
        {
            if(TrimenSelection.IsTheTrimen(Dice1, Dice2))
            {
                _Trimen = _CurrentPlayer;
                _LookingForTrimen = false;
                SwitchToNextPlayer();
            }
            else
            {
                SwitchToNextPlayer();
                TrimenSelection.UpdateCurrentPlayerText(_CurrentPlayer);                
            }
        }
        // Game
        else
        {
            bool ruleValuated = false;
            CheckRules(Dice1, Dice2, ref ruleValuated);

            if (ruleValuated)
            {
                GameScript.RulesToApply(_rulesToApply.Count);
            }
            else
            {
                GameScript.NothingHappen();
            }
        }
    }

    // ------------------------------------------------------------------------
    // CHECK RULES 
    // ------------------------------------------------------------------------

    static void CheckRules(int iDice1, int iDice2, ref bool oRuleValuated)
    {
        NativeRule.CheckNativeRules(iDice1, iDice2, ref oRuleValuated);

        _rulesToApply = NativeRule._returnRulesActions;

        foreach (string action in _rulesToApply)
        {
            Debug.Log(action);
        }
        
    }

    // ------------------------------------------------------------------------
    // TOOLS 
    // ------------------------------------------------------------------------

    static public void SwitchToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    static public void GoBackToTrimenSelectionScene()
    {
        SceneManager.LoadScene(1);
    }

    static public void SwitchToNextPlayer()
    {
        _CurrentPlayer++;
        _CurrentPlayer = _CurrentPlayer >= _Players.Count ? 0 : _CurrentPlayer;
    }


    // ------------------------------------------------------------------------
    // ACCESSORS 
    // ------------------------------------------------------------------------

    static public void setPlayers(List<Player> iPlayers)
    {
        _Players = iPlayers;
    }

    static public void setCurrentPlayer(int iCurrentPlayerIndex)
    {
        _CurrentPlayer = iCurrentPlayerIndex;
    }

    static public int getCurrentPlayer()
    {
        return _CurrentPlayer;
    }

    static public int getPreviousPlayer()
    {
        int previousPlayerIndex = _CurrentPlayer == 0 ? _Players.Count - 1 : _CurrentPlayer - 1;
        return previousPlayerIndex;
    }

    static public int getNextPlayer()
    {
        int nextPlayerIndex = _CurrentPlayer == _Players.Count ? 0 : _CurrentPlayer + 1;
        return nextPlayerIndex;
    }

    static public int getTrimen()
    {
        return _Trimen;
    }

    static public void setFirstGame(bool iFirstGame)
    {
        _FirstGame = iFirstGame;
    }

    static public bool getFirstGame()
    {
        return _FirstGame;
    }

    static public void setLookingForTrimen(bool iLookingForTrimen)
    {
        _LookingForTrimen = iLookingForTrimen;
    }
}
