using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChainMapLib;

public struct ChainMap<TKey, TValue> : IDictionary<TKey, TValue>
{
    private List<Dictionary<TKey, TValue>> _dictionaries = new List<Dictionary<TKey, TValue>>();
    private Dictionary<TKey, TValue> _mainDictionary = new Dictionary<TKey, TValue>();

    public List<Dictionary<TKey, TValue>> Dictionaries
    {
        get => _dictionaries;
    }
    
    public ChainMap(params Dictionary<TKey, TValue>[] dictionaries)
    {
        if (dictionaries.Length != 0)
        {
            _mainDictionary = dictionaries[0];
            _dictionaries = new List<Dictionary<TKey, TValue>>(dictionaries);
        }
    }
    
    public ChainMap(){}

    public TValue this[TKey key]
    {
        get
        {
            foreach (var d in _dictionaries)
            {
                if (d.ContainsKey(key)) return d[key];
            }

            throw new KeyNotFoundException($"Key {key} is missing in dictionary");
        }
        set
        {
            foreach (var d in _dictionaries)
            {
                if (d.ContainsKey(key))
                {
                    _mainDictionary[key] = value;
                    _dictionaries[0][key] = value;
                    return;
                }
            }
        }
    }
    
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        foreach (var d in _dictionaries)
        {
            foreach (var line in d)
            {
                yield return new KeyValuePair<TKey, TValue>(line.Key, line.Value);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public void Add(TKey key, TValue value)
    {
        if (_mainDictionary.ContainsKey(key))
        {
            throw new ArgumentException($"Key {key} already exists!");
        }
        
        _mainDictionary[key] = value;
        _dictionaries[0][key] = value;
    }
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
    

    public bool TryAdd(TKey key, TValue value)
    {
        if (_mainDictionary.ContainsKey(key)) return false;
        
        _mainDictionary[key] = value;
        _dictionaries[0][key] = value;
        return true;
    }

    public void Clear()
    {
        _mainDictionary.Clear();
        _dictionaries[0].Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public int Count => _dictionaries.Sum(d => d.Count);
    public bool IsReadOnly => false;

    public bool ContainsKey(TKey key) => _dictionaries.Any(d => d.ContainsKey(key));

    public bool ContainsValue(TValue value) => _dictionaries.Any(d => d.ContainsValue(value));
    
    public bool Remove(TKey key)
    {
        if (_mainDictionary.ContainsKey(key))
        {
            _mainDictionary.Remove(key);
            _dictionaries[0].Remove(key);
            return true;
        }

        return false;
    }
    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

    public bool TryGetValue(TKey key, out TValue value)
    {
        foreach (var d in _dictionaries)
        {
            if (d.ContainsKey(key))
            {
                value = d[key];
                return true;
            }
        }

        value = default(TValue);
        return false;
    }

    public ICollection<TKey> Keys => _dictionaries.SelectMany(d => d.Keys).Distinct().ToList();
    public ICollection<TValue> Values => _dictionaries.SelectMany(d => d.Values).Distinct().ToList();
    
    public void AddDictionary(Dictionary<TKey, TValue> dictToAdd, int index)
    {
        index++;
        
        if (index != 0)
        {
            if (index < 1)
            {
                _dictionaries.Add(dictToAdd);
            }else if (index >= _dictionaries.Count)
            {
                _dictionaries.Insert(1,dictToAdd);
            }
            else
            {
                _dictionaries.Insert(index, dictToAdd);
            }
        }
    }
    
    public void RemoveDictionary(int index)
    {
        index++;
        
        if (index >= 1 && index < _dictionaries.Count)
        {
            _dictionaries.RemoveAt(index);
        }
    }

    public void ClearDictionaries()
    {
        _dictionaries.Clear();
        _mainDictionary = new Dictionary<TKey, TValue>();
    }

    public int CountDictionaries() => _dictionaries.Count;

    public ReadOnlyCollection<Dictionary<TKey, TValue>> GetDictionaries() => new (_dictionaries.GetRange(1, _dictionaries.Count - 1));

    public ReadOnlyDictionary<TKey, TValue> GetDictionary(int index) => new(_dictionaries[index + 1]);

    public Dictionary<TKey, TValue> GetMainDictionary() => _mainDictionary;

    public Dictionary<TKey, TValue> Merge()
    {
        var result = new Dictionary<TKey, TValue>();

        foreach (var d in _dictionaries)
        {
            foreach (var line in d)
            {
                if (!result.ContainsKey(line.Key))
                {
                    result[line.Key] = line.Value;
                }
            }
        }

        return result;
    }
}