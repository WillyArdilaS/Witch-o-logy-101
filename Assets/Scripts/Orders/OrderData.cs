using System.Collections;
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
    [SerializeField] private RecipeData requiredRecipe;
    [SerializeField] private BottleData.BottleType requiredBottle;

    // === Properties ===
    public string OrderID => orderID;
    public Sprite OrderImg => orderImg;
    public float LifeTime => lifeTime;
    public OrderState State { get => state; set => state = value; }
    public RecipeData RequiredRecipe => requiredRecipe;
    public BottleData.BottleType RequiredBottle => requiredBottle;

    // === Constructor ===
    public OrderData(string orderID, Sprite orderImg, float lifeTime, RecipeData requiredRecipe, BottleData.BottleType requiredBottle)
    {
        this.orderID = orderID;
        this.orderImg = orderImg;
        this.lifeTime = lifeTime;
        state = OrderState.Pending;
        this.requiredRecipe = requiredRecipe;
        this.requiredBottle = requiredBottle;
    }

    // === Order expiration method ===
    public IEnumerator LifeTimer()
    {
        while (lifeTime > 0 && state == OrderState.Pending)
        {
            lifeTime--;
            yield return new WaitForSeconds(1);
        }

        if (lifeTime == 0)
        {
            state = OrderState.Failed;
            Debug.Log($"La orden {OrderID} no fue entregada");
        }
    }
}