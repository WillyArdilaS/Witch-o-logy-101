using UnityEngine;

[System.Serializable]
public class OrderData
{
    // === Order data ===
    public enum OrderState { Pending, Delivered, Failed }
    [SerializeField] private string orderID;
    [SerializeField] private Sprite orderImg;
    [SerializeField] private float lifeTime;
    [SerializeField] private OrderState state;
    [SerializeField] private RecipeData recipe;
    [SerializeField] private BottleData.BottleType requiredBottle;

    // === Properties ===
    public string OrderID => orderID;
    public Sprite OrderImg => orderImg;
    public float LifeTime => lifeTime;
    public OrderState State { get => state; set => state = value; }
    public RecipeData Recipe => recipe;
    public BottleData.BottleType RequiredBottle => requiredBottle;

    // === Constructor ===
    public OrderData(string orderID, Sprite orderImg, RecipeData recipe, BottleData.BottleType requiredBottle)
    {
        this.orderID = orderID;
        this.orderImg = orderImg;
        lifeTime = 15f;
        state = OrderState.Pending;
        this.recipe = recipe;
        this.requiredBottle = requiredBottle;
    }
}