using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingController : MonoBehaviour 
{
    public GameObject CraftingSlot1;
    public GameObject CraftingSlot2;
    public GameController gc;

    public GameObject objApple; //0
    public GameObject objBanana;
    public GameObject objBlueberry;
    public GameObject objCarrot;
    public GameObject objCherry;
    public GameObject objWatermelon;

    public GameObject objBlueDrug; //6
    public GameObject objGreenDrug;
    public GameObject objRedDrug;
    public GameObject objYellowDrug;

    public GameObject objGlass; //10
    public GameObject objIron;
    public GameObject objPlastic;

    public GameObject objPill; //13
    public GameObject objBluePill;
    public GameObject objGreenPill;
    public GameObject objRedPill;
    public GameObject objYellowPill;

    public GameObject objPotion; //18
    public GameObject objBluePotion;
    public GameObject objGreenPotion;
    public GameObject objRedPotion;
    public GameObject objYellowPotion;

    public GameObject objSyringe; //23
    public GameObject objBlueSyringe;
    public GameObject objGreenSyringe;
    public GameObject objRedSyringe;
    public GameObject objYellowSyringe;

    string obj1, obj2;
    private bool[] allowedItems;

    // Start is called before the first frame update
    void Start()
    {
        gc = FindObjectOfType<GameController>();
        allowedItems = gc.allowedItems;
    }

    void CraftObject(GameObject go)
    {
        Destroy(CraftingSlot1.GetComponent<CraftingSlot>().inSlot);
        Destroy(CraftingSlot2.GetComponent<CraftingSlot>().inSlot);
        CraftingSlot1.GetComponent<CraftingSlot>().inSlot = null;
        CraftingSlot2.GetComponent<CraftingSlot>().inSlot = null;
        Vector3 pos = gameObject.transform.position;
        //pos.x += 0.08f;
        //pos.y -= 0.19f;
        Instantiate(go, pos, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        if (CraftingSlot1.GetComponent<CraftingSlot>().inSlot != null &&
            CraftingSlot2.GetComponent<CraftingSlot>().inSlot != null)
        {
            obj1 = CraftingSlot1.GetComponent<CraftingSlot>().inSlot.name;
            obj2 = CraftingSlot2.GetComponent<CraftingSlot>().inSlot.name;
            if (string.Compare(obj1, obj2) == 1)
            {
                string temp = obj2;
                obj2 = obj1;
                obj1 = temp;
            }

            // ------  RAW DRUGS  ------
            if (allowedItems[6] && obj1.StartsWith("Blueberry") && obj2.StartsWith("Watermelon"))
            {
                CraftObject(objBlueDrug);
            }
            if (allowedItems[7] && obj1.StartsWith("Apple") && obj2.StartsWith("Watermelon"))
            {
                CraftObject(objGreenDrug);
            }
            if (allowedItems[8] && obj1.StartsWith("Carrot") && obj2.StartsWith("Cherry"))
            {
                CraftObject(objRedDrug);
            }
            if (allowedItems[9] && obj1.StartsWith("Banana") && obj2.StartsWith("Carrot"))
            {
                CraftObject(objYellowDrug);
            }

            // ----- RAW DELIVERIES -----
            if (allowedItems[18] && obj1.StartsWith("Glass") && obj2.StartsWith("Plastic"))
            {
                CraftObject(objPotion);
            }
            if (allowedItems[23] && obj1.StartsWith("Glass") && obj2.StartsWith("Iron"))
            {
                CraftObject(objSyringe);
            }
            if (allowedItems[13] && obj1.StartsWith("Plastic") && obj2.StartsWith("Plastic"))
            {
                CraftObject(objPill);
            }

            // ----- FINAL SYRINGES -----

            if (allowedItems[24] && obj1.StartsWith("BlueDrug") && obj2.StartsWith("Syringe"))
            {
                CraftObject(objBlueSyringe);
            }
            if (allowedItems[25] && obj1.StartsWith("GreenDrug") && obj2.StartsWith("Syringe"))
            {
                CraftObject(objGreenSyringe);
            }
            if (allowedItems[26] && obj1.StartsWith("RedDrug") && obj2.StartsWith("Syringe"))
            {
                CraftObject(objRedSyringe);
            }
            if (allowedItems[27] && obj1.StartsWith("Syringe") && obj2.StartsWith("YellowDrug"))
            {
                CraftObject(objYellowSyringe);
            }

            // ----- FINAL PILLS -----

            
            if (allowedItems[14] && obj1.StartsWith("BlueDrug") && obj2.StartsWith("Pill"))
            {
                CraftObject(objBluePill);
            }
            if (allowedItems[15] && obj1.StartsWith("GreenDrug") && obj2.StartsWith("Pill"))
            {
                CraftObject(objGreenPill);
            }
            if (allowedItems[16] && obj1.StartsWith("Pill") && obj2.StartsWith("RedDrug"))
            {
                CraftObject(objRedPill);
            }
            if (allowedItems[17] && obj1.StartsWith("Pill") && obj2.StartsWith("YellowDrug"))
            {
                CraftObject(objYellowPill);
            }

            // ----- FINAL POTIONS -----

            if (allowedItems[18] && obj1.StartsWith("BlueDrug") && obj2.StartsWith("Potion"))
            {
                CraftObject(objBluePotion);
            }
            if (allowedItems[19] && obj1.StartsWith("GreenDrug") && obj2.StartsWith("Potion"))
            {
                CraftObject(objGreenPotion);
            }
            if (allowedItems[20] && obj1.StartsWith("Potion") && obj2.StartsWith("RedDrug"))
            {
                CraftObject(objRedPotion);
            }
            if (allowedItems[21] && obj1.StartsWith("Potion") && obj2.StartsWith("YellowDrug"))
            {
                CraftObject(objYellowPotion);
            }
        }
        
    }
}
