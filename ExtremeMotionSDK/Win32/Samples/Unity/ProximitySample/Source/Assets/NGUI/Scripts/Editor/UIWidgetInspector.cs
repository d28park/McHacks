//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections.Generic; ††††ഀ਀⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀⼀⼀⼀ 䤀渀猀瀀攀挀琀漀爀 挀氀愀猀猀 甀猀攀搀 琀漀 攀搀椀琀 唀䤀圀椀搀最攀琀猀⸀ഀ਀⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀嬀䌀甀猀琀漀洀䔀搀椀琀漀爀⠀琀礀瀀攀漀昀⠀唀䤀圀椀搀最攀琀⤀⤀崀ഀ਀瀀甀戀氀椀挀 挀氀愀猀猀 唀䤀圀椀搀最攀琀䤀渀猀瀀攀挀琀漀爀 㨀 䔀搀椀琀漀爀ഀ਀笀ഀ਀ऀ攀渀甀洀 䄀挀琀椀漀渀ഀ਀ऀ笀ഀ਀ऀऀ一漀渀攀Ⰰഀ਀ऀऀ䴀漀瘀攀Ⰰഀ਀ऀऀ匀挀愀氀攀Ⰰഀ਀†ऀऀ刀漀琀愀琀攀Ⰰഀ਀ऀ紀ഀ਀ഀ਀ऀ䄀挀琀椀漀渀 洀䄀挀琀椀漀渀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀ䄀挀琀椀漀渀 洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀ戀漀漀氀 洀䄀氀氀漀眀匀攀氀攀挀琀椀漀渀 㴀 琀爀甀攀㬀ഀ਀ഀ਀ऀ瀀爀漀琀攀挀琀攀搀 唀䤀圀椀搀最攀琀 洀圀椀搀最攀琀㬀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 瀀爀漀琀攀挀琀攀搀 戀漀漀氀 洀唀猀攀匀栀愀搀攀爀 㴀 昀愀氀猀攀㬀ഀ਀ऀ猀琀愀琀椀挀 䌀漀氀漀爀 洀伀甀琀氀椀渀攀䌀漀氀漀爀 㴀 䌀漀氀漀爀⸀最爀攀攀渀㬀ഀ਀ऀ猀琀愀琀椀挀 䜀唀䤀匀琀礀氀攀 洀匀攀氀攀挀琀攀搀䐀漀琀 㴀 渀甀氀氀㬀ഀ਀ऀ猀琀愀琀椀挀 䜀唀䤀匀琀礀氀攀 洀一漀爀洀愀氀䐀漀琀 㴀 渀甀氀氀㬀ഀ਀ऀ猀琀愀琀椀挀 䴀漀甀猀攀䌀甀爀猀漀爀 洀䌀甀爀猀漀爀 㴀 䴀漀甀猀攀䌀甀爀猀漀爀⸀䄀爀爀漀眀㬀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀嬀崀 洀倀椀瘀漀琀猀 㴀ഀ਀उ		{
		UIWidget.Pivot.TopLeft,
		UIWidget.Pivot.BottomLeft,
		UIWidget.Pivot.BottomRight,
		UIWidget.Pivot.TopRight,
		UIWidget.Pivot.Left,ऊउऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀Ⰰഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀Ⰰഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀Ⰰഀ਀ऀ紀㬀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 椀渀琀 猀开䠀愀猀栀 㴀 ∀圀椀搀最攀琀䠀愀猀栀∀⸀䜀攀琀䠀愀猀栀䌀漀搀攀⠀⤀㬀ഀ਀ऀ嘀攀挀琀漀爀㌀ 洀匀琀愀爀琀倀漀猀 㴀 嘀攀挀琀漀爀㌀⸀稀攀爀漀㬀ഀ਀ऀ嘀攀挀琀漀爀㌀ 洀匀琀愀爀琀匀挀愀氀攀 㴀 嘀攀挀琀漀爀㌀⸀稀攀爀漀㬀ഀ਀ऀ嘀攀挀琀漀爀㌀ 洀匀琀愀爀琀䐀爀愀最 㴀 嘀攀挀琀漀爀㌀⸀稀攀爀漀㬀ഀ਀ऀ嘀攀挀琀漀爀㈀ 洀匀琀愀爀琀䴀漀甀猀攀 㴀 嘀攀挀琀漀爀㈀⸀稀攀爀漀㬀ഀ਀ऀ嘀攀挀琀漀爀㌀ 洀匀琀愀爀琀刀漀琀 㴀 嘀攀挀琀漀爀㌀⸀稀攀爀漀㬀ഀ਀†⼯ 	Vector3 mStartDir = Vector3.right;
	UIWidget.Pivot mDragPivot = UIWidget.Pivot.Center; †††† 	bool mInitialized = false;
	bool mDepthCheck = false;

	/// <summary>
	/// Register an Undo command with the Unity editor.
	/// </summary>ऊउഀ਀††† 	void RegisterUndo ()
	{
		NGUIEditorTools.RegisterUndo("Widget Change", mWidget);
	}

	/// <summary>ऊउऀ⼀⼀⼀ 刀愀礀挀愀猀琀 椀渀琀漀 琀栀攀 猀挀爀攀攀渀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 戀漀漀氀 刀愀礀挀愀猀琀 ⠀嘀攀挀琀漀爀㌀嬀崀 挀漀爀渀攀爀猀Ⰰ 漀甀琀 嘀攀挀琀漀爀㌀ 栀椀琀⤀ഀ਀ऀ笀ഀ਀†† 		Plane plane = new Plane(corners[0], corners[1], corners[2]);
		Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
		float dist = 0f;
		bool isHit = plane.Raycast(ray, out dist);
		hit = isHit ? ray.GetPoint(dist) : Vector3.zero;
		return isHit;
	}

	/// <summary>
	/// Draw a control dot at the specified world position.
	/// </summary>

	static void DrawKnob (Vector3 point, bool selected, int id)
	{
		if (mSelectedDot == null) mSelectedDot = "sv_label_5";
		if (mNormalDot == null) mNormalDot = "sv_label_3";
ऊउऀऀ嘀攀挀琀漀爀㈀ 猀挀爀攀攀渀倀漀椀渀琀 㴀 䠀愀渀搀氀攀唀琀椀氀椀琀礀⸀圀漀爀氀搀吀漀䜀唀䤀倀漀椀渀琀⠀瀀漀椀渀琀⤀㬀ഀ਀ഀ਀ऀऀ刀攀挀琀 爀攀挀琀 㴀 渀攀眀 刀攀挀琀⠀猀挀爀攀攀渀倀漀椀渀琀⸀砀 ⴀ 㜀昀Ⰰ 猀挀爀攀攀渀倀漀椀渀琀⸀礀 ⴀ 㜀昀Ⰰ ㄀㐀昀Ⰰ ㄀㐀昀⤀㬀ഀ਀ऀऀ椀昀 ⠀猀攀氀攀挀琀攀搀⤀ 洀匀攀氀攀挀琀攀搀䐀漀琀⸀䐀爀愀眀⠀爀攀挀琀Ⰰ 䜀唀䤀䌀漀渀琀攀渀琀⸀渀漀渀攀Ⰰ 椀搀⤀㬀ഀ਀ऀऀ攀氀猀攀 洀一漀爀洀愀氀䐀漀琀⸀䐀爀愀眀⠀爀攀挀琀Ⰰ 䜀唀䤀䌀漀渀琀攀渀琀⸀渀漀渀攀Ⰰ 椀搀⤀㬀ഀ਀†⼯† 	}

	/// <summary>
	/// Whether the mouse position is within one of the specified rectangles.
	/// </summary>

	static bool IsMouseOverRect (Vector2 mouse, List<Rect> rects)
	{
		for (int i = 0; i < rects.Count; ++i)
		{
			Rect r = rects[i];
			if (r.Contains(mouse)) return true;
		}
		return false;
	}

	/// <summary>
	/// Screen-space distance from the mouse position to the specified world position.
	/// </summary>
 ††⼯ 	static float GetScreenDistance (Vector3 worldPos, Vector2 mousePos)
	{
		Vector2 screenPos = HandleUtility.WorldToGUIPoint(worldPos);
		return Vector2.Distance(mousePos, screenPos);
	}

	/// <summary>
	/// Closest screen-space distance from the mouse position to one of the specified world points.
	/// </summary>


	static float GetScreenDistance (Vector3[] worldPoints, Vector2 mousePos, out int index)
	{
		float min = float.MaxValue;
		index = 0;

		for (int i = 0; i < worldPoints.Length; ++i)
		{
			float distance = GetScreenDistance(worldPoints[i], mousePos);
			
			if (distance < min)
			{
				index = i;
				min = distance;
			}
		}
		return min;
	}

	/// <summary>
	/// Set the mouse cursor rectangle, refreshing the screen when it gets changed.
	/// </summary>

	static void SetCursorRect (Rect rect, MouseCursor cursor)
	{
		EditorGUIUtility.AddCursorRect(rect, cursor);
 ††††ऀऀ椀昀 ⠀䔀瘀攀渀琀⸀挀甀爀爀攀渀琀⸀琀礀瀀攀 㴀㴀 䔀瘀攀渀琀吀礀瀀攀⸀䴀漀甀猀攀䴀漀瘀攀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ椀昀 ⠀洀䌀甀爀猀漀爀 ℀㴀 挀甀爀猀漀爀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ洀䌀甀爀猀漀爀 㴀 挀甀爀猀漀爀㬀ഀ਀ऀऀऀऀ䔀瘀攀渀琀⸀挀甀爀爀攀渀琀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀ紀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 刀愀礀挀愀猀琀 椀渀琀漀 琀栀攀 猀挀爀攀攀渀Ⰰ 爀攀琀甀爀渀椀渀最 愀渀 愀爀爀愀礀 漀昀 眀椀搀最攀琀猀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 唀䤀圀椀搀最攀琀嬀崀 刀愀礀挀愀猀琀 ⠀唀䤀圀椀搀最攀琀 眀椀搀最攀琀Ⰰ 嘀攀挀琀漀爀㈀ 洀漀甀猀攀倀漀猀⤀ഀ਀†⼯†ऀ笀ഀ਀ऀऀ䜀愀洀攀伀戀樀攀挀琀 爀漀漀琀 㴀 眀椀搀最攀琀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀ唀䤀倀愀渀攀氀 瀀愀渀攀氀 㴀 一䜀唀䤀吀漀漀氀猀⸀䘀椀渀搀䤀渀倀愀爀攀渀琀猀㰀唀䤀倀愀渀攀氀㸀⠀爀漀漀琀⤀㬀ഀ਀ऀऀ爀攀琀甀爀渀 ⠀瀀愀渀攀氀 ℀㴀 渀甀氀氀⤀ 㼀 一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀愀礀挀愀猀琀⠀瀀愀渀攀氀Ⰰ 洀漀甀猀攀倀漀猀⤀ 㨀 渀攀眀 唀䤀圀椀搀最攀琀嬀崀 笀紀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 䐀爀愀眀 琀栀攀 漀渀ⴀ猀挀爀攀攀渀 猀攀氀攀挀琀椀漀渀Ⰰ 欀渀漀戀猀Ⰰ 愀渀搀 栀愀渀搀氀攀 愀氀氀 椀渀琀攀爀愀挀琀椀漀渀 氀漀最椀挀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀उऀ瀀甀戀氀椀挀 瘀漀椀搀 伀渀匀挀攀渀攀䜀唀䤀 ⠀⤀ഀ਀ऀ笀ഀ਀ऀऀ⼀⼀吀漀漀氀猀⸀挀甀爀爀攀渀琀 㴀 吀漀漀氀⸀嘀椀攀眀㬀ഀ਀ഀ਀ऀऀ洀圀椀搀最攀琀 㴀 琀愀爀最攀琀 愀猀 唀䤀圀椀搀最攀琀㬀ഀ਀ഀ਀ऀऀ䠀愀渀搀氀攀猀⸀挀漀氀漀爀 㴀 洀伀甀琀氀椀渀攀䌀漀氀漀爀㬀ഀ਀ऀऀ吀爀愀渀猀昀漀爀洀 琀 㴀 洀圀椀搀最攀琀⸀挀愀挀栀攀搀吀爀愀渀猀昀漀爀洀㬀ഀ਀ഀ਀उउऀऀ䔀瘀攀渀琀 攀 㴀 䔀瘀攀渀琀⸀挀甀爀爀攀渀琀㬀ഀ਀ऀऀ椀渀琀 椀搀 㴀 䜀唀䤀唀琀椀氀椀琀礀⸀䜀攀琀䌀漀渀琀爀漀氀䤀䐀⠀猀开䠀愀猀栀Ⰰ 䘀漀挀甀猀吀礀瀀攀⸀倀愀猀猀椀瘀攀⤀㬀ഀ਀ऀऀ䔀瘀攀渀琀吀礀瀀攀 琀礀瀀攀 㴀 攀⸀䜀攀琀吀礀瀀攀䘀漀爀䌀漀渀琀爀漀氀⠀椀搀⤀㬀ഀ਀ഀ਀ऀऀ嘀攀挀琀漀爀㌀嬀崀 挀漀爀渀攀爀猀 㴀 一䜀唀䤀䴀愀琀栀⸀䌀愀氀挀甀氀愀琀攀圀椀搀最攀琀䌀漀爀渀攀爀猀⠀洀圀椀搀最攀琀⤀㬀ഀ਀ऀऀ䠀愀渀搀氀攀猀⸀䐀爀愀眀䰀椀渀攀⠀挀漀爀渀攀爀猀嬀　崀Ⰰ 挀漀爀渀攀爀猀嬀㄀崀⤀㬀ഀ਀ऀऀ䠀愀渀搀氀攀猀⸀䐀爀愀眀䰀椀渀攀⠀挀漀爀渀攀爀猀嬀㄀崀Ⰰ 挀漀爀渀攀爀猀嬀㈀崀⤀㬀ഀ਀ऀऀ䠀愀渀搀氀攀猀⸀䐀爀愀眀䰀椀渀攀⠀挀漀爀渀攀爀猀嬀㈀崀Ⰰ 挀漀爀渀攀爀猀嬀㌀崀⤀㬀ഀ਀ऀऀ䠀愀渀搀氀攀猀⸀䐀爀愀眀䰀椀渀攀⠀挀漀爀渀攀爀猀嬀　崀Ⰰ 挀漀爀渀攀爀猀嬀㌀崀⤀㬀ഀ਀ഀ਀ऀऀ嘀攀挀琀漀爀㌀嬀崀 眀漀爀氀搀倀漀猀 㴀 渀攀眀 嘀攀挀琀漀爀㌀嬀㠀崀㬀ഀ਀ऀऀഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀　崀 㴀 挀漀爀渀攀爀猀嬀　崀㬀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㄀崀 㴀 挀漀爀渀攀爀猀嬀㄀崀㬀ഀ਀†††† 		worldPos[2] = corners[2]; ऀऀ眀漀爀氀搀倀漀猀嬀㌀崀 㴀 挀漀爀渀攀爀猀嬀㌀崀㬀ഀ਀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㐀崀 㴀 ⠀挀漀爀渀攀爀猀嬀　崀 ⬀ 挀漀爀渀攀爀猀嬀㄀崀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㔀崀 㴀 ⠀挀漀爀渀攀爀猀嬀㄀崀 ⬀ 挀漀爀渀攀爀猀嬀㈀崀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㘀崀 㴀 ⠀挀漀爀渀攀爀猀嬀㈀崀 ⬀ 挀漀爀渀攀爀猀嬀㌀崀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㜀崀 㴀 ⠀挀漀爀渀攀爀猀嬀　崀 ⬀ 挀漀爀渀攀爀猀嬀㌀崀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ഀ਀ऀऀ嘀攀挀琀漀爀㈀嬀崀 猀挀爀攀攀渀倀漀猀 㴀 渀攀眀 嘀攀挀琀漀爀㈀嬀㠀崀㬀ഀ਀ऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 㠀㬀 ⬀⬀椀⤀ 猀挀爀攀攀渀倀漀猀嬀椀崀 㴀 䠀愀渀搀氀攀唀琀椀氀椀琀礀⸀圀漀爀氀搀吀漀䜀唀䤀倀漀椀渀琀⠀眀漀爀氀搀倀漀猀嬀椀崀⤀㬀ഀ਀ഀ਀ऀऀ䈀漀甀渀搀猀 戀 㴀 渀攀眀 䈀漀甀渀搀猀⠀猀挀爀攀攀渀倀漀猀嬀　崀Ⰰ 嘀攀挀琀漀爀㌀⸀稀攀爀漀⤀㬀ഀ਀ऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 ㄀㬀 椀 㰀 㠀㬀 ⬀⬀椀⤀ 戀⸀䔀渀挀愀瀀猀甀氀愀琀攀⠀猀挀爀攀攀渀倀漀猀嬀椀崀⤀㬀ഀ਀ഀ਀ऀऀ⼀⼀ 吀椀洀攀 琀漀 昀椀最甀爀攀 漀甀琀 眀栀愀琀 欀椀渀搀 漀昀 愀挀琀椀漀渀 椀猀 甀渀搀攀爀渀攀愀琀栀 琀栀攀 洀漀甀猀攀ഀ਀ऀऀ䄀挀琀椀漀渀 愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 洀䄀挀琀椀漀渀㬀ഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀唀渀搀攀爀䴀漀甀猀攀 㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䌀攀渀琀攀爀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀一漀渀攀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ椀渀琀 椀渀搀攀砀 㴀 　㬀ഀ਀††††††† 			float dist = GetScreenDistance(worldPos, e.mousePosition, out index);

			if (dist < 10f)
			{
				pivotUnderMouse = mPivots[index];
				actionUnderMouse = Action.Scale;
			}
			else if (e.modifiers == 0 && NGUIEditorTools.DistanceToRectangle(corners, e.mousePosition) == 0f)
			{
				if (Tools.current != Tool.Rotate && Tools.current != Tool.Scale)
				{
					actionUnderMouse = Action.Move;
				}
			}
			else if (dist < 30f)
			{
				actionUnderMouse = Action.Rotate;
			}
		}

		// Change the mouse cursor to a more appropriate one
#if !UNITY_3_5
		{
			Vector2 min = b.min;
			Vector2 max = b.max;
 †⼠ ऀऀऀ洀椀渀⸀砀 ⴀ㴀 ㌀　昀㬀ഀ਀†††††††† 			max.x += 30f;ऊउ				min.y -= 30f;
			max.y += 30f;

			Rect rect = new Rect(min.x, min.y, max.x - min.x, max.y - min.y);
 †ऀऀऀ椀昀 ⠀愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀刀漀琀愀琀攀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ匀攀琀䌀甀爀猀漀爀刀攀挀琀⠀爀攀挀琀Ⰰ 䴀漀甀猀攀䌀甀爀猀漀爀⸀刀漀琀愀琀攀䄀爀爀漀眀⤀㬀ഀ਀ऀऀऀ紀ഀ਀ 			else if (actionUnderMouse == Action.Move)
			{
				SetCursorRect(rect, MouseCursor.MoveArrow);
			}
			else if (actionUnderMouse == Action.Scale)
			{
				SetCursorRect(rect, MouseCursor.ScaleArrow);
			}
			else SetCursorRect(rect, MouseCursor.Arrow);
		}
#endif

		switch (type)
		{
			case EventType.Repaint:
			{
				Handles.BeginGUI();
				{
					for (int i = 0; i < 8; ++i)
					{ †ऀऀऀऀऀऀ䐀爀愀眀䬀渀漀戀⠀眀漀爀氀搀倀漀猀嬀椀崀Ⰰ 洀圀椀搀最攀琀⸀瀀椀瘀漀琀 㴀㴀 洀倀椀瘀漀琀猀嬀椀崀Ⰰ 椀搀⤀㬀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ䠀愀渀搀氀攀猀⸀䔀渀搀䜀唀䤀⠀⤀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 䔀瘀攀渀琀吀礀瀀攀⸀䴀漀甀猀攀䐀漀眀渀㨀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ洀匀琀愀爀琀䴀漀甀猀攀 㴀 攀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀ洀䄀氀氀漀眀匀攀氀攀挀琀椀漀渀 㴀 琀爀甀攀㬀ഀ਀ഀ਀ऀऀऀऀ椀昀 ⠀攀⸀戀甀琀琀漀渀 㴀㴀 ㄀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀 䜀唀䤀唀琀椀氀椀琀礀⸀欀攀礀戀漀愀爀搀䌀漀渀琀爀漀氀 㴀 椀搀㬀ഀ਀ऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ攀氀猀攀 椀昀 ⠀攀⸀戀甀琀琀漀渀 㴀㴀 　 ☀☀ 愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 ℀㴀 䄀挀琀椀漀渀⸀一漀渀攀 ☀☀ 刀愀礀挀愀猀琀⠀挀漀爀渀攀爀猀Ⰰ 漀甀琀 洀匀琀愀爀琀䐀爀愀最⤀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ洀匀琀愀爀琀倀漀猀 㴀 琀⸀瀀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀ洀匀琀愀爀琀刀漀琀 㴀 琀⸀氀漀挀愀氀刀漀琀愀琀椀漀渀⸀攀甀氀攀爀䄀渀最氀攀猀㬀ഀ਀ऀऀऀऀऀ洀匀琀愀爀琀䐀椀爀 㴀 洀匀琀愀爀琀䐀爀愀最 ⴀ 琀⸀瀀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀ洀匀琀愀爀琀匀挀愀氀攀 㴀 琀⸀氀漀挀愀氀匀挀愀氀攀㬀ഀ਀ऀऀऀऀऀ洀䐀爀愀最倀椀瘀漀琀 㴀 瀀椀瘀漀琀唀渀搀攀爀䴀漀甀猀攀㬀ഀ਀ऀऀऀऀऀ洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀㬀ഀ਀ऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀 䜀唀䤀唀琀椀氀椀琀礀⸀欀攀礀戀漀愀爀搀䌀漀渀琀爀漀氀 㴀 椀搀㬀ഀ਀ऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀उऀऀऀऀ紀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀†††††††† 			case EventType.MouseDrag: †††††ऀऀऀ笀ഀ਀ऀऀऀऀ⼀⼀ 倀爀攀瘀攀渀琀 猀攀氀攀挀琀椀漀渀 漀渀挀攀 琀栀攀 搀爀愀最 漀瀀攀爀愀琀椀漀渀 戀攀最椀渀猀ഀ਀ऀऀऀऀ戀漀漀氀 搀爀愀最匀琀愀爀琀攀搀 㴀 ⠀攀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀 ⴀ 洀匀琀愀爀琀䴀漀甀猀攀⤀⸀洀愀最渀椀琀甀搀攀 㸀 ㌀昀㬀ഀ਀ऀऀऀऀ椀昀 ⠀搀爀愀最匀琀愀爀琀攀搀⤀ 洀䄀氀氀漀眀匀攀氀攀挀琀椀漀渀 㴀 昀愀氀猀攀㬀ഀ਀ഀ਀ऀऀऀऀ椀昀 ⠀䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀㴀 椀搀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ഀ਀ऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀 ℀㴀 䄀挀琀椀漀渀⸀一漀渀攀 簀簀 洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 ℀㴀 䄀挀琀椀漀渀⸀一漀渀攀⤀ഀ਀ऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀ嘀攀挀琀漀爀㌀ 瀀漀猀㬀ഀ਀ഀ਀ऀऀऀऀऀऀ椀昀 ⠀刀愀礀挀愀猀琀⠀挀漀爀渀攀爀猀Ⰰ 漀甀琀 瀀漀猀⤀⤀ഀ਀ऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀一漀渀攀 ☀☀ 洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 ℀㴀 䄀挀琀椀漀渀⸀一漀渀攀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ⼀⼀ 圀愀椀琀 甀渀琀椀氀 琀栀攀 洀漀甀猀攀 洀漀瘀攀猀 戀礀 洀漀爀攀 琀栀愀渀 愀 昀攀眀 瀀椀砀攀氀猀ഀ਀ऀऀऀऀऀऀऀऀ椀昀 ⠀搀爀愀最匀琀愀爀琀攀搀⤀ഀ਀ऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀䴀漀瘀攀⤀ഀ਀ऀऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀऀ洀匀琀愀爀琀倀漀猀 㴀 琀⸀瀀漀猀椀琀椀漀渀㬀ഀ਀उउ											NGUIEditorTools.RegisterUndo("Move widget", t);
									}
									else if (mActionUnderMouse == Action.Rotate)
									{
										mStartRot = t.localRotation.eulerAngles;
										mStartDir = mStartDrag - t.position; †⼠ †ऀऀऀऀऀऀऀऀऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀刀漀琀愀琀攀 眀椀搀最攀琀∀Ⰰ 琀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀऀऀ攀氀猀攀 椀昀 ⠀洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀匀挀愀氀攀⤀ഀ਀ऀऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀऀ洀匀琀愀爀琀倀漀猀 㴀 琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀㬀ഀ਀उ											mStartScale = t.localScale;
										mDragPivot = pivotUnderMouse;
										NGUIEditorTools.RegisterUndo("Scale widget", t);
									}
									mAction = actionUnderMouse; ††††††††ऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ഀ਀ऀऀऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀 ℀㴀 䄀挀琀椀漀渀⸀一漀渀攀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀䴀漀瘀攀⤀ഀ਀ऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀ琀⸀瀀漀猀椀琀椀漀渀 㴀 洀匀琀愀爀琀倀漀猀 ⬀ ⠀瀀漀猀 ⴀ 洀匀琀愀爀琀䐀爀愀最⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ瀀漀猀 㴀 琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ瀀漀猀⸀砀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀吀漀䤀渀琀⠀瀀漀猀⸀砀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ瀀漀猀⸀礀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀吀漀䤀渀琀⠀瀀漀猀⸀礀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀऀ攀氀猀攀 椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀刀漀琀愀琀攀⤀ഀ਀ऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀ嘀攀挀琀漀爀㌀ 搀椀爀 㴀 瀀漀猀 ⴀ 琀⸀瀀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ昀氀漀愀琀 愀渀最氀攀 㴀 嘀攀挀琀漀爀㌀⸀䄀渀最氀攀⠀洀匀琀愀爀琀䐀椀爀Ⰰ 搀椀爀⤀㬀ഀ਀ഀ਀ 									if (angle > 0f)
									{ ऀऀऀऀऀऀऀऀऀऀ昀氀漀愀琀 搀漀琀 㴀 嘀攀挀琀漀爀㌀⸀䐀漀琀⠀嘀攀挀琀漀爀㌀⸀䌀爀漀猀猀⠀洀匀琀愀爀琀䐀椀爀Ⰰ 搀椀爀⤀Ⰰ 琀⸀昀漀爀眀愀爀搀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ椀昀 ⠀搀漀琀 㰀 　昀⤀ 愀渀最氀攀 㴀 ⴀ愀渀最氀攀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ愀渀最氀攀 㴀 洀匀琀愀爀琀刀漀琀⸀稀 ⬀ 愀渀最氀攀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ椀昀 ⠀攀⸀洀漀搀椀昀椀攀爀猀 ℀㴀 䔀瘀攀渀琀䴀漀搀椀昀椀攀爀猀⸀匀栀椀昀琀⤀ 愀渀最氀攀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀⠀愀渀最氀攀 ⼀ ㄀㔀昀⤀ ⨀ ㄀㔀昀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ攀氀猀攀 愀渀最氀攀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀⠀愀渀最氀攀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀刀漀琀愀琀椀漀渀 㴀 儀甀愀琀攀爀渀椀漀渀⸀䔀甀氀攀爀⠀洀匀琀愀爀琀刀漀琀⸀砀Ⰰ 洀匀琀愀爀琀刀漀琀⸀礀Ⰰ 愀渀最氀攀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀऀ攀氀猀攀 椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀匀挀愀氀攀⤀ഀ਀ऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀ⼀⼀ 圀漀爀氀搀ⴀ猀瀀愀挀攀 搀攀氀琀愀 猀椀渀挀攀 琀栀攀 搀爀愀最 猀琀愀爀琀攀搀ഀ਀ऀऀऀऀऀऀऀऀऀ嘀攀挀琀漀爀㌀ 搀攀氀琀愀 㴀 瀀漀猀 ⴀ 洀匀琀愀爀琀䐀爀愀最㬀ഀ਀ഀ਀ऀऀऀऀऀऀऀऀऀ⼀⼀ 䄀搀樀甀猀琀 琀栀攀 眀椀搀最攀琀✀猀 瀀漀猀椀琀椀漀渀 愀渀搀 猀挀愀氀攀 戀愀猀攀搀 漀渀 琀栀攀 搀攀氀琀愀Ⰰ 爀攀猀琀爀椀挀琀攀搀 戀礀 琀栀攀 瀀椀瘀漀琀ഀ਀ऀऀऀऀऀऀऀऀऀ䄀搀樀甀猀琀倀漀猀䄀渀搀匀挀愀氀攀⠀洀圀椀搀最攀琀Ⰰ 洀匀琀愀爀琀倀漀猀Ⰰ 洀匀琀愀爀琀匀挀愀氀攀Ⰰ 搀攀氀琀愀Ⰰ 洀䐀爀愀最倀椀瘀漀琀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀ紀ഀ਀††⼯†ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 䔀瘀攀渀琀吀礀瀀攀⸀䴀漀甀猀攀唀瀀㨀ഀ਀ऀऀऀ笀ഀ਀†ऀऀऀऀ椀昀 ⠀䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀㴀 椀搀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀欀攀礀戀漀愀爀搀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ഀ਀ऀऀऀऀऀ椀昀 ⠀攀⸀戀甀琀琀漀渀 㰀 ㈀⤀ഀ਀ऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀ戀漀漀氀 栀愀渀搀氀攀搀 㴀 昀愀氀猀攀㬀ഀ਀ഀ਀ऀऀऀऀऀऀ椀昀 ⠀攀⸀戀甀琀琀漀渀 㴀㴀 ㄀⤀ഀ਀ऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀ⼀⼀ 刀椀最栀琀ⴀ挀氀椀挀欀㨀 匀攀氀攀挀琀 琀栀攀 眀椀搀最攀琀 戀攀氀漀眀ഀ਀ऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀 氀愀猀琀 㴀 渀甀氀氀㬀ഀ਀ऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀嬀崀 眀椀搀最攀琀猀 㴀 刀愀礀挀愀猀琀⠀洀圀椀搀最攀琀Ⰰ 攀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀⤀㬀ഀ਀ഀ਀ऀऀऀऀऀऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 眀椀搀最攀琀猀⸀䰀攀渀最琀栀㬀 椀 㸀 　㬀 ⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀 眀 㴀 眀椀搀最攀琀猀嬀ⴀⴀ椀崀㬀ഀ਀††††††ऀऀऀऀऀऀऀऀ椀昀 ⠀眀 㴀㴀 洀圀椀搀最攀琀⤀ 戀爀攀愀欀㬀ഀ਀ऀऀऀऀऀऀऀऀ氀愀猀琀 㴀 眀㬀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀†††††††ഀ਀ऀऀऀऀऀऀऀ椀昀 ⠀氀愀猀琀 ℀㴀 渀甀氀氀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 氀愀猀琀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀††††††† 								handled = true;
							}
						} ††† 						else if (mAction == Action.None)
						{
							if (mAllowSelection)
							{
								// Left-click: Select the widget above
								UIWidget last = null;
								UIWidget[] widgets = Raycast(mWidget, e.mousePosition);

								if (widgets.Length > 0)
								{
									for (int i = 0; i < widgets.Length; ++i)
									{
										UIWidget w = widgets[i];

										if (w == mWidget) ††††††ऀऀऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀऀऀ椀昀 ⠀氀愀猀琀 ℀㴀 渀甀氀氀⤀ 匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 氀愀猀琀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀†ऀऀऀऀऀऀऀऀऀऀऀ栀愀渀搀氀攀搀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀऀ戀爀攀愀欀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀऀऀऀ氀愀猀琀 㴀 眀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ紀ഀ਀ഀ਀ऀऀऀऀऀऀऀऀऀ椀昀 ⠀℀栀愀渀搀氀攀搀⤀ഀ਀ऀऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀऀ匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 眀椀搀最攀琀猀嬀　崀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ栀愀渀搀氀攀搀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀऀ紀ഀ਀उउ								}
						}
						else
						{
							// Finished dragging something
							mAction = Action.None;
							mActionUnderMouse = Action.None; ††ऀऀऀऀऀऀऀ嘀攀挀琀漀爀㌀ 瀀漀猀 㴀 琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀऀऀ嘀攀挀琀漀爀㌀ 猀挀愀氀攀 㴀 琀⸀氀漀挀愀氀匀挀愀氀攀㬀ഀ਀ഀ਀ऀऀऀऀऀऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀瀀椀砀攀氀倀攀爀昀攀挀琀䄀昀琀攀爀刀攀猀椀稀攀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀匀挀愀氀攀 㴀 猀挀愀氀攀㬀ഀ਀ഀ਀††††††† 								mWidget.MakePixelPerfect();
							}
							else
							{
								pos.x = Mathf.Round(pos.x);
								pos.y = Mathf.Round(pos.y);
								scale.x = Mathf.Round(scale.x); ††††††† 								scale.y = Mathf.Round(scale.y);

								t.localPosition = pos;
								t.localScale = scale;
							}
							handled = true;
						}

						if (handled)
						{
							mActionUnderMouse = Action.None;
							mAction = Action.None;
							e.Use();
						}
					}
				}
				else if (mAllowSelection) ††††††††ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ唀䤀圀椀搀最攀琀嬀崀 眀椀搀最攀琀猀 㴀 刀愀礀挀愀猀琀⠀洀圀椀搀最攀琀Ⰰ 攀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀⤀㬀ഀ਀ऀऀऀऀऀ椀昀 ⠀眀椀搀最攀琀猀⸀䰀攀渀最琀栀 㸀 　⤀ 匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 眀椀搀最攀琀猀嬀　崀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ洀䄀氀氀漀眀匀攀氀攀挀琀椀漀渀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 䔀瘀攀渀琀吀礀瀀攀⸀䬀攀礀䐀漀眀渀㨀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ椀昀 ⠀攀⸀欀攀礀䌀漀搀攀 㴀㴀 䬀攀礀䌀漀搀攀⸀唀瀀䄀爀爀漀眀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ嘀攀挀琀漀爀㌀ 瀀漀猀 㴀 琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀ瀀漀猀⸀礀 ⬀㴀 ㄀昀㬀ഀ਀ऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ攀氀猀攀 椀昀 ⠀攀⸀欀攀礀䌀漀搀攀 㴀㴀 䬀攀礀䌀漀搀攀⸀䐀漀眀渀䄀爀爀漀眀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ嘀攀挀琀漀爀㌀ 瀀漀猀 㴀 琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀㬀ഀ਀†††††††† 					pos.y -= 1f;
					t.localPosition = pos;
					e.Use();
				} ⼯† 				else if (e.keyCode == KeyCode.LeftArrow)
				{
					Vector3 pos = t.localPosition;
					pos.x -= 1f;
					t.localPosition = pos;
					e.Use();
				}
				else if (e.keyCode == KeyCode.RightArrow)
				{
					Vector3 pos = t.localPosition;
					pos.x += 1f;ऊउऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ攀氀猀攀 椀昀 ⠀攀⸀欀攀礀䌀漀搀攀 㴀㴀 䬀攀礀䌀漀搀攀⸀䔀猀挀愀瀀攀⤀ഀ਀††ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ椀昀 ⠀䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀㴀 椀搀⤀ഀ਀ऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀 ℀㴀 䄀挀琀椀漀渀⸀一漀渀攀⤀ഀ਀ऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀䴀漀瘀攀⤀ഀ਀†⼯†ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀瀀漀猀椀琀椀漀渀 㴀 洀匀琀愀爀琀倀漀猀㬀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀ攀氀猀攀 椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀刀漀琀愀琀攀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀刀漀琀愀琀椀漀渀 㴀 儀甀愀琀攀爀渀椀漀渀⸀䔀甀氀攀爀⠀洀匀琀愀爀琀刀漀琀⤀㬀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀ攀氀猀攀 椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀匀挀愀氀攀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀瀀漀猀椀琀椀漀渀 㴀 洀匀琀愀爀琀倀漀猀㬀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀匀挀愀氀攀 㴀 洀匀琀愀爀琀匀挀愀氀攀㬀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀ紀ഀ਀ഀ਀††ऀऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ऀऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀欀攀礀戀漀愀爀搀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ഀ਀ऀऀऀऀऀऀ洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀऀऀऀऀऀ洀䄀挀琀椀漀渀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀ攀氀猀攀ഀ਀ऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀ匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 渀甀氀氀㬀ഀ਀ऀऀऀऀऀऀ吀漀漀氀猀⸀挀甀爀爀攀渀琀 㴀 吀漀漀氀⸀䴀漀瘀攀㬀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ऀऀ紀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 䄀搀樀甀猀琀 琀栀攀 琀爀愀渀猀昀漀爀洀✀猀 瀀漀猀椀琀椀漀渀 愀渀搀 猀挀愀氀攀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 瘀漀椀搀 䄀搀樀甀猀琀倀漀猀䄀渀搀匀挀愀氀攀 ⠀唀䤀圀椀搀最攀琀 眀Ⰰ 嘀攀挀琀漀爀㌀ 猀琀愀爀琀䰀漀挀愀氀倀漀猀Ⰰ 嘀攀挀琀漀爀㌀ 猀琀愀爀琀䰀漀挀愀氀匀挀愀氀攀Ⰰ 嘀攀挀琀漀爀㌀ 眀漀爀氀搀䐀攀氀琀愀Ⰰ 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 搀爀愀最倀椀瘀漀琀⤀ഀ਀ऀ笀ഀ਀ऀऀ吀爀愀渀猀昀漀爀洀 琀 㴀 眀⸀挀愀挀栀攀搀吀爀愀渀猀昀漀爀洀㬀ഀ਀ऀऀ吀爀愀渀猀昀漀爀洀 瀀愀爀攀渀琀 㴀 琀⸀瀀愀爀攀渀琀㬀ഀ਀ऀऀ䴀愀琀爀椀砀㐀砀㐀 瀀愀爀攀渀琀吀漀䰀漀挀愀氀 㴀 ⠀瀀愀爀攀渀琀 ℀㴀 渀甀氀氀⤀ 㼀 琀⸀瀀愀爀攀渀琀⸀眀漀爀氀搀吀漀䰀漀挀愀氀䴀愀琀爀椀砀 㨀 䴀愀琀爀椀砀㐀砀㐀⸀椀搀攀渀琀椀琀礀㬀ഀ਀ऀऀ䴀愀琀爀椀砀㐀砀㐀 眀漀爀氀搀吀漀䰀漀挀愀氀 㴀 瀀愀爀攀渀琀吀漀䰀漀挀愀氀㬀ഀ਀ऀऀ儀甀愀琀攀爀渀椀漀渀 椀渀瘀刀漀琀 㴀 儀甀愀琀攀爀渀椀漀渀⸀䤀渀瘀攀爀猀攀⠀琀⸀氀漀挀愀氀刀漀琀愀琀椀漀渀⤀㬀ഀ਀ऀऀ眀漀爀氀搀吀漀䰀漀挀愀氀 㴀 眀漀爀氀搀吀漀䰀漀挀愀氀 ⨀ 䴀愀琀爀椀砀㐀砀㐀⸀吀刀匀⠀嘀攀挀琀漀爀㌀⸀稀攀爀漀Ⰰ 椀渀瘀刀漀琀Ⰰ 嘀攀挀琀漀爀㌀⸀漀渀攀⤀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㌀ 氀漀挀愀氀䐀攀氀琀愀 㴀 眀漀爀氀搀吀漀䰀漀挀愀氀⸀䴀甀氀琀椀瀀氀礀嘀攀挀琀漀爀⠀眀漀爀氀搀䐀攀氀琀愀⤀㬀ഀ਀ഀ਀ऀऀ戀漀漀氀 挀愀渀䈀攀匀焀甀愀爀攀 㴀 昀愀氀猀攀㬀ഀ਀ऀऀ昀氀漀愀琀 氀攀昀琀 㴀 　昀㬀ഀ਀ऀऀ昀氀漀愀琀 爀椀最栀琀 㴀 　昀㬀ഀ਀†⼯†ऀऀ昀氀漀愀琀 琀漀瀀 㴀 　昀㬀ഀ਀ऀऀ昀氀漀愀琀 戀漀琀琀漀洀 㴀 　昀㬀ഀ਀ഀ਀ऀऀ猀眀椀琀挀栀 ⠀搀爀愀最倀椀瘀漀琀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀䰀攀昀琀㨀ഀ਀ऀऀऀ挀愀渀䈀攀匀焀甀愀爀攀 㴀 ⠀眀⸀瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀刀椀最栀琀⤀㬀ഀ਀ऀऀऀ氀攀昀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀ऀऀऀ琀漀瀀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䰀攀昀琀㨀ഀ਀ऀऀऀ氀攀昀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀䰀攀昀琀㨀ഀ਀ऀऀऀ挀愀渀䈀攀匀焀甀愀爀攀 㴀 ⠀眀⸀瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀⤀㬀ഀ਀ऀऀऀ氀攀昀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀††⼠ †ऀऀऀ戀漀琀琀漀洀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀㨀ഀ਀ऀऀऀ琀漀瀀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀㨀ഀ਀ऀऀऀ戀漀琀琀漀洀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀㨀ഀ਀ऀऀऀ挀愀渀䈀攀匀焀甀愀爀攀 㴀 ⠀眀⸀瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀䰀攀昀琀⤀㬀ഀ਀ऀऀऀ挀愀渀䈀攀匀焀甀愀爀攀 㴀 ⠀眀⸀瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀䰀攀昀琀⤀㬀ഀ਀ऀऀऀ爀椀最栀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀ऀऀऀ琀漀瀀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀㨀ഀ਀ऀऀऀ爀椀最栀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀刀椀最栀琀㨀ഀ਀ऀऀऀ挀愀渀䈀攀匀焀甀愀爀攀 㴀 ⠀眀⸀瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀䰀攀昀琀⤀㬀ഀ਀				right = localDelta.x;
			bottom = localDelta.y;
			break;
		}
 †††ऀऀ䄀搀樀甀猀琀圀椀搀最攀琀⠀眀Ⰰ 猀琀愀爀琀䰀漀挀愀氀倀漀猀Ⰰ 猀琀愀爀琀䰀漀挀愀氀匀挀愀氀攀Ⰰ 氀攀昀琀Ⰰ 琀漀瀀Ⰰ 爀椀最栀琀Ⰰ 戀漀琀琀漀洀Ⰰ 挀愀渀䈀攀匀焀甀愀爀攀 ☀☀ 䔀瘀攀渀琀⸀挀甀爀爀攀渀琀⸀洀漀搀椀昀椀攀爀猀 㴀㴀 䔀瘀攀渀琀䴀漀搀椀昀椀攀爀猀⸀匀栀椀昀琀⤀㬀ഀ਀ऀ紀ഀ਀⼠ ऀഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 䄀搀樀甀猀琀 琀栀攀 眀椀搀最攀琀✀猀 爀攀挀琀愀渀最氀攀 戀愀猀攀搀 漀渀 琀栀攀 猀瀀攀挀椀昀椀攀搀 洀漀搀椀昀椀攀爀 瘀愀氀甀攀猀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 瘀漀椀搀 䄀搀樀甀猀琀圀椀搀最攀琀 ⠀唀䤀圀椀搀最攀琀 眀Ⰰ 嘀攀挀琀漀爀㌀ 瀀漀猀Ⰰ 嘀攀挀琀漀爀㌀ 猀挀愀氀攀Ⰰ 昀氀漀愀琀 氀攀昀琀Ⰰ 昀氀漀愀琀 琀漀瀀Ⰰ 昀氀漀愀琀 爀椀最栀琀Ⰰ 昀氀漀愀琀 戀漀琀琀漀洀Ⰰ 戀漀漀氀 洀愀欀攀匀焀甀愀爀攀⤀ഀ਀††††††† 	{
		Vector2 offset = w.pivotOffset;
		Vector4 padding = w.relativePadding;
		Vector2 size = w.relativeSize;

		offset.x -= padding.x; ††††††ऀऀ漀昀昀猀攀琀⸀礀 ⴀ㴀 瀀愀搀搀椀渀最⸀礀㬀ഀ਀ऀऀ猀椀稀攀⸀砀 ⬀㴀 瀀愀搀搀椀渀最⸀砀 ⬀ 瀀愀搀搀椀渀最⸀稀㬀ഀ਀ऀऀ猀椀稀攀⸀礀 ⬀㴀 瀀愀搀搀椀渀最⸀礀 ⬀ 瀀愀搀搀椀渀最⸀眀㬀ഀ਀ऀऀഀ਀ऀऀ猀挀愀氀攀⸀匀挀愀氀攀⠀猀椀稀攀⤀㬀ഀ਀ഀ਀ऀऀ漀昀昀猀攀琀⸀礀 㴀 ⴀ漀昀昀猀攀琀⸀礀㬀ഀ਀ഀ਀ऀऀ吀爀愀渀猀昀漀爀洀 琀 㴀 眀⸀挀愀挀栀攀搀吀爀愀渀猀昀漀爀洀㬀ഀ਀ऀऀ儀甀愀琀攀爀渀椀漀渀 爀漀琀 㴀 琀⸀氀漀挀愀氀刀漀琀愀琀椀漀渀㬀ഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀 㴀 眀⸀瀀椀瘀漀琀㬀ഀ਀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀吀䰀 㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀氀攀昀琀Ⰰ 琀漀瀀⤀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀吀刀 㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀爀椀最栀琀Ⰰ 琀漀瀀⤀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀䈀䰀 㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀氀攀昀琀Ⰰ 戀漀琀琀漀洀⤀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀䈀刀 㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀爀椀最栀琀Ⰰ 戀漀琀琀漀洀⤀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀䰀  㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀氀攀昀琀Ⰰ 　昀⤀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀刀  㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀爀椀最栀琀Ⰰ 　昀⤀㬀ഀ਀†ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀吀  㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀　昀Ⰰ 琀漀瀀⤀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 爀漀琀愀琀攀搀䈀  㴀 渀攀眀 嘀攀挀琀漀爀㈀⠀　昀Ⰰ 戀漀琀琀漀洀⤀㬀ഀ਀ऀऀഀ਀ऀऀ爀漀琀愀琀攀搀吀䰀 㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀吀䰀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀吀刀 㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀吀刀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀䈀䰀 㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䈀䰀㬀ഀ਀†ऀऀ爀漀琀愀琀攀搀䈀刀 㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䈀刀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀䰀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䰀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀刀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀刀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀吀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀吀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀䈀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䈀㬀ഀ਀ഀ਀ऀऀ猀眀椀琀挀栀 ⠀瀀椀瘀漀琀⤀ഀ਀ऀऀ笀ഀ਀ऀऀ笀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀䰀攀昀琀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀吀䰀⸀砀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀吀䰀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀刀椀最栀琀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀䈀刀⸀砀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀䈀刀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀䰀攀昀琀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀䈀䰀⸀砀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀䈀䰀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀吀刀⸀砀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀吀刀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀उऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䰀攀昀琀㨀ഀ਀				pos.x += rotatedL.x + (rotatedT.x + rotatedB.x) * 0.5f;
			pos.y += rotatedL.y + (rotatedT.y + rotatedB.y) * 0.5f;
			break;

			case UIWidget.Pivot.Right:
			pos.x += rotatedR.x + (rotatedT.x + rotatedB.x) * 0.5f;
			pos.y += rotatedR.y + (rotatedT.y + rotatedB.y) * 0.5f;
			break;

			case UIWidget.Pivot.Top:
			pos.x += rotatedT.x + (rotatedL.x + rotatedR.x) * 0.5f;
			pos.y += rotatedT.y + (rotatedL.y + rotatedR.y) * 0.5f;
			break;

			case UIWidget.Pivot.Bottom:
			pos.x += rotatedB.x + (rotatedL.x + rotatedR.x) * 0.5f;
			pos.y += rotatedB.y + (rotatedL.y + rotatedR.y) * 0.5f;
			break;

			case UIWidget.Pivot.Center:
			pos.x += (rotatedL.x + rotatedR.x + rotatedT.x + rotatedB.x) * 0.5f;
			pos.y += (rotatedT.y + rotatedB.y + rotatedL.y + rotatedR.y) * 0.5f;
			break;
		}

		scale.x -= left - right;
		scale.y += top - bottom; †††† 
		scale.x /= size.x;
		scale.y /= size.y;

		Vector4 border = w.border;
		float minx = Mathf.Max(2f, padding.x + padding.z + border.x + border.z);
		float miny = Mathf.Max(2f, padding.y + padding.w + border.y + border.w);

		if (scale.x < minx) scale.x = minx;
		if (scale.y < miny) scale.y = miny;

		// NOTE: This will only work correctly when dragging the corner opposite of the pivot point
		if (makeSquare)
		{
			scale.x = Mathf.Min(scale.x, scale.y);
			scale.y = scale.x;
		}

		t.localPosition = pos;
		t.localScale = scale;
	}

	/// <summary>
	/// Draw the inspector widget. † 	/// </summary>

	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		mWidget = target as UIWidget;

		if (!mInitialized)
		{
			mInitialized = true;
			OnInit();
		}

		//NGUIEditorTools.DrawSeparator();
		EditorGUILayout.Space();

		// Check to see if we can draw the widget's default properties to begin with
		if (DrawProperties())
		{
			// Draw all common properties next
			DrawCommonProperties();
			DrawExtraProperties();
		}
	}

	/// <summary>
	/// All widgets have depth, color and make pixel-perfect options
	/// </summary>

	protected void DrawCommonProperties ()ऊ		{
#if UNITY_3_4ऊ			PrefabType type = EditorUtility.GetPrefabType(mWidget.gameObject);
#else
		PrefabType type = PrefabUtility.GetPrefabType(mWidget.gameObject);
#endif

		NGUIEditorTools.DrawSeparator();

#if UNITY_3_5
		// Pivot point -- old school drop-down style †⼯†ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀 㴀 ⠀唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⤀䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀䔀渀甀洀倀漀瀀甀瀀⠀∀倀椀瘀漀琀∀Ⰰ 洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀 ℀㴀 瀀椀瘀漀琀⤀ഀ਀ऀऀ笀ഀ਀† 		    NGUIEditorTools.RegisterUndo("Pivot Change", mWidget);
		    mWidget.pivot = pivot;
		}
#else
		// Pivot point -- the new, more visual style
		GUILayout.BeginHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Label("Pivot", GUILayout.Width(76f));
		Toggle("◄", "ButtonLeft", UIWidget.Pivot.Left, true);
		Toggle("▬", "ButtonMid", UIWidget.Pivot.Center, true);
		Toggle("►", "ButtonRight", UIWidget.Pivot.Right, true);
		Toggle("▲", "ButtonLeft", UIWidget.Pivot.Top, false);
		Toggle("▌", "ButtonMid", UIWidget.Pivot.Center, false);
		Toggle("▼", "ButtonRight", UIWidget.Pivot.Bottom, false);
		GUILayout.EndHorizontal();
#endif

		// Depth navigation
		if (type != PrefabType.Prefab)ऊउ			{
			GUILayout.Space(2f); ††††††††ऀऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䈀攀最椀渀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀倀爀攀昀椀砀䰀愀戀攀氀⠀∀䐀攀瀀琀栀∀⤀㬀ഀ਀ഀ਀ऀऀऀऀ椀渀琀 搀攀瀀琀栀 㴀 洀圀椀搀最攀琀⸀搀攀瀀琀栀㬀ഀ਀ऀऀऀऀ椀昀 ⠀䜀唀䤀䰀愀礀漀甀琀⸀䈀甀琀琀漀渀⠀∀䈀愀挀欀∀Ⰰ 䜀唀䤀䰀愀礀漀甀琀⸀圀椀搀琀栀⠀㘀　昀⤀⤀⤀ ⴀⴀ搀攀瀀琀栀㬀ഀ਀ऀऀऀऀ搀攀瀀琀栀 㴀 䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀䤀渀琀䘀椀攀氀搀⠀搀攀瀀琀栀⤀㬀ഀ਀ऀऀऀऀ椀昀 ⠀䜀唀䤀䰀愀礀漀甀琀⸀䈀甀琀琀漀渀⠀∀䘀漀爀眀愀爀搀∀Ⰰ 䜀唀䤀䰀愀礀漀甀琀⸀圀椀搀琀栀⠀㘀　昀⤀⤀⤀ ⬀⬀搀攀瀀琀栀㬀ഀ਀ഀ਀ऀऀऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀搀攀瀀琀栀 ℀㴀 搀攀瀀琀栀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀䐀攀瀀琀栀 䌀栀愀渀最攀∀Ⰰ 洀圀椀搀最攀琀⤀㬀ഀ਀ऀऀऀऀऀ洀圀椀搀最攀琀⸀搀攀瀀琀栀 㴀 搀攀瀀琀栀㬀ഀ਀ऀऀऀऀऀ洀䐀攀瀀琀栀䌀栀攀挀欀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀऀ紀ഀ਀†ऀऀऀ紀ഀ਀ऀऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䔀渀搀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ഀ਀ऀऀऀ唀䤀倀愀渀攀氀 瀀愀渀攀氀 㴀 洀圀椀搀最攀琀⸀瀀愀渀攀氀㬀ഀ਀ഀ਀ऀऀऀ椀昀 ⠀瀀愀渀攀氀 ℀㴀 渀甀氀氀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ椀渀琀 挀漀甀渀琀 㴀 　㬀ഀ਀ഀ਀ऀऀऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 瀀愀渀攀氀⸀眀椀搀最攀琀猀⸀猀椀稀攀㬀 ⬀⬀椀⤀ഀ਀ऀऀऀऀ笀ഀ਀††† 					UIWidget w = panel.widgets[i];
					if (w != null && w.depth == mWidget.depth && w.material == mWidget.material) ++count;
				}

				if (count > 1)
				{
					EditorGUILayout.HelpBox(count + " widgets are using the depth value of " + mWidget.depth +
						". It may not be clear what should be in front of what.", MessageType.Warning);
				}

				if (mDepthCheck)
				{
					if (panel.drawCalls.size > 1)
					{
						EditorGUILayout.HelpBox("The widgets underneath this panel are using more than one atlas. You may need to adjust transform position's Z value instead. When adjusting the Z, lower value means closer to the camera.", MessageType.Warning);
					}
				}
			}
		}

		// Pixel-correctness
		if (type != PrefabType.Prefab)
		{
			GUILayout.BeginHorizontal();
			{
				EditorGUILayout.PrefixLabel("Correction");

				if (GUILayout.Button("Make Pixel-Perfect"))
				{
					NGUIEditorTools.RegisterUndo("Make Pixel-Perfect", mWidget.transform);
					mWidget.MakePixelPerfect();
				}
			}
			GUILayout.EndHorizontal();
		}

		//NGUIEditorTools.DrawSeparator();
		EditorGUILayout.Space();

		// Color tint
		GUILayout.BeginHorizontal();
		Color color = EditorGUILayout.ColorField("Color Tint", mWidget.color);
		if (GUILayout.Button("Copy", GUILayout.Width(50f)))
		if (GUILayout.Button("Copy", GUILayout.Width(50f)))
			NGUISettings.color = color;
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		NGUISettings.color = EditorGUILayout.ColorField("Clipboard", NGUISettings.color);  		if (GUILayout.Button("Paste", GUILayout.Width(50f)))
			color = NGUISettings.color;
		GUILayout.EndHorizontal();
ऊऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀挀漀氀漀爀 ℀㴀 挀漀氀漀爀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀䌀漀氀漀爀 䌀栀愀渀最攀∀Ⰰ 洀圀椀搀最攀琀⤀㬀ഀ਀ऀऀऀ洀圀椀搀最攀琀⸀挀漀氀漀爀 㴀 挀漀氀漀爀㬀ഀ਀ऀऀ紀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀उउऀ⼀⼀⼀ 䐀爀愀眀 愀 琀漀最最氀攀 戀甀琀琀漀渀 昀漀爀 琀栀攀 瀀椀瘀漀琀 瀀漀椀渀琀⸀ഀ਀†⼠/	/// </summary>
 †⼯† 	void Toggle (string text, string style, UIWidget.Pivot pivot, bool isHorizontal)
	{
		bool isActive = false;

		switch (pivot)
		{
			case UIWidget.Pivot.Left:
			isActive = IsLeft(mWidget.pivot);
			break;

			case UIWidget.Pivot.Right:
			isActive = IsRight(mWidget.pivot);
			break;

			case UIWidget.Pivot.Top:
			isActive = IsTop(mWidget.pivot);
			break;

			case UIWidget.Pivot.Bottom:
			isActive = IsBottom(mWidget.pivot);
			break; †††††††  †††† 			case UIWidget.Pivot.Center:
			isActive = isHorizontal ? pivot == GetHorizontal(mWidget.pivot) : pivot == GetVertical(mWidget.pivot);
			break;
		}

		if (GUILayout.Toggle(isActive, text, style) != isActive)
			SetPivot(pivot, isHorizontal);
	}

	static bool IsLeft (UIWidget.Pivot pivot)
	{
		return pivot == UIWidget.Pivot.Left ||
			pivot == UIWidget.Pivot.TopLeft ||
			pivot == UIWidget.Pivot.BottomLeft;
	}

	static bool IsRight (UIWidget.Pivot pivot)
	{
		return pivot == UIWidget.Pivot.Right ||
			pivot == UIWidget.Pivot.TopRight || ⼯† 			pivot == UIWidget.Pivot.BottomRight;
	}

	static bool IsTop (UIWidget.Pivot pivot) †⼯ 	{
		return pivot == UIWidget.Pivot.Top ||
			pivot == UIWidget.Pivot.TopLeft ||
			pivot == UIWidget.Pivot.TopRight;
	}

	static bool IsBottom (UIWidget.Pivot pivot)
	{
		return pivot == UIWidget.Pivot.Bottom ||
			pivot == UIWidget.Pivot.BottomLeft || †⼯ 			pivot == UIWidget.Pivot.BottomRight;
	}

	static UIWidget.Pivot GetHorizontal (UIWidget.Pivot pivot)
	{
		if (IsLeft(pivot)) return UIWidget.Pivot.Left;ऊऀऀ椀昀 ⠀䤀猀刀椀最栀琀⠀瀀椀瘀漀琀⤀⤀ 爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀㬀ഀ਀ऀऀ爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䌀攀渀琀攀爀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 䜀攀琀嘀攀爀琀椀挀愀氀 ⠀唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀⤀ഀ਀ऀ笀ഀ਀ऀऀ椀昀 ⠀䤀猀䈀漀琀琀漀洀⠀瀀椀瘀漀琀⤀⤀ 爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀㬀ഀ਀ऀऀ爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䌀攀渀琀攀爀㬀ഀ਀ऀ紀ഀ਀††††††ഀ਀††††ऀ猀琀愀琀椀挀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 䌀漀洀戀椀渀攀 ⠀唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 栀漀爀椀稀漀渀琀愀氀Ⰰ 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瘀攀爀琀椀挀愀氀⤀ഀ਀ऀ笀ഀ਀ऀऀ椀昀 ⠀栀漀爀椀稀漀渀琀愀氀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䰀攀昀琀⤀ഀ਀†††† 		{
			if (vertical == UIWidget.Pivot.Top) return UIWidget.Pivot.TopLeft;ऊउउऀऀऀ椀昀 ⠀瘀攀爀琀椀挀愀氀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀⤀ 爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀䰀攀昀琀㬀ഀ਀ऀऀऀ爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䰀攀昀琀㬀ഀ਀ऀऀ紀ഀ਀ഀ਀ऀऀ椀昀 ⠀栀漀爀椀稀漀渀琀愀氀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀⤀ഀ਀ऀऀ笀ഀ਀ऀऀ笀ഀ਀ऀऀऀ椀昀 ⠀瘀攀爀琀椀挀愀氀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀⤀ 爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀㬀ഀ਀ऀऀऀ椀昀 ⠀瘀攀爀琀椀挀愀氀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀⤀ 爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀㬀ഀ਀ऀऀऀ椀昀 ⠀瘀攀爀琀椀挀愀氀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀⤀ 爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀刀椀最栀琀㬀ഀ਀ऀऀऀ爀攀琀甀爀渀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀㬀ഀ਀ऀऀ紀ഀ਀ऀऀ爀攀琀甀爀渀 瘀攀爀琀椀挀愀氀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ瘀漀椀搀 匀攀琀倀椀瘀漀琀 ⠀唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀Ⰰ 戀漀漀氀 椀猀䠀漀爀椀稀漀渀琀愀氀⤀ഀ਀ऀ笀ഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 栀漀爀椀稀漀渀琀愀氀 㴀 䜀攀琀䠀漀爀椀稀漀渀琀愀氀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瘀攀爀琀椀挀愀氀 㴀 䜀攀琀嘀攀爀琀椀挀愀氀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ഀ਀ऀऀ瀀椀瘀漀琀 㴀 椀猀䠀漀爀椀稀漀渀琀愀氀 㼀 䌀漀洀戀椀渀攀⠀瀀椀瘀漀琀Ⰰ 瘀攀爀琀椀挀愀氀⤀ 㨀 䌀漀洀戀椀渀攀⠀栀漀爀椀稀漀渀琀愀氀Ⰰ 瀀椀瘀漀琀⤀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀 ℀㴀 瀀椀瘀漀琀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀倀椀瘀漀琀 挀栀愀渀最攀∀Ⰰ 洀圀椀搀最攀琀⤀㬀ഀ਀ऀऀऀ洀圀椀搀最攀琀⸀瀀椀瘀漀琀 㴀 瀀椀瘀漀琀㬀ഀ਀ऀऀ紀ഀ਀ऀ紀ഀ਀†††††† 
	/// <summary>
	/// Any and all derived functionality.
	/// </summary>

	protected virtual void OnInit() { }
	protected virtual bool DrawProperties () { return true; }
	protected virtual void DrawExtraProperties () { }
}
