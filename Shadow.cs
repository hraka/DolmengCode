using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shadow : MonoBehaviour
{

    [Header("속도, 반지름")]

    [SerializeField] [Range(0f, 10f)]
    public float speed = 3.7f;

    [SerializeField] [Range(0f, 10f)]
    public float radius = 0.4f;

    public float runningTime = 0;
    private Vector2 newPos = new Vector2();

    void Start() {

        //Destroy(gameObject, 5f);
        // this.UpdateAsObservable()
        //     .Subscribe(_ =>
        //     {
        //         runningTime += Time.deltaTime * speed;
        //         float x = radius * Mathf.Cos(runningTime);
        //         float y = radius * Mathf.Sin(runningTime);
        //         newPos = new Vector2(x, y);
        //         this.transform.position = newPos;

        //     });
    }
    void FixedUpdate() {
                 runningTime += Time.deltaTime * speed;
                float x = radius * Mathf.Cos(runningTime);
                float y = radius * (Mathf.Sin(runningTime)-1)*.5f;
                newPos = new Vector2(x, y);
                this.transform.localPosition = newPos;
         
    }

    public Vector2 GetTimingVec() {
        return newPos;
    }


    public float GetRunningTime() {
        return runningTime;
    }

    public void SetRunningTime(float setNum) {
        runningTime = setNum;
    }

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }


    private void onDestroy() {

        Debug.Log(name + " 파괴됨!");
    }


}
