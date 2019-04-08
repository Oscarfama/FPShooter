using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    Animator anim;

     void Start()
    {
        anim = GetComponent<Animator>();
    }
     void Update()
    {
       if(HealthManager.health<= 0)
        {
            anim.SetTrigger("GameOver");
            Application.Quit();
        }
    }
}
