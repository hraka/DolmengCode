using TMPro;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI MoveValue;
    //플레이어가 캐릭터를 조작하는 일
    //게임 매니저와 상호작용하는 것은, 게임 매니저의 함수를 호출하는 방식으로 한다.


    private float moveSpeed = 4.2f;
    private Rigidbody2D rigid2D;
    private Animator animator;
    private Vector3 dirVec = new Vector3(1, -1, 0);
    private GameObject meetObject;
    public GameManager manager;

    private AudioSource audioSource;

    private bool isSit = false;

    public float newX;
    public float newY;

    private float friction = 1f;

    private bool mobile = false;


    //캐릭터 움직임
    public void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    public void Update()
    {
        //움직일 수 없는 상태면 방향값은 0이다.

        float x = manager.isMoveable? Input.GetAxisRaw("Horizontal") : 0;
        float y = manager.isMoveable? Input.GetAxisRaw("Vertical") : 0;



        //키보드 입력에 의한 값변화가 확인되지 않은 경우 버튼 입력을 확인해 덮어씌운다.
        if(x == 0 && y == 0) {
            x = newX;
            y = newY;

        }


        //현재 매 순간 x와 y 값을 확인하고 이에 따라 캐릭터를 이동시킨다.

        if (x < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
            dirVec = new Vector3(-1, -1, 0);                     //방향을 저장
        }

        else if (x > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            dirVec = new Vector3(1, -1, 0);
        }

        if (x == 0 && y == 0) {
            animator.SetBool("isMoving", false);

            audioSource.Play();

        }
        else {
            animator.SetBool("isMoving", true);

        }

        
        rigid2D.velocity = new Vector3(x, y, 0) * moveSpeed * friction;
        
        

        
        //rigidBody2D를 통해 충돌을 비롯한 물리적 처리를 가져올 수 있다.






        //돌을 집어 올리면.
        //돌 앞에서 키를 누르면
        //플레이어의 상태에 따라 다른 동작을 한다.

        
        if(Input.GetKeyDown("x")){

            SitChange();

        }
        if(Input.GetMouseButtonDown(0)) { //겹치면 안된다.
            //if(manager.isStackMode) {
            if(manager.canPutDown) {
                Debug.Log("클릭해서 내려놓는거야~~~");
                manager.PutDown();
                animator.SetBool("isPickUp", false);
            }
        }
        if(Input.GetMouseButtonDown(1)) {
            if(manager.isPicked && !manager.isStackMode) {
                //Debug.Log("바로 내려놓기");
                manager.PutDown();
                animator.SetBool("isPickUp", false);
            }
        } 
        if(Input.GetButtonDown("Jump"))
        {
            PickAndStack();
    
        }
            
        
        



        //Raycast - Fixed?
        Debug.DrawRay(rigid2D.position, dirVec * 0.6f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid2D.position, dirVec, 0.6f, LayerMask.GetMask("Object"));

        if (rayHit.collider != null)  //사물이 있다.
            meetObject = rayHit.collider.gameObject;
        else
            meetObject = null;




        if(Input.GetKeyDown("1")) {
            if(animator.GetBool("isMoodGood"))
                animator.SetBool("isMoodGood", false);
            else
                animator.SetBool("isMoodGood", true);
        }

        if(Input.GetKeyDown("2")) {
            if(animator.GetBool("isZzaran")) {
                animator.SetBool("isZzaran", false);
            }else {
                animator.SetBool("isZzaran", true);

            }
        }

        


    }
    //키보드로 움직이는 경우
    //AddForce로 움직이기
    //velocity로 움직이기
    //translate로 움직이기

    //마우스로 움직이는 경우


    public void PickAndStack() {
        if(manager.isStackMode) {
                //Debug.Log("스택모드에서 스페이스");
                //스택 중 픽업으로
                manager.PickUpFromStackMode();
                animator.SetBool("isPickUp", true);
            }
            else if(manager.isPicked) {
               //Debug.Log("스택모드");
                manager.StackMode();
                animator.SetBool("isPickUp", false);
            }
            else if(meetObject != null){ //스택모드도 픽업상태도 아님
                //Debug.Log("this is " + meetObject);
                manager.PickUp(meetObject);
                animator.SetBool("isPicking", true);
                animator.SetBool("isPickUp", true);
            }
    }

    private void SitChange() {
        if(isSit) {
            //앉은 경우
            animator.SetBool("isSiting", true);
            //manager.isMoveable = false;
            friction = 0f;
            isSit = false;
            
        } else {
            //일어선 경우
            animator.SetBool("isSiting", false);
            friction = 1f;
            //manager.isMoveable = true;
            isSit = true;
        }
    }

    private void StandCharactor() {
        
    }

    public void ActCode(int codeNum) {
        if(codeNum == 1) { //앉기
            SitChange();
        }
        else if(codeNum == 2) { //들기
            PickAndStack();
        }
    }
    public void SetX(float mx) { //캐릭터 이동 함수
        newX = mx;
    }
    public void LookBack() //애니메이션에서 호출하면 캐릭터가 뒤를 돌까 기대하며 만든 함수
    {
        transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        Debug.Log("LookBack");
    }



    
}
