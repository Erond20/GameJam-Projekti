
using UnityEngine;

public class gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
   
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        void shoot()
        {
            RaycastHit hit;
            if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
               Target target = hit.transform.GetComponent<Target>();

                if(target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }
    }
}
