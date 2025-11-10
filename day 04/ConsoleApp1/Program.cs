// See https://aka.ms/new-console-template for more information
using System;

IPaymentStrategy bankPayment = PaymentFactory.CreatePayment("bank", "BCA", "12345678");
bankPayment.ProsesPembayaran(25000);

IPaymentStrategy ovoPayment = PaymentFactory.CreatePayment("ovo", "081234567");
ovoPayment.ProsesPembayaran(100000);

public interface IPaymentStrategy
{
    string NamaMethode { get; }
    decimal HitungBiayaAdmin(decimal jumlah);
    bool ProsesPembayaran(decimal jumlah);
}

public class TransferBankPayment: IPaymentStrategy
{
    public string NamaMethode => "Transfer Bank";
    public string NamaBank { get; set; }
    public string NomorRekening { get; set; }

    public TransferBankPayment(string namaBank, string nomorRekening)
    {
        NamaBank = namaBank;
        NomorRekening = nomorRekening;
    }

    public decimal HitungBiayaAdmin(decimal jumlah)
    {
        return 5000;
    }

    public bool ProsesPembayaran(decimal jumlah)
    {
        Random rand = new Random();
        bool success = rand.Next(100) < 90;
        Console.WriteLine("Transfer Bank Payment");
        if (success)
        {
            Console.WriteLine($"Transfer berhasil.");
        }
        else
        {
            Console.WriteLine($"Transfer gagal.");
        }
        
        return success;
    }

}

public class EWalletPayment : IPaymentStrategy
{
    public string NamaMethode { get; private set; }
    public string NomorHP { get; set; }

    public EWalletPayment(string jenisEWallet, string nomorHP)
    {
        NamaMethode = jenisEWallet; //OVO, Dana, Gopay
        NomorHP = nomorHP;
    }

    public decimal HitungBiayaAdmin(decimal jumlah)
    {
        return 0;
    }

    public bool ProsesPembayaran(decimal jumlah)
    {
        Random rand = new Random();
        bool success = rand.Next(100) < 90;
        Console.WriteLine($"Ewallet Payment {NamaMethode}");
        if (success)
        {
            Console.WriteLine($"Transfer berhasil.");
        }
        else
        {
            Console.WriteLine($"Transfer gagal.");
        }

        return success;
    }
}

public class KartuKreditPayment : IPaymentStrategy
{
    public string NamaMethode => "Kartu Kredit";
    public string NomorKartu { get; set; }
    public string NamaPemegang { get; set; }

    public KartuKreditPayment(string nomorKartu, string namaPemegang)
    {
        NomorKartu = nomorKartu;
        NamaPemegang = namaPemegang;
    }

    public decimal HitungBiayaAdmin(decimal jumlah)
    {
        return jumlah * 0.02m;
    }

    public bool ProsesPembayaran(decimal jumlah)
    {
        Random rand = new Random();
        bool success = rand.Next(100) < 90;
        Console.WriteLine($"Kartu Kredit Payment");
        if (success)
        {
            Console.WriteLine($"Transfer berhasil.");
        }
        else
        {
            Console.WriteLine($"Transfer gagal.");
        }

        return success;
    }
}

public class PaymentFactory
{
    public static IPaymentStrategy CreatePayment(string tipe, params string[] par)
    {
        switch (tipe.ToLower())
        {
            case "bank":
                if (par.Length >= 2)
                {
                    return new TransferBankPayment(par[0], par[1]);
                }
                break;
            case "gopay":
            case "ovo":
            case "dana":
                if (par.Length >= 1)
                {
                    return new EWalletPayment(tipe.ToUpper(), par[0]);
                }
                break;
            case "kredit":
                if (par.Length >= 2)
                {
                    return new KartuKreditPayment(par[0], par[1]);
                }
                break;
        }
        throw new ArgumentException($"Tipe pembayaran '{tipe}' tidak valid");
    }
}