using System;
using System.Collections;
using System.Collections.Generic;
using TonSdk.Connect;
using UnityEngine;

public class LabData : MonoBehaviour
{
    private string _wallet;
    [SerializeField] private TonConnectHandler tonConnectHandler;

    private void Awake()
    {
        TonConnectHandler.OnProviderStatusChanged += WalletCheck;

    }
    
    private void WalletCheck(Wallet wallet)
    {
        if(tonConnectHandler.tonConnect.IsConnected)
        {
            _wallet = wallet.Account.Address.ToString();
            Debug.Log(wallet.Account.Address);
        }
    }

    public string GetWalletAddress()
    {
        return _wallet;
    }

}
