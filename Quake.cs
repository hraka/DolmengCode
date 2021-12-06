using System.Collections;
using UnityEngine;

public class Quake : MonoBehaviour
{

    [SerializeField]
    private float forcePower;

    public float intensity; //받아오는 것
    public float time;
    private float shakeIntensity;
    private float shakeTime;

    private Rigidbody2D rigid;
    private void Start() {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(Input.GetKeyDown("=")) {
            //Force();
            StartShake(intensity, time);
        }
    }
    private void Force() {
        rigid.AddForce(new Vector2(0, 1) * forcePower, ForceMode2D.Impulse);
        Debug.Log("Quake");
    }

    public void StartShake(float intensity, float time) {
        this.shakeIntensity = intensity; //이거 대체 왜함?
        this.shakeTime = time;

        StartCoroutine("ShakeByPosition");
        //Shake(shakeIntensity, shakeTime);
        //StartCoroutine("ShakeByRotation");
    }

    private IEnumerator ShakeByPosition() {
        Vector3 startPosition = transform.localPosition;
        Debug.Log("time: " + shakeTime + " intensity: " + shakeIntensity);
        while(shakeTime > 0.0f) {
            float x = Random.Range(-1f, 1f);
            float y = Random.Range(-1f, 1f);
            float z = 0;

            transform.localPosition = startPosition + new Vector3(x, y, z) * intensity;
            //rigid.AddForce(new Vector2(0,1) * intensity, ForceMode2D.Impulse);
            shakeTime -= Time.deltaTime;

            yield return null;
         
        }

        transform.localPosition = startPosition;
        yield return null;
    }

    private IEnumerator ShakeByRotation() {
        Vector3 startRotation = transform.eulerAngles;
        float power = 10f;

        while(shakeTime > 0.0f) {
            float x = 0;
            float y = 0;
            float z = Random.Range(-1f, 1f);
            transform.rotation = Quaternion.Euler(startRotation + new Vector3(x, y, z) * shakeIntensity * power);

            shakeTime -= Time.deltaTime;

            yield return null;
        }

        transform.rotation = Quaternion.Euler(startRotation);
    }
}
