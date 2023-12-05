using Stock;

var filePath = "Phones.csv";
var lines = File.ReadAllLines(filePath);

var allProducts = new List<Product>();

foreach (var line in lines.Skip(1))
{
    var row = line.Replace(", ", ",");
    var data = line.Split(',');

    var newProduct = new Product();
    newProduct.Id = int.Parse(data[0]);
    newProduct.Name = data[1];
    newProduct.Quantity = int.Parse(data[2]);
    newProduct.Price = decimal.Parse(data[3].Replace(".", ","));
    allProducts.Add(newProduct);
}

// Console-ban kérjük be, hogy melyik termék árát akarjuk módosítani
Product product;
do
{
    Console.WriteLine("Melyik telefon árát szeretnénk módosítani? (Id)");
    if (int.TryParse(Console.ReadLine(), out int productId))
    {
        product = allProducts.FirstOrDefault(product => product.Id == productId);
        if (product is not null)
            break;

        Console.WriteLine("Telefon nem található");
        continue;
    }

    Console.WriteLine("Hibás formátumú Id-t adott meg");
} while (true);

Console.WriteLine("Kiválasztott telefon:");
Console.WriteLine($"{product.Id} {product.Name} {product.Quantity} {product.Price}");

Console.WriteLine("Do you wanna change the price of the phone: ");
string answer =  Console.ReadLine();

do {
    if (answer.Contains("yes"))
    {
        try
        {
            Console.WriteLine("Tell me the price (100-1000): ");
            int price = Convert.ToInt32(Console.ReadLine());
            if (price > 1000 || price < 100)
            {
                Console.WriteLine("Az ár nincs benne a megadott intervallumban (100;1000)!");
            }
            else
            {   
                product.exception = null;
                product.Price = price;
                Console.WriteLine($"Telefon a módosított árral: {product.Id}, {product.Name}, {product.Quantity}, {product.Price}");

                string writable_str = $"{product.Id}, {product.Name}, {product.Quantity}, {product.Price}";
                var textlist = new List<string>();
                textlist.Add(writable_str);
                File.WriteAllLines("Phones.csv", textlist);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Rossz adat!");
            product.exception = e.ToString();
        }
    }
    else
{
    Console.WriteLine("Alright then! Bye!");
    Environment.Exit(0);
}

} while (product.exception is not null);



