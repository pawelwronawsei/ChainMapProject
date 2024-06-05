using System.Collections;
using System.Linq;

namespace ChainMapLib;

public struct ChainMap<TKey, TValue> : IDictionary<TKey, TValue>
{
    private List<Dictionary<TKey, TValue>> _dictionaries = new List<Dictionary<TKey, TValue>>();
    private Dictionary<TKey, TValue> _mainDictionary = new Dictionary<TKey, TValue>();

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
                    return;
                }
            }
        }
    }

    public void AddDictionary(Dictionary<TKey, TValue> dictToAdd, int position)
    {
        if (position != 0 && position < _dictionaries.Count)
        {
            _dictionaries.Insert(position, dictToAdd);
        }
    }
    
    public void RemoveDictionary(int position)
    {
        if (position != 0 && position < _dictionaries.Count)
        {
            _dictionaries.RemoveAt(position);
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
    }
    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
    

    public bool TryAdd(TKey key, TValue value)
    {
        if (_mainDictionary.ContainsKey(key)) return false;
        
        _mainDictionary[key] = value;
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
}