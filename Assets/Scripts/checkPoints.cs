using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoints : MonoBehaviour
{
    public bool raceFinished;
    public float finishingTime;
    // Start is called before the first frame update
    void Start()
    {
        //obj2.tag = obj2.name;
        raceFinished = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        //print(transform.position - temp.position);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "car" && this.gameObject.tag == "end")
        {
            timer myTimer = collision.gameObject.GetComponent<timer>();
            Transform carTransform = collision.collider.transform;
            Debug.Log("End Plane got hit");
            Vector3 direction = transform.position - carTransform.position;
            Vector3 localDirection = carTransform.InverseTransformDirection(direction);


            if(localDirection.z < 0)
            {
                print("wrong way");
                
            }
            else if(localDirection.z >= 0)
            {
                //print("Finish race: " + myTimer.currentTime);
                finishingTime = myTimer.currentTime;
                raceFinished = true;
                myTimer.FinishRace(finishingTime);
                //print(raceFinished);
            }
        }
        else if (collision.gameObject.tag == "car" && this.gameObject.tag == "Start")
        {
            Debug.Log("Start plane got hit");
        }
        //else if (collision.gameObject.tag == "endPlane")
        //{
        //    Debug.Log("startPlane got hit");
        //}
        
    }
}
