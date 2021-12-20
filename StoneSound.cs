using UnityEngine;

public class StoneSound : MonoBehaviour
{

    AudioSource audioSource;

    private void Awake(){
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    //public bool soundTrigger = true;

    // public void SetSoundTrigger(bool valueSet) {
    //     soundTrigger = valueSet;
    // }
    public void RunSound() {

        audioSource.Play();
        // if(soundTrigger) {
            
        //     audioSource.Play();
        //     Debug.Log("돌소리 재생" + soundTrigger);

            
        // }
    }

    // void OnTriggerEnter2D(Collider2D collider) {
    //     Debug.Log("돌충돌!2");
    //     RunSound();
    //     // if(collider.CompareTag("Object")) {
    //     //     Debug.Log("돌충돌!2");
    //     //     RunSound();
    //     // }
        
    // }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("돌충도로오롱");
        RunSound();
    }
 }
