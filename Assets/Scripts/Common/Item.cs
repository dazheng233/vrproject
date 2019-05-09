using UnityEngine;

public class Item
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Sprite Icon { get; private set; }

    public Item(int id, string name, string description)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        LoadIcon();
    }

    public void LoadIcon()
    {
        Icon=Resources.Load<Sprite>("Icon/"+Name);
    }
}
