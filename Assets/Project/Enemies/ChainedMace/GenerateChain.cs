using UnityEngine;

[ExecuteAlways]
public class GenerateChain : MonoBehaviour
{
    public int fragmentsCount = 0;
    public GameObject chainFragmentAsset;

    private Transform fragmentsTransform;

    public void Start()
    {
        fragmentsTransform = transform.Find("Fragments");
        UpdateFragmentsCount();
    }

    public void Update()
    {
        UpdateFragmentsCount();
    }

    private void UpdateFragmentsCount()
    {
        if (fragmentsCount < 0)
        {
            Debug.LogWarning($"{gameObject}: fragmentsCount must be >= 0");
            return;
        }

        int currentCount = fragmentsTransform.childCount;
        int newCount = fragmentsCount;

        if (newCount > currentCount)
        {
            AddFragments(newCount - currentCount);
        }
        else if (newCount < currentCount)
        {
            RemoveFragments(currentCount - newCount);
        }
    }

    private void AddFragments(int count)
    {
        Rigidbody2D lastChainPart;

        if (fragmentsTransform.transform.childCount == 0)
        {
            lastChainPart =
                transform.Find("LastPart").gameObject.GetComponent<Rigidbody2D>();
        }
        else
        {
            Transform transform =
                fragmentsTransform.GetChild(fragmentsTransform.childCount - 1);

            lastChainPart = transform.GetComponent<Rigidbody2D>();
        }

        for (int i = 0; i < count; ++i)
        {
            GameObject obj =
                Instantiate(chainFragmentAsset, fragmentsTransform);

            ChainFragment chainFragment = obj.GetComponent<ChainFragment>();

            chainFragment.SetPreviousChainPart(lastChainPart);
            lastChainPart = chainFragment.lastPartRigidbody;
        }
    }

    private void RemoveFragments(int count)
    {
        for (int i = fragmentsTransform.childCount - 1 - count; i >= 0; --i)
        {
            GameObject fragment = fragmentsTransform.GetChild(i).gameObject;

            if (Application.isEditor)
            {
                DestroyImmediate(fragment);
            }
            else
            {
                Destroy(fragment);
            }
        }
    }
}
