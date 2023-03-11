using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeRule : Rule
{
    static public List<string> _returnRulesActions = new List<string>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    static public void CheckNativeRules(int iDice1, int iDice2, ref bool oValidated)
    {
        _returnRulesActions.Clear();

        // Check all native rules and add actions in an ordered way in the action list        
        bool rule1 = CheckOneFigerRule(iDice1, iDice2);
        bool rule2 = CheckTwoFigerRule(iDice1, iDice2);
        bool rule3 = CheckFistRule(iDice1, iDice2);        
        bool rule5 = CheckDoubleRule(iDice1, iDice2);
        bool rule6 = CheckPreviousPlayerRule(iDice1, iDice2);
        bool rule7 = CheckNextPlayerRule(iDice1, iDice2);
        bool rule8 = CheckAllPlayerRule(iDice1, iDice2);
        bool rule4 = CheckTrimenRule(iDice1, iDice2);

        if (rule1 || rule2 || rule3 || rule4 || rule5 || rule6 || rule7 || rule8)
            oValidated = true;
        else
            oValidated = false;
    }

    static public bool CheckOneFigerRule(int iDice1, int iDice2)
    {
        if ((1 == iDice1 && 6 == iDice2) || (6 == iDice1 && 1 == iDice2))
        {
            string Action = "Le dernier joueur a poser un doigt sur la table prend un Trims";
            _returnRulesActions.Add(Action);

            return true;
        }
        else
            return false;
    }

    static public bool CheckTwoFigerRule(int iDice1, int iDice2)
    {
        if ((2 == iDice1 && 6 == iDice2) || (6 == iDice1 && 2 == iDice2))
        {
            string Action = "Le dernier joueur a poser 2 doigts sur la table prend un Trims";
            _returnRulesActions.Add(Action);

            return true;
        }
        else
            return false;
    }

    static public bool CheckFistRule(int iDice1, int iDice2)
    {
        if ((3 == iDice1 && 6 == iDice2) || (6 == iDice1 && 3 == iDice2))
        {
            string Action = "Le dernier joueur a poser le poing sur la table prend un Trims";
            _returnRulesActions.Add(Action);

            return true;
        }
        else
            return false;
    }

    static public bool CheckTrimenRule(int iDice1, int iDice2)
    {
        if (3 == iDice1 || 3 == iDice2 || (3 == iDice1 + iDice2))
        {
            string Action;
            if (iDice1 == iDice2)
                Action = "Le Trimen prend 2 Trims";
            else
                Action = "Le Trimen prend un Trims";

            _returnRulesActions.Add(Action);

            return true;
        }
        else
            return false;
    }


    static public bool CheckDoubleRule(int iDice1, int iDice2)    
    {
        // If the two dices have the same value the rule is valid

        if (iDice1 == iDice2)
        {
            if(iDice1 == 1)
            {
                string Action = GetCurrentPlayerString() + " prend un Trims";
                _returnRulesActions.Add(Action);
            }
            else
            {                
                string Action = GetCurrentPlayerString() + " distribue " + iDice1.ToString() + " Trims";
                _returnRulesActions.Add(Action);
            }            

            return true;
        }
        else
            return false;
    }

    static public bool CheckPreviousPlayerRule(int iDice1, int iDice2)
    {
        if (7 == iDice1 + iDice2)
        {
            string Action = "Le joueur precedent (" + GetPreviousPlayerString() + ") prend un Trims";
            _returnRulesActions.Add(Action);

            return true;
        }
        else
            return false;
    }

    static public bool CheckNextPlayerRule(int iDice1, int iDice2)
    {
        if (9 == iDice1 + iDice2)
        {
            string Action = "Le joueur suivant (" + GetNextPlayerString() + ") prend un Trims";
            _returnRulesActions.Add(Action);

            return true;
        }
        else
            return false;
    }

    static public bool CheckAllPlayerRule(int iDice1, int iDice2)
    {
        if (11 == iDice1 + iDice2)
        {
            string Action = "Tous les joueurs prennent un Trims";
            _returnRulesActions.Add(Action);

            return true;
        }
        else
            return false;
    }

    




    // ------------------------------------------------------------------------
    // TOOLS 
    // ------------------------------------------------------------------------

    static public string GetCurrentPlayerString()
    {
        int currentPlayerNumber = GameController.getCurrentPlayer() + 1;

        string ret = "Joueur " + currentPlayerNumber.ToString();
        return ret;
    }

    static public string GetPreviousPlayerString()
    {
        int currentPlayerNumber = GameController.getPreviousPlayer() + 1;

        string ret = "Joueur " + currentPlayerNumber.ToString();
        return ret;
    }

    static public string GetNextPlayerString()
    {
        int currentPlayerNumber = GameController.getNextPlayer() + 1;

        string ret = "Joueur " + currentPlayerNumber.ToString();
        return ret;
    }
}
