using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject pickedObject;
    public bool isPicked;
    public bool isStackMode;
    public bool isMoveable;

    private bool isStick;
    private GameObject getPoint;
    private Rigidbody2D pickedRigid;
    private SpriteRenderer pickedSpriteRenderer;

    private GameObject shadow;

    private SpriteRenderer shadowRenderer;

    //private Shadow timing;

    private Vector3 standardSize = new Vector3(1, 1, 1);


    private GameObject center;

    private GameObject range1;
    private GameObject range2;
    private GameObject range3;

    private float timingSpeed = 3.7f;


    private float range1Ratio =.2f;
    private float range2Ratio = .5f;
    private float range3Ratio = .8f;

    private float flashTime = 0.4f;

    public bool canPutDown = false;


    [SerializeField]
    private Camera stackModeCam;

    //어떤 조작이 가능한지 판별 (다른 스크립트에서 게임매니저 호출 가능)
    //게임 모드 (돌쌓기 모드)

    [SerializeField]
    private GameObject rangeObj;

    [SerializeField]
    private GameObject centerObj;

    [SerializeField]
    private GameObject[] UINormalOn;

    [SerializeField]
    private GameObject[] UIStackOn;

    [SerializeField] private GameObject frontHand;
    private GameObject bothHand;


    private void Awake() //Awake는 뭐고 Start는 뭐지?
    {
        bothHand = GameObject.FindGameObjectWithTag("Get");
        Invoke("Move", 2);
    }

    private void Move() {
        isMoveable = true;
    }

    private void Update() {
        if(isStackMode) {
            if(pickedObject != null) { //매번 체크하는게 과연 좋을까? 업데이트인데...

                if(isStick) {
                    //pickedObject.transform.localRotation = Quaternion.Euler(0f, mousePos().y, 0f);
                    TurnRotation();
                    
                } else {
                    pickedObject.transform.localPosition = mousePos();
                }
//물체 회전
                
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    pickedObject.transform.Rotate(0.0f, 0.0f, 35.0f);// * Time.deltaTime을 곱하면 천천히 회전함 (Down빼고)
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pickedObject.transform.Rotate(0.0f, 0.0f, -35.0f);
                }

        
                
            }
        }
    }

    public void TurnRotation() {
        Vector3 mouseTarget = mousePos();
        Vector3 oPosition = pickedObject.transform.position;

        float dy = mouseTarget.y - oPosition.y;
        float dx = mouseTarget.x - oPosition.x;

        float rotateDegree = (Mathf.Atan2(dy, dx) - 90f) * Mathf.Rad2Deg;

        pickedObject.transform.rotation = Quaternion.Euler (0f, 0f, rotateDegree);


    }


    //돌들기 모드
    public void PickUp(GameObject meetObject)
    {
        //판정을 어디서 해야 하지?
        //돌을 들 수 있으면 돌을 든다에서 조건 부분은 함수 호출전에 검토되어야 하는가?
        //함수 호출 후에 검토되는게 나은가?
        //클래스끼리 변수를 주고 받아 멀리서 판정하는게 과연 합당한가?

        if (pickedObject != null)
        {
            //들고 있는 물체 변수에 뭔가 있으면
            //그대로 들고 있는다?
        }
        else
        {
            if(meetObject == null) {
                return;
            }
            if(meetObject.GetComponent<ObjData>() == null) {
                Debug.Log("오브젝트 데이터 컴포넌트 없음");
                return;
            }
            
            
            pickedObject = meetObject;
            int objId = pickedObject.GetComponent<ObjData>().id;
            if(objId == 2) { 
                getPoint = frontHand;
                pickedObject.transform.localRotation = Quaternion.identity;
                isStick = true;
            } else {
                getPoint = bothHand;
                isStick = false;
            }

            pickedObject.GetComponent<ObjOnAir>().SetPickedState(true);
            pickedObject.transform.SetParent(getPoint.transform);
            pickedObject.transform.localPosition = Vector3.zero;
            //다른 돌과 충돌하지 않는 상태
             //색 변화를 위한 디버깅용 함수. 그러나 모든 오브젝트에 다 있지는 않다.
            // if(objId == 2) {
            //     pickedObject.GetComponent<ObjOnAir>().SetState(16);
            // } else {
            //     pickedObject.GetComponent<ObjOnAir>().SetState(10);
            // }
            //pickedObject.layer = 10;
            pickedObject.GetComponent<ObjOnAir>().SetState(10);

            pickedRigid = pickedObject.GetComponent<Rigidbody2D>(); //이런식으로 일시적인 변수를(null이 되기도하는) 전역변수로 써도 괜찮은 걸까?
            //rigid.gravityScale = 0;
            pickedRigid.isKinematic = true;
            pickedRigid.velocity = Vector3.zero;
            pickedRigid.angularVelocity = 0f;
            //실험
            //pickedRigid.freezeRotation = true; //와.. 이거 잘못켰다가.. ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ
            //pickedRigid.gravityScale = 0;
            //돌의 레이어까지 조정할 것.
            isPicked = true; //이건 언제 나와야 하는지
        }


    }

    public void PickUpFromStackMode() {
        if(pickedObject != null) {
            isStackMode = false;
            pickedObject.transform.SetParent(getPoint.transform);
            stackModeCam.enabled = false;
            isMoveable = true;

            pickedObject.transform.localPosition = Vector3.zero;

            Destroy(shadow); //없어도 되는거 아니야?
            //Destroy(center);

            NormalModeUI();

        } else {
            Debug.Log("들고 있지 않은데 들었다고?");
        }
        
    }

    private void CanPutDown() {
        canPutDown = true;
    }
    //돌 쌓기 모드
    public void StackMode() {
        if(pickedObject != null) {
            StackModeUI();
            isStackMode = true;
            getPoint.transform.DetachChildren();
            stackModeCam.enabled = true;
            isMoveable = false;

            pickedSpriteRenderer = pickedObject.GetComponent<SpriteRenderer>();

            //shadow = AddShadow(pickedObject);   //그림자를 저장해야 나중에 삭제할 수 있다.

            //DrawRange(shadow);
            //center = DrawCenter(pickedObject);
            //Debug.Log(range3);
            //Debug.Log(timing.GetRunningTime());


            //스택모드 조작 활성화 시간
            Invoke("CanPutDown", .1f);
        } else {
            Debug.Log("오류발생");
        }
        
        //돌의 종속이 풀린다.
        //돌의 위치가 마우스의 위치로 온다.
        //캐릭터가 움직이지 못한다.
        //카메라가 바뀐다.
        //스택모드로 처리된다.

    }

    private void StackModeUI() {

        for(int i = 0; i < UINormalOn.Length; i++) {
            UINormalOn[i].gameObject.SetActive(false);
        }

        for(int j = 0; j < UIStackOn.Length; j++) {
            UIStackOn[j].gameObject.SetActive(true);
        }

        
    }

    private void NormalModeUI() {

        for(int i = 0; i < UINormalOn.Length; i++) {
            UINormalOn[i].gameObject.SetActive(true);
        }

        for(int j = 0; j < UIStackOn.Length; j++) {
            UIStackOn[j].gameObject.SetActive(false);
        }
    }


    private Vector3 mousePos() {
        Vector3 mousePos;
        mousePos = Input.mousePosition;
        mousePos = stackModeCam.ScreenToWorldPoint(mousePos);

        return new Vector3(mousePos.x, mousePos.y, 0);
    }

   

    //돌 내려놓기
    public void PutDown() {
        Debug.Log("내려놓음");
        if(pickedObject != null) {

            canPutDown = false;

            /*잠시 지우는 타이밍 체크*/
            //int check = TimingCheck(timing.GetTimingVec());
            //TimingFeedback(check, pickedObject);


            //timing.SetSpeed(0f);

            getPoint.transform.DetachChildren();
            pickedObject.GetComponent<ObjOnAir>().SetPickedState(false);
            pickedObject.GetComponent<ObjOnAir>().SetState(16); //임시로 공중에 뜬 상태로 만든다. 바닥에 닿으면 바로 바닥 상태로 변하더라.
            //pickedObject.layer = 16; 

            /*타이밍 체크를 위해 기다리는 시간 잠시 삭제*/
            MomentRelease(); //Invoke("MomentRelease", 0.16f);

            pickedObject = null;

            //카메라가 바뀐다. (단 스택모드에서 이동할 때만...)
            //돌의 속성이 바뀐다.
            //조작시 모드가 연출 딜레이 후에 바뀐다.

            Invoke("OutMode",.8f);
        }
        else {
            //아무것도 없을일은 없는데...
            isPicked = false;
        }
    }

    private void OutMode() {
        isStackMode = false;
        stackModeCam.enabled = false;
        isMoveable = true;
        isPicked = false;

        NormalModeUI();
    }

    private void MomentRelease() { //떨궈지는 액션에 모멘트를 주기 위해 만들었던 함수
        pickedRigid.isKinematic = false;
        Destroy(shadow);
    }

    public void PutDownFromPickUp() { //굳이 있어야 할까? 아직 안씀. 
        if(pickedObject != null) {
            //getPoint.transform.DetachChildren();
        }
    }

    public void ThrowObj(float dirX) {
        if(pickedObject == null) {
            return;
        }
        Debug.Log("던졌당");
        pickedRigid.isKinematic = false;
        float objMass = pickedRigid.mass;
        Vector3 throwingForce = new Vector3(dirX * 260f * objMass, 180f * objMass, 0);
        
        pickedRigid.AddForce(throwingForce);
        pickedRigid.AddTorque(80f * -dirX);

        PutDown();
    }



    /////타이밍바/////


    private GameObject AddShadow(GameObject getting) {


        GameObject shadow = Instantiate(getting);
        shadow.transform.parent = getting.transform;
        shadow.name = "그림자";
        shadow.layer = 10;


        shadowRenderer = shadow.GetComponent<SpriteRenderer>();
        //shadowRenderer.color = new Color(0f, 0f, 1f, .35f);
        shadowRenderer.color = new Color(0f, 0f, 1f, 0f);


        //shadow.transform.localScale = getting.transform.sc;

        //timing = shadow.AddComponent<Shadow>();
        //timing.SetRunningTime(Random.Range(0, 10f));
        //timing.SetSpeed(timingSpeed);

        return shadow;
    }

    private void DrawRange(GameObject shadow) {
        //판정 범위를 그린다.
       
        range3 = Instantiate(rangeObj);
        range3.transform.parent = shadow.transform;
        range3.transform.localPosition = shadow.transform.localPosition;
        range3.transform.localScale = standardSize * range3Ratio;
        range3.GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f, 0f);

        range2 = Instantiate(rangeObj);
        range2.transform.parent = shadow.transform;
        range2.transform.localPosition = shadow.transform.localPosition;
        range2.transform.localScale = standardSize * range2Ratio;
        range2.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0f, 0f);

        range1 = Instantiate(rangeObj);
        range1.name = "Range1";
        range1.transform.parent = shadow.transform;
        range1.transform.localPosition = shadow.transform.localPosition;
        range1.transform.localScale = standardSize * range1Ratio; //돌의 크기와 상관 없어져 버리는...
        range1.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 0f);
    }

    private GameObject DrawCenter(GameObject mainObj) {
        //판정 범위를 그린다.
        center = Instantiate(centerObj);
        center.transform.parent = mainObj.transform;
        center.transform.localPosition = Vector3.zero;
        center.transform.localScale = standardSize * range1Ratio; //돌의 크기와 상관 없어져 버리는...

        return center;
    }

    private int TimingCheck(Vector2 timingPos) {
        if(timingPos.x < .1f && timingPos.x > -.1f) {
            if(timingPos.y < .05f && timingPos.y > -.05f) {
                Debug.Log("Good");
                return 1;
            }
        } else if (timingPos.x < .2f && timingPos.y > -.2f) {
            if(timingPos.y < .2f && timingPos.y > -.2f) {
                Debug.Log("미끌");
                return 2;
            }
        } else {
            Debug.Log("띠용");
            return 3;
        }
        return 0;
    }

    private void TimingFeedback(int check, GameObject picked) {
        StartCoroutine(Flashing(check, picked));

    }

    IEnumerator Flashing(int check, GameObject picked) {

        if(check == 1 ){
            pickedSpriteRenderer.color = new Color(0, 1, 1, 1);
            //StartCoroutine(FeedbackofPerfect(picked));
            picked.GetComponent<Rigidbody2D>().mass += 5;
            timingSpeed = timingSpeed * 1.1f;
        } else if (check == 2) {
            pickedSpriteRenderer.color = new Color(1, 0, 1, 1);
            //picked.GetComponent<Rigidbody2D>().drag = 5;
        } else if (check == 3) {
            pickedSpriteRenderer.color = new Color(1, 1, 0, 1);
            //picked.GetComponent<Rigidbody2D>().drag = 0;
            
        }
        yield return new WaitForSeconds(flashTime);
        pickedSpriteRenderer.color = new Color(1, 1, 1, 1);

    }

    IEnumerator FeedbackofPerfect(GameObject picked){
        //picked.GetComponent<Rigidbody2D>().drag = 50;
        yield return new WaitForSeconds(1f);
        //picked.GetComponent<Rigidbody2D>().drag = 0;
    }
}
