using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int HP = 3;
    int MaxHp;

    public float speed;
    public float JumpPower;

    public GameObject Hit_Prefab;
    public GameObject Hit_Prefab2;
    public GameObject GameOverObj;
    public GameObject GameClearObj;

    bool isJump = false;
    bool isOver = false;

    Rigidbody2D rigidbody;
    Animator anim;
    public Animator TramAnimation;

    public Text Hp_Text;
    public Slider slider;
   



    private void Start()
    {
        MaxHp = HP;
        slider.value =  (float)HP/ (float)MaxHp;

        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();


        Hp_Text.text = "HP: " + HP.ToString();
    }

    
    void Update()
    {
        

        slider.value = (float)HP / (float)MaxHp;


        if (Input.GetKey(KeyCode.RightArrow))
        {
           //transform.Translate(speed*Time.deltaTime, 0, 0);
            rigidbody.velocity = new Vector2(speed,rigidbody.velocity.y);


            GetComponent<SpriteRenderer>().flipX =false;
            //transform.localScale=new Vector3(-5.0f, transform.localScale.y, transform.localScale.z);
            if (isJump == false)
            {
                AnimatorChange("isRUN");
            }
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
               {
            //transform.Translate(-speed*Time.deltaTime, 0, 0);
            rigidbody.velocity = new Vector2(-speed, rigidbody.velocity.y);

            GetComponent<SpriteRenderer>().flipX = true;
            // transform.localScale = new Vector3(-5.0f, transform.localScale.y, transform.localScale.z);
            if (isJump == false)
            {
                AnimatorChange("isRUN");
            }

        }
        else
        {
            if (isJump == false)
            {
                AnimatorChange("isIDLE");
            }
                rigidbody.velocity=new Vector2(0.0f, rigidbody.velocity.y);
        }


        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            anim.SetTrigger("isJUMP");
            //GetComponent<SpriteRenderer>().sprite = JumpSprite;??????????????
            isJump = true;

            AnimatorChange("isJUMP");
            
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower);
        }

        if (rigidbody.velocity.y == 0.0f)
            isJump = false;
        

                }
    
    private void AnimatorChange(string temp)
    {
        anim.SetBool("isIDLE", false);
        anim.SetBool("isRUN", false);

        if(temp=="isJUMP")
        { 
            anim.SetTrigger("isJUMP");
            return;
        }
        anim.SetBool(temp, true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.name != "Tilemap")
        //Debug.Log(collision.gameObject.name+ " : ENTER");
      
        if(collision.gameObject.tag == "Obstacle")
        {
            HP--;
            Hp_Text.text = "HP: " + HP.ToString();

            if (HP == 0)
            {
                isOver = true;
                GameOverObj.SetActive(true);

                Destroy(gameObject);
            }


            GameObject go = Instantiate(Hit_Prefab, transform.position, Quaternion.identity);
            Destroy(go, 1.0f);

            anim.SetTrigger("isHit");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.name != "Tilemap")
       // Debug.Log(collision.gameObject.name + " :STAY");
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.name != "Tilemap")
        //Debug.Log(collision.gameObject.name + " : EXIT");
    }


    //private void OnTriggerEnter2D(Collider2D collision)


    //private void OnTriggerStay2D(Collider2D collision)



    // private void OnTriggerExit2D(Collider2D collision)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag =="Tram")
        {
            isJump = true;
            TramAnimation.SetTrigger("isJump");
            AnimatorChange("isJump");
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, JumpPower*2);
        }


        if(collision.gameObject.tag == "End")
        {
            GameClearObj.SetActive(true);
        }
    } 

    


}

