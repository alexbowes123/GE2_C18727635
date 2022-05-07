using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float health = 10;
    public float ammo = 1;
    
    public GameObject bullet;
    public GameObject enemy;
    public TMPro.TextMeshPro text;


    IEnumerator reload()
    {
        while(true)
        {
            if(ammo <= 0.0f)
            {
                ammo++;
                Debug.Log("Reloaded, current ammo is "+ammo);
            }
            yield return new WaitForSeconds(5.0f);
        }
    }
    void Start()
    {
         StartCoroutine(reload());
    }

    void Update()
    {       
       
    }


}