using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public GameObject objScore;
    public int requiredScore;
    public GameObject objTimer;
    public GameObject gameOverText;
    public GameObject gameOverPanel;
    public GameObject gameWinText;
    public GameObject gameWinPanel;
    private int score = 0;
    public float totalTime = 120f;

    public float patience;
    public GameObject face1;
    public GameObject face2;
    public GameObject face3;
    public GameObject face4;

    public GameObject objBluePill;
    public GameObject objGreenPill;
    public GameObject objRedPill;
    public GameObject objYellowPill;

    public GameObject objBluePotion;
    public GameObject objGreenPotion;
    public GameObject objRedPotion;
    public GameObject objYellowPotion;

    public GameObject objBlueSyringe;
    public GameObject objGreenSyringe;
    public GameObject objRedSyringe;
    public GameObject objYellowSyringe;

    public GameObject winFaces;
    public bool[] allowedItems = new bool[28];
    public AudioManager am;
    public float boundary;
    private double[] xPos = new double[5] {-4.5, -1.63, 1.21, 4.1, 7};
    public List<GameObject> wishes = new List<GameObject>();

    public bool[] taken = new bool[5];
    public int activePeople = 0;
    bool gameOver = false;
    // Start is called before the first frame update
    public void RetryLevel()
    {
        am.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        am.Play("ButtonClick");
        Debug.Log("Quit");
        Application.Quit();
    }
    public void NextLevel()
    {
        am.Play("ButtonClick");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void Start()
    {
        if (allowedItems[14])
            wishes.Add(objBluePill);
        if (allowedItems[15])
            wishes.Add(objGreenPill);
        if (allowedItems[16])
            wishes.Add(objRedPill);
        if (allowedItems[17])
            wishes.Add(objYellowPill);

        if (allowedItems[19])
            wishes.Add(objBluePotion);
        if (allowedItems[20])
            wishes.Add(objGreenPotion);
        if (allowedItems[21])
            wishes.Add(objRedPotion);
        if (allowedItems[22])
            wishes.Add(objYellowPotion);

        if (allowedItems[24])
            wishes.Add(objBlueSyringe);
        if (allowedItems[25])
            wishes.Add(objGreenSyringe);
        if (allowedItems[26])
            wishes.Add(objRedSyringe);
        if (allowedItems[27])
            wishes.Add(objYellowSyringe);

        am = FindObjectOfType<AudioManager>();

        UpdateScoreText();

        int i = (int)Random.Range(0, 3.999f);
        am.Play("Theme" + (i+1));
    }
    public void AddScore()
    {
        if (!gameOver)
        {
            score++;
            UpdateScoreText();
        }
    }
    void UpdateScoreText()
    {
        objScore.GetComponent<TextMeshProUGUI>().text = "Score: " + score + " / " + requiredScore;
    }
    void UpdateTimeText()
    {
        if (totalTime <= 0)
        {
            EndGame();
            return;
        }
        objTimer.GetComponent<TextMeshProUGUI>().text = "Time left: " + Mathf.Floor(totalTime);
    }
    void EndGame()
    {
        gameOver = true;
        if (score >= requiredScore)
        {
            if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                //last level
                am.Play("GameWin");
                gameWinText.GetComponent<TextMeshProUGUI>().text = "Congratulations, you managed to provide enough medicine and save everyone!\nYour score: " + score;
                gameWinPanel.SetActive(true);
                winFaces.SetActive(true);
            }
            else
            {
                am.Play("GameWin");
                gameWinText.GetComponent<TextMeshProUGUI>().text = "Congratulations, you met the goal for today!\nYour score: " + score;
                gameWinPanel.SetActive(true);
            }
        }
        else
        {
            am.Play("GameLose");
            gameOverText.GetComponent<TextMeshProUGUI>().text = "Unfortunately you didn't meet the goal for today!\nYour score: " + score;
            gameOverPanel.SetActive(true);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            totalTime -= Time.deltaTime;
            UpdateTimeText();
            float i = Random.value;
            if (i < boundary/(activePeople+1)/(activePeople+1))
            {
                i = Random.value * 5;
                int j = (int)Mathf.Floor(i);
                if (j == 5) j--;
                if (!taken[j])
                {
                    activePeople++;
                    taken[j] = true;
                    Vector3 pos = new Vector3 ((float)xPos[j],-3.7f,0);
                    i = Random.value * 4;
                    int k = (int)Mathf.Floor(i);
                    GameObject created = null;
                    if (k == 0)
                    {
                        created = Instantiate(face1, pos, Quaternion.identity);
                    }
                    else if (k == 1)
                    {
                        created = Instantiate(face2, pos, Quaternion.identity);
                    }
                    else if (k == 2)
                    {
                        created = Instantiate(face3, pos, Quaternion.identity);
                    }
                    else
                    {
                        created = Instantiate(face4, pos, Quaternion.identity);
                    }
                    created.GetComponent<PatientScript>().idx = j;
                    int idx = Random.Range(0, wishes.Count);
                    GameObject wish = wishes[idx];
                    GameObject created2 = Instantiate(wish, pos, Quaternion.identity);
                    created.GetComponent<PatientScript>().itemNeeded = wish.name;
                    created2.transform.parent = created.transform;
                    created2.transform.position = created2.transform.position + new Vector3(0.6336f, 2.304f, 0);
                }
            }
        }
    }
}
