using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Votanic.vXR.vGear;


public class musicnote : MonoBehaviour
{

    private Animator animator; 
    public GameObject Musicobject;
    private AudioSource Music;
    void Start()
    {
       
        animator = GetComponent<Animator>();
        Musicobject.SetActive(false);
        Music = Musicobject.GetComponent<AudioSource>();
        animator.SetBool("act", true);
    }

    void OnCollisionEnter(Collision collision)
    {
   
        if (collision.gameObject.CompareTag("hand"))
        {
            Musicobject.SetActive(true);
            Music.Play();
            //Debug.Log("touch");
      
            if (animator != null)
            {
                animator.SetBool("act", false);
            }

          
            gameObject.SetActive(false);
        }
    }
}