namespace Core.Purchase
{
    public interface IPurchasableItem
    {
        string PurchaseItemId { get; }
        int Price { get; }
        PurchasableItemView View { get; }
        bool IsPlayerInsideDetector { get; }
        void PurchaseImmediately();
        void Purchase();
        void OnDispose();
        void SetMoneyToSpend(int amount);
    }
}