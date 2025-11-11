// See https://aka.ms/new-console-template for more information
using System;
using System.Collections;
using System.Collections.Generic;

Console.WriteLine("Hello, World!");

Console.WriteLine("---DELEGATES---");
Delegates();
Console.WriteLine("---EVENTS---");
Events();
Console.WriteLine("---TRY & EXCEPTION---");
TryAndException();
Console.WriteLine("---ENUMERATORS---");
Enumerators();

void Delegates()
{
    int Square(int x) => x * x;
    Transformer t = Square;

    Console.WriteLine(t(5));

}

void Events()
{
    Console.WriteLine("--light switch event--");
    LightSwitch mySwitch = new LightSwitch();
    Light myLight = new Light();

    // Subscribe ke event
    mySwitch.OnSwitchFlipped += (sender, e) =>
    {
        if (e.IsOn)
            myLight.TurnOn();
        else
            myLight.TurnOff();
    };

    mySwitch.FlipSwitch(); // Output: Lampu menyala!
    mySwitch.FlipSwitch(); // Output: Lampu mati!

    //-----------------------------------
    Console.WriteLine("--order system event--");
    // Buat sistem pesanan
    OrderSystem orderSystem = new OrderSystem();

    // Buat service-service
    EmailService emailService = new EmailService();
    SMSService smsService = new SMSService();
    InventorySystem inventorySystem = new InventorySystem();

    // Daftarkan service ke event
    // (Subscribe: mereka akan mendengarkan event)
    orderSystem.OnOrderCreated += emailService.SendConfirmationEmail;
    orderSystem.OnOrderCreated += smsService.SendNotificationSMS;
    orderSystem.OnOrderCreated += inventorySystem.UpdateStock;

    // Buat pesanan - ini akan trigger event
    orderSystem.CreateOrder("ORD001", "Budi", 150000);

    Console.WriteLine("---\n");

    orderSystem.CreateOrder("ORD002", "Ani", 250000);


    // ----------------------------
    Console.WriteLine("---stock event---");
    var stock = new Stock("ABC");
    stock.OnPriceChanged += (oldP, newP) => Console.WriteLine($"Price changed from {oldP} to {newP}");
    stock.Price = 100;
    stock.Price = 200;
}

void TryAndException()
{
    try
    {
        Console.WriteLine("1. Di Try");
        throw new Exception("Error!");
        // Console.WriteLine("2. (Tidak dijalankan)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"3. Di Catch {ex}");
    }
    finally
    {
        Console.WriteLine("4. Di Finally (selalu jalan)");
    }
    Console.WriteLine("5. Setelah blok try-catch-finally");
}

void Enumerators()
{
    ArrayList numbers = new ArrayList { 10, 23, 235, 32, 74 };
    IEnumerator enumerator = numbers.GetEnumerator();

    while (enumerator.MoveNext())
    {
        Console.WriteLine($"Nilai: {enumerator.Current}");
    }

    foreach (var num in numbers)
    {
        Console.WriteLine($"Nilai2: {num}");
    }

    var collection = new MyCollection();

    foreach (var item in collection)
    {
        Console.WriteLine(item);
    }
}

class MyCollection: IEnumerable<int>
{
    private int[] data = [10, 20, 44, 56, 74];

    public IEnumerator<int> GetEnumerator()
    {
        for (int i = 0; i < data.Length; i++) yield return data[i];
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
public delegate void PriceChangedHandler(decimal oldPrice, decimal newPrice);
public class Stock
{
    string symbol;
    decimal price;

    public Stock(string symbol) => this.symbol = symbol;

    public event PriceChangedHandler OnPriceChanged;

    public decimal Price
    {
        get => price;
        set
        {
            if (price == value) return; // kalau tidak berubah, keluar

            decimal oldPrice = price;
            price = value;

            // panggil event (jika ada subscriber)
            if (OnPriceChanged != null)
                OnPriceChanged(oldPrice, price);
        }
    }

}

// 1. Custom event args
public class OrderEventArgs : EventArgs
{
    public string? OrderId { get; set; }
    public string? CustomerName { get; set; }
    public decimal Amount { get; set; }
}

// Class yang memicu event (publisher)
public class OrderSystem
{
    // public event EventHandler<OrderEventArgs> OnOrderCreated;

    // Custom Delegate - definisikan signature sendiri
    public delegate void OrderCreatedEventHandler(object sender, OrderEventArgs e);

    // Event menggunakan custom delegate
    public event OrderCreatedEventHandler OnOrderCreated;
    
    public void CreateOrder(string orderId, string customerName, decimal amount)
    {
        Console.WriteLine($"✓ Pesanan {orderId} dibuat!\n");

        OnOrderCreated?.Invoke(this, new OrderEventArgs
        {
            OrderId = orderId,
            CustomerName = customerName,
            Amount = amount
        });
    }
}

// 3. Buat class yang mendengarkan event (subscriber)
public class EmailService
{
    public void SendConfirmationEmail(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"📧 Email Service: Mengirim email konfirmasi ke {e.CustomerName}");
        Console.WriteLine($"   Pesanan {e.OrderId} sebesar Rp {e.Amount}\n");
    }
}

public class SMSService
{
    public void SendNotificationSMS(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"📱 SMS Service: Mengirim SMS ke {e.CustomerName}");
        Console.WriteLine($"   Pesanan Anda telah diterima (ID: {e.OrderId})\n");
    }
}

public class InventorySystem
{
    public void UpdateStock(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"📦 Inventory System: Mengupdate stok untuk pesanan {e.OrderId}\n");
    }
}


// 1. Custom EventArgs
public class LightSwithcEventArgs : EventArgs
{
    public bool IsOn { get; set; }
}

// 2. Class yang menerbitkan event
public class LightSwitch
{
    // Delegate
    public delegate void SwitchFlippedEventHandler(object sender, LightSwithcEventArgs e);

    // Event
    public event SwitchFlippedEventHandler OnSwitchFlipped;

    private bool _isOn = false;

    public void FlipSwitch()
    {
        _isOn = !_isOn;

        // Trigger event
        OnSwitchFlipped?.Invoke(this, new LightSwithcEventArgs { IsOn = _isOn });
    }
}

public class Light
{
    public void TurnOn()
    {
        Console.WriteLine("Lampu menyala");
    }
    public void TurnOff()
    {
        Console.WriteLine("Lampu mati");
    }
}

delegate int Transformer(int x);
