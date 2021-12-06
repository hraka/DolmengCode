
using UnityEngine;

public class TimingManager : MonoBehaviour
{

    [SerializeField]
    Transform Center = null;

    [SerializeField]
    GameObject[] timingObj = null;
    // Start is called before the first frame update

    Vector2[] timingCircles = null;
    void Start()
    {
        // timingCircles = new Vector2[timingObj.Length];
        // for(int i = 0; i < timingObj.Length; i++) {
        //     timingCircles[i].Set(Center.localPosition.x-timingObj)
        // }
        // timingBoxes = new Vector2[timingRect.Length];

        // for(int i = 0; i < timingRect.Length; i++) {
        //     timingBoxes[i].Set(Center.localPosition.x-timingRect)
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
