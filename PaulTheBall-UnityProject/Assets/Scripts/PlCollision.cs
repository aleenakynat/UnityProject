using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlCollision : MonoBehaviour
{
    public void OnTriggerEnter(Collider col)
    {
        //obstacle collision
        if(col.gameObject.tag == "Obstacle")
        {
            Debug.Log("Collided!");
            GameManager.Instance.GameOver();
        }
        //end game
        if (col.gameObject.tag == "Bonus")
        {
            Destroy(col.gameObject);
            GameManager.Instance.CollectBonus();
        }
        if (col.gameObject.tag == "wall")
        {
            GameManager.Instance.CompleteLevel();
        }
    }
}
