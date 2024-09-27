using System.Collections.ObjectModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class RuneLayoutMapper : Singleton<RuneLayoutMapper>
{
#if UNITY_EDITOR

    public const float ParallelHexRatio = 0.8660254f;
    public const float LongDiagHexRatio = 1.1547005f;

    private static readonly ReadOnlyCollection<ReadOnlyCollection<Vector2>> regularNodes = new ReadOnlyCollection<ReadOnlyCollection<Vector2>>( new ReadOnlyCollection<Vector2>[3] {
        new ReadOnlyCollection<Vector2>(new Vector2[20]{
            new Vector2( -1,7 ),
            new Vector2( 0,7 ),
            new Vector2( 1,7 ),
            new Vector2( -3,6 ),
            new Vector2( -2,6 ),
            new Vector2( -1,6 ),
            new Vector2( 0,6 ),
            new Vector2( 1,6 ),
            new Vector2( 2,6 ),
            new Vector2( -3,5 ),
            new Vector2( -2,5 ),
            new Vector2( -1,5 ),
            new Vector2( 0,5 ),
            new Vector2( 1,5 ),
            new Vector2( 2,5 ),
            new Vector2( 3,5 ),
            new Vector2( -3,4 ),
            new Vector2( -2,4 ),
            new Vector2( 1,4 ),
            new Vector2( 2,4 )
        }),
        new ReadOnlyCollection<Vector2>(new Vector2[20]{
            new Vector2( 4,1 ),
            new Vector2( 5,1 ),
            new Vector2( 3,0 ),
            new Vector2( 4,0 ),
            new Vector2( 5,0 ),
            new Vector2( 4,-1 ),
            new Vector2( 5,-1 ),
            new Vector2( 3,-2 ),
            new Vector2( 4,-2 ),
            new Vector2( 5,-2 ),
            new Vector2( 2,-3 ),
            new Vector2( 3,-3 ),
            new Vector2( 4,-3 ),
            new Vector2( 5,-3 ),
            new Vector2( 1,-4 ),
            new Vector2( 2,-4 ),
            new Vector2( 3,-4 ),
            new Vector2( 4,-4 ),
            new Vector2( 2,-5 ),
            new Vector2( 3,-5 )
        }),
        new ReadOnlyCollection<Vector2>(new Vector2[20]{
            new Vector2( -5,1 ),
            new Vector2( -4,1 ),
            new Vector2( -6,0 ),
            new Vector2( -5,0 ),
            new Vector2( -4,0 ),
            new Vector2( -5,-1 ),
            new Vector2( -4,-1 ),
            new Vector2( -6,-2 ),
            new Vector2( -5,-2 ),
            new Vector2( -4,-2 ),
            new Vector2( -5,-3 ),
            new Vector2( -4,-3 ),
            new Vector2( -3,-3 ),
            new Vector2( -2,-3 ),
            new Vector2( -5,-4 ),
            new Vector2( -4,-4 ),
            new Vector2( -3,-4 ),
            new Vector2( -2,-4 ),
            new Vector2( -3,-5 ),
            new Vector2( -2,-5 )
        })
    });

    private static readonly ReadOnlyCollection<Vector2> capstoneNodes = new ReadOnlyCollection<Vector2>(
        new Vector2[3]{
            new Vector2( -4.5f,3.333333f ),
            new Vector2( 4.5f,3.333333f ),
            new Vector2( -0.5f,-5.666667f )
        }
    );

    [SerializeField] bool RebuildHexes;
    [SerializeField] bool UpdateHexes;
    [SerializeField] int NonNotchGameobjects = 1;
    [SerializeField] private float RegularNodeScale = 0.5f;
    [SerializeField] private float CapstoneNodeScale = 3f;
    [SerializeField] private int GridDims = 12;

    [SerializeField] private GameObject node;
    [SerializeField] private GameObject capstoneNode;

    private float TrailingGridHeight = 1000f;
    private float TrailingGridWidth = 866.0254f;

    private void OnValidate()
    {
        if (RebuildHexes)
            UnityEditor.EditorApplication.delayCall += () => RebuildGrid();
        RebuildHexes = false;
        UnityEditor.EditorApplication.delayCall += () => UpdateGrid();
        UpdateHexes = false;
    }

    private void RebuildGrid()
    {
        //delete all existing objects and groups
        while(transform.parent.childCount > NonNotchGameobjects)
            DestroyImmediate(transform.parent.GetChild(NonNotchGameobjects).gameObject);

        //Iterate over Groups
        for (int groupIterator = 0; groupIterator < regularNodes.Count; groupIterator++)
        {
            GameObject group = new GameObject("Notch Group " + (groupIterator + 1).ToString());
            group.transform.SetParent(transform.parent, false);
            group.tag = "Group";

            //Iterate over Nodes
            for (int nodeIterator = 0; nodeIterator < regularNodes[groupIterator].Count; nodeIterator++)
            {
                GameObject hex = Instantiate(node, group.transform, false);
                hex.name = "Group " + (groupIterator + 1).ToString() + " notch " + (nodeIterator + 1).ToString(); 
                hex.GetComponent<HexData>().coords = regularNodes[groupIterator][nodeIterator];
            }
        }

        //Iterate over Capstones
        for (int capstoneIterator = 0; capstoneIterator < capstoneNodes.Count; capstoneIterator++)
        {
            GameObject hex = Instantiate(capstoneNode, transform.parent, false);
            hex.name = "Capstone notch " + (capstoneIterator + 1).ToString();
            hex.GetComponent<HexData>().coords = capstoneNodes[capstoneIterator];
        }
    }

    public void UpdateGrid()
    {
        //Grab rect transform of container
        RectTransform screenspace = transform.parent.GetComponent<RectTransform>();
        LayoutElement container = screenspace.GetComponent<LayoutElement>();

        //Apply hex Aspect ratio to container
        if (container.minHeight != TrailingGridHeight)
        {
            container.minWidth = ParallelHexRatio * container.minHeight;
            container.minHeight = screenspace.sizeDelta.y;
        }
        else if (container.minWidth != TrailingGridWidth)
        {
            container.minWidth = screenspace.sizeDelta.x;
            container.minHeight = LongDiagHexRatio * container.minWidth;
        }

        //Update Cache
        TrailingGridHeight = container.minHeight;
        TrailingGridWidth = container.minWidth;

        //Calc "grid" size
        float slotWidth = container.minWidth / GridDims;
        float slotHeight = slotWidth * LongDiagHexRatio;

        //Sort locations of children Hexes.
        for (int childIterator = NonNotchGameobjects; childIterator < transform.parent.childCount; childIterator++)
        {
            //Grab the child
            Transform child = transform.parent.GetChild(childIterator);

            //Check if it is a hex group or not using generic "group" tag
            if (child.CompareTag("Group"))
            {
                //If it is a group, repeat on children
                for (int grandChildIterator = 0; grandChildIterator < child.childCount; grandChildIterator++)
                    HandleHex(child.GetChild(grandChildIterator), slotWidth, slotHeight);
            }
            //Else, handle it as a hex
            else
                HandleHex(child, slotWidth, slotHeight);
        }
    }

    public void HandleHex(Transform hex, float slotWidth, float slotHeight)
    {
        //Manhandle the child (grab it's data)
        HexData hexData = hex.GetComponent<HexData>();
        RectTransform hexRect = hex.GetComponent<RectTransform>();

        //Set size
        hexRect.sizeDelta = new Vector2(slotWidth, slotHeight);
        if (hexData.capstone)
            hexRect.sizeDelta *= CapstoneNodeScale;
        else
            hexRect.sizeDelta *= RegularNodeScale;

        //Translate 2d coords to hex grid
        float xpos = hexData.coords.x;
        float ypos = hexData.coords.y * 0.75f - 0.25f;

        //Adjust for odd/even size grid (the extra 1/2 a row :(  )
        if (GridDims % 2 != 0)
        {
            xpos += 0.5f;
            ypos += 0.25f;
        }

        //Using Positive adjusted coords, adjust for "shunted" rows on every even number
        //
        // NOTE: 0.5f is there to add "closest vertex snapping" for deliberately placed
        // decimal coords for off-grid placement
        //
        if ((hexData.coords.y + 2 * math.trunc(GridDims / 2) - 0.5f) % 2 >= 1)
            xpos += 0.5f;

        hexRect.localPosition = new Vector2(slotWidth * xpos, slotHeight * ypos);
        ///ASHDAFHERIWBVHEAORUBHWEORB
    }

    #endif
}