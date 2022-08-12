using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;
    
    public ChallengeList Challenges;
    [HideInInspector] public int numPlayers;

    [Header("Selectors")]
    public TextMeshProUGUI sliderTitle;
    public TextMeshProUGUI sliderDescription;
    public Slider slider;
    
    public TMP_InputField playerCounter;

    public Toggle includeTerrible;
    public Toggle includeDeep;
    
    private void Awake()
    {
        if (Instance != null) return;
        
        Instance = this;
        UpdateSlider();
        DontDestroyOnLoad(this);
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
    
    private int GetSliderValue()
    {
        return (int) slider.value;
    }

    public void UpdateSlider()
    {
        switch (GetSliderValue())
        {
            case 1:
                sliderTitle.SetText("Family Friendly");
                sliderDescription.SetText("Play with your grandma! It's all fun and games here.");
                break;
            case 2:
                sliderTitle.SetText("PG-13");
                sliderDescription.SetText("Talks about kissing & politics; Don't play with people you wouldn't give your phone password to.");
                break;
            case 3:
                sliderTitle.SetText("Terrible Individuals");
                sliderDescription.SetText("Yeah, this one's definitely not-safe-for work; recommended only for ages 18+.");
                break;
            default:
                sliderTitle.SetText("Unknown Tier");
                sliderDescription.SetText("Whoops, wiggle the slider a little bit.");
                break;
        }
    }

    public void AddPlayer()
    {
        if (!ValidateText()) return;
        
        var current = int.Parse(playerCounter.text);
        current++;
        playerCounter.SetTextWithoutNotify(current.ToString());
    }
    
    public void SubtractPlayer()
    {
        if (!ValidateText()) return;
        
        var current = int.Parse(playerCounter.text);
        if (current > 3) current--;
        playerCounter.SetTextWithoutNotify(current.ToString()); }

    public void UpdateText()
    {
        ValidateText();
        var current = int.Parse(playerCounter.text);

        if (current < 3) ResetPlayerCounter();
    }

    private bool ValidateText()
    {
        bool isValid = int.TryParse(playerCounter.text, out _);
        if (!isValid) ResetPlayerCounter();
        return isValid;
    }

    private void ResetPlayerCounter()
    {
        playerCounter.SetTextWithoutNotify("3"); 
    }

    private void ChangedActiveScene(Scene arg0, Scene arg1)
    {
        if (arg1.buildIndex != 1) return;
        
        numPlayers = int.Parse(playerCounter.text);
        Challenges = new ChallengeList(GetSliderValue(), includeDeep.isOn, includeTerrible.isOn);
        Debug.Log("Created game session's challenges " + Challenges);
        
        FindObjectOfType<GameManager>().InitializeGame();
    }

}