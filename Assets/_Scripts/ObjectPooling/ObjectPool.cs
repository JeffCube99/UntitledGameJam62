using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObjectPool", menuName = "ScriptableObjects/ObjectPool")]
public class ObjectPool : ScriptableObject
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int maxObjects;

    private Queue<PooledObjectReference> spawnedItems;

    private PooledObjectReference SpawnObject()
    {
        GameObject newObject = Instantiate(prefab);
        ComponentOfPooledObject[] components = newObject.GetComponentsInChildren<ComponentOfPooledObject>();
        return new PooledObjectReference(newObject, components);
    }

    // Returns a PooledObjectReference from the ObjectPool
    // If the pool queue is empty or if it is less than full we spawn a new object, add its reference to the queue, and return its reference
    // If the pool queue is full we get the oldest reference from the queue:
    //      If the reference object still exists we add the reference back into the queue and return the reference
    //      If the reference object no longer exists, we spawn a new object, add its reference to the queue, and return its reference.
    private PooledObjectReference GetObjectReferenceFromPool()
    {
        if (spawnedItems == null)
        {
            spawnedItems = new Queue<PooledObjectReference>();
        }
        
        if (spawnedItems.Count < maxObjects)
        {
            PooledObjectReference newReference = SpawnObject();
            spawnedItems.Enqueue(newReference);
            return newReference;
        }
        else
        {
            PooledObjectReference oldReference = spawnedItems.Dequeue();
            if (oldReference.gameObject == null)
            {
                // If the object reference is null, the object has likely been destroyed. Therefore spawn a new object
                PooledObjectReference newReference = SpawnObject();
                spawnedItems.Enqueue(newReference);
                return newReference;
            }
            else
            {
                spawnedItems.Enqueue(oldReference);
                return oldReference;
            }
        }
    }

    // Returns a game object from the object pool with its position and rotation set in world space
    public GameObject Instantiate(Vector3 position, Quaternion rotation)
    {
        return Instantiate(position, rotation, null);
    }

    // Returns a game object from the object pool parented to partent and with its position and rotation set relative to the parent.
    public GameObject Instantiate(Vector3 localPosition, Quaternion localRotation, Transform parent)
    {
        PooledObjectReference reference = GetObjectReferenceFromPool();
        GameObject pooledObject = reference.gameObject;
        // Deactivate the object prior to altering its transform
        pooledObject.SetActive(false);
        pooledObject.transform.parent = parent;
        pooledObject.transform.localPosition = localPosition;
        pooledObject.transform.localRotation = localRotation;
        pooledObject.SetActive(true);
        // Invoke OnSpawn events in the game object's components
        reference.InvokeOnSpawn();
        return pooledObject;
    }

}

// In addition to the object reference we also store references to 
// PooledObjectReference components to improve performance by eliminating
// repeated calls to pooledObject.GetComponentsInChildren<ComponentOfPooledObject> 
public class PooledObjectReference
{
    public GameObject gameObject;
    public ComponentOfPooledObject[] components;

    public PooledObjectReference(GameObject gameObjectInstance, ComponentOfPooledObject[] componentInstances)
    {
        gameObject = gameObjectInstance;
        components = componentInstances;
    }

    // Invokes the OnSpawn method contained within each pooledObjectComponent
    public void InvokeOnSpawn()
    {
        foreach (ComponentOfPooledObject component in components)
        {
            component.OnSpawn();
        }
    }
}
