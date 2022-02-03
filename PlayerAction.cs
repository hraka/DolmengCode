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

    private Vector3 upVec = new Vector3(1, 1, 0);
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


        if(Random.Range(1,25000) <= 1) {
            animator.SetTrigger("looking");
            // Debug.Log("랜덤발동!");
        }
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
            dirVec = new Vector3(-1, -1, 0);                     //방향을 저장 //너무 낭비가 크지 않나? x값 y값만 저장해도 되지 않을까?
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

        //던지기는 스택모드 전, 들고 있는 상태에서만 발동한다고 가정한다.
        if(Input.GetKeyDown("c")) {
            if(manager.isPicked) {
                //스택모드에서도 던질 수 있어도 재미있지 않을까?
                manager.ThrowObj(dirVec.x);
                animator.SetBool("isPickUp", false);
            }
        }
        if(Input.GetKeyDown("v")) {
            Debug.Log("v누름");
            if(manager.isPicked) {
                manager.JjockjiAction();
            }
            
        }
        if(Input.GetMouseButtonUp(0)) { //겹치면 안된다. //터치 모드를 위해 Up으로 변경
            
            PutDown();
        }
        //키보드+마우스 조작과 터치 조작 방식이 극명하게 달라질 것 같다.
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



        //올라가기
        upVec = new Vector3(dirVec.x, 0, 0);
        Debug.DrawRay(rigid2D.position, upVec * 0.6f, new Color(1, 0, 0));
        RaycastHit2D upRayHit = Physics2D.Raycast(rigid2D.position, upVec, 0.6f, LayerMask.GetMask("Object"));

        if(Input.GetKeyDown("1")) {
            ChangeMood(1);
        }

        if(Input.GetKeyDown("2")) {
            ChangeMood(2);
        }

        


    }
    //키보드로 움직이는 경우
    //AddForce로 움직이기
    //velocity로 움직이기
    //translate로 움직이기

    //마우스로 움직이는 경우

    public void ChangeMood(int moodNum) {
        if(moodNum == 1) {
            if(animator.GetBool("isMoodGood"))
                animator.SetBool("isMoodGood", false);
            else
                animator.SetBool("isMoodGood", true);
        }
        else if(moodNum == 2) {
            if(animator.GetBool("isZzaran")) {
                animator.SetBool("isZzaran", false);
            }else {
                animator.SetBool("isZzaran", true);
            }
        }
    }

    public void PutDown() {
        //스텍 모드 돌입 이후 시간이 흐른 후에 키가 동작하도록 함.
        //플레이어 액션에서의 풋 다운은 매니저에서의 풋 다운을 호출한다. 음... 왜 굳이 이런 구조를...?
        //if(manager.isStackMode) {
        if(manager.canPutDown) {
            manager.PutDown();
            animator.SetBool("isPickUp", false);
            //animator.SetTrigger("looking");
        }

    }

    public void PickAndStack() {
        if(manager.isStackMode) {
                //스택 중 픽업으로
                manager.PickUpFromStackMode();
                //animator.SetBool("isPickUp", true); 
            }
            else if(manager.isPicked) {
               //Debug.Log("스택모드");
                manager.StackMode();
                //animator.SetBool("isPickUp", true);
            }
            else if(meetObject != null){ //스택모드도 픽업상태도 아님
                manager.PickUp(meetObject);
                //animator.SetBool("isPicking", true);
                
                if(meetObject.GetComponent<ObjData>().id == 0) {
                    animator.SetBool("isPickUp", true);
                }
                
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

    public void ActCode(int codeNum) {
        if(codeNum == 1) { //앉기
            SitChange();
        }
        else if(codeNum == 2) { //들기
            PickAndStack();
        }
        else if(codeNum == 3) { //스택모드에서 내려놓기
            PutDown();
        }
        else if(codeNum == 11) {
            ChangeMood(1);
        }
        else if(codeNum == 12) {
            ChangeMood(2);
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
