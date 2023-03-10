using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public float rotationSpeed = 10000f;
    float remainingRotationTime = 0.5f;

    bool diceRolling = false;
    static public bool diceRollingEnable = true;

    public static int Dice1Value = 0;
    public static int Dice2Value = 0;

    public GameObject Dice1;
    public GameObject Dice2;

    private void Start()
    {
        diceRollingEnable = true;
    }


    void FixedUpdate()
    {     
        // We clicked on the dice
        if (diceRolling)
        {
            // We rotate the dice in random directions
            if (remainingRotationTime > 0)
            {
                remainingRotationTime -= Time.deltaTime;

                float x = Random.Range(0f, 1f);
                float y = Random.Range(0f, 1f);
                float z = Random.Range(0f, 1f);

                Vector3 rotationVector = new Vector3(x, y, z);

                Dice1.transform.Rotate(rotationVector, rotationSpeed * Time.deltaTime);
                Dice2.transform.Rotate(rotationVector, rotationSpeed * Time.deltaTime);
            }
            else
            {
                diceRolling = false;

                // We associate a rotation vector to the dice value we have
                Vector3 finalRotationVector1 = new Vector3(13.787f, -41.36f, -11.849f);
                switch (Dice1Value)
                {
                    case 1:
                        finalRotationVector1 = new Vector3(0f, 0f, 0f);
                        break;
                    case 2:
                        finalRotationVector1 = new Vector3(0f, -90f, 0f);
                        break;
                    case 3:
                        finalRotationVector1 = new Vector3(-90f, 0f, 0f);
                        break;
                    case 4:
                        finalRotationVector1 = new Vector3(90f, 0f, 0f);
                        break;
                    case 5:
                        finalRotationVector1 = new Vector3(0f, 90f, 0f);
                        break;
                    case 6:
                        finalRotationVector1 = new Vector3(0f, 180f, 0f);
                        break;
                }

                Vector3 finalRotationVector2 = new Vector3(13.787f, -41.36f, -11.849f);
                switch (Dice2Value)
                {
                    case 1:
                        finalRotationVector2 = new Vector3(0f, 0f, 0f);
                        break;
                    case 2:
                        finalRotationVector2 = new Vector3(0f, -90f, 0f);
                        break;
                    case 3:
                        finalRotationVector2 = new Vector3(-90f, 0f, 0f);
                        break;
                    case 4:
                        finalRotationVector2 = new Vector3(90f, 0f, 0f);
                        break;
                    case 5:
                        finalRotationVector2 = new Vector3(0f, 90f, 0f);
                        break;
                    case 6:
                        finalRotationVector2 = new Vector3(0f, 180f, 0f);
                        break;
                }

                // Set the proper orientation to the dice
                Dice1.transform.eulerAngles = finalRotationVector1;
                Dice2.transform.eulerAngles = finalRotationVector2;

                // Sent the dices value to the Game Controller
                GameController.DiceRolled(Dice1Value, Dice2Value);
            }
        }
    }


    public void RollTheDice()
    {
        if (diceRollingEnable)
        {
            Dice1Value = Random.Range(1, 6);
            Dice2Value = Random.Range(1, 6);

            remainingRotationTime = 0.5f;
            diceRolling = true;

            // A mettre autre part sans le check 
            if (GameObject.Find("TrimensSelectionCanvas") != null)
                TrimenSelection.ClearTrimensMessage();


            Dice1Value = 3;
            Dice2Value = 6;
        }        
    }

    static public void EnableDicerRolling()
    {
        diceRollingEnable = true;
    }

    static public void DisableDicerRolling()
    {
        diceRollingEnable = false;
    }
}
