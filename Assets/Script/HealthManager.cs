using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health;
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        health = 5;
    }

    void Update()
    {
        text.text = "Health: " + health;
    }
}
