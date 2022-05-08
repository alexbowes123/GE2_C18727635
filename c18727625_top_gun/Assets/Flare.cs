using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlareLife());
    }

    IEnumerator FlareLife()
    {
        yield return new WaitForSeconds(1);
 
        float timePassed = 0;
        while (timePassed < 3)
        {
            yield return null;
        }

        Debug.Log("Flare Gone");
        Destroy(this.gameObject);
    }



}
