namespace Core.Purchase
{
    public class PurchasableItem : IPurchasableItem
    {
        public PurchasableItem(PurchasableItemView view)
        {
            View = view;
            PurchaseItemId = view.Config.Id;
            Price = view.Config.Price;

            View.PlayerDetector.PlayerWentInside += OnPlayerWentInside;
            View.PlayerDetector.PlayerWentOutside += OnPlayerWentOutside;
        }
        
        public bool IsPlayerInsideDetector { get; private set; }

        private void OnPlayerWentOutside()
        {
            IsPlayerInsideDetector = false;
        }

        private void OnPlayerWentInside()
        {
            IsPlayerInsideDetector = true;
        }

        public string PurchaseItemId { get; }
        public int Price { get; }
        public PurchasableItemView View { get; }

        public void PurchaseImmediately()
        {
            foreach (var animatedAppearObject in View.ObjectsToDisable)
            {
                animatedAppearObject.Hide();
            }
            
            foreach (var animatedAppearObject in View.ObjectsToEnable)
            {
                animatedAppearObject.AppearImmediate();
            }
        }

        public void Purchase()
        {
            foreach (var animatedAppearObject in View.ObjectsToDisable)
            {
                animatedAppearObject.Hide();
            }
            
            foreach (var animatedAppearObject in View.ObjectsToEnable)
            {
                animatedAppearObject.AppearSmooth();
            }
        }

        public void SetMoneyToSpend(int amount)
        {
            if (amount <= 0)
            {
                View.gameObject.SetActive(false);
                return;
            }
            View.PriceText.SetText("{0:0}", amount);
        }

        public void OnDispose()
        {
            View.PlayerDetector.PlayerWentInside -= OnPlayerWentInside;
            View.PlayerDetector.PlayerWentOutside -= OnPlayerWentOutside;
        }
    }
}