using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    // 1
    public gamebehaviour GameManager;
    void Start()
    {
        // 2
        GameManager = GameObject.Find("game manager")
            .GetComponent<gamebehaviour>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        // 2
        if (collision.gameObject.name == "Player")
        {
            // 3
            Destroy(this.gameObject);

            var audioSource = GetComponent<AudioSource>();
            audioSource.Play();

            Debug.Log("Item collected!");

            Debug.Log(GameManager.Items);

            GameManager.Items += 1;

            Debug.Log(GameManager.Items);

            GameManager.PrintLootReport();

            
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
