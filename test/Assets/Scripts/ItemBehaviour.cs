using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    // 1
    public gamebehaviour GameManager;
    void Start()
    {
        // 2
        GameManager = GameObject.Find("Game Manager")
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
            // 4
            Debug.Log("Item collected!");

            GameManager.Items += 1;
        }

        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Item collected!");
            // 3
            GameManager.Items += 1;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
