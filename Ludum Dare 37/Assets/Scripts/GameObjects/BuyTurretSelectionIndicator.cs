using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTurretSelectionIndicator : MonoBehaviour {

    float timeDelta;

    float r;
    float t;

    float newY;

    float startY;

    static float speedFactor = 1f;

    RectTransform rt;

    float dist;

    void Awake()
    {
        startY = this.transform.position.y;
        t = 1f;
        rt = this.GetComponent<RectTransform>();
        dist = 3f;
    }

	// Update is called once per frame
	void Update () {

        timeDelta = Time.deltaTime * GameController.GlobalSpeedFactor * speedFactor;

        if (this.r < 1f)
        {
            newY = Mathf.Lerp(startY - dist, startY + dist, r);

            r += timeDelta;

            if (r > 1f)
            {
                t = 0f;
            }
        }
 
        if (this.t < 1f)
        {
            newY = Mathf.Lerp(startY + dist, startY - dist, t);

            t += timeDelta; 

            if (t > 1f)
            {
                r = 0f;
            }

        }   

        //this.transform.position = new Vector3(this.transform.position.x, 0, newY);
        rt.position = new Vector3(rt.transform.position.x, newY, 0);

    }
}
