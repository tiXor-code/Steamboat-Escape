using UnityEngine;

public class SetChildrenTag : MonoBehaviour
{
    public string newTag;
    public void Awake()
    {
        SetTagForAllChildren();
    }
    // Call this method to set the tag of all children
    public void SetTagForAllChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.tag = newTag;
        }
    }
}
