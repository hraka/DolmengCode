using UnityEngine;

public class ObjOnAir : MonoBehaviour
{
    public bool isOnAir;
    
    private bool isPicked;

    private Rigidbody2D rigid2D;

    void Awake() {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }
    // void OnTriggerEnter2D(Collider2D col) {
    //     if (col.tag == "Ground") {
    //         //Debug.Log("땅에 닿았당" + col);
    //         isOnAir = false;
    //         SetState(9);
    //         gameObject.tag = "Ground";
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D col) {
    //     if(col.tag == "Ground") {
    //         //Debug.Log("땅에서 떨어졌당" + col);
    //         gameObject.tag = "Untagged";
            
    //         isOnAir = true;
    //         if(!isPicked) {
    //             SetState(16);
    //         }
    //     }
    // }

    private void OnCollisionStay2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
            
            gameObject.tag = "Ground";
            isOnAir = false;
            SetState(9);

           // gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            
            // gameObject.layer = 9;
        }
    }   

    private void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground") {
            //Debug.Log("땅에서 떨어졌당");
            gameObject.tag = "Untagged";
            //gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            isOnAir = true;
            if(!isPicked) {
                SetState(16);
            }
        }
        
    }
    // private void OnCollisionEnter2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "Ground") {
    //         //Debug.Log("땅에 닿았당");
    //         isOnAir = false;
    //         SetState(9);
    //         gameObject.tag = "Ground";
            
    //         // gameObject.layer = 9;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision) {
    //     if(collision.gameObject.tag == "Ground") {
    //         //Debug.Log("땅에서 떨어졌당");
    //         gameObject.tag = "Untagged";
            
    //         isOnAir = true;
    //         if(!isPicked) {
    //             SetState(16);
    //         }
    //     }
    // }

    
    // void Update() {
    //     //충돌 오류가 날 수 있으므로 보정.
        
    //     // if(isOnAir == false && gameObject.layer == 16) {
    //     //     gameObject.layer = 9;
    //     // }


    //     // if(!isPicked && isOnAir == true) { //오류 보정이 필요한 순간
    //     //     if(rigid2D.velocity.x == 0f && rigid2D.velocity.y == 0f) {
    //     //         SetState(9);
    //     //     } else {
    //     //         SetState(16);
    //     //     }
    //     // }
        
    // }


    public void SetPickedState(bool state) {
        isPicked = state;
    }

    public void SetState(int state) { //디버깅을 위한 색상변화이긴 하다. 레이어 한줄로 대체 가능.
        if(state == 9) {
            
            gameObject.layer = 9;
            //this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(state == 16) {
            gameObject.layer = 16;
            //this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if(state == 10) {
            gameObject.layer = 10;
            //this.GetComponent<SpriteRenderer>().color = Color.yellow;
        } else {
            Debug.Log("오류: 없는 번호");
        }        

    }
}
