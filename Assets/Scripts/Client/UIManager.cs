using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region References

    private Character currentCharacter;
    private GameObject currentActiveMenu;
    private GameObject CS_currentActiveTab;

    [SerializeField] private GameObject mainMenu;

    [Header("Character Creation")]
    [SerializeField] private GameObject characterCreationMenu;
    [Space]
    [SerializeField] private TMP_Text scorePointsRemaining;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text strengthScore_CC;
    [SerializeField] private TMP_Text dexterityScore_CC;
    [SerializeField] private TMP_Text constitutionScore_CC;
    [SerializeField] private TMP_Text intelligenceScore_CC;
    [SerializeField] private TMP_Text charismaScore_CC;
    [SerializeField] private TMP_Text awarenessScore_CC;
    [Space]
    [SerializeField] private GameObject addSkillMenu;
    [SerializeField] private TMP_Text skillPointsRemainingText;
    [SerializeField] private TMP_InputField skillName1;
    [SerializeField] private TMP_Text skillScoreText1;
    [SerializeField] private TMP_InputField skillName2;
    [SerializeField] private TMP_Text skillScoreText2;
    [SerializeField] private TMP_InputField skillName3;
    [SerializeField] private TMP_Text skillScoreText3;
    [SerializeField] private TMP_InputField skillName4;
    [SerializeField] private TMP_Text skillScoreText4;
    [SerializeField] private TMP_InputField skillName5;
    [SerializeField] private TMP_Text skillScoreText5;

    [Header("Load Character")]
    [SerializeField] private GameObject loadCharacterMenu;
    [SerializeField] private TMP_Dropdown loadCharacterDropdown;

    [Header("Character Sheet")]
    [SerializeField] private GameObject characterSheetMenu;

    [Header("Character Header")]
    [SerializeField] private TMP_Text CS_characterName;
    [SerializeField] private TMP_Text CS_characterLevel;
    [SerializeField] private Slider CS_xpSlider;

    [SerializeField] private TMP_Text CS_characterHealth;
    [SerializeField] private TMP_Text CS_characterMana;

    [Header("Character Stats")]
    [SerializeField] private TMP_Text CS_strengthScore;
    [SerializeField] private TMP_Text CS_dexterityScore;
    [SerializeField] private TMP_Text CS_constitutionScore;
    [SerializeField] private TMP_Text CS_intelligenceScore;
    [SerializeField] private TMP_Text CS_charismaScore;
    [SerializeField] private TMP_Text CS_awarenessScore;

    [Header("Character Skills")]


    [Header("Character Inventory")]


    [Header("Character Achievement")]


    #endregion

    #region Variables

    #region Character Creation

    private int pointsRemaining = 14;

    private int strength;
    private int dexterity;
    private int constitution;
    private int intelligence;
    private int charisma;
    private int awareness;

    private int skillPointsRemaining = 15;

    private int skillLevel1;
    private int skillLevel2;
    private int skillLevel3;
    private int skillLevel4;
    private int skillLevel5;

    #endregion

    #region Load Character

    private List<Character> savedCharacters;

    #endregion

    #endregion


    private void Start()
    {
        mainMenu.SetActive(true);
        currentActiveMenu = mainMenu;
        currentCharacter = new Character();
    }

    private void Update()
    {
        
    }

    #region Character Creation

    public void OpenCreateCharacterMenu()
    {
        currentActiveMenu.SetActive(false);
        characterCreationMenu.SetActive(true);
        currentActiveMenu = characterCreationMenu;
    }

    public void ModifyStrength(int amount)
    {
        var str = strength + amount;
        if (str < 0 || str > 4) return;
        strength += amount;
        pointsRemaining -= amount;

        strengthScore_CC.text = strength.ToString();
        scorePointsRemaining.text = pointsRemaining.ToString();
    }

    public void ModifyDexterity(int amount)
    {
        var dex = dexterity + amount;
        if (dex < 0 || dex > 4) return;
        dexterity += amount;
        pointsRemaining -= amount;

        dexterityScore_CC.text = dexterity.ToString();
        scorePointsRemaining.text = pointsRemaining.ToString();
    }

    public void ModifyConstitution(int amount)
    {
        var con = constitution + amount;
        if (con < 0 || con > 4) return;
        constitution += amount;
        pointsRemaining -= amount;

        constitutionScore_CC.text = con.ToString();
        scorePointsRemaining.text = pointsRemaining.ToString();
    }

    public void ModifyIntelligence(int amount)
    {
        var intel = intelligence + amount;
        if (intel < 0 || intel > 4) return;
        intelligence += amount;
        pointsRemaining -= amount;

        intelligenceScore_CC.text = intelligence.ToString();
        scorePointsRemaining.text = pointsRemaining.ToString();
    }

    public void ModifyCharisma(int amount)
    {
        var ris = charisma + amount;
        if (ris < 0 || ris > 4) return;
        charisma += amount;
        pointsRemaining -= amount;

        charismaScore_CC.text = charisma.ToString();
        scorePointsRemaining.text = pointsRemaining.ToString();
    }

    public void ModifyAwareness(int amount)
    {
        var aware = awareness + amount;
        if (aware < 0 || aware > 4) return;
        awareness += amount;
        pointsRemaining -= amount;

        awarenessScore_CC.text = awareness.ToString();
        scorePointsRemaining.text = pointsRemaining.ToString();
    }

    public void ModifySkill1(int amount)
    {
        var level = skillLevel1 + amount;
        if (level < 0 || level > 6) return;
        skillLevel1 += amount;
        skillPointsRemaining -= amount;

        skillScoreText1.text = skillLevel1.ToString();
        skillPointsRemainingText.text = skillPointsRemaining.ToString();
    }

    public void ModifySkill2(int amount)
    {
        var level = skillLevel2 + amount;
        if (level < 0 || level > 6) return;
        skillLevel2 += amount;
        skillPointsRemaining -= amount;

        skillScoreText2.text = skillLevel2.ToString();
        skillPointsRemainingText.text = skillPointsRemaining.ToString();
    }

    public void ModifySkill3(int amount)
    {
        var level = skillLevel3 + amount;
        if (level < 0 || level > 6) return;
        skillLevel3 += amount;
        skillPointsRemaining -= amount;

        skillScoreText3.text = skillLevel3.ToString();
        skillPointsRemainingText.text = skillPointsRemaining.ToString();
    }

    public void ModifySkill4(int amount)
    {
        var level = skillLevel4 + amount;
        if (level < 0 || level > 6) return;
        skillLevel4 += amount;
        skillPointsRemaining -= amount;

        skillScoreText4.text = skillLevel4.ToString();
        skillPointsRemainingText.text = skillPointsRemaining.ToString();
    }

    public void ModifySkill5(int amount)
    {
        var level = skillLevel5 + amount;
        if (level < 0 || level > 6) return;
        skillLevel5 += amount;
        skillPointsRemaining -= amount;

        skillScoreText5.text = skillLevel5.ToString();
        skillPointsRemainingText.text = skillPointsRemaining.ToString();
    }

    public void ToSkillsMenu()
    {
        currentActiveMenu.SetActive(false);
        addSkillMenu.SetActive(true);
        currentActiveMenu = addSkillMenu;
    }

    public void BackFromSkillsMenu()
    {
        currentActiveMenu.SetActive(false);
        characterCreationMenu.SetActive(true);
        currentActiveMenu = characterCreationMenu;
    }

    public void ConfirmCharacterCreation()
    {
        var skills = new List<Skill>();

        if (skillLevel1 > 0)
            skills.Add(new() { Name = skillName1.text, Score = skillLevel1 });
        if (skillLevel2 > 0)
            skills.Add(new() { Name = skillName2.text, Score = skillLevel2 });
        if (skillLevel3 > 0)
            skills.Add(new() { Name = skillName3.text, Score = skillLevel3 });
        if (skillLevel4 > 0)
            skills.Add(new() { Name = skillName4.text, Score = skillLevel4 });
        if (skillLevel5 > 0)
            skills.Add(new() { Name = skillName5.text, Score = skillLevel5 });


        var character = new Character()
        {
            Name = nameInput.text,
            Level = 0,
            Health = constitution + 2,
            MaxHealth = constitution + 2,
            Mana = intelligence + 2,
            MaxMana = intelligence + 2,
            Strength = strength,
            Dexterity = dexterity,
            Constitution = constitution,
            Intelligence = intelligence,
            Charisma = charisma,
            Awareness = awareness,
            Skills = skills,
            EntropyTokens = 0,
        };

        currentCharacter = character;
        CharacterHelper.SaveCharacter(character);
        OpenCharacterSheetMenu(currentCharacter);
    }

    #endregion

    #region Load Character

    public void OpenLoadCharacterMenu()
    {
        currentActiveMenu.SetActive(false);
        loadCharacterMenu.SetActive(true);
        currentActiveMenu = loadCharacterMenu;

        InitializeLoadCharacters();
    }

    private void InitializeLoadCharacters()
    {
        savedCharacters = CharacterHelper.GetCharacters();
        var characterNames = new List<string>();
        foreach (var character in savedCharacters)
        {
            characterNames.Add(character.Name);
        }

        loadCharacterDropdown.AddOptions(characterNames);
    }

    public void SelectCharacter()
    {
        var characterSelect = loadCharacterDropdown.value;
        currentCharacter = savedCharacters[characterSelect];
        OpenCharacterSheetMenu(currentCharacter);
    }

    #endregion

    #region Character Sheet

    public void OpenCharacterSheetMenu(Character character)
    {
        currentActiveMenu.SetActive(false);
        characterSheetMenu.SetActive(true);
        currentActiveMenu = characterSheetMenu;

        InitializeCharacterSheet();
    }

    private void InitializeCharacterSheet()
    {
        CS_characterName.text = currentCharacter.Name;
        CS_characterLevel.text = "Level: " + currentCharacter.Level.ToString();

        CS_xpSlider.maxValue = 100;
        CS_xpSlider.value = currentCharacter.XP;

        CS_characterHealth.text = currentCharacter.Health.ToString() + "/" + currentCharacter.MaxHealth.ToString();

        CS_characterMana.text = currentCharacter.Mana.ToString() + "/" + currentCharacter.MaxMana.ToString();

        CS_strengthScore.text = currentCharacter.Strength.ToString();
        CS_dexterityScore.text = currentCharacter.Dexterity.ToString();
        CS_constitutionScore.text = currentCharacter.Constitution.ToString();
        CS_intelligenceScore.text = currentCharacter.Intelligence.ToString();
        CS_charismaScore.text = currentCharacter.Charisma.ToString();
        CS_awarenessScore.text = currentCharacter.Awareness.ToString();
    }

    public void CS_OpenStatsTab()
    {

    }


    #endregion
}