namespace TextIO;

public static class DictionaryExtension
{
    public static bool TryAddOrIncrement(this Dictionary<string, int> dictionary, string key)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] ++;
            return true;
        }
        else
        {
            dictionary.Add(key, 1);
            return false;
        }
    }
}
