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
        StartCoroutine(Homing());
    }



    // Update is called once per frame
    void Update()
    {
   
    }

    IEnumerator Homing()
    {
     // This will wait 1 second like Invoke could do, remove this if you don't need it
     yield return new WaitForSeconds(1);
 
 
        float timePassed = 0;
        while (timePassed < 6)
        {
           

            //get child of name "Flare"

            Transform flareChild = target.transform.Find("Flare(Clone)");

            if(flareChild != null)
            {
                transform.position += (flareChild.transform.position - transform.position).normalized * speed * Time.deltaTime;
                transform.LookAt(flareChild.transform);
                transform.Rotate(0,0,90);
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

   
}
