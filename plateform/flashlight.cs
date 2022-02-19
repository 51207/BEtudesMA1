using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Flashlight;
    public GameObject Light;
    bool Isactive = false;
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && Isactive == false)
        {
            Flashlight.SetActive(true);
            
            Light.SetActive(true);
            Isactive = true;
           // var resolv = Flashlight.GetComponent<Transform>();
            
        }
        else if (Input.GetKeyDown(KeyCode.F) && Isactive == true)
        {
            Flashlight.SetActive(false);
            Light.SetActive(false);
            Isactive = false;
        }
    }
}
