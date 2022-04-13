using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhtScript : MonoBehaviour
{

    public float Health = 100f;

    public void DeductHealth(float deductHealth)
    {
        Health -= deductHealth;

        if (Health <= 0 )
        { Destroy(this.gameObject);
        }
    }
}
