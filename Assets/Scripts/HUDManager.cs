using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [SerializeField] private string healthBarElementName;
    [SerializeField] private Player player;
    [SerializeField] private Color healthBarBackgroundColour;
    [SerializeField] private Color healthBarProgressColour;

    private VisualElement hud;
    private ProgressBar healthBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        hud = GetComponent<UIDocument>().rootVisualElement;
    }

    private void Start()
    {
        player.OnHurt.AddListener(ChangeHealth);
        ChangeHealth(player);
    }

    private void OnEnable()
    {
        healthBar = hud.Q<ProgressBar>(healthBarElementName);
        ChangeHealth(player);
    }

    private void ChangeHealth(LivingObject livingObject)
    {
        healthBar.value = player.Health;
    }
}
