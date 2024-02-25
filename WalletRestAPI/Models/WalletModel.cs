namespace WalletRestAPI.Models;

// See https://aka.ms/new-console-template for more information
using System.Dynamic;
using Microsoft.VisualBasic;

public abstract class Currency
{   
    public static string? code;
}

public class Dollar : Currency {
    public static new string code = "USD";
}

public class Euro : Currency{
    public static new string code = "EUR";
}

public class Wallet<T> where T:Currency
{   
    public int Id { get; set; }
    public string? Name { get; set; }
    private string _walletCode= typeof(T).GetField("code").GetValue(null).ToString();

    public string WalletCode { get; set; }
    //private string _walletType = nameof(T);

    private decimal _balance;
    public decimal Balance { 
        get{return _balance;} 
        set{_balance = Balance;} 
        }

    public Wallet(){_balance = 0m;}

    public void AddFunds(decimal funds,string currencyCode,decimal ratio = 1m){ // Convert first to recieve in another Currency.
    
        if (funds>0){ // Cant add negative values
            if (currencyCode != _walletCode) // If the wallet type is different (Dollar != Euro) 
            {
                var convertedFunds = ConvertFunds(funds,ratio);
                _balance += convertedFunds; // adds funds in the wallet currency type
            }
        else{ 
                _balance+=funds;
            }
        }
    }

    public decimal ConvertFunds(decimal funds,decimal ratio = 1m){
        funds *= ratio;
        return funds;
        //balance += funds;
    }

    public decimal ConvertBalance(decimal ratio=1){
        Console.WriteLine(_balance);
         _balance *= ratio;
         return _balance;
    }

    public Wallet<U> ConvertWallet<U>(decimal ratio) where U : Currency, new()
    {
        var convertedWallet = new Wallet<U>
        {
            _balance = ConvertBalance(ratio)
        };
        Console.WriteLine(convertedWallet.Balance);

        _balance = 0m; // Reset the balance after conversion

        return convertedWallet;
    }
}