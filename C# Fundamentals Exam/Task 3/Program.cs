Dictionary<string, List<string>> stores = new Dictionary<string, List<string>>();
List<string> storeItems = new List<string>();
List<string> importantItems = new List<string>();

string input = Console.ReadLine();

while (input != "Go Shopping")
{
    string[] splittedInput = input.Split("->");

    string action = splittedInput[0];
    string store = splittedInput[1];

    switch (action)
    {
        case "Add":
            {
                string[] items = splittedInput[2].Split(',');

                if (!stores.ContainsKey(store))
                {
                    stores[store] = new List<string>();

                }
                foreach (var item in items)
                {
                    if (!storeItems.Contains(item))
                    {
                        stores[store].Add(item);
                        storeItems.Add(item);
                    }

                }
                break;

            }

        case "Important":
            {
                string item = splittedInput[2];

                if (!storeItems.Contains(item))
                {
                    if (!stores.ContainsKey(store))
                    {
                        stores[store] = new List<string>();

                    }

                    stores[store].Insert(0, item);
                    storeItems.Add(item);
                    importantItems.Add(item);


                }
                break;

            }

        case "Remove":
            {
                if (stores.ContainsKey(store))
                {
                    bool hasImportantItem = stores[store].Intersect(importantItems).Any();

                    if (!hasImportantItem)
                    {

                        foreach (var item in stores[store])
                        {
                            storeItems.Remove(item);
                           
                        }

                        stores.Remove(store);

                    }
                }
                break;
            }

    }
    input = Console.ReadLine();
}

foreach (var store in stores)
{
    Console.WriteLine($"{store.Key}:");

    foreach (var item in store.Value)
    {
        Console.WriteLine($"  - {item}");
    }
}