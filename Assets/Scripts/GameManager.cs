using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public bool gameStarted = false;

    public GameObject platformSpawner;

    private int score = 0;

    private int highScore;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text HighScoreText;

    [SerializeField] private GameObject gamePlayUI;

    [SerializeField] private GameObject menuUI;

    private AudioSource audioSource;
    [SerializeField] private AudioClip gameMenuMusic, gameStartMusic;
    [SerializeField] private AudioClip[] soundEffect;

    [SerializeField] private GameObject cochecito;


    int adCounter = 0;

    public void Awake(){
        if(instance==null){
            instance=this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    void Start(){
        highScore = PlayerPrefs.GetInt("HighScore");

        HighScoreText.text = "record: " + highScore;

        CheckAdCount();        
    }

    void Update(){
        if(!gameStarted){
            if(Input.GetMouseButtonDown(0)){
                GameStart();
            }
        }
    }

    public void GameStart(){
        gameStarted=true;
        platformSpawner.SetActive(true);
        gamePlayUI.SetActive(true);
        menuUI.SetActive(false);

        audioSource.clip = gameStartMusic;
        audioSource.Play();

        StartCoroutine("UpdateScore");        
    }

    public void GameOver(){
        platformSpawner.SetActive(false);
        StopCoroutine("UpdateScore"); 
        SaveHighScore();  

        if(adCounter>=4){
            adCounter=0;
            PlayerPrefs.SetInt("AdCount",0);
            Invoke(nameof(PreAds), 1f);
                       
        }
        else{
            Invoke(nameof(ReloadLevel), 1f);
        }              
    }

    public void ReloadLevel(){
        SceneManager.LoadScene("Game");
    }

    public void PreAds(){        
        AdsManager.instance.ShowAd();
    }

    public void PostAds(){        
        cochecito.transform.position = Platform.instance.UltimoPisado();
        platformSpawner.SetActive(true);
        StartCoroutine("UpdateScore"); 
        SaveHighScore(); 
    }

    IEnumerator UpdateScore(){
        while(true){
            yield return new WaitForSeconds(1f);
            score++;
            scoreText.text = score.ToString();
        }
    }

    public void IncrementScore(int incremento){
        score += incremento;
        scoreText.text = score.ToString();
        float volumen;
        if(incremento<3){
            volumen = 0.2f;
        }
        else{
            volumen = 0.5f;
        }
        int sonidoRandom = Random.Range(0,soundEffect.Length);
        audioSource.PlayOneShot(soundEffect[sonidoRandom], volumen);
    }

    public void SaveHighScore(){
        if(PlayerPrefs.HasKey("HighScore")){
            if(score > PlayerPrefs.GetInt("HighScore")){
                PlayerPrefs.SetInt("HighScore",score);
            }
        }
        else{
            PlayerPrefs.SetInt("HighScore",score);
        }
    }

    void CheckAdCount(){
        if(PlayerPrefs.HasKey("AdCount")){
            adCounter = PlayerPrefs.GetInt("AdCount");
            adCounter++;
            PlayerPrefs.SetInt("AdCount", adCounter);
        }
        else{
            PlayerPrefs.SetInt("AdCount", 0);
        }
    }
}
