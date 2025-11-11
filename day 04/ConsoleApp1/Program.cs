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

public interface IPaymentObserver
{
    void Update(string transaksiId, bool success, decimal jumlah, string methode);
}

public class CustomerNotification : IPaymentObserver
{
    private string namaCustomer;
    private string email;
    public CustomerNotification(string namaCustomer, string email)
    {
        this.namaCustomer = namaCustomer;
        this.email = email;
    }

    public void Update(string transaksiId, bool success, decimal jumlah, string metode)
    {
        Console.WriteLine("\n📧 [EMAIL CUSTOMER]");
        Console.WriteLine($"Kepada: {namaCustomer} ({email})");

        if (success)
        {
            Console.WriteLine($"✓ Pembayaran Anda sebesar Rp {jumlah:N0} via {metode} telah berhasil!");
            Console.WriteLine($"ID Transaksi: {transaksiId}");
            Console.WriteLine("Pesanan Anda sedang diproses.");
        }
        else
        {
            Console.WriteLine($"✗ Pembayaran Anda sebesar Rp {jumlah:N0} via {metode} gagal.");
            Console.WriteLine("Silakan coba metode pembayaran lain atau hubungi CS.");
        }
    }
}

public class AdminNotification : IPaymentObserver
{
    public void Update(string transaksiId, bool success, decimal jumlah, string metode)
    {
        Console.WriteLine("\n🔔 [NOTIFIKASI ADMIN]");
        Console.WriteLine($"Transaksi ID: {transaksiId}");
        Console.WriteLine($"Status: {(success ? "BERHASIL ✓" : "GAGAL ✗")}");
        Console.WriteLine($"Metode: {metode}");
        Console.WriteLine($"Jumlah: Rp {jumlah:N0}");
        Console.WriteLine($"Waktu: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
    }
}

public class LogSystem : IPaymentObserver
{
    private List<string> logs = new List<string>();
    public void Update(string transaksiId, bool success, decimal jumlah, string metode)
    {
        string status = success ? "SUCCESS" : "FAILED";
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {transaksiId} | {metode} | Rp {jumlah:N0} | {status}";
        logs.Add(logEntry);

        Console.WriteLine("\n📝 [LOG SYSTEM]");
        Console.WriteLine(logEntry);
    }

    public void TampilkanSemuaLog()
    {
        Console.WriteLine("\n===== HISTORY LOG TRANSAKSI =====");
        foreach (var log in logs)
        {
            Console.WriteLine(log);
        }
        Console.WriteLine("=================================\n");
    }
}

public class PaymentProcessor
{
    private List<IPaymentObserver> observers = new List<IPaymentObserver>();
    private IPaymentStrategy paymentStrategy;
}