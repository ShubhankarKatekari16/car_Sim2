using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedometer : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        StartCoroutine(calcSpeed());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator calcSpeed()
    {
        while (true)
        {
            Vector3 prevPos = transform.position;

            yield return new WaitForFixedUpdate();

            speed = Mathf.RoundToInt(Vector3.Distance(transform.position, prevPos) / Time.fixedDeltaTime);
            //print(speed + " : " + GetComponent<Rigidbody>().velocity.magnitude);
        }
    }
}
