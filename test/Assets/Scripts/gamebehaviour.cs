using CustomExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamebehaviour : MonoBehaviour, IManager 
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;
    public int MaxItems = 4;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;
    public Button WinButton;
    public Button LossButton;
    private string _state;
    


   
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = "Items Collected: " + Items;
            if (_itemsCollected >= MaxItems)
            {
              
                WinButton.gameObject.SetActive(true);
                UpdateScene("You've found all the items!");
            }
            else
            {
                ProgressText.text = "Item found, only " + (MaxItems - _itemsCollected) + " more!";
            }


        }
    }

    public void RestartScene()
    {
        Utilities.RestartLevel(0);
    }
    public int HP
    {
        get { return _playerHP; }
        set{
            _playerHP = value;
            HealthText.text = "Health: " + HP;  
            if(_playerHP <= 0)
            {
                ProgressText.text = "You want another life with that?";
                LossButton.gameObject.SetActive(true);
                
            }
            else
            {
                ProgressText.text = "Ouch... that's gotta hurt.";
            }
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
        Initialize();
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScene(string updatedText)
    {
        ProgressText.text = updatedText;
        Time.timeScale = 0f;
    }

    public string State
    {
        get { return _state; }
        set { _state = value; }
    }

    public void Initialize()
    {
        _state = "Game Manager initialized..";
        _state.FancyDebug();
        Debug.Log(_state);
    }




}
