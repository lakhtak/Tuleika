using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler
{
    private Button button;
    private Menu mainMenu;

    void Start()
    {
        mainMenu = GameObject.FindWithTag("MainMenu").GetComponent<Menu>();

        button = GetComponent<Button>();
        button.onClick.AddListener(ClickHandler);
    }

    void Destroy()
    {
        button.onClick.RemoveListener(ClickHandler);
    }

    private void ClickHandler()
    {
        mainMenu.PlayClickSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mainMenu.PlaySelectSound();
    }
}
