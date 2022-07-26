using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public bool holdingBall = true;
    public Rigidbody ball;
    public Transform target;
    public float h = 15f;
    public float gravity = -18f;

    public Animator ballAnim;
    public Button play;
    public GameObject camera;

    public Text score;
    public static int numberOfPoint;
    public GameObject missArea;
    public Text miss;
    public static int missPoint;
    private int highScore;
    public Text highScoreText;

    public Text gameover;
    public Button throwBall;
    public Button spawnBall;

    public GameObject slider;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ball.useGravity = false;
        ball.freezeRotation = false;
        ball.isKinematic = false;
        numberOfPoint = 0;
        missPoint = 0;
        ballAnim.enabled = false;  
        play.gameObject.SetActive(true);
        gameover.gameObject.SetActive(false);

        slider.gameObject.SetActive(false);
        
        throwBall.gameObject.SetActive(false);
        spawnBall.gameObject.SetActive(false);

        highScore = PlayerPrefs.GetInt("hScore", 0);

    }

    // Update is called once per frame
    void Update()
    {
        score.text = numberOfPoint.ToString();
        miss.text = missPoint.ToString();

        if(missPoint == 3)
        {
            GameOver();
        }
    }

    public void GamePlay()
    {
        play.gameObject.SetActive(false);
        gameover.gameObject.SetActive(false);
        camera.transform.position = new Vector3(0.1f, 7.35f, -8.82f);
        spawnBall.gameObject.SetActive(true);
        throwBall.gameObject.SetActive(true);
        slider.gameObject.SetActive(true);
        numberOfPoint = 0;
        ball.transform.position = new Vector3(0, 2.68f, -3.66f);
        ball.useGravity = false;
        ball.isKinematic = true;
        ball.freezeRotation = true;
        ballAnim.enabled = false;
        throwBall.enabled = true;
    }

    public void ThrowBall()
    {
        camera.transform.position = new Vector3(0f, 6.59f, -18.92f);
        ballAnim.enabled = true;
        ball.isKinematic = false;
        ball.freezeRotation = false;
        holdingBall = false;
        Physics.gravity = Vector3.up * gravity;
        ball.useGravity = true;
        ball.velocity = CalculateThrow();
    }

    Vector3 CalculateThrow()
    {
        float displacementY = target.position.y - ball.position.y;
        Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2*h/gravity) + Mathf.Sqrt(2*(displacementY - h)/gravity));

        return velocityXZ + velocityY;
    }

    public void Spwan()
    {
        throwBall.enabled = true;
        ball.useGravity = false;
        ball.transform.position = new Vector3(0, 2.68f, -3.66f);
        ball.isKinematic = true;
        ball.freezeRotation = true;
        camera.transform.position = new Vector3(0.1f, 7.35f, -8.82f);
        missArea.gameObject.SetActive(true);
        ballAnim.enabled = false;
    }

    public void AdjustHeight(float height)
    {
        h = height;
    }

    public void GameOver()
    {
        slider.gameObject.SetActive(false);
        gameover.gameObject.SetActive(true);
        play.gameObject.SetActive(true);
        throwBall.gameObject.SetActive(false);
        spawnBall.gameObject.SetActive(false);
        missPoint = 0;
        if(numberOfPoint > highScore)
        {
            highScore = numberOfPoint;
            highScoreText.text = highScore.ToString();
            PlayerPrefs.SetInt("hScore", highScore);
            PlayerPrefs.Save();
        }
    }
}
