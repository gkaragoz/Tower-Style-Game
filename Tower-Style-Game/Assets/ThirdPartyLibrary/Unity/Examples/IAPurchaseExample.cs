using Library.Purchasing;
using UnityEngine;

public class IAPurchaseExample : MonoBehaviour {
    private IAPurchase purchase;

    private void Awake() {
        purchase = new IAPurchase();

        InitializeIAPServices();
    }

    // Initialize IAP Services
    public void InitializeIAPServices() {
        purchase.InitializeServices();
    }

    // Buy NO AD Product
    public void BuyNoADProduct() {
        purchase.BuyNoAddProduct();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            BuyNoADProduct();
        }
    }

}
