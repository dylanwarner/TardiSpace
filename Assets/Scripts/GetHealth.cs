using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHealth : MonoBehaviour
{

    public int health = 3;
    public Image[] healthBar;

    void Update()
    {
        GetHP();
    }
    void GetHP()
    {
        if (health == 2)
        {
            Destroy(healthBar[2]);
        }

        if (health == 1)
        {
            Destroy(healthBar[1]);
        }
        if (health == 0)
        {
            Destroy(healthBar[0]);
        }
    }
}
