using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour {


    private float movableRange = 3.4f;

    private Rigidbody myRigidbody;

    private float forwardForce = 800.0f;

    private Animator myAnimator;

    private float turnForce = 500.0f;

    private float upForce = 500.0f;

    private float MovableRange = 3.4f;

    private float coefficient = 0.95f;

    private bool isEnd = false;

    private GameObject resultText;

    private int score = 0;

    private bool isLButtonDown = false;

    private bool isRButtonDown = false;

    private GameObject stateText;

    private readonly float visiblePosZ = 0;


    // Use this for initialization
    void Start () {

        this.myAnimator = GetComponent<Animator>();

        this.myAnimator.SetFloat("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();

        this.resultText = GameObject.Find("GameResultText");

        this.stateText = GameObject.Find("ScoreText");

        


    }
	
	// Update is called once per frame
	void Update () {

        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        if (Input.GetKey(KeyCode.LeftArrow) && -this.MovableRange < this.transform.position.x)
        {

            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < this.movableRange)
        {

            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool ("Jump", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);

            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }

        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;
        }

        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);


        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            //左に移動
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && this.transform.position.x < this.movableRange)
        {
            //右に移動
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        

    }

     



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            this.isEnd = true;

            this.resultText.GetComponent<Text>().text = "GAME OVER";
        }

        if (other.gameObject.tag == "GoalTag")
        {
            isEnd = true;

            this.resultText.GetComponent<Text>() .text = "CLEAR!!";
        }

        if (other.gameObject.tag == "CoinTag")
        { 

            GetComponent<ParticleSystem>().Play();

            Destroy(other.gameObject);

            score += 10;

            this.stateText .GetComponent<Text>().text = "Score " + this.score + "pt";
        }



    }

    private void DestroyObject(bool v)
    {
        if (transform.position.z < visiblePosZ)
        {
            DestroyObject(gameObject.tag == "CarTag" || gameObject.tag == "CoinTag" || gameObject.tag == "TrafficCone");
        }
    }



    private static void DestroyObject()
    {
  
    }

    public void GetMyJumpButtonDown()
    {
        if (this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }

    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;
    }

    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }

    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }

}
