using WalletRestAPI.Models;

namespace WalletRestAPI.Services;

public static class WalletService
{
    static List<Wallet<Dollar>> Wallets { get; }
    static int nextId = 3;
    static WalletService()
    {
        Wallets = new List<Wallet<Dollar>>
        {
            new Wallet<Dollar> { Id = 1, Name = "John",WalletCode="USD",Balance=0}, // Initial values for tests
            new Wallet<Dollar> { Id = 2, Name = "Fred",WalletCode="USD",Balance=0}
        };
    }

    public static List<Wallet<Dollar>> GetAll() => Wallets;

    public static Wallet<Dollar>? Get(int id) => Wallets.FirstOrDefault(p => p.Id == id);

    public static void Add(Wallet<Dollar> wallet)
    {
        wallet.Id = nextId++;
        Wallets.Add(wallet);
    }

    public static void Delete(int id)
    {
        var wallet = Get(id);
        if(wallet is null)
            return;

        Wallets.Remove(wallet);
    }

    public static void Update(Wallet<Dollar> wallet)
    {
        var index = Wallets.FindIndex(p => p.Id == wallet.Id);
        if(index == -1)
            return;

        Wallets[index] = wallet;
    }
}