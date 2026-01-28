using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        // 2
        if (collision.gameObject.name == "Player")
        {
            // 3
            Destroy(this.transform.gameObject);
            // 4
            Debug.Log("Item collected!");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
