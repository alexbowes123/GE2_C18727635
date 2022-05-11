using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    // Start is called before the first frame update
    public void onEnable()
    {
        StartCoroutine(FlareLife());
    }

    IEnumerator FlareLife()
    {
        yield return new WaitForSeconds(1);
    
 
        float timePassed = 0;
        while (timePassed < 3)
        {
         
            timePassed += Time.deltaTime;
            Debug.Log(timePassed);
            
        }

        Debug.Log("Flare Gone");
        Destroy(this.gameObject);
    
    }

}
