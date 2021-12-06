using System.Collections;
using UnityEngine;

public class TimeChecker : MonoBehaviour
{
    private float timeSpan;
    private float checkTime;

    public Quake earth;
    public Quake mainCam;
    public Quake stackModeCam;
    // Start is called before the first frame update
    void Start()
    {
        timeSpan = 0.0f;
        checkTime = 90.0f;
        
        StartCoroutine("TimePass");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private IEnumerator TimePass() {
        while(true) {
            timeSpan += Time.deltaTime;
            if(timeSpan > checkTime)
            {
                //이벤트
                earth.StartShake(0.23f, 2f);
                mainCam.StartShake(10f, 2f);
                stackModeCam.StartShake(10f, 2f);
                timeSpan = 0.0f;
                Debug.Log("지진!");
            }

            yield return null;
        }
        
    }
}
