using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier : MonoBehaviour
{

    public float jetResources = 5;
    public float subResources = 5;
    public float cHealth = 30;

    public GameObject jetPrefab;
    public GameObject subPrefab;

    public Vector3 jetPos;

    // Start is called before the first frame update
    void Start()
    {
        // jetPos.Set(11.6f,169.2f,-558f);
        jetPos.Set(0.0f,5.0f,-116f);
        StartCoroutine(GainResources());
        StartCoroutine(spawnMachines());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnMachines()
    {
       while(true)
       {
            if(jetResources >= 15)
            {
                    GameObject newJet = GameObject.Instantiate<GameObject>(jetPrefab, transform.position + jetPos, Quaternion.identity);
                    // newJet.transform.localScale = jetScale;
                    newJet.GetComponent<HeroController>().myCarrier = this;
                   
                    newJet.transform.parent = this.transform;

                    GameObject toCam = newJet.transform.FindChild("TakeOffCam").gameObject;
                    GameObject followCam = newJet.transform.FindChild("FollowHero").gameObject;
                    // toCam.GetComponent<AudioListener>().enabled = false;
                    // followCam.GetComponent<AudioListener>().enabled = false;

                    toCam.SetActive(false);
                    followCam.SetActive(false);
                    // newJet.GetComponent<FollowHero>();
                
                    jetResources -= 15;

            }
            if(subResources >= 15)
            {

            }

            yield return new WaitForSeconds(1.0f);
       }
 
    }

    IEnumerator GainResources()
    {
        while (true)
        {
            jetResources++;
            subResources++;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void onEnable()
    {
        

    }
}
