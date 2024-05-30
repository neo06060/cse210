using System;
using System.Collections.Generic;

class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;
    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }
    public double GetTotalCost()
    {
        return price * quantity;
    }
    public string GetPackingLabel()
    {
        return $"Product: {name}, ID: {productId}";
    }
}

class Address
{
    private string streetAddress;
    private string city;
    private string state;
    private string country;
    public Address(string streetAddress, string city, string state, string country)
    {
        this.streetAddress = streetAddress;
        this.city = city;
        this.state = state;
        this.country = country;
    }
    public bool IsInUSA()
    {
        return country == "USA";
    }
    public string GetFullAddress()
    {
        return $"{streetAddress}\n{city}, {state}\n{country}";
    }
}

class Customer
{
    private string name;
    private Address address;
    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }
    public bool IsInUSA()
    {
        return address.IsInUSA();
    }
    public string GetName()
    {
        return name;
    }
    public Address GetAddress()
    {
        return address;
    }
}

class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        this.products = new List<Product>();
    }
    public void AddProduct(Product product)
    {
        products.Add(product);
    }
    public double GetTotalCost()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.GetTotalCost();
        }
        if (customer.IsInUSA())
        {
            total += 5;
        }
        else
        {
            total += 35;
        }
        return total;
    }
    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in products)
        {
            label += product.GetPackingLabel() + "\n";
        }
        return label;
    }
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress().GetFullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Product product1 = new Product("Laptop", "P001", 10.0, 2);
        Product product2 = new Product("Mouse", "P002", 15.0, 3);
        Product product3 = new Product("Keyboard", "P003", 5.0, 5);
        Address address1 = new Address("123 Main St", "Los Angeles", "California", "USA");
        Address address2 = new Address("456 Another St", "Toronto", "Ontario", "Canada");
        Customer customer1 = new Customer("Rosa Melano", address1);
        Customer customer2 = new Customer("Elver Galarga", address2);
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        Order order2 = new Order(customer2);
        order2.AddProduct(product2);
        order2.AddProduct(product3);
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.GetTotalCost()}");
        Console.WriteLine();
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.GetTotalCost()}");
    }
}
