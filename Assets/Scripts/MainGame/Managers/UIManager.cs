using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI hpText;

    [SerializeField] private PlayerCharacterController bobby;
    [SerializeField] private SkillButtonUI[] skillButtonUI;
    [SerializeField] private Sprite[] skillIcons;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        bobby.onTakeDamageEventAction += RefreshHPText;
    }

    private void Start()
    {
        hpText.text = bobby.Hp.ToString();
        SetSkillButtonUI();
    }

    public void RefreshHPText(int newHP)
    {
        hpText.text = newHP.ToString();
    }

    private const string SKILL_LABEL_PREFIX = "Skill ";

    private void SetSkillButtonUI()
    {
        for (int i = 0; i < skillButtonUI.Length; i++)
        {
            if (skillIcons == null || skillIcons.Length == 0) return;

            skillButtonUI[i].skillIcon.sprite = skillIcons[i];
            skillButtonUI[i].skillNameText.text = SKILL_LABEL_PREFIX + (i + 1);
        }
    }
}