using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 25.0f;
    public GameObject target;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("KillMe", 10);
        // transform.Rotate(0,90,90);
        StartCoroutine(Homing());
    }

    // public void KillMe()
    // {
    //     Destroy(this.gameObject);
    // }

    // Update is called once per frame
    void Update()
    {
        //    transform.Translate(0,  speed * Time.deltaTime,0);

        

       
    }

    IEnumerator Homing()
    {
     // This will wait 1 second like Invoke could do, remove this if you don't need it
     yield return new WaitForSeconds(1);
 
 
        float timePassed = 0;
        while (timePassed < 6)
        {
            // Code to go left here
            // Debug.Log("Time is"+timePassed);

            //get child of name "Flare"

            Transform flareChild = target.transform.Find("Flare(Clone)");

            if(flareChild != null)
            {
                Debug.Log("Found flare at "+flareChild.transform.position);
                transform.position += (flareChild.transform.position - transform.position).normalized * speed * Time.deltaTime;
                timePassed += Time.deltaTime;
            }
            else
            {
                transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
                timePassed += Time.deltaTime;

            }

     

            if(Vector3.Distance(target.transform.position, transform.position) < 0.3f)
            {
                Debug.Log("HIT!");
                Destroy(this.gameObject);
                GameObject explode = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
            }
    
            yield return null;
        }
        Debug.Log("Time up");
        Destroy(this.gameObject);
        // GameObject explode1 = GameObject.Instantiate(explosion, transform.position, Quaternion.identity);
    }

    // public IEnumerator Homing()
    // {
    //     while(Vector3.Distance(target.transform.position, transform.position) > 1.0f)
    //     {
    //         // Debug.Log(transform.name +"is at "+transform.position);
    //         //
            
    //         transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
    //         // transform.LookAt(target.transform);
    //         yield return null;
    //     }
    //     Destroy(this.gameObject);


    // }
}
