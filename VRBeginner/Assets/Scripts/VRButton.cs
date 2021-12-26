using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag.Equals("Hand"))
       // {
            Debug.Log("Button Pressed!");
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Button Pressed - collision enter!");
    }
}
