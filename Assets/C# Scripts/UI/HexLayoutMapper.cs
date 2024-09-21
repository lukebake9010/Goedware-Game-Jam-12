using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class HexLayoutMapper : Singleton<HexLayoutMapper>
{
#if UNITY_EDITOR

    public const float ParallelHexRatio = 0.8660254f;
    public const float LongDiagHexRatio = 1.1547005f;

    private static readonly ReadOnlyCollection<ReadOnlyCollection<Vector2>> regularNodes = new ReadOnlyCollection<ReadOnlyCollection<Vector2>>(
        new ReadOnlyCollection<Vector2>[3] {
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
        }
    );
    private static readonly ReadOnlyCollection<Vector2> capstoneNodes = new ReadOnlyCollection<Vector2>(
        new Vector2[3]{
            new Vector2( -4.5f,3.333333f ),
            new Vector2( 4.5f,3.333333f ),
            new Vector2( -0.5f,-5.666667f )
        }
    );

    [SerializeField] bool RebuildHexes;
    [SerializeField] bool UpdateHexes;
    [SerializeField] private float SpriteVerticalHeight = 50f;
    [SerializeField] private float SpriteHorizontalWidth = 43.30127f;
    [SerializeField] private float CapstoneNodeScale = 3f;
    [SerializeField] private int GridDims = 18;

    [SerializeField] private GameObject node;
    [SerializeField] private GameObject capstoneNode;

    private float TrailingSpriteHeight = 50f;
    private float TrailingSpriteWidth = 43.30127f;
    private float TrailingGridHeight = 1000f;
    private float TrailingGridWidth = 866.0254f;
    private Vector2 TrailingGridDims = new Vector2(18, 18);

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
        while(transform.parent.childCount > 1)
            DestroyImmediate(transform.parent.GetChild(1).gameObject);


        //Iterate over Groups
        for (int groupIterator = 0; groupIterator < regularNodes.Count; groupIterator++)
        {
            GameObject group = new GameObject("Hex Group " + (groupIterator + 1).ToString());
            group.transform.SetParent(transform.parent, false);
            group.tag = "Group";

            //Iterate over Nodes
            for (int nodeIterator = 0; nodeIterator < regularNodes[groupIterator].Count; nodeIterator++)
            {
                GameObject hex = Instantiate(node);
                hex.transform.SetParent(group.transform, false);
                hex.GetComponent<HexData>().coords = regularNodes[groupIterator][nodeIterator];
            }
        }

        //Iterate over Capstones
        for (int capstoneIterator = 0; capstoneIterator < capstoneNodes.Count; capstoneIterator++)
        {
            GameObject hex = Instantiate(capstoneNode);
            hex.transform.SetParent(transform.parent, false);
            hex.GetComponent<HexData>().coords = capstoneNodes[capstoneIterator];
        }
    }

    public void UpdateGrid()
    {
        //Apply hex Aspect ratio to sprites
        if (SpriteVerticalHeight != TrailingSpriteHeight)
            SpriteHorizontalWidth = ParallelHexRatio * SpriteVerticalHeight;
        else if(SpriteHorizontalWidth != TrailingSpriteWidth)
            SpriteVerticalHeight = LongDiagHexRatio * SpriteHorizontalWidth;

        TrailingSpriteHeight = SpriteVerticalHeight;
        TrailingSpriteWidth = SpriteHorizontalWidth;

        //Grab rect transform of container
        RectTransform screenspace = transform.parent.GetComponent<RectTransform>();
        Rect rectSpace = screenspace.rect;
        Rect anchorRect = new Rect(screenspace.anchorMin.x, screenspace.anchorMin.y, screenspace.anchorMax.x - screenspace.anchorMin.x, screenspace.anchorMax.y - screenspace.anchorMin.y);

        //Apply hex Aspect ratio to container
        if (rectSpace.height != TrailingGridHeight)
            screenspace.sizeDelta = new Vector2(ParallelHexRatio * rectSpace.height - anchorRect.width, screenspace.sizeDelta.y);
        else if (rectSpace.width != TrailingGridWidth)
            screenspace.sizeDelta = new Vector2(screenspace.sizeDelta.x, LongDiagHexRatio * rectSpace.width - anchorRect.height);

        //Update Cache
        rectSpace = screenspace.rect;
        TrailingGridHeight = rectSpace.height;
        TrailingGridWidth = rectSpace.width;

        //Calc "grid" size
        float slotWidth = rectSpace.width / GridDims;
        float slotHeight = slotWidth * LongDiagHexRatio;

        //Sort locations of children Hexes.
        for (int childIterator = 1; childIterator < transform.parent.childCount; childIterator++)
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
        hexRect.sizeDelta = new Vector2(SpriteHorizontalWidth, SpriteVerticalHeight);
        if (hexData.capstone)
            hexRect.sizeDelta *= CapstoneNodeScale;

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
