using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 20.0f;
    public GameObject target;

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

    public IEnumerator Homing()
    {
        while(Vector3.Distance(target.transform.position, transform.position) > 0.6f)
        {
            // Debug.Log(transform.name +"is at "+transform.position);
            transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
            // transform.LookAt(target.transform);
            yield return null;
        }
        Destroy(this.gameObject);


    }
}
