using System.Collections.ObjectModel;

namespace ChainMapLib;

public class Tests
{
    [Test]
    public void IndexerGetTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" },{ "c", "33" },{ "d", "44" }};
        var dict3 = new Dictionary<string, string>{ { "c", "333" },{ "d", "444" },{ "e", "555" }};
        
        var chainMap = new ChainMap<string, string>(dict1, dict2, dict3);
        
        //Act
        string firstIndexToTest = chainMap["a"]; //"1"
        string secondIndexToTest = chainMap["b"]; //"2"
        string thirdIndexToTest = chainMap["d"]; //"44"
        string forthIndexToTest = chainMap["e"]; //"555"

        var tupleFromIndexes = (firstIndexToTest, secondIndexToTest, thirdIndexToTest, forthIndexToTest);
        
        //Assert
        Assert.AreEqual(tupleFromIndexes, ("1", "2", "44", "555"));
    }
    
    [Test]
    public void IndexerSetTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" },{ "c", "3" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" },{ "c", "33" },{ "d", "44" }};
        var dict3 = new Dictionary<string, string>{ { "c", "333" },{ "d", "444" },{ "e", "555" }};
        
        var chainMap = new ChainMap<string, string>(dict1, dict2, dict3);
        
        //Act
        chainMap["a"] = "100";
        chainMap["d"] = "25";

        var tupleFromIndexes = (chainMap["a"], chainMap["d"]);
        
        //Assert
        Assert.AreEqual(tupleFromIndexes, ("100", "25"));
    }
    
    [Test]
    public void EnumeratorTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        var expectedResult = new List<(string, string)>{("a", "1"), ("b", "2"), ("b", "22")};
        
        //Act
        var result = new List<(string, string)> { };

        foreach (var d in chainMap)
        {
            result.Add((d.Key, d.Value));
        }
        
        //Assert
        Assert.AreEqual(result, expectedResult);
    }
    
    [Test]
    public void AddTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" }, {"c", "51"}};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        chainMap.Add("c", "1234");
        var result = chainMap["c"];
        
        //Assert
        Assert.AreEqual(result, "1234");
    }
    
    [Test]
    public void TryAddTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" }, {"c", "51"}};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        bool resultTrue = chainMap.TryAdd("c", "1234");
        bool resultFalse = chainMap.TryAdd("b", "2345");
        
        //Assert
        Assert.IsTrue(resultTrue);
        Assert.IsFalse(resultFalse);
    }
    
    [Test]
    public void ClearTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" }, {"c", "51"}};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        chainMap.Clear();
        
        //Assert
        Assert.IsFalse(chainMap.ContainsKey("a"));
    }
    
    [Test]
    public void CountTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" }, {"c", "51"}};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var result = chainMap.Count();
        
        //Assert
        Assert.IsTrue(result == 4);
    }
    
    [Test]
    public void ContainsKeyTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        
        //Assert
        Assert.IsTrue(chainMap.ContainsKey("a") && chainMap.ContainsKey("b") && chainMap.ContainsKey("c"));
    }
    
    [Test]
    public void ContainsValueTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        
        //Assert
        Assert.IsTrue(chainMap.ContainsValue("1") && chainMap.ContainsValue("2") && chainMap.ContainsValue("22"));
    }
    
    [Test]
    public void RemoveTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var result = chainMap.Remove("a");
        
        //Assert
        Assert.IsTrue(result && chainMap.ContainsKey("a") == false);
    }
    
    [Test]
    public void TryGetValueTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var result = chainMap["c"];
        
        //Assert
        Assert.AreEqual(result, "22");
    }
    
    [Test]
    public void KeysTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var result = chainMap.Keys;
        
        //Assert
        Assert.AreEqual(result, new List<string>(){"a", "b", "c"});
    }
    
    [Test]
    public void ValuesTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var result = chainMap.Values;
        
        //Assert
        Assert.AreEqual(result, new List<string>(){"1", "2", "22"});
    }
    
    [Test]
    public void AddDictionaryTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var testDictionary = new Dictionary<string, string>() { { "a", "122" }, { "j", "1234" } };
        
        chainMap.AddDictionary(testDictionary, 5);
        
        //Assert
        Assert.AreEqual(chainMap.Dictionaries[1], testDictionary);
    }
    
    [Test]
    public void RemoveDictionaryTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var testDictionary = new Dictionary<string, string>() { { "a", "122" }, { "j", "1234" } };
        
        chainMap.AddDictionary(testDictionary, 5);
        chainMap.RemoveDictionary(0);
        
        //Assert
        Assert.AreNotEqual(chainMap.Dictionaries[1], testDictionary);
    }
    
    [Test]
    public void ClearDictionariesTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        chainMap.ClearDictionaries();
        
        //Assert
        Assert.IsTrue(chainMap.Dictionaries.Count == 0);
    }
    
    [Test]
    public void CountDictionariesTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var dict3 = new Dictionary<string, string>{{ "d", "52" }};
        var dict4 = new Dictionary<string, string>{{ "e", "22123" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2, dict3, dict4);
        
        //Act
        var result = chainMap.CountDictionaries();
        
        //Assert
        Assert.AreEqual(result, 4);
    }
    
    [Test]
    public void GetDictionaryTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var result = chainMap.GetDictionary(0);
        
        //Assert
        Assert.AreEqual(result, new ReadOnlyDictionary<string, string>(dict2));
    }
    
    [Test]
    public void GetMainDictionaryTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "c", "22" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2);
        
        //Act
        var result = chainMap.GetMainDictionary();
        
        //Assert
        Assert.AreEqual(result, dict1);
    }
    
    [Test]
    public void MergeTest()
    {
        //Arrange
        var dict1 = new Dictionary<string, string>{{ "a", "1" },{ "b", "2" }};
        var dict2 = new Dictionary<string, string>{{ "b", "22" }, {"c", "51"}};
        var dict3 = new Dictionary<string, string>{{ "d", "1234" }};
        var dict4 = new Dictionary<string, string>{{ "d", "50505" }, { "f", "1212" }};
        var chainMap = new ChainMap<string, string>(dict1, dict2, dict3, dict4);
        
        //Act
        var result = chainMap.Merge();
        var expectedResult = new Dictionary<string, string>()
        {
            {"a", "1"},
            {"b", "2"},
            {"c", "51"},
            {"d", "1234"},
            {"f", "1212"}
        };
        
        //Assert
        Assert.AreEqual(expectedResult, result);
    }
}
