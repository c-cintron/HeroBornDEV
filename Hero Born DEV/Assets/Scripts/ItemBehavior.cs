using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior _gameManager;

    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            switch (this.gameObject.name)
            {
                case "Health_Pickup":
                    {
                        Debug.Log("Health Item collected!");
                        Destroy(this.transform.parent.gameObject);
                        break;
                    }
                case "Strength_Pickup":
                    {
                        Debug.Log("Strength Item collected!");
                        Destroy(this.transform.parent.gameObject);
                        break;
                    }
                case "Speed_Pickup":
                    {
                        Debug.Log("Speed Item collected!");
                        Destroy(this.transform.parent.gameObject);
                        break;
                    }
                case "Pogo_Pickup":
                    {
                        Debug.Log("Pogo Item collected!");
                        Destroy(this.transform.parent.gameObject);
                        break;
                    }
            }
        }
    }
}
