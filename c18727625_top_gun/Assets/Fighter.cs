using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public float health = 10;
    public float ammo = 1;
    public float flareCount = 1;
    
    public GameObject bullet;
    public GameObject enemy;
    public GameObject flare;
    public TMPro.TextMeshPro text;


    IEnumerator reload()
    {
        while(true)
        {
            if(ammo <= 0.0f)
            {
                ammo++;
                // Debug.Log("Reloaded, current ammo is "+ammo);
            }
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator reloadFlare()
    {
        while(true)
        {
            if(flareCount <= 0.0f)
            {
                flareCount++;
                // Debug.Log("Reloaded, current flare is "+flareCount);
            }
            yield return new WaitForSeconds(8.0f);
        }
    }

    void Start()
    {
         StartCoroutine(reload());
         StartCoroutine(reloadFlare());
    }

    void Update()
    {       
       
    }


}