using System.Net.Http.Headers;
using складоваСистема;

List<Products> products = new List<Products>();

while (true)
{
    ShowMenu();
    string? inputOption = Console.ReadLine();
    if (!int.TryParse(inputOption, out int option))
    {
        Console.WriteLine("Невалиден избор!");
        continue;
    }

    switch (option)
    {
        case 0: return;

        case 1:
            Console.WriteLine("Name = ");
            string? name = Console.ReadLine() ?? "";

            Console.WriteLine("id=");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Невалидно ID.");
                break;
            }

            Console.WriteLine("Price = ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                Console.WriteLine("Невалидна цена.");
                break;
            }

            Console.WriteLine("Quantity = ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Невалидно количество.");
                break;
            }

            Console.WriteLine("Category =");
            string? category = Console.ReadLine() ?? "";

            Products one = new Products(id, name, price, quantity, category);
            products.Add(one);
            break;

        case 2:
            foreach (var p in products)
                Console.WriteLine(p.ToString());
            break;

        case 3:
            Console.WriteLine("Name =");
            string? name1 = Console.ReadLine();
            foreach (var p in products)
                if (p.Name == name1)
                    Console.WriteLine(p.ToString());
            break;

        case 4:
            Console.WriteLine("id = ");
            if (!int.TryParse(Console.ReadLine(), out int idForDelete))
            {
                Console.WriteLine("Невалидно ID.");
                break;
            }
            var productToRemove = products.FirstOrDefault(p => p.Id == idForDelete);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                Console.WriteLine("Продуктът беше изтрит успешно.");
            }
            else Console.WriteLine("Продукт с такова ID не беше намерен.");
            break;

        case 5:
            Console.WriteLine("id = ");
            if (!int.TryParse(Console.ReadLine(), out int ID))
            {
                Console.WriteLine("Невалидно ID.");
                break;
            }
            var productToEdit = products.FirstOrDefault(p => p.Id == ID);
            if (productToEdit != null)
            {
                Console.WriteLine($"Текущи данни: {productToEdit}");

                Console.Write("Ново име (или Enter за без промяна): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                    productToEdit.Name = newName;

                Console.Write("Нова цена (или Enter за без промяна): ");
                string newPriceInput = Console.ReadLine();
                if (double.TryParse(newPriceInput, out double newPrice))
                    productToEdit.Price = newPrice;

                Console.Write("Ново количество (или Enter за без промяна): ");
                string newQtyInput = Console.ReadLine();
                if (int.TryParse(newQtyInput, out int newQty))
                    productToEdit.Quantity = newQty;

                Console.Write("Нова категория (или Enter за без промяна): ");
                string newCategory = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newCategory))
                    productToEdit.Category = newCategory;

                Console.WriteLine("Продуктът беше успешно обновен.");
            }
            else
            {
                Console.WriteLine("Продукт с това ID не беше намерен.");
            }
            break;

        case 6:
            SaveToFile(products);
            break;

        case 7:
            products = LoadFromFile();
            break;

        default:
            Console.WriteLine("Невалидна опция.");
            break;
    }
}

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("====== МЕНЮ ЗА УПРАВЛЕНИЕ НА ПРОДУКТИ ======");
    Console.WriteLine("1. Добави нов продукт");
    Console.WriteLine("2. Покажи всички продукти");
    Console.WriteLine("3. Търси продукт по име");
    Console.WriteLine("4. Изтрий продукт");
    Console.WriteLine("5. Редактирай продукт");
    Console.WriteLine("6. Запиши във файл");
    Console.WriteLine("7. Зареди от файл");
    Console.WriteLine("0. Изход");
    Console.Write("Избери опция: ");
}

void SaveToFile(List<Products> products)
{
    List<string> lines = new();
    foreach (var product in products)
        lines.Add($"{product.Id},{product.Name},{product.Price},{product.Quantity},{product.Category}");

    File.WriteAllLines("products.txt", lines);
    Console.WriteLine("Продуктите са записани във файл.");
}

List<Products> LoadFromFile()
{
    List<Products> loaded = new();

    if (!File.Exists("products.txt"))
    {
        Console.WriteLine("Файлът не съществува.");
        return loaded;
    }

    string[] lines = File.ReadAllLines("products.txt");
    foreach (var line in lines)
    {
        string[] parts = line.Split(',');
        int id = int.Parse(parts[0]);
        string name = parts[1];
        double price = double.Parse(parts[2]);
        int quantity = int.Parse(parts[3]);
        string category = parts[4];

        Products p = new(id, name, price, quantity, category);
        loaded.Add(p);
    }

    Console.WriteLine("Продуктите са заредени от файл.");
    return loaded;
}
