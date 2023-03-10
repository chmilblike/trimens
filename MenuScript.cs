using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MenuScript : MonoBehaviour
{
    public int _playerNumber;
    public GameObject _textGameObject;


    // Start is called before the first frame update
    void Start()
    {
        // Init player number
        _playerNumber = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreasePlayerNumber()
    {
        if (_playerNumber + 1 > 99)
            return;
        else
        {
            _playerNumber++;
            _textGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = _playerNumber.ToString();
        }
    }

    public void DecreasePlayerNumber()
    {
        if (_playerNumber - 1 < 2)
            return;
        else
        {
            _playerNumber--;
            _textGameObject.GetComponent<TMPro.TextMeshProUGUI>().text = _playerNumber.ToString();
        }
    }

    public void StartGame()
    {
        // Create Player List
        List<Player> Players = new List<Player>();

        for (int i = 1; i<= _playerNumber; i++)
        {
            Player player = new Player();
            player.Name = "Player" + i.ToString();
            Players.Add(player);
        }

        // Set the Player List
        GameController.setPlayers(Players);

        // Load the "Trimen Selection" scene
        GameController.SwitchToNextScene();
    }


}
