
using UnityEngine;

public class Force : MonoBehaviour
{

    private Rigidbody2D rigid;
    private void Start() {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(Input.GetKeyDown("=")) {
            rigid.AddForce(new Vector2(0, 1) * 150f * 8f, ForceMode2D.Force);
            Debug.Log("부딪" + gameObject);
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "Earthquake") {
    //         rigid.AddForce(new Vector2(0, 1) * 2f, ForceMode2D.Impulse);
    //         Debug.Log("부딪" + gameObject);
    //     }
            
    // }

}
