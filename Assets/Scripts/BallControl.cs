using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{
    public float forceMax;
    public float deadDistance;
    public Vector3 force;
    private Vector3 BallToCamera;
    public bool isEat;
    public Text Text;
    public Text Settlement;
    private string state;
    private int score=0;
    // Start is called before the first frame update
    void Start()
    {
        BallToCamera = Camera.main.transform.position - transform.position;
        ChangeMode(true);
    }

    // Update is called once per frame
    void Update()
    {
        force.x = Input.GetAxis("Horizontal");
        force.z = Input.GetAxis("Vertical");
        this.GetComponent<Rigidbody>().AddForce(force*forceMax);
        Text.text = "Your score is " + score.ToString() + "!\nYour Mode is " + state;
        Camera.main.transform.position = transform.position + BallToCamera;
        if (transform.position.y <= -deadDistance)
        {
            Settlement.text = "You Died!\nYour Score is " + score.ToString() + "!";
            Settlement.gameObject.SetActive(true);
        }
            

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isEat&&collision.gameObject.layer==LayerMask.NameToLayer("Cube"))
        {
            if (collision.gameObject.transform.parent.childCount == 1)
            {
                GameObject.Destroy(collision.gameObject);
                score++;
                Settlement.text = "You Win!\nYour Score is " + score.ToString() + "!";
                Settlement.gameObject.SetActive(true);
            }
            else
            {
                GameObject.Destroy(collision.gameObject);
                score++;
            }
        }
    }

    public void ChangeMode(bool isEat)
    {
        this.isEat = isEat;
        if (isEat)
            state = "Eat Status!";
        else
            state = "Crash Status!";
    }
    public void ModeChange()
    {
        this.isEat = !isEat;
        if (isEat)
            state = "Eat Status!";
        else
            state = "Crash Status!";
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
