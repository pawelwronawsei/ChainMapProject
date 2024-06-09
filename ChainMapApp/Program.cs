using System;
using System.Collections.Generic;
namespace ChainMapLib;

class Program
{
    static void Main()
    {
        var dict1 = new Dictionary<string, string>
        {
            { "a", "1" },
            { "b", "2" },
            { "c", "3" }
        };

        var dict2 = new Dictionary<string, string>
        {
            { "b", "22" },
            { "c", "33" },
            { "d", "44" }
        };

        var dict3 = new Dictionary<string, string>
        {
            { "c", "333" },
            { "d", "444" },
            { "e", "555" }
        };

        //TWORZĘ CHAINMAP
        var chainMap = new ChainMap<string, string>(dict1, dict2, dict3);
        
        //1. WYŚWIETLENIE WSZYSTKICH SŁOWNIKÓW
        Console.WriteLine("1. WYŚWIETLENIE WSZYSTKICH SŁOWNIKÓW");
        foreach (var d in chainMap)
        {
            Console.WriteLine((d.Key, d.Value));
        }
        Console.WriteLine();
        
        //2. ZMIANA WARTOŚCI a:
        Console.WriteLine("2. ZMIANA WARTOŚCI a:");
        chainMap["a"] = "1234";
        Console.WriteLine($"Wartość dla klucza a jest teraz równa {chainMap["a"]}\n");
        
        //3. ZMIANA WARTOŚCI DLA KLUCZA d W SŁOWNIKU GŁÓWNYM:
        Console.WriteLine("3. ZMIANA WARTOŚCI DLA KLUCZA d W SŁOWNIKU GŁÓWNYM:");
        chainMap.Add("d", "121212");
        Console.WriteLine($"Zmieniona wartość: {chainMap["d"]}\n");
        
        //4. UŻYCIE TryAdd DO DODANIA ELEMENTU Z KLUCZEM d (KTÓRY JUZ ZNAJDUJE SIĘ W SŁOWNIKU GŁÓWNYM):
        Console.WriteLine("4. UŻYCIE TryAdd DO DODANIA ELEMENTU Z KLUCZEM d (KTÓRY JUZ ZNAJDUJE SIĘ W SŁOWNIKU GŁÓWNYM):");
        chainMap.TryAdd("d", "121212");
        Console.WriteLine($"TryAdd zwraca {chainMap.TryAdd("d", "121212")} ponieważ ten klucz już znajduje się w słowniku\n");
        
        //5. POLICZENIE ILE W SUMIE JEST WARTOŚCI W CHAINMAP:
        Console.WriteLine("5. POLICZENIE ILE W SUMIE JEST WARTOŚCI W CHAINMAP:");
        Console.WriteLine(chainMap.Count + "\n");
        
        //6. SPRAWDZENIE CZY KLUCZ d ISTENIEJE W ChainMap
        Console.WriteLine("6. SPRAWDZENIE CZY KLUCZ d ISTENIEJE W ChainMap");
        Console.WriteLine($"Klucz 'd' istnieje w chainMap: {chainMap.ContainsKey("d")}\n");
        
        //7. SPRAWDZENIE CZY WARTOŚĆ ISTENIEJE W ChainMap
        Console.WriteLine("7. SPRAWDZENIE CZY WARTOŚĆ ISTENIEJE W ChainMap");
        Console.WriteLine($"Wartość 121212 istnieje w chainMap: {chainMap.ContainsValue("121212")}");
        Console.WriteLine($"Wartość 01012024 istnieje w chainMap: {chainMap.ContainsValue("01012024")}\n");
        
        //8. USUWANIE WPISU Z KLUCZEM a ZE SŁOWNIKA GŁÓWNEGO
        Console.WriteLine("8. USUWANIE WPISU Z KLUCZEM a ZE SŁOWNIKA GŁÓWNEGO");
        bool wasKeyRemoved = chainMap.Remove("a");
        Console.WriteLine($"Czy wpis z kluczem a został usunięcty: {wasKeyRemoved}");
        Console.WriteLine($"Czy chainMap posiada teraz klucz a: {chainMap.ContainsKey("a")}\n");
        
        //9. UŻYCIE TryGetValue, KTÓRY ZWRACA WARTOŚĆ JEŚLI ISTNIEJE W ChainMap, W PRZECIWNYM RAZIE ZWRACA FALSE
        Console.WriteLine("9. UŻYCIE TryGetValue, KTÓRY ZWRACA WARTOŚĆ JEŚLI KLUCZ ISTNIEJE W ChainMap, W PRZECIWNYM RAZIE ZWRACA FALSE");
        bool isKeyAInChainMap = chainMap.TryGetValue("a", out var valueA);
        bool isKeyBInChainMap = chainMap.TryGetValue("b", out var valueB);
        Console.WriteLine($"Czy klucz a ma wartość w chainmap: {isKeyAInChainMap}");
        Console.WriteLine($"Wartość dla klucza a: {valueA}");
        Console.WriteLine($"Czy klucz b ma wartość w chainmap: {isKeyBInChainMap}");
        Console.WriteLine($"Wartość dla klucza b: {valueB}\n");
        
        //10. UŻYCIE Keys I Values ABY UZYSKAĆ LISTY KLUCZY I WARTOŚCI
        Console.WriteLine("10. UŻYCIE Keys I Values ABY UZYSKAĆ LISTY KLUCZY I WARTOŚCI");
        var keys = chainMap.Keys;
        var values = chainMap.Values;
        Console.WriteLine($"Klucze: {string.Join(", ", keys)}");
        Console.WriteLine($"Wartości: {string.Join(", ", values)}\n"); 
        
        //11. DODANIE SŁOWNIKA DO LISTY SŁOWNIKÓW POŁĄCZONYCH
        Console.WriteLine("11. DODANIE SŁOWNIKA DO LISTY SŁOWNIKÓW POŁĄCZONYCH");
        var newDict = new Dictionary<string, string> { { "a", "555" } };
        chainMap.AddDictionary(newDict, 2);
        //Ponieważ chainmap przed dodanie tego słownika zawierał 3 słowniki, w tym pierwszy jest słownikiem głównym
        //to dodanie słownika na indexie 2 tak naprawdę dodaje słownik na indexie 3
        var addedDict = chainMap.Dictionaries[3];
        Console.WriteLine("Dodany słownik:");
        foreach (KeyValuePair<string, string> row in addedDict)
        {
            Console.WriteLine($"Klucz: {row.Key}, Wartość: {row.Value}");
        }
        Console.WriteLine();

        //12. USUNIĘCIE SŁOWNIKA Z LISTY SŁOWNIKÓW POŁĄCZONYCH
        Console.WriteLine("12. USUNIĘCIE SŁOWNIKA Z LISTY SŁOWNIKÓW POŁĄCZONYCH");
        Console.WriteLine($"Ilość słowników w słowniku połączonym przed usunięciem: {chainMap.Dictionaries.Count}");
        chainMap.RemoveDictionary(2);
        Console.WriteLine($"Ilość słowników w słowniku połączonym po usunięciu: {chainMap.Dictionaries.Count}\n");
        
        //13. ZAMIAST chainMap.Dictionaries.Count można też użyć chainMap.CountDictionaries
        Console.WriteLine("13. ZAMIAST chainMap.Dictionaries.Count można też użyć chainMap.CountDictionaries");
        Console.WriteLine($"Użycie chainMap.Dictionaries.Count: {chainMap.Dictionaries.Count}");
        Console.WriteLine($"Użycie chainMap.CountDictionaries: {chainMap.CountDictionaries}\n");
        
        //14. DOSTĘP DO SŁOWNIKÓW W SŁOWNIKU POŁĄCZONYM JEST ZA POMOCĄ chainMap.GetDictionaries
        Console.WriteLine("14. DOSTĘP DO SŁOWNIKÓW W SŁOWNIKU POŁĄCZONYM JEST ZA POMOCĄ chainMap.GetDictionaries");
        var dictionaries = chainMap.GetDictionaries();
        Console.WriteLine($"Ilość słowników w słowniku połączonym: {dictionaries.Count}\n");
        
        //15. DOSTĘP DO SŁOWNIKA GŁÓWNEGO
        Console.WriteLine("15. DOSTĘP DO SŁOWNIKA GŁÓWNEGO");
        var mainDict = chainMap.GetMainDictionary();
        Console.WriteLine("Wiersze słownika głównego:");
        foreach (KeyValuePair<string, string> row in mainDict)
        {
            Console.WriteLine($"Klucz: {row.Key}, Wartość: {row.Value}");
        }
        Console.WriteLine();
        
        //16. POŁĄCZENIE SŁOWNIKÓW Z chainMap
        Console.WriteLine("16. POŁĄCZENIE SŁOWNIKÓW Z chainMap");
        var mergedChainMap = chainMap.Merge();
        Console.WriteLine("Wiersze słownika połączonego:");
        foreach (KeyValuePair<string, string> row in mergedChainMap)
        {
            Console.WriteLine($"Klucz: {row.Key}, Wartość: {row.Value}");
        }
    }
}