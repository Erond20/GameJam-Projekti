using System;
using System.Collections;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    public GameObject Axe;
    public bool Swinging = false;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Swinging = true;
            StartCoroutine(SwingAxe());
        }
    }
    IEnumerator SwingAxe()
    {
        Axe.GetComponent<Animation>().Play("AxeSwing");
        yield return new WaitForSeconds(0.40f);
        Swinging = false;
    }
}
