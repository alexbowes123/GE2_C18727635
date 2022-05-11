using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SubController : MonoBehaviour
{ 
    // Start is called before the first frame update

    public Carrier myCarrier;

    public Vector3 jetScale = new Vector3(10.0f,10.0f,10.0f);
    // public Vector3 jetPos = new Vector3(0.0f,5.0f,-116f);


    

    void OnTriggerEnter(Collider col)
    {
       
    
        if(col.tag == "Missile")
        {
            Debug.Log("Sub Damage taken!");

            if(GetComponent<Fighter>().health > 0)
            {            
                GetComponent<Fighter>().health --;
            }
            else
            {
                GetComponent<StateMachine>().SetGlobalState(new Dead());
            }
          
            if (GetComponent<StateMachine>().currentState.GetType() != typeof(Dead))
            {
                // GetComponent<StateMachine>().ChangeState(new DefendState());   
                Debug.Log("Stand up for yourself!");
                
            }
            else
            {
                Debug.Log("Dead now!");
            }
        }
    }

    void Start()
    {

        // transform.localScale = jetScale;
        // transform.position = jetPos;

        GetComponent<StateMachine>().ChangeState(new TakeOffState());
        GetComponent<StateMachine>().SetGlobalState(new Alive());  

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
