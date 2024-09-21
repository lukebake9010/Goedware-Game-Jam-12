using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

#if UNITY_EDITOR
[RequireComponent(typeof(RectTransform))]
[ExecuteInEditMode]
public class HexLayoutMapper : MonoBehaviour
{
    public const float ParallelHexRatio = 0.8660254f;
    public const float LongDiagHexRatio = 1.1547005f;

    [SerializeField] bool ForceValidate;
    [SerializeField] private float SpriteVerticalHeight = 100f;
    [SerializeField] private float SpriteHorizontalWidth = 86.60254f;
    [SerializeField] private int GridDims = 18;

    private float TrailingSpriteHeight = 50f;
    private float TrailingSpriteWidth = 43.30127f;
    private float TrailingGridHeight = 1000f;
    private float TrailingGridWidth = 866.0254f;
    private Vector2 TrailingGridDims = new Vector2(18, 18);

    private void OnValidate()
    {
        ForceValidate = false;
        UpdateGrid();
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
        for (int i = 1; i < transform.parent.childCount; i++)
        {
            //Manhandle the child (grab it's data)
            Transform hex = transform.parent.GetChild(i).transform;
            HexData hexData = hex.GetComponent<HexData>();
            RectTransform hexRect = hex.GetComponent<RectTransform>();

            //Set size
            hexRect.sizeDelta = new Vector2(SpriteHorizontalWidth, SpriteVerticalHeight) * hexData.scale;

            //Translate 2d coords to hex grid
            float xpos = hexData.coords.x;
            float ypos = hexData.coords.y * 0.75f  - 0.25f;

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
            if ((hexData.coords.y + 2 * math.trunc(GridDims / 2) - 0.5f ) % 2 >= 1)
                xpos += 0.5f;

            hexRect.localPosition = new Vector2(slotWidth * xpos, slotHeight * ypos);
            ///ASHDAFHERIWBVHEAORUBHWEORB
        }
    }
}

#endif