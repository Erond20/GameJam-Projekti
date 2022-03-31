using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loadlevel2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name =="Player")
        {
            SceneManager.LoadScene("LoadLevel2");
        }
        
    }
}
