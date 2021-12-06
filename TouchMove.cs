
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField]
    GameObject player;

    [SerializeField]

    float xValue;
    PlayerAction playerAct;
    public void Awake(){
         playerAct = player.GetComponent<PlayerAction>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        playerAct.SetX(xValue);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        playerAct.SetX(0);
    }
}
