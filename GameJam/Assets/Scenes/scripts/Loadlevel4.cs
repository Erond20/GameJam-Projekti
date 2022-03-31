using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loadlevel4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        SceneManager.LoadScene("level4");
    }
}
