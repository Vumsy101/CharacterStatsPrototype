using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayerStats : MonoBehaviour
{
    public CharacterStat Health;
    public CharacterStat Strength;
    public List<Item> Loot = new List<Item>();
    public ItemDatabase ItemDatabase;
    public Player player => GetComponent<Player>();
    public int ItemCount = 0;

    

    private void Start()
    {
        Health.BaseValue = 50;
        Strength.BaseValue = 10;

        player.UpdateStats();

        AddFlatModifier(Health, 0);
        AddFlatModifier(Strength, 0);
    }
    public void GiveItem(int id)
    {
        Item itemToAdd = ItemDatabase.GetItem(id);
        Loot.Add(itemToAdd);
        Debug.Log("Got " + itemToAdd.title + itemToAdd.description);
        ItemCount += 1;
        AddFlatModifier(itemToAdd.statType, itemToAdd.statValue);
        Item itemCheck = CheckforItems(id);
    }
    private void UpdateItemInfo()
    {
        Debug.Log("I have " + ItemCount + " items and " + Health.Value + "Health and " + Strength.Value + "Strength");
    }
    public Item CheckforItems(int id)
    {
        return Loot.Find(item => item.id == id);
    }
    
    
    public void AddFlatModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.Flat, this));
        UpdateItemInfo();
        player.UpdateStats();
    }
    public void AddPercentModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.Percent, this));
        UpdateItemInfo();
        player.UpdateStats();
    }
    public void AddPercentMultModifier(CharacterStat statType, float statValue)
    {
        statType.AddModifier(new StatModifier(statValue, StatModType.PercentMult, this));
        UpdateItemInfo();
        player.UpdateStats();
    }

    public void RemoveItem(int id)
    {
        Item item = CheckforItems(id);
        if (item != null)
        {
            RemoveModifier();
            Loot.Remove(item);
            ItemCount -= 1;
            player.UpdateStats();
        }
    }
    public void RemoveModifier()
    {
        {
            Strength.RemoveAllModifiersFromSource(this);
        }
    }
}
