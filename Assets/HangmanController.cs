using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HangmanController : MonoBehaviour
{
    [SerializeField] GameObject WordContainer;
    [SerializeField] GameObject KeyBoardContainer;
    [SerializeField] GameObject LetterContainer;
    [SerializeField] GameObject[] HangmanStages;
    [SerializeField] GameObject LetterButton;
    [SerializeField] TextAsset PossibleWord;

    private string word;
    private int incorrectGuesses, correctGuesses;

    public AudioClip answerSound;
    public AudioClip wrongSound;
    AudioSource audioSource;

    void Start()
    {
        InitialiseButtons();
        InitialiseGame();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

//65���� 90�� ���ĺ� �빮�� �ƽ�Ű�ڵ�
    private void InitialiseButtons()
    {
        for(int i = 65; i <= 90; i++)
        {
            CreateButton(i);
            if (i == 67) // C
            {
                CreateUniButton("\u0108");
            }
            if (i == 71) // G
            {
                CreateUniButton("\u011C");
            }
            if (i == 72) // H
            {
                CreateUniButton("\u0124");
            }
            if (i == 74) // J
            {
                CreateUniButton("\u0134");
            }
            if (i == 83) // S
            {
                CreateUniButton("\u015C");
            }
            if (i == 85) // U
            {
                CreateUniButton("\u016C");
            }
        }
    }

    private void InitialiseGame()
    {
        incorrectGuesses = 0;
        correctGuesses = 0;
        foreach(Button child in KeyBoardContainer.GetComponentsInChildren<Button>())
        {
            child.interactable = true;
        }
        foreach(Transform child in WordContainer.GetComponentInChildren<Transform>())
        {
            Destroy(child.gameObject);
        }
        foreach(GameObject stage in HangmanStages)
        {
            stage.SetActive(false);
        }

        word = generateWord().ToUpper();
        foreach(char letter in word)
        {
            var temp = Instantiate(LetterContainer, WordContainer.transform);
        }
    }

    private void CreateButton(int i)
    {
        GameObject temp = Instantiate(LetterButton, KeyBoardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = ((char)i).ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter(((char)i).ToString()); });
    }
    private void CreateUniButton(string s)
    {
        GameObject temp = Instantiate(LetterButton, KeyBoardContainer.transform);
        temp.GetComponentInChildren<TextMeshProUGUI>().text = s.ToString();
        temp.GetComponent<Button>().onClick.AddListener(delegate { CheckLetter(s.ToString()); });
    }

    // ���ܾ� ���� �뷫 3~11�ڱ��� ����, words.txt���� �������� ���� �ʼ�(���ϸ� ������ �ܾ �ȵ�)
    private string generateWord()
    {
        string[] wordList = PossibleWord.text.Split("\n");
        string line = wordList[Random.Range(0, wordList.Length - 1)];
        return line.Substring(0, line.Length - 1);
    }

    private void CheckLetter(string inputLetter)
    {
        bool letterInWord = false;
        for(int i = 0; i < word.Length; i++)
        {
            if(inputLetter == word[i].ToString())
            {
                letterInWord = true;
                correctGuesses++;
                WordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = inputLetter;
            }
        }
        if(letterInWord == false)
        {
            incorrectGuesses++;
            HangmanStages[incorrectGuesses - 1].SetActive(true);
        }
        CheckOutcome();
    }

    private void CheckOutcome()
    {
        if(correctGuesses == word.Length)
        {
            for(int i = 0; i < word.Length; i++)
            {
                WordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.green;
            }
            audioSource.PlayOneShot(answerSound); // ���� ȿ����
            Invoke("InitialiseGame", 1.5f); // ������ ���� �� �ܾ� ǥ���Ҷ������� �ð�
        }
        if(incorrectGuesses == HangmanStages.Length)
        {
            for(int i = 0; i < word.Length; i++)
            {
                WordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].color = Color.red;
                WordContainer.GetComponentsInChildren<TextMeshProUGUI>()[i].text = word[i].ToString();
            }
            audioSource.PlayOneShot(wrongSound, 2); // ���� ȿ����, ���� = 2
            Invoke("InitialiseGame", 1.5f);
        }
    }

}
