using System;
using UnityEngine;

[ExecuteAlways]
public class GenerateChain : MonoBehaviour
{
    public int fragmentsCount = 0;
    public GameObject chainFragmentAsset;

    private Rigidbody2D myLastChainPart;
    private Transform fragmentsTransform;
    private MaceEnd mace;

    public void Start()
    {
        myLastChainPart = transform.Find("LastPart").GetComponent<Rigidbody2D>();
        fragmentsTransform = transform.Find("Fragments");
        mace = transform.Find("MaceEnd").GetComponent<MaceEnd>();

        UpdateFragmentsCount();
    }

#if UNITY_EDITOR
    public void Update()
    {
        UpdateFragmentsCount();
    }
#endif

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
        Rigidbody2D lastChainPart = GetLastChainPart();

        for (int i = 0; i < count; ++i)
        {
            GameObject obj =
                Instantiate(chainFragmentAsset, fragmentsTransform);

            ChainFragment chainFragment = obj.GetComponent<ChainFragment>();

            chainFragment.SetPreviousChainPart(lastChainPart);
            lastChainPart = chainFragment.lastPartRigidbody;
        }

        //mace.ConnectTo(lastChainPart);
    }

    private void RemoveFragments(int count)
    {
        int removeCount = Math.Min(fragmentsTransform.childCount, count);
        int removed = 0;

        while (removed < removeCount)
        {
            int lastIndex = fragmentsTransform.childCount - 1;
            GameObject fragment = fragmentsTransform.GetChild(lastIndex).gameObject;

#if UNITY_EDITOR
            DestroyImmediate(fragment, allowDestroyingAssets: false);
#else
            Destroy(fragment);
#endif
            ++removed;
        }

        //mace.ConnectTo(GetLastChainPart());
    }

    private Rigidbody2D GetLastChainPart()
    {
        if (fragmentsTransform.childCount == 0)
        {
            return myLastChainPart;
        }

        Transform transform =
            fragmentsTransform.GetChild(fragmentsTransform.childCount - 1);

        ChainFragment chainFragment = transform.GetComponent<ChainFragment>();

        return chainFragment.lastPartRigidbody;
    }
}
