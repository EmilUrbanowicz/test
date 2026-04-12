using CustomExtensions;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq; 
using static System.Net.Mime.MediaTypeNames;


public class gamebehaviour : MonoBehaviour, IManager 
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;
    public int MaxItems = 10;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;
    public Button WinButton;
    public Button LossButton;
    private string _state;
    public Stack<Loot> LootStack = new Stack<Loot>();




    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            ItemText.text = "Items Collected: " + Items;
            if (_itemsCollected >= 10)
            {
              
                WinButton.gameObject.SetActive(true);
                UpdateScene("You've found all the items!");
            }
            else
            {
                ProgressText.text = "Item found, only " + (10 - _itemsCollected) + " more!";
            }


        }
    }

    public void RestartScene()
    {
        Utilties.RestartLevel(0);
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

        LootStack.Push(new Loot("Sword of Doom", 5));
        LootStack.Push(new Loot("HP Boost", 1));
        LootStack.Push(new Loot("Golden Key", 3));
        LootStack.Push(new Loot("pair of Winged Boots", 2));
        LootStack.Push(new Loot("Mythril Bracer", 4));
        FilterLoot();
    }

    public void PrintLootReport()
    {
        var currentItem = LootStack.Pop();
        var nextItem = LootStack.Peek();
        Debug.LogFormat("you got a {0}! You've got a good chance of finding a {1} next!", currentItem.Name, nextItem.Name);
        Debug.LogFormat("there are {0} random loot items waiting for you!", LootStack.Count);
    }

    public void FilterLoot()
    {
        var rareLoot = from item in LootStack
                           // 2
                       where item.rarity >= 3
                       // 3
                       orderby item.rarity
                       // 4
                       select item;


        foreach (var item in rareLoot)
        {
            Debug.LogFormat("rare item: {0}!", item.Name);
        }
    }

    public bool LootPredicate(Loot loot)
    {
        return loot.rarity >= 3;
    }








}
