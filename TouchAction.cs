
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchAction : MonoBehaviour, IPointerDownHandler
{

    [SerializeField]
    GameObject player;

    [SerializeField]

    int codeNum;

    PlayerAction playerAct;



    public void Awake(){
         playerAct = player.GetComponent<PlayerAction>();
    }
    
     public void OnPointerDown(PointerEventData eventData)
    {
        
        playerAct.ActCode(codeNum);

    }

}
