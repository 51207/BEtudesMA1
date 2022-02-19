using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moovplayer : MonoBehaviour
{
    // Start is called before the first frame update
    float MouvementY;
    float MouvementX;
    float mousesensivity = 90f;


    public Rigidbody rigidbody;


    // Start is called before the first frame update  
    void Start()
    {
        Cursor.visible = false;

          Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame  
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime*5);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //  this.transform.Rotate(Vector3.up, 10);
            // this.transform.Rotate(Vector3.up, 10);
            this.transform.Translate(Vector3.right * Time.deltaTime);
        }
       // float orientationPlayerZ = this.transform.eulerAngles.z;
        float orientationsouris = Mathf.Asin(Input.mousePosition.y /Input.mousePosition.x);


        //on a l'entr�e de la souris en X et en Y:
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mousesensivity;
        float m_X = Input.GetAxis("Mouse X") * Time.deltaTime * mousesensivity;

       // MouvementY c'est le mouvement du haut en bas;
       // meme si la valeur mouvement est negatif , ce n'est pas un probleme car la valeur doit etre entre -90 et 90
        MouvementY -= mouseY;
         MouvementX += m_X;
        //Math.clamp donne une valeur entre -90 et 90
        MouvementY = Mathf.Clamp(MouvementY, -90f, 90f);
        //pour le mouvement de haut en bas 
        // this.transform.localEulerAngles = new Vector3(MouvementY, 0f, 0f);
        //pour la rotation  dans tous les angles: mouvementX permet la rotation autour de soit , mais 
        //this.transform.localEulerAngles = new Vector3(MouvementY, MouvementX, 0f);
        this.transform.localEulerAngles = new Vector3((float)Tag.TagOrientX1, (float)Tag.TagOrientY1, (float)Tag.TagOrientZ1);

    }
  
}
