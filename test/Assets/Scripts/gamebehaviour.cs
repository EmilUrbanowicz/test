using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamebehaviour : MonoBehaviour
{
    private int _itemsCollected = 0;
    private int _playerHP = 10;
    public int MaxItems = 4;
    public TMP_Text HealthText;
    public TMP_Text ItemText;
    public TMP_Text ProgressText;
    public Button WinButton;


   
    public int Items
    {
        get { return _itemsCollected; }
        set
        {
            _itemsCollected = value;
            // 5
            ItemText.text = "Items: " + Items;
            // 6
            if (_itemsCollected >= MaxItems)
            {
                ProgressText.text = "You've found all the items!";
                WinButton.gameObject.SetActive(true);

                Time.timeScale = 0f;
            }
            else
            {
                ProgressText.text = "Item found, only " +
                    (MaxItems - _itemsCollected) + " more!";
            }


        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f; 
    }
    public int HP
    {
        get { return _playerHP; }
        set{
            _playerHP = value;
            HealthText.text = "Health: " + HP;
            Debug.LogFormat("Lives: {0}", _playerHP);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ItemText.text += _itemsCollected;
        HealthText.text += _playerHP;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

}
