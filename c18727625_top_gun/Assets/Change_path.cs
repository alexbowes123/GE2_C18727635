using UnityEngine;

public class Change_path : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.GetComponent<Collider>().name == "change_path")
        {
            Debug.Log("Hi change_path!");

            // change boid's Public Path path_take_off (script) to be 
            // Public Path path(script)

          
           
        }
    }

}
