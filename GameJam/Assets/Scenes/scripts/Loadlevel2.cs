using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loadlevel2 : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("level2");
        }
    }
}
