using UnityEngine;

// A base class for any script that is attached to a pooled game object and needs to execute
// some logic when the object is spawned by the pool. OnSpawn will execute even if this
// is a component of a child object of the pooled object
public abstract class ComponentOfPooledObject : MonoBehaviour
{
    // Function called whenever the pooled object is spawned from the object pool
    public abstract void OnSpawn();
}
