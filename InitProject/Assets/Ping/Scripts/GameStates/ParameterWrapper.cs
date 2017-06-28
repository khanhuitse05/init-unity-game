using System.Collections.Generic;

public class ParameterWrapper
{
    Dictionary<string, object> parameters;
    public ParameterWrapper()
    {
        parameters = new Dictionary<string, object>();
    }
    public object this[string key]
    {
        get
        {
            if (!parameters.ContainsKey(key))
                return null;
            return parameters[key];
        }
        set { parameters[key] = value; }
    }

    public T get<T>(string key, T v)
    {
        if (!parameters.ContainsKey(key))
            return v;
        return (T)parameters[key];
    }

    public bool Remove(string key)
    {
        return parameters.Remove(key);
    }
    public void Clear()
    {
        parameters.Clear();
    }
}