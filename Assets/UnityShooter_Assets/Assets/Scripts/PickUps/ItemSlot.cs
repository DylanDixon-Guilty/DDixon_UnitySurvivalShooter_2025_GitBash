using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{

    //==Item Slot==//
    public TextMeshProUGUI onNumber1;
    public TextMeshProUGUI onNumber2;
    public Color numberSelected = Color.green;
    public Color numberUnSelected = Color.white;

    private void Awake()
    {
        onNumber1.color = numberSelected; //This way the Rifle (1) is Green before the game starts
    }

    private void Update()
    {
        ItemSelected();
    }

    //When player presses One or Two, the number on the Item list will appear Green. Indicating which weapon the player is using
    public void ItemSelected()
    {
        if(Input.GetButtonDown("ItemOne"))
        {
            onNumber1.color = numberSelected;
            onNumber2.color = numberUnSelected;
        }
        else if (Input.GetButtonDown("ItemTwo"))
        {
            onNumber1.color= numberUnSelected;
            onNumber2.color = numberSelected;
        }
    }
}
