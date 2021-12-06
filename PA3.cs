using UnityEngine;

public class PA3 : MonoBehaviour
{

    //빠른 내려놓기가 스페이스인 경우.



    //플레이어가 캐릭터를 조작하는 일
    //게임 매니저와 상호작용하는 것은, 게임 매니저의 함수를 호출하는 방식으로 한다.


    private float moveSpeed = 4.5f;
    private Rigidbody2D rigid2D;
    private Animator animator;
    private Vector3 dirVec = new Vector3(1, -1, 0);
    private GameObject meetObject;
    public GameManager manager;

    private AudioSource audioSource;

    //캐릭터 움직임
    public void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        float x = manager.isMoveable? Input.GetAxisRaw("Horizontal") : 0;
        float y = manager.isMoveable? Input.GetAxisRaw("Vertical") : 0;

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

        rigid2D.velocity = new Vector3(x, y, 0) * moveSpeed;
        //rigidBody2D를 통해 충돌을 비롯한 물리적 처리를 가져올 수 있다.






        //돌을 집어 올리면.
        //돌 앞에서 키를 누르면
        //플레이어의 상태에 따라 다른 동작을 한다.

        if(Input.GetButtonDown("Jump"))
        {
            if(manager.isStackMode) {
                Debug.Log("스택모드에서 스페이스");
                manager.PickUpFromStackMode(); //되돌아감
                animator.SetBool("isPickUp", true);
            }
            else if(manager.isPicked) { //들고 있는 상태에서 스페이스
                Debug.Log("픽업에서 스페이스");
                Debug.Log("바로 내려놓기");
                manager.PutDown();
                animator.SetBool("isPickUp", false);
            }
            else if(meetObject != null){ //스택모드도 픽업상태도 아님
                Debug.Log("this is " + meetObject);
                manager.PickUp(meetObject);
                animator.SetBool("isPicking", true);
                animator.SetBool("isPickUp", true);
            }
            
        }
        if(Input.GetMouseButtonDown(0)) {
            if(manager.isStackMode) {
                Debug.Log("내려놓기");
                manager.PutDown();
                animator.SetBool("isPickUp", false);
            } else if(manager.isPicked) {
                manager.StackMode(); //들고 있는 상태에서 마우스
                animator.SetBool("isPickUp", false);
            }
        }
        if(Input.GetMouseButtonDown(1)) {
            if(manager.isStackMode) {
                 manager.PickUpFromStackMode();
                 animator.SetBool("isPickUp", true);
              
            }
            else if(manager.isPicked) {
                Debug.Log("바로 내려놓기");
                manager.PutDown();
                animator.SetBool("isPickUp", false);
            }
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

    public void LookBack() //애니메이션에서 호출하면 캐릭터가 뒤를 돌까 기대하며 만든 함수
    {
        transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
        Debug.Log("LookBack");
    }



    
}
