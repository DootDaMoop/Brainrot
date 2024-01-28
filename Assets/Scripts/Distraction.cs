using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distraction : MonoBehaviour
{
    public int Health;
    private int ClickCounter;

    public Distraction(int health) {
        this.Health = health;
    }

    /*private void Update() {
        float swayX = Mathf.Sin(Time.time*2f * 2f) * 1.5f;
        float swayY = Mathf.Sin(Time.time*2f * 0.5f * 2f) * 1.5f;
        transform.position = new Vector3(swayX,swayY,0f);
    }*/

    public void DistractionDamaged() {
        ClickCounter++;
        if(ClickCounter == Health) {
            Destroy(gameObject);
        }
    }

}
