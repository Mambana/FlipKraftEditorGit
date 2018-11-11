using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCard : MonoBehaviour {

    Dictionary<int, Dictionary<string, string>> model;
    static int i = 0;
    // Use this for initialization
    void Start()
    {
        model = new Dictionary<int, Dictionary<string, string>>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addCollections(string name, string desc, string projectId)
    {
        Dictionary<string, string> toAdd = new Dictionary<string, string>();

        toAdd.Add("name", name);
        toAdd.Add("description", desc);
        toAdd.Add("project_id", projectId);
        toAdd.Add("id", ModelCard.i.ToString());
        model.Add(i, toAdd);
        ModelCard.i += 1;
    }

    public string find(int id, string req)
    {
        return (model[id][req]);
    }


    public int getNbElement()
    {
        return (ModelCard.i);
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
