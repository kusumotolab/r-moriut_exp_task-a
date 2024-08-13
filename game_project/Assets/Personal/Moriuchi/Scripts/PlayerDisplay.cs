using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDisplay : MonoBehaviour
{
    [SerializeField] private Sprite Upsprite;
    [SerializeField] private Sprite Downsprite;
    [SerializeField] private Sprite Leftsprite;
    [SerializeField] private GameObject player;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update(){
        this.transform.position = player.transform.position;
        Vector3 localAngle= player.transform.localEulerAngles;
        SpriteSet(localAngle);
    }

    private void SpriteSet(Vector3 localAngle)
    {
        spriteRenderer.flipX = false;
        Debug.Log(localAngle.z);

        if((44 <= localAngle.z && localAngle.z <= 135)){
            Debug.Log("左");
            spriteRenderer.sprite = Leftsprite;
        }else if(136 <= localAngle.z && localAngle.z <= 225){
            Debug.Log("下");
            spriteRenderer.sprite = Downsprite;
        }else if(226 <= localAngle.z && localAngle.z <= 315){
            Debug.Log("右");
            spriteRenderer.sprite = Leftsprite;
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.sprite = Upsprite;
        }
    }
}
