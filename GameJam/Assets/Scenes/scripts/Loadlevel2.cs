using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loadlevel2 : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
    {
        SceneManager.LoadScene("level2");
    }
}
