using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelTest : MonoBehaviour {
    Dictionary<int, Dictionary<string, string>> model;
    static int i = 0;
	// Use this for initialization
	void Start () {
        model = new Dictionary<int, Dictionary<string, string>>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addCollections(string name, string min, string max,
        string desc, string nb_card, string nb_re)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("min", min);
        toAdd.Add("max", max);
        toAdd.Add("description", desc);
        toAdd.Add("nb_cards", nb_card);
        toAdd.Add("nb_ressources", nb_re);
        toAdd.Add("id", ModelTest.i.ToString());
        model.Add(i, toAdd);
        ModelTest.i += 1;
    }

    public string find(int id, string req)
    {
        return (model[id][req]);
    }


    public int getNbElement()
    {
        return (ModelTest.i);
    }

    public Dictionary<int, Dictionary<string, string>> getAll()
    {
        return (model);
    }

    public void updateField(int id, string field, string value)
    {
        model[id][field] = value;
    }

    public void removeElem(int id)
    {
        model.Remove(id);
    }
}
