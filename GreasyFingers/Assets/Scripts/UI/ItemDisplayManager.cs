using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplayManager : MonoBehaviour
{
    [SerializeField] private Item displayType;
    private PlayerInventory inventory;

    [SerializeField] private ItemLibrary imageLibrary;
    [SerializeField] private SpriteLibrary backgroundLibrary;

    [SerializeField] private Image itemDisplay;
    [SerializeField] private Image backgroundDisplay;
    [SerializeField] private TextMeshProUGUI countText;
    public bool isLevelDisplay = false;

    private int count = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int c = inventory.Get(displayType);
        if (isLevelDisplay)
            c = inventory.GetStaticLevelItem(displayType);
        
        if (c != count) {
            if (c > 0) {
                itemDisplay.sprite = imageLibrary.GetFull(displayType);
                countText.text = "x" + c;
                if (isLevelDisplay)
                    backgroundDisplay.sprite = backgroundLibrary.Get(2);
                else {
                    if (inventory.GetLevelItem(displayType) > 0)
                        backgroundDisplay.sprite = backgroundLibrary.Get(1);
                    else
                        backgroundDisplay.sprite = backgroundLibrary.Get(0);
                }
            } else {
                countText.text = "";
                itemDisplay.sprite = imageLibrary.GetEmpty(displayType);
                backgroundDisplay.sprite = backgroundLibrary.Get(0);
            }

            count = c;
        }
    }

    public void SetInventory(PlayerInventory inv) {
        inventory = inv;
    }
}
