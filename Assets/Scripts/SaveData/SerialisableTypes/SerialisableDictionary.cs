using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SerialisableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{

    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List <TValue> values = new List<TValue>();

    //saves dictionary to lists
   public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    //loads dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();
        if(keys.Count != values.Count)
        {
            Debug.LogError($"Tried to deserialise a serialisable, but the amount of keys{keys.Count} does not match the number of values" +
                $"{values.Count} which indicates something went horribly wrong, good luck sorting that out");
        }

        for(int i=0; i<keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }

    }

}
