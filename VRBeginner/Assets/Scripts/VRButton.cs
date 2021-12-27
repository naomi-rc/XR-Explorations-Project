using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRButton : MonoBehaviour
{
    [SerializeField] GameObject button;
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
        
        Debug.Log(gameObject.tag);
        SceneManager.LoadScene(gameObject.tag);
        /*if (collision.collider.gameObject.tag.Equals("EscapeRoom"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.collider.tag.Equals("PrototypeScene"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        else if (collision.collider.tag.Equals("MyScene"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        else if (collision.collider.gameObject.tag.Equals("SampleScene"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
        }*/
    }
}
