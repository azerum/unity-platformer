using System;
using UnityEngine;

[ExecuteInEditMode]
public class GenerateChainInEditMode : MonoBehaviour
{
    public int fragmentsCount = 0;

    public JointParent maceStart;
    public JointChild maceEnd;

    public GameObject chainFragmentAsset;
    public Transform fragmentsContainer;

    public void Start()
    {
        maceEnd.ConnectTo(maceStart);
    }

    public void Update()
    {
        int newCount = fragmentsCount;

        if (newCount < 0)
        {
            Debug.LogWarning($"{gameObject}: {nameof(fragmentsCount)} must be >= 0");
            return;
        }

        int currentCount = fragmentsContainer.childCount;

        if (currentCount < newCount)
        {
            AddLinksPairs(newCount - currentCount);
        }
        else if (currentCount > newCount)
        {
            RemoveLinksPairs(currentCount - newCount);
        }
    }

    private void AddLinksPairs(int count)
    {
        JointParent lastPart = GetLastPart();

        for (int i = 0; i < count; ++i)
        {
            GameObject obj = Instantiate(chainFragmentAsset, fragmentsContainer);

            JointParent asParent = obj.GetComponent<JointParent>();
            JointChild asChild = obj.GetComponent<JointChild>();

            asChild.ConnectTo(lastPart);
            lastPart = asParent;
        }

        maceEnd.ConnectTo(lastPart);
    }

    private void RemoveLinksPairs(int count)
    {
        int currentCount = fragmentsContainer.childCount;
        int removeCount = Math.Min(currentCount, count);

        for (int childIndex = currentCount - 1 - removeCount; childIndex >= 0; --childIndex)
        {
            GameObject child = fragmentsContainer.GetChild(childIndex).gameObject;

            if (Application.isEditor)
            {
                DestroyImmediate(child, allowDestroyingAssets: false);
            }
            else
            {
                Destroy(child);
            }
        }

        maceEnd.ConnectTo(GetLastPart());
    }

    private JointParent GetLastPart()
    {
        if (fragmentsContainer.childCount == 0)
        {
            return maceStart;
        }

        Transform lastFragment =
            fragmentsContainer.GetChild(fragmentsContainer.childCount - 1);

        return lastFragment.GetComponent<JointParent>();
    }
}
