using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int combo;
    private Coroutine comboResetCoroutine;

    private float delayBeforeResetCombo = 3f;
    public int time;
    public int timeMax = 60;
    private int score = 0;
    
    public Counter counter;
    public PlayerController playerController;

    private bool gameOver = false;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text comboText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private TextMeshProUGUI finalScoreText;

    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject gameOverUI;


    [SerializeField]
    private GameObject panier;
    [SerializeField]
    private float xMax;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float zMax;
    [SerializeField]
    private float startY;

    private float moveSpeedX = 0;
    private float moveSpeedY = 0;
    private float moveSpeedZ = 0;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        audioSource = GetComponent<AudioSource>();
        playerController.onShoot.AddListener(onShoot);
        counter.onMark.AddListener(onMark);
    }

    // Update is called once per frame
    void Update()
    {
        MovePanier();
    }

    public void StartGame()
    {
        time = timeMax;
        playerController.StartGame();
        menuUI.SetActive(false);
        inGameUI.SetActive(true);
        StartCoroutine(timer());
    }

    private void onMark()
    {
        if(!gameOver)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
            combo++;
            score += combo;
            StopCoroutine(comboResetCoroutine);
            updateCombo();
            updateScore();
        }
        
    }

    private void onShoot()
    {
        comboResetCoroutine = StartCoroutine(resetCombo(delayBeforeResetCombo));
    }

    private IEnumerator timer()
    {
        while(time > 0)
        {
            time--;
            updateTimer();

            if (time == Mathf.Round(timeMax / 4 * 3))
                UpdateDifficulty(1);
            if (time == Mathf.Round(timeMax / 2))
                UpdateDifficulty(2);
            if (time == Mathf.Round(timeMax / 4))
                UpdateDifficulty(3);

            yield return new WaitForSeconds(1);
        }
        onFinish();
    }

    private IEnumerator resetCombo(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        combo = 0;
        comboText.text = "";
    }

    private void updateTimer()
    {
        timeText.text = "Time : " + time;
    }

    private void updateScore()
    {
        scoreText.text = "Score : " + score;
    }

    private void updateCombo()
    {
        comboText.text = "Combo : " + combo;
    }



    private void MovePanier()
    {
        panier.transform.position += new Vector3(moveSpeedX * 0.01f, moveSpeedY * 0.01f, moveSpeedZ * 0.01f);

        if(panier.transform.position.x > xMax || panier.transform.position.x < -xMax)
        {
            moveSpeedX = -moveSpeedX;
        }
        if (panier.transform.position.z > zMax || panier.transform.position.z < -zMax)
        {
            moveSpeedZ = -moveSpeedZ;
        }
        if (panier.transform.position.y > yMax + startY || panier.transform.position.y < startY)
        {
            moveSpeedY = -moveSpeedY;
        }
    }

    private void UpdateDifficulty(int difficulty)
    {
        if (difficulty == 0)
        {
            return;
        }
        else if (difficulty == 1)
        {
            moveSpeedZ = 1;
        }
        else if (difficulty == 2)
        {
            moveSpeedX = 1;
        }
        else if (difficulty == 3)
        {
            moveSpeedY = 1;
        }

    }

    private void onFinish()
    {
        playerController.canPlay = false;
        gameOver = true;
        gameOverUI.SetActive(true);
        inGameUI.SetActive(false);
        finalScoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
