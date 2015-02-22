//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2013 Tasharen Entertainment †⼀⼀ⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀⴀഀ਀ഀ਀甀猀椀渀最 唀渀椀琀礀䔀渀最椀渀攀㬀ഀ਀甀猀椀渀最 唀渀椀琀礀䔀搀椀琀漀爀㬀ഀ਀甀猀椀渀最 匀礀猀琀攀洀⸀䌀漀氀氀攀挀琀椀漀渀猀⸀䜀攀渀攀爀椀挀㬀ഀ਀ഀ਀⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀⼀⼀⼀ 䤀渀猀瀀攀挀琀漀爀 挀氀愀猀猀 甀猀攀搀 琀漀 攀搀椀琀 唀䤀圀椀搀最攀琀猀⸀ഀ਀⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀嬀䌀甀猀琀漀洀䔀搀椀琀漀爀⠀琀礀瀀攀漀昀⠀唀䤀圀椀搀最攀琀⤀⤀崀ഀ਀瀀甀戀氀椀挀 挀氀愀猀猀 唀䤀圀椀搀最攀琀䤀渀猀瀀攀挀琀漀爀 㨀 䔀搀椀琀漀爀ഀ਀笀ഀ਀ऀ攀渀甀洀 䄀挀琀椀漀渀ഀ਀ऀ笀ഀ਀ऀऀ一漀渀攀Ⰰഀ਀ऀऀ䴀漀瘀攀Ⰰഀ਀ऀऀ匀挀愀氀攀Ⰰഀ਀ऀऀ刀漀琀愀琀攀Ⰰഀ਀ऀ紀ഀ਀ഀ਀†⼯ 	Action mAction = Action.None;
	Action mActionUnderMouse = Action.None; † 	bool mAllowSelection = true;

	protected UIWidget mWidget;

	static protected bool mUseShader = false;
	static Color mOutlineColor = Color.green; ⼠  	static GUIStyle mSelectedDot = null;
	static GUIStyle mNormalDot = null;
	static MouseCursor mCursor = MouseCursor.Arrow;

	static UIWidget.Pivot[] mPivots =
	{
		UIWidget.Pivot.TopLeft,
		UIWidget.Pivot.BottomLeft,
		UIWidget.Pivot.BottomRight,
		UIWidget.Pivot.TopRight,
		UIWidget.Pivot.Left,
		UIWidget.Pivot.Bottom,
		UIWidget.Pivot.Right,
		UIWidget.Pivot.Top,
	};

	static int s_Hash = "WidgetHash".GetHashCode();
	Vector3 mStartPos = Vector3.zero; ††††††† 	Vector3 mStartScale = Vector3.zero;
	Vector3 mStartDrag = Vector3.zero;
	Vector2 mStartMouse = Vector2.zero;
	Vector3 mStartRot = Vector3.zero;
	Vector3 mStartDir = Vector3.right;
	UIWidget.Pivot mDragPivot = UIWidget.Pivot.Center;
	bool mInitialized = false;
	bool mDepthCheck = false;

	/// <summary>
	/// Register an Undo command with the Unity editor.
	/// </summary>

	void RegisterUndo ()
	{
		NGUIEditorTools.RegisterUndo("Widget Change", mWidget);
	}

	/// <summary>
	/// Raycast into the screen.
	/// </summary>
 ††††††ऀ猀琀愀琀椀挀 戀漀漀氀 刀愀礀挀愀猀琀 ⠀嘀攀挀琀漀爀㌀嬀崀 挀漀爀渀攀爀猀Ⰰ 漀甀琀 嘀攀挀琀漀爀㌀ 栀椀琀⤀ഀ਀ऀ笀ഀ਀ऀऀ倀氀愀渀攀 瀀氀愀渀攀 㴀 渀攀眀 倀氀愀渀攀⠀挀漀爀渀攀爀猀嬀　崀Ⰰ 挀漀爀渀攀爀猀嬀㄀崀Ⰰ 挀漀爀渀攀爀猀嬀㈀崀⤀㬀ഀ਀ऀऀ刀愀礀 爀愀礀 㴀 䠀愀渀搀氀攀唀琀椀氀椀琀礀⸀䜀唀䤀倀漀椀渀琀吀漀圀漀爀氀搀刀愀礀⠀䔀瘀攀渀琀⸀挀甀爀爀攀渀琀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀⤀㬀ഀ਀ऀऀ昀氀漀愀琀 搀椀猀琀 㴀 　昀㬀ഀ਀ऀऀ戀漀漀氀 椀猀䠀椀琀 㴀 瀀氀愀渀攀⸀刀愀礀挀愀猀琀⠀爀愀礀Ⰰ 漀甀琀 搀椀猀琀⤀㬀ഀ਀ऀऀ栀椀琀 㴀 椀猀䠀椀琀 㼀 爀愀礀⸀䜀攀琀倀漀椀渀琀⠀搀椀猀琀⤀ 㨀 嘀攀挀琀漀爀㌀⸀稀攀爀漀㬀ഀ਀ऀऀ爀攀琀甀爀渀 椀猀䠀椀琀㬀ഀ਀ऀ紀ഀ਀ഀ਀††† 	/// <summary>
	/// Draw a control dot at the specified world position.
	/// </summary>

	static void DrawKnob (Vector3 point, bool selected, int id)
	{
		if (mSelectedDot == null) mSelectedDot = "sv_label_5";
		if (mNormalDot == null) mNormalDot = "sv_label_3";

		Vector2 screenPoint = HandleUtility.WorldToGUIPoint(point);

		Rect rect = new Rect(screenPoint.x - 7f, screenPoint.y - 7f, 14f, 14f); †ऀऀ椀昀 ⠀猀攀氀攀挀琀攀搀⤀ 洀匀攀氀攀挀琀攀搀䐀漀琀⸀䐀爀愀眀⠀爀攀挀琀Ⰰ 䜀唀䤀䌀漀渀琀攀渀琀⸀渀漀渀攀Ⰰ 椀搀⤀㬀ഀ਀ऀऀ攀氀猀攀 洀一漀爀洀愀氀䐀漀琀⸀䐀爀愀眀⠀爀攀挀琀Ⰰ 䜀唀䤀䌀漀渀琀攀渀琀⸀渀漀渀攀Ⰰ 椀搀⤀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 圀栀攀琀栀攀爀 琀栀攀 洀漀甀猀攀 瀀漀猀椀琀椀漀渀 椀猀 眀椀琀栀椀渀 漀渀攀 漀昀 琀栀攀 猀瀀攀挀椀昀椀攀搀 爀攀挀琀愀渀最氀攀猀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 戀漀漀氀 䤀猀䴀漀甀猀攀伀瘀攀爀刀攀挀琀 ⠀嘀攀挀琀漀爀㈀ 洀漀甀猀攀Ⰰ 䰀椀猀琀㰀刀攀挀琀㸀 爀攀挀琀猀⤀ഀ਀†††ऀ笀ഀ਀ऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 爀攀挀琀猀⸀䌀漀甀渀琀㬀 ⬀⬀椀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ刀攀挀琀 爀 㴀 爀攀挀琀猀嬀椀崀㬀ഀ਀ऀऀऀ椀昀 ⠀爀⸀䌀漀渀琀愀椀渀猀⠀洀漀甀猀攀⤀⤀ 爀攀琀甀爀渀 琀爀甀攀㬀ഀ਀ऀऀ紀ഀ਀ऀऀ爀攀琀甀爀渀 昀愀氀猀攀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 匀挀爀攀攀渀ⴀ猀瀀愀挀攀 搀椀猀琀愀渀挀攀 昀爀漀洀 琀栀攀 洀漀甀猀攀 瀀漀猀椀琀椀漀渀 琀漀 琀栀攀 猀瀀攀挀椀昀椀攀搀 眀漀爀氀搀 瀀漀猀椀琀椀漀渀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 昀氀漀愀琀 䜀攀琀匀挀爀攀攀渀䐀椀猀琀愀渀挀攀 ⠀嘀攀挀琀漀爀㌀ 眀漀爀氀搀倀漀猀Ⰰ 嘀攀挀琀漀爀㈀ 洀漀甀猀攀倀漀猀⤀ഀ਀ऀ笀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 猀挀爀攀攀渀倀漀猀 㴀 䠀愀渀搀氀攀唀琀椀氀椀琀礀⸀圀漀爀氀搀吀漀䜀唀䤀倀漀椀渀琀⠀眀漀爀氀搀倀漀猀⤀㬀ഀ਀ऀऀ爀攀琀甀爀渀 嘀攀挀琀漀爀㈀⸀䐀椀猀琀愀渀挀攀⠀洀漀甀猀攀倀漀猀Ⰰ 猀挀爀攀攀渀倀漀猀⤀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 䌀氀漀猀攀猀琀 猀挀爀攀攀渀ⴀ猀瀀愀挀攀 搀椀猀琀愀渀挀攀 昀爀漀洀 琀栀攀 洀漀甀猀攀 瀀漀猀椀琀椀漀渀 琀漀 漀渀攀 漀昀 琀栀攀 猀瀀攀挀椀昀椀攀搀 眀漀爀氀搀 瀀漀椀渀琀猀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 昀氀漀愀琀 䜀攀琀匀挀爀攀攀渀䐀椀猀琀愀渀挀攀 ⠀嘀攀挀琀漀爀㌀嬀崀 眀漀爀氀搀倀漀椀渀琀猀Ⰰ 嘀攀挀琀漀爀㈀ 洀漀甀猀攀倀漀猀Ⰰ 漀甀琀 椀渀琀 椀渀搀攀砀⤀ഀ਀ऀ笀ഀ਀ऀऀ昀氀漀愀琀 洀椀渀 㴀 昀氀漀愀琀⸀䴀愀砀嘀愀氀甀攀㬀ഀ਀ऀऀ椀渀搀攀砀 㴀 　㬀ഀ਀ഀ਀ऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 眀漀爀氀搀倀漀椀渀琀猀⸀䰀攀渀最琀栀㬀 ⬀⬀椀⤀ഀ਀†⼯ऀऀ笀ഀ਀ऀऀऀ昀氀漀愀琀 搀椀猀琀愀渀挀攀 㴀 䜀攀琀匀挀爀攀攀渀䐀椀猀琀愀渀挀攀⠀眀漀爀氀搀倀漀椀渀琀猀嬀椀崀Ⰰ 洀漀甀猀攀倀漀猀⤀㬀ഀ਀ऀऀऀഀ਀ऀऀऀ椀昀 ⠀搀椀猀琀愀渀挀攀 㰀 洀椀渀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ椀渀搀攀砀 㴀 椀㬀ഀ਀ऀऀऀऀ洀椀渀 㴀 搀椀猀琀愀渀挀攀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀ紀ഀ਀†⼯ 		return min;
	} †⼯†ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 匀攀琀 琀栀攀 洀漀甀猀攀 挀甀爀猀漀爀 爀攀挀琀愀渀最氀攀Ⰰ 爀攀昀爀攀猀栀椀渀最 琀栀攀 猀挀爀攀攀渀 眀栀攀渀 椀琀 最攀琀猀 挀栀愀渀最攀搀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 瘀漀椀搀 匀攀琀䌀甀爀猀漀爀刀攀挀琀 ⠀刀攀挀琀 爀攀挀琀Ⰰ 䴀漀甀猀攀䌀甀爀猀漀爀 挀甀爀猀漀爀⤀ഀ਀ऀ笀ഀ਀ऀऀ䔀搀椀琀漀爀䜀唀䤀唀琀椀氀椀琀礀⸀䄀搀搀䌀甀爀猀漀爀刀攀挀琀⠀爀攀挀琀Ⰰ 挀甀爀猀漀爀⤀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀䔀瘀攀渀琀⸀挀甀爀爀攀渀琀⸀琀礀瀀攀 㴀㴀 䔀瘀攀渀琀吀礀瀀攀⸀䴀漀甀猀攀䴀漀瘀攀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ椀昀 ⠀洀䌀甀爀猀漀爀 ℀㴀 挀甀爀猀漀爀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ洀䌀甀爀猀漀爀 㴀 挀甀爀猀漀爀㬀ഀ਀ऀऀऀऀ䔀瘀攀渀琀⸀挀甀爀爀攀渀琀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀ紀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 刀愀礀挀愀猀琀 椀渀琀漀 琀栀攀 猀挀爀攀攀渀Ⰰ 爀攀琀甀爀渀椀渀最 愀渀 愀爀爀愀礀 漀昀 眀椀搀最攀琀猀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 唀䤀圀椀搀最攀琀嬀崀 刀愀礀挀愀猀琀 ⠀唀䤀圀椀搀最攀琀 眀椀搀最攀琀Ⰰ 嘀攀挀琀漀爀㈀ 洀漀甀猀攀倀漀猀⤀ഀ਀ऀ笀ഀ਀ऀऀ䜀愀洀攀伀戀樀攀挀琀 爀漀漀琀 㴀 眀椀搀最攀琀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀ唀䤀倀愀渀攀氀 瀀愀渀攀氀 㴀 一䜀唀䤀吀漀漀氀猀⸀䘀椀渀搀䤀渀倀愀爀攀渀琀猀㰀唀䤀倀愀渀攀氀㸀⠀爀漀漀琀⤀㬀ഀ਀†††† 		return (panel != null) ? NGUIEditorTools.Raycast(panel, mousePos) : new UIWidget[] {};
	}

	/// <summary> †††† 	/// Draw the on-screen selection, knobs, and handle all interaction logic.ऊ		/// </summary>

	public void OnSceneGUI ()
	{
		if (!EditorPrefs.GetBool("New GUI", true)) return;
		//Tools.current = Tool.View;

		mWidget = target as UIWidget;

		Handles.color = mOutlineColor;
		Transform t = mWidget.cachedTransform; †††††† 
		Event e = Event.current;
		int id = GUIUtility.GetControlID(s_Hash, FocusType.Passive);
		EventType type = e.GetTypeForControl(id);

		Vector3[] corners = NGUIMath.CalculateWidgetCorners(mWidget);
		Handles.DrawLine(corners[0], corners[1]);
		Handles.DrawLine(corners[1], corners[2]);
		Handles.DrawLine(corners[2], corners[3]);
		Handles.DrawLine(corners[0], corners[3]);

		Vector3[] worldPos = new Vector3[8];
		
		worldPos[0] = corners[0];
		worldPos[1] = corners[1];
		worldPos[2] = corners[2]; †ऀऀ眀漀爀氀搀倀漀猀嬀㌀崀 㴀 挀漀爀渀攀爀猀嬀㌀崀㬀ഀ਀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㐀崀 㴀 ⠀挀漀爀渀攀爀猀嬀　崀 ⬀ 挀漀爀渀攀爀猀嬀㄀崀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㔀崀 㴀 ⠀挀漀爀渀攀爀猀嬀㄀崀 ⬀ 挀漀爀渀攀爀猀嬀㈀崀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀ眀漀爀氀搀倀漀猀嬀㘀崀 㴀 ⠀挀漀爀渀攀爀猀嬀㈀崀 ⬀ 挀漀爀渀攀爀猀嬀㌀崀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ 		worldPos[7] = (corners[0] + corners[3]) * 0.5f;

		Vector2[] screenPos = new Vector2[8];
		for (int i = 0; i < 8; ++i) screenPos[i] = HandleUtility.WorldToGUIPoint(worldPos[i]);

		Bounds b = new Bounds(screenPos[0], Vector3.zero);
		for (int i = 1; i < 8; ++i) b.Encapsulate(screenPos[i]);

		// Time to figure out what kind of action is underneath the mouse
		Action actionUnderMouse = mAction;ऊ			UIWidget.Pivot pivotUnderMouse = UIWidget.Pivot.Center;

		if (actionUnderMouse == Action.None)
		{
			int index = 0;
			float dist = GetScreenDistance(worldPos, e.mousePosition, out index);

			if (dist < 10f)
			{
				pivotUnderMouse = mPivots[index];
				actionUnderMouse = Action.Scale;
			}
			else if (e.modifiers == 0 && NGUIEditorTools.DistanceToRectangle(corners, e.mousePosition) == 0f)
			{
				if (Tools.current != Tool.Rotate && Tools.current != Tool.Scale) ⼯ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 䄀挀琀椀漀渀⸀䴀漀瘀攀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ攀氀猀攀 椀昀 ⠀搀椀猀琀 㰀 ㌀　昀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 䄀挀琀椀漀渀⸀刀漀琀愀琀攀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀ紀ഀ਀ഀ਀ऀऀ⼀⼀ 䌀栀愀渀最攀 琀栀攀 洀漀甀猀攀 挀甀爀猀漀爀 琀漀 愀 洀漀爀攀 愀瀀瀀爀漀瀀爀椀愀琀攀 漀渀攀ഀ਀⌀椀昀 ℀唀一䤀吀夀开㌀开㔀ഀ਀ऀऀ笀ഀ਀ऀऀऀ嘀攀挀琀漀爀㈀ 洀椀渀 㴀 戀⸀洀椀渀㬀ഀ਀ऀऀऀ嘀攀挀琀漀爀㈀ 洀愀砀 㴀 戀⸀洀愀砀㬀ഀ਀ഀ਀ऀऀऀ洀椀渀⸀砀 ⴀ㴀 ㌀　昀㬀ഀ਀ऀऀऀ洀愀砀⸀砀 ⬀㴀 ㌀　昀㬀ഀ਀ऀऀऀ洀椀渀⸀礀 ⴀ㴀 ㌀　昀㬀ഀ਀ऀऀऀ洀愀砀⸀礀 ⬀㴀 ㌀　昀㬀ഀ਀ഀ਀ऀऀऀ刀攀挀琀 爀攀挀琀 㴀 渀攀眀 刀攀挀琀⠀洀椀渀⸀砀Ⰰ 洀椀渀⸀礀Ⰰ 洀愀砀⸀砀 ⴀ 洀椀渀⸀砀Ⰰ 洀愀砀⸀礀 ⴀ 洀椀渀⸀礀⤀㬀ഀ਀ഀ਀ऀऀऀ椀昀 ⠀愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀刀漀琀愀琀攀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ匀攀琀䌀甀爀猀漀爀刀攀挀琀⠀爀攀挀琀Ⰰ 䴀漀甀猀攀䌀甀爀猀漀爀⸀刀漀琀愀琀攀䄀爀爀漀眀⤀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ攀氀猀攀 椀昀 ⠀愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀䴀漀瘀攀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ匀攀琀䌀甀爀猀漀爀刀攀挀琀⠀爀攀挀琀Ⰰ 䴀漀甀猀攀䌀甀爀猀漀爀⸀䴀漀瘀攀䄀爀爀漀眀⤀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ攀氀猀攀 椀昀 ⠀愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀匀挀愀氀攀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ匀攀琀䌀甀爀猀漀爀刀攀挀琀⠀爀攀挀琀Ⰰ 䴀漀甀猀攀䌀甀爀猀漀爀⸀匀挀愀氀攀䄀爀爀漀眀⤀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ攀氀猀攀 匀攀琀䌀甀爀猀漀爀刀攀挀琀⠀爀攀挀琀Ⰰ 䴀漀甀猀攀䌀甀爀猀漀爀⸀䄀爀爀漀眀⤀㬀ഀ਀ऀऀ紀ഀ਀⌀攀渀搀椀昀ഀ਀ഀ਀ऀऀ猀眀椀琀挀栀 ⠀琀礀瀀攀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ挀愀猀攀 䔀瘀攀渀琀吀礀瀀攀⸀刀攀瀀愀椀渀琀㨀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ䠀愀渀搀氀攀猀⸀䈀攀最椀渀䜀唀䤀⠀⤀㬀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 㠀㬀 ⬀⬀椀⤀ഀ਀ऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀ䐀爀愀眀䬀渀漀戀⠀眀漀爀氀搀倀漀猀嬀椀崀Ⰰ 洀圀椀搀最攀琀⸀瀀椀瘀漀琀 㴀㴀 洀倀椀瘀漀琀猀嬀椀崀Ⰰ 椀搀⤀㬀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ䠀愀渀搀氀攀猀⸀䔀渀搀䜀唀䤀⠀⤀㬀ഀ਀††⼯† 			}
			break;

			case EventType.MouseDown:
			{
				mStartMouse = e.mousePosition;
				mAllowSelection = true; †††††††ഀ਀ऀऀऀऀ椀昀 ⠀攀⸀戀甀琀琀漀渀 㴀㴀 ㄀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ椀昀 ⠀攀⸀洀漀搀椀昀椀攀爀猀 㴀㴀 　⤀ഀ਀ऀऀऀऀऀ笀ഀ਀†††††††ऀऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀 䜀唀䤀唀琀椀氀椀琀礀⸀欀攀礀戀漀愀爀搀䌀漀渀琀爀漀氀 㴀 椀搀㬀ഀ਀ऀऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ攀氀猀攀 椀昀 ⠀攀⸀戀甀琀琀漀渀 㴀㴀 　 ☀☀ 愀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 ℀㴀 䄀挀琀椀漀渀⸀一漀渀攀 ☀☀ 刀愀礀挀愀猀琀⠀挀漀爀渀攀爀猀Ⰰ 漀甀琀 洀匀琀愀爀琀䐀爀愀最⤀⤀ഀ਀ऀऀऀऀ笀ഀ਀††††† 					mStartPos = t.position;
					mStartRot = t.localRotation.eulerAngles;
					mStartDir = mStartDrag - t.position;
					mStartScale = t.localScale;
					mDragPivot = pivotUnderMouse;
					mActionUnderMouse = actionUnderMouse;
					GUIUtility.hotControl = GUIUtility.keyboardControl = id;
					e.Use();
				}
			}
			break;

			case EventType.MouseDrag:
			{
				// Prevent selection once the drag operation begins
				bool dragStarted = (e.mousePosition - mStartMouse).magnitude > 3f;
				if (dragStarted) mAllowSelection = false;

				if (GUIUtility.hotControl == id)
				{ऊ						e.Use();

					if (mAction != Action.None || mActionUnderMouse != Action.None)
					{
						Vector3 pos;

						if (Raycast(corners, out pos))
						{
							if (mAction == Action.None && mActionUnderMouse != Action.None)
							{ऊउउऀऀऀऀऀऀऀऀ⼀⼀ 圀愀椀琀 甀渀琀椀氀 琀栀攀 洀漀甀猀攀 洀漀瘀攀猀 戀礀 洀漀爀攀 琀栀愀渀 愀 昀攀眀 瀀椀砀攀氀猀ഀ਀ऀऀऀऀऀऀऀऀ椀昀 ⠀搀爀愀最匀琀愀爀琀攀搀⤀ഀ਀ऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀ椀昀 ⠀洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀㴀 䄀挀琀椀漀渀⸀䴀漀瘀攀⤀ഀ਀ऀऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀऀ洀匀琀愀爀琀倀漀猀 㴀 琀⸀瀀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀䴀漀瘀攀 眀椀搀最攀琀∀Ⰰ 琀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ紀ഀ਀†† 									else if (mActionUnderMouse == Action.Rotate)
									{
										mStartRot = t.localRotation.eulerAngles;
										mStartDir = mStartDrag - t.position;
										NGUIEditorTools.RegisterUndo("Rotate widget", t);
									}
									else if (mActionUnderMouse == Action.Scale)
									{
										mStartPos = t.localPosition;
										mStartScale = t.localScale;
										mDragPivot = pivotUnderMouse;
										NGUIEditorTools.RegisterUndo("Scale widget", t);
									}
									mAction = actionUnderMouse;
								}
							}

							if (mAction != Action.None)
							{
								if (mAction == Action.Move)
								{
									t.position = mStartPos + (pos - mStartDrag);
									pos = t.localPosition;
									pos.x = Mathf.RoundToInt(pos.x);
									pos.y = Mathf.RoundToInt(pos.y);
									t.localPosition = pos;
								}
								else if (mAction == Action.Rotate)
								{
									Vector3 dir = pos - t.position;
									float angle = Vector3.Angle(mStartDir, dir);

									if (angle > 0f)
									{
										float dot = Vector3.Dot(Vector3.Cross(mStartDir, dir), t.forward);
										if (dot < 0f) angle = -angle;
										angle = mStartRot.z + angle;
										if (e.modifiers != EventModifiers.Shift) angle = Mathf.Round(angle / 15f) * 15f;
										else angle = Mathf.Round(angle);
										t.localRotation = Quaternion.Euler(mStartRot.x, mStartRot.y, angle);
									}
								}
								else if (mAction == Action.Scale)
								{
									// World-space delta since the drag started
									Vector3 delta = pos - mStartDrag;

									// Adjust the widget's position and scale based on the delta, restricted by the pivot
									AdjustPosAndScale(mWidget, mStartPos, mStartScale, delta, mDragPivot);
								}
							}
						}
					}
				}
			}
			break;

			case EventType.MouseUp:
			{ऊऀऀऀऀ椀昀 ⠀䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀㴀 椀搀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀欀攀礀戀漀愀爀搀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ഀ਀ऀऀऀऀऀ椀昀 ⠀攀⸀戀甀琀琀漀渀 㰀 ㈀⤀ഀ਀ऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀ戀漀漀氀 栀愀渀搀氀攀搀 㴀 昀愀氀猀攀㬀ഀ਀ഀ਀ऀऀऀऀऀऀ椀昀 ⠀攀⸀戀甀琀琀漀渀 㴀㴀 ㄀⤀ഀ਀ऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀ⼀⼀ 刀椀最栀琀ⴀ挀氀椀挀欀㨀 匀攀氀攀挀琀 琀栀攀 眀椀搀最攀琀 戀攀氀漀眀ഀ਀ऀऀऀऀऀऀऀ⼀⼀ 刀椀最栀琀ⴀ挀氀椀挀欀㨀 匀攀氀攀挀琀 琀栀攀 眀椀搀最攀琀 戀攀氀漀眀ഀ਀ऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀 氀愀猀琀 㴀 渀甀氀氀㬀ഀ਀ऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀嬀崀 眀椀搀最攀琀猀 㴀 刀愀礀挀愀猀琀⠀洀圀椀搀最攀琀Ⰰ 攀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀⤀㬀ഀ਀ഀ਀ऀऀऀऀऀऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 眀椀搀最攀琀猀⸀䰀攀渀最琀栀㬀 椀 㸀 　㬀 ⤀ഀ਀ 							{ ††††††††ऀऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀 眀 㴀 眀椀搀最攀琀猀嬀ⴀⴀ椀崀㬀ഀ਀ऀऀऀऀऀऀऀऀ椀昀 ⠀眀 㴀㴀 洀圀椀搀最攀琀⤀ 戀爀攀愀欀㬀ഀ਀ऀऀऀऀऀऀऀऀ氀愀猀琀 㴀 眀㬀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ഀ਀ऀऀऀऀऀऀऀ椀昀 ⠀氀愀猀琀 ℀㴀 渀甀氀氀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 氀愀猀琀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀऀऀऀऀऀऀ栀愀渀搀氀攀搀 㴀 琀爀甀攀㬀ഀ਀†⼯ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀ攀氀猀攀 椀昀 ⠀洀䄀挀琀椀漀渀 㴀㴀 䄀挀琀椀漀渀⸀一漀渀攀⤀ഀ਀††⼯ऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀ椀昀 ⠀洀䄀氀氀漀眀匀攀氀攀挀琀椀漀渀⤀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ⼀⼀ 䰀攀昀琀ⴀ挀氀椀挀欀㨀 匀攀氀攀挀琀 琀栀攀 眀椀搀最攀琀 愀戀漀瘀攀ഀ਀ऀऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀 氀愀猀琀 㴀 渀甀氀氀㬀ഀ਀ऀऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀嬀崀 眀椀搀最攀琀猀 㴀 刀愀礀挀愀猀琀⠀洀圀椀搀最攀琀Ⰰ 攀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀⤀㬀ഀ਀ഀ਀ऀऀऀऀऀऀऀऀ椀昀 ⠀眀椀搀最攀琀猀⸀䰀攀渀最琀栀 㸀 　⤀ഀ਀ऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 眀椀搀最攀琀猀⸀䰀攀渀最琀栀㬀 ⬀⬀椀⤀ഀ਀ऀऀऀऀऀऀऀऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 眀椀搀最攀琀猀⸀䰀攀渀最琀栀㬀 ⬀⬀椀⤀ഀ਀ऀऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀऀ唀䤀圀椀搀最攀琀 眀 㴀 眀椀搀最攀琀猀嬀椀崀㬀ഀ਀ഀ਀ऀऀऀऀऀऀऀऀऀऀ椀昀 ⠀眀 㴀㴀 洀圀椀搀最攀琀⤀ഀ਀ऀऀऀऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀऀऀऀ椀昀 ⠀氀愀猀琀 ℀㴀 渀甀氀氀⤀ 匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 氀愀猀琀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀऀ栀愀渀搀氀攀搀 㴀 琀爀甀攀㬀ഀ਀† 											break;
										}
										last = w;
									}

									if (!handled)
									{ †⼠ †ऀऀऀऀऀऀऀऀऀऀ匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 眀椀搀最攀琀猀嬀　崀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀऀऀऀऀऀऀऀऀ栀愀渀搀氀攀搀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀ攀氀猀攀ഀ਀ऀऀऀऀऀऀ笀ഀ਀उउ								// Finished dragging something
							mAction = Action.None;
							mActionUnderMouse = Action.None;
							Vector3 pos = t.localPosition;
							Vector3 scale = t.localScale;

							if (mWidget.pixelPerfectAfterResize)ऊउउऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀匀挀愀氀攀 㴀 猀挀愀氀攀㬀ഀ਀†† ऊउऀऀऀऀऀऀऀऀ洀圀椀搀最攀琀⸀䴀愀欀攀倀椀砀攀氀倀攀爀昀攀挀琀⠀⤀㬀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀ攀氀猀攀ഀ਀ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ瀀漀猀⸀砀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀⠀瀀漀猀⸀砀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀ瀀漀猀⸀礀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀⠀瀀漀猀⸀礀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀ猀挀愀氀攀⸀砀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀⠀猀挀愀氀攀⸀砀⤀㬀ഀ਀ऀऀऀऀऀऀऀऀ猀挀愀氀攀⸀礀 㴀 䴀愀琀栀昀⸀刀漀甀渀搀⠀猀挀愀氀攀⸀礀⤀㬀ഀ਀ഀ਀⼠ ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀匀挀愀氀攀 㴀 猀挀愀氀攀㬀ഀ਀ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀऀ栀愀渀搀氀攀搀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀऀऀऀ紀ഀ਀ഀ਀ऀऀऀऀऀऀ椀昀 ⠀栀愀渀搀氀攀搀⤀ഀ਀ऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀ洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀऀऀऀऀऀऀ洀䄀挀琀椀漀渀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀऀऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ攀氀猀攀 椀昀 ⠀洀䄀氀氀漀眀匀攀氀攀挀琀椀漀渀⤀ഀ਀ऀऀऀऀऀ唀䤀圀椀搀最攀琀嬀崀 眀椀搀最攀琀猀 㴀 刀愀礀挀愀猀琀⠀洀圀椀搀最攀琀Ⰰ 攀⸀洀漀甀猀攀倀漀猀椀琀椀漀渀⤀㬀ഀ਀ऀऀऀऀऀ椀昀 ⠀眀椀搀最攀琀猀⸀䰀攀渀最琀栀 㸀 　⤀ 匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 眀椀搀最攀琀猀嬀　崀⸀最愀洀攀伀戀樀攀挀琀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ洀䄀氀氀漀眀匀攀氀攀挀琀椀漀渀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 䔀瘀攀渀琀吀礀瀀攀⸀䬀攀礀䐀漀眀渀㨀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ椀昀 ⠀攀⸀欀攀礀䌀漀搀攀 㴀㴀 䬀攀礀䌀漀搀攀⸀唀瀀䄀爀爀漀眀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ嘀攀挀琀漀爀㌀ 瀀漀猀 㴀 琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀ瀀漀猀⸀礀 ⬀㴀 ㄀昀㬀ഀ਀ऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀऀ攀氀猀攀 椀昀 ⠀攀⸀欀攀礀䌀漀搀攀 㴀㴀 䬀攀礀䌀漀搀攀⸀䐀漀眀渀䄀爀爀漀眀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ嘀攀挀琀漀爀㌀ 瀀漀猀 㴀 琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀㬀ഀ਀ऀऀऀऀऀ瀀漀猀⸀礀 ⴀ㴀 ㄀昀㬀ഀ਀ऀऀऀऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀†ऀऀऀऀ紀ഀ਀उउ					else if (e.keyCode == KeyCode.LeftArrow)
				{
					Vector3 pos = t.localPosition;
					pos.x -= 1f;
					t.localPosition = pos;
					e.Use();
				}
				else if (e.keyCode == KeyCode.RightArrow)
				{
					Vector3 pos = t.localPosition;
					pos.x += 1f;
					t.localPosition = pos;
					e.Use();
				}
				else if (e.keyCode == KeyCode.Escape)
				{
					if (GUIUtility.hotControl == id)
					{
						if (mAction != Action.None)
						{
							if (mAction == Action.Move)
							{
								t.position = mStartPos;
							}
							else if (mAction == Action.Rotate)
							{
								t.localRotation = Quaternion.Euler(mStartRot);
							}
							else if (mAction == Action.Scale) †††ऀऀऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀瀀漀猀椀琀椀漀渀 㴀 洀匀琀愀爀琀倀漀猀㬀ഀ਀ऀऀऀऀऀऀऀऀ琀⸀氀漀挀愀氀匀挀愀氀攀 㴀 洀匀琀愀爀琀匀挀愀氀攀㬀ഀ਀††††ऀऀऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀऀ紀ഀ਀ഀ਀ऀऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀栀漀琀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ऀऀऀऀऀऀ䜀唀䤀唀琀椀氀椀琀礀⸀欀攀礀戀漀愀爀搀䌀漀渀琀爀漀氀 㴀 　㬀ഀ਀ഀ਀ऀऀऀऀऀऀ洀䄀挀琀椀漀渀唀渀搀攀爀䴀漀甀猀攀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀऀऀऀऀऀ洀䄀挀琀椀漀渀 㴀 䄀挀琀椀漀渀⸀一漀渀攀㬀ഀ਀ऀऀऀऀऀऀ攀⸀唀猀攀⠀⤀㬀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀऀ攀氀猀攀ഀ਀ऀऀऀऀऀ笀ഀ਀ऀऀऀऀऀऀ匀攀氀攀挀琀椀漀渀⸀愀挀琀椀瘀攀䜀愀洀攀伀戀樀攀挀琀 㴀 渀甀氀氀㬀ഀ਀ऀऀऀऀऀऀ吀漀漀氀猀⸀挀甀爀爀攀渀琀 㴀 吀漀漀氀⸀䴀漀瘀攀㬀ഀ਀ऀऀऀऀऀ紀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ऀऀ紀ഀ਀उऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 䄀搀樀甀猀琀 琀栀攀 琀爀愀渀猀昀漀爀洀✀猀 瀀漀猀椀琀椀漀渀 愀渀搀 猀挀愀氀攀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 瘀漀椀搀 䄀搀樀甀猀琀倀漀猀䄀渀搀匀挀愀氀攀 ⠀唀䤀圀椀搀最攀琀 眀Ⰰ 嘀攀挀琀漀爀㌀ 猀琀愀爀琀䰀漀挀愀氀倀漀猀Ⰰ 嘀攀挀琀漀爀㌀ 猀琀愀爀琀䰀漀挀愀氀匀挀愀氀攀Ⰰ 嘀攀挀琀漀爀㌀ 眀漀爀氀搀䐀攀氀琀愀Ⰰ 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 搀爀愀最倀椀瘀漀琀⤀ഀ਀ऀ笀ഀ਀††††††† 		Transform t = w.cachedTransform;
		Transform parent = t.parent;
		Matrix4x4 parentToLocal = (parent != null) ? t.parent.worldToLocalMatrix : Matrix4x4.identity;
		Matrix4x4 worldToLocal = parentToLocal;
		Quaternion invRot = Quaternion.Inverse(t.localRotation);
		worldToLocal = worldToLocal * Matrix4x4.TRS(Vector3.zero, invRot, Vector3.one);
		Vector3 localDelta = worldToLocal.MultiplyVector(worldDelta);

		bool canBeSquare = false;
		float left = 0f;
		float right = 0f;
		float top = 0f;
		float bottom = 0f;

		switch (dragPivot)
		{
			case UIWidget.Pivot.TopLeft:
			canBeSquare = (w.pivot == UIWidget.Pivot.BottomRight);
			left = localDelta.x; ††††† 			top = localDelta.y;
			break;

			case UIWidget.Pivot.Left:
			left = localDelta.x;
			break;

			case UIWidget.Pivot.BottomLeft:
			canBeSquare = (w.pivot == UIWidget.Pivot.TopRight);
			left = localDelta.x;
			bottom = localDelta.y;
			break;

			case UIWidget.Pivot.Top:
			top = localDelta.y;
			break;

			case UIWidget.Pivot.Bottom:
			bottom = localDelta.y;
			break;ऊഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀㨀ഀ਀ऀऀऀ挀愀渀䈀攀匀焀甀愀爀攀 㴀 ⠀眀⸀瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀䰀攀昀琀⤀㬀ഀ਀ऀऀऀ爀椀最栀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀ऀऀऀ琀漀瀀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀礀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀㨀ഀ਀ऀऀऀ爀椀最栀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀刀椀最栀琀㨀ഀ਀ऀऀऀ挀愀渀䈀攀匀焀甀愀爀攀 㴀 ⠀眀⸀瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀䰀攀昀琀⤀㬀ഀ਀ऀऀऀ爀椀最栀琀 㴀 氀漀挀愀氀䐀攀氀琀愀⸀砀㬀ഀ਀†† 			bottom = localDelta.y;
			break;
		}

		AdjustWidget(w, startLocalPos, startLocalScale, left, top, right, bottom, canBeSquare && Event.current.modifiers == EventModifiers.Shift);
	}
	
	/// <summary>
	/// Adjust the widget's rectangle based on the specified modifier values.
	/// </summary>

	static void AdjustWidget (UIWidget w, Vector3 pos, Vector3 scale, float left, float top, float right, float bottom, bool makeSquare) ऀ笀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 漀昀昀猀攀琀 㴀 眀⸀瀀椀瘀漀琀伀昀昀猀攀琀㬀ഀ਀ऀऀ嘀攀挀琀漀爀㐀 瀀愀搀搀椀渀最 㴀 眀⸀爀攀氀愀琀椀瘀攀倀愀搀搀椀渀最㬀ഀ਀ऀऀ嘀攀挀琀漀爀㈀ 猀椀稀攀 㴀 眀⸀爀攀氀愀琀椀瘀攀匀椀稀攀㬀ഀ਀ഀ਀ऀऀ漀昀昀猀攀琀⸀砀 ⴀ㴀 瀀愀搀搀椀渀最⸀砀㬀ഀ਀उउ			offset.y -= padding.y;
		size.x += padding.x + padding.z;
		size.y += padding.y + padding.w;
		
		scale.Scale(size); ††††† 
		offset.y = -offset.y;
		offset.y = -offset.y;

		Transform t = w.cachedTransform;
		Quaternion rot = t.localRotation;
		UIWidget.Pivot pivot = w.pivot;
 †⼠/		Vector2 rotatedTL = new Vector2(left, top);
		Vector2 rotatedTR = new Vector2(right, top);
		Vector2 rotatedBL = new Vector2(left, bottom);
		Vector2 rotatedBR = new Vector2(right, bottom);ऊउ			Vector2 rotatedL  = new Vector2(left, 0f);
		Vector2 rotatedR  = new Vector2(right, 0f);
		Vector2 rotatedT  = new Vector2(0f, top);
		Vector2 rotatedB  = new Vector2(0f, bottom);
		
		rotatedTL = rot * rotatedTL;ऊऀऀ爀漀琀愀琀攀搀吀刀 㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀吀刀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀䈀䰀 㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䈀䰀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀䈀刀 㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䈀刀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀䰀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䰀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀刀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀刀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀吀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀吀㬀ഀ਀ऀऀ爀漀琀愀琀攀搀䈀  㴀 爀漀琀 ⨀ 爀漀琀愀琀攀搀䈀㬀ഀ਀ഀ਀ऀऀ猀眀椀琀挀栀 ⠀瀀椀瘀漀琀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀䰀攀昀琀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀吀䰀⸀砀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀吀䰀⸀礀㬀ഀ਀††††† 			break;

			case UIWidget.Pivot.BottomRight:
			pos.x += rotatedBR.x;
			pos.y += rotatedBR.y;
			break;

			case UIWidget.Pivot.BottomLeft:
			pos.x += rotatedBL.x;
			pos.y += rotatedBL.y;
			break;

			case UIWidget.Pivot.TopRight:
			pos.x += rotatedTR.x;
			pos.y += rotatedTR.y;
			break;

			case UIWidget.Pivot.Left: †ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀䰀⸀砀 ⬀ ⠀爀漀琀愀琀攀搀吀⸀砀 ⬀ 爀漀琀愀琀攀搀䈀⸀砀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀䰀⸀礀 ⬀ ⠀爀漀琀愀琀攀搀吀⸀礀 ⬀ 爀漀琀愀琀攀搀䈀⸀礀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀刀⸀砀 ⬀ ⠀爀漀琀愀琀攀搀吀⸀砀 ⬀ 爀漀琀愀琀攀搀䈀⸀砀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀刀⸀礀 ⬀ ⠀爀漀琀愀琀攀搀吀⸀礀 ⬀ 爀漀琀愀琀攀搀䈀⸀礀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀吀⸀砀 ⬀ ⠀爀漀琀愀琀攀搀䰀⸀砀 ⬀ 爀漀琀愀琀攀搀刀⸀砀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀吀⸀礀 ⬀ ⠀爀漀琀愀琀攀搀䰀⸀礀 ⬀ 爀漀琀愀琀攀搀刀⸀礀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀㨀ഀ਀ऀऀऀ瀀漀猀⸀砀 ⬀㴀 爀漀琀愀琀攀搀䈀⸀砀 ⬀ ⠀爀漀琀愀琀攀搀䰀⸀砀 ⬀ 爀漀琀愀琀攀搀刀⸀砀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ瀀漀猀⸀礀 ⬀㴀 爀漀琀愀琀攀搀䈀⸀礀 ⬀ ⠀爀漀琀愀琀攀搀䰀⸀礀 ⬀ 爀漀琀愀琀攀搀刀⸀礀⤀ ⨀ 　⸀㔀昀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀††††††† 			case UIWidget.Pivot.Center:
			pos.x += (rotatedL.x + rotatedR.x + rotatedT.x + rotatedB.x) * 0.5f;
			pos.y += (rotatedT.y + rotatedB.y + rotatedL.y + rotatedR.y) * 0.5f;
			break;
		}
 ††ऀऀ猀挀愀氀攀⸀砀 ⴀ㴀 氀攀昀琀 ⴀ 爀椀最栀琀㬀ഀ਀††††† 		scale.y += top - bottom;

		scale.x /= size.x;
		scale.y /= size.y;
 ††⼯ऀऀ嘀攀挀琀漀爀㐀 戀漀爀搀攀爀 㴀 眀⸀戀漀爀搀攀爀㬀ഀ਀ऀऀ昀氀漀愀琀 洀椀渀砀 㴀 䴀愀琀栀昀⸀䴀愀砀⠀㈀昀Ⰰ 瀀愀搀搀椀渀最⸀砀 ⬀ 瀀愀搀搀椀渀最⸀稀 ⬀ 戀漀爀搀攀爀⸀砀 ⬀ 戀漀爀搀攀爀⸀稀⤀㬀ഀ਀ऀऀ昀氀漀愀琀 洀椀渀礀 㴀 䴀愀琀栀昀⸀䴀愀砀⠀㈀昀Ⰰ 瀀愀搀搀椀渀最⸀礀 ⬀ 瀀愀搀搀椀渀最⸀眀 ⬀ 戀漀爀搀攀爀⸀礀 ⬀ 戀漀爀搀攀爀⸀眀⤀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀猀挀愀氀攀⸀砀 㰀 洀椀渀砀⤀ 猀挀愀氀攀⸀砀 㴀 洀椀渀砀㬀ഀ਀ऀऀ椀昀 ⠀猀挀愀氀攀⸀礀 㰀 洀椀渀礀⤀ 猀挀愀氀攀⸀礀 㴀 洀椀渀礀㬀ഀ਀ഀ਀ऀऀ⼀⼀ 一伀吀䔀㨀 吀栀椀猀 眀椀氀氀 漀渀氀礀 眀漀爀欀 挀漀爀爀攀挀琀氀礀 眀栀攀渀 搀爀愀最最椀渀最 琀栀攀 挀漀爀渀攀爀 漀瀀瀀漀猀椀琀攀 漀昀 琀栀攀 瀀椀瘀漀琀 瀀漀椀渀琀ഀ਀ऀऀ椀昀 ⠀洀愀欀攀匀焀甀愀爀攀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ猀挀愀氀攀⸀砀 㴀 䴀愀琀栀昀⸀䴀椀渀⠀猀挀愀氀攀⸀砀Ⰰ 猀挀愀氀攀⸀礀⤀㬀ഀ਀ऀऀऀ猀挀愀氀攀⸀礀 㴀 猀挀愀氀攀⸀砀㬀ഀ਀ऀऀ紀ഀ਀ഀ਀ऀऀ琀⸀氀漀挀愀氀倀漀猀椀琀椀漀渀 㴀 瀀漀猀㬀ഀ਀ऀऀ琀⸀氀漀挀愀氀匀挀愀氀攀 㴀 猀挀愀氀攀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 䐀爀愀眀 琀栀攀 椀渀猀瀀攀挀琀漀爀 眀椀搀最攀琀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ瀀甀戀氀椀挀 漀瘀攀爀爀椀搀攀 瘀漀椀搀 伀渀䤀渀猀瀀攀挀琀漀爀䜀唀䤀 ⠀⤀ഀ਀ऀ笀ഀ਀ऀऀ䔀搀椀琀漀爀䜀唀䤀唀琀椀氀椀琀礀⸀䰀漀漀欀䰀椀欀攀䌀漀渀琀爀漀氀猀⠀㠀　昀⤀㬀ഀ਀ऀऀ洀圀椀搀最攀琀 㴀 琀愀爀最攀琀 愀猀 唀䤀圀椀搀最攀琀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀℀洀䤀渀椀琀椀愀氀椀稀攀搀⤀ഀ਀†††††† 		{
			mInitialized = true;
			OnInit();
		}


		//NGUIEditorTools.DrawSeparator();
		EditorGUILayout.Space();
 †† 		// Check to see if we can draw the widget's default properties to begin with
		if (DrawProperties())
		{
			// Draw all common properties next
			DrawCommonProperties(); ††† 			DrawExtraProperties();
		}
	}
ऊ		/// <summary>
	/// All widgets have depth, color and make pixel-perfect options
	/// </summary>

	protected void DrawCommonProperties ()
	{
#if UNITY_3_4
		PrefabType type = EditorUtility.GetPrefabType(mWidget.gameObject);
		PrefabType type = EditorUtility.GetPrefabType(mWidget.gameObject); †††††††⌀攀氀猀攀ഀ਀ऀऀ倀爀攀昀愀戀吀礀瀀攀 琀礀瀀攀 㴀 倀爀攀昀愀戀唀琀椀氀椀琀礀⸀䜀攀琀倀爀攀昀愀戀吀礀瀀攀⠀洀圀椀搀最攀琀⸀最愀洀攀伀戀樀攀挀琀⤀㬀ഀ਀⌀攀渀搀椀昀ഀ਀ഀ਀ऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀䐀爀愀眀匀攀瀀愀爀愀琀漀爀⠀⤀㬀ഀ਀ഀ਀⌀椀昀 唀一䤀吀夀开㌀开㔀ഀ਀ऀऀ⼀⼀ 倀椀瘀漀琀 瀀漀椀渀琀 ⴀⴀ 漀氀搀 猀挀栀漀漀氀 搀爀漀瀀ⴀ搀漀眀渀 猀琀礀氀攀ഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀 㴀 ⠀唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⤀䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀䔀渀甀洀倀漀瀀甀瀀⠀∀倀椀瘀漀琀∀Ⰰ 洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀 ℀㴀 瀀椀瘀漀琀⤀ഀ਀ऀऀ笀ഀ਀ऀऀ    一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀倀椀瘀漀琀 䌀栀愀渀最攀∀Ⰰ 洀圀椀搀最攀琀⤀㬀ഀ਀†††† 		    mWidget.pivot = pivot;
		}
#else
		// Pivot point -- the new, more visual style
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
		if (type != PrefabType.Prefab)
		{
		{
			GUILayout.Space(2f);
			GUILayout.BeginHorizontal();
			{
				EditorGUILayout.PrefixLabel("Depth"); †††††††ഀ਀ऀऀऀऀ椀渀琀 搀攀瀀琀栀 㴀 洀圀椀搀最攀琀⸀搀攀瀀琀栀㬀ഀ਀ऀऀऀऀ椀昀 ⠀䜀唀䤀䰀愀礀漀甀琀⸀䈀甀琀琀漀渀⠀∀䈀愀挀欀∀Ⰰ 䜀唀䤀䰀愀礀漀甀琀⸀圀椀搀琀栀⠀㘀　昀⤀⤀⤀ ⴀⴀ搀攀瀀琀栀㬀ഀ਀ऀऀऀऀ搀攀瀀琀栀 㴀 䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀䤀渀琀䘀椀攀氀搀⠀搀攀瀀琀栀⤀㬀ഀ਀ऀऀऀऀ椀昀 ⠀䜀唀䤀䰀愀礀漀甀琀⸀䈀甀琀琀漀渀⠀∀䘀漀爀眀愀爀搀∀Ⰰ 䜀唀䤀䰀愀礀漀甀琀⸀圀椀搀琀栀⠀㘀　昀⤀⤀⤀ ⬀⬀搀攀瀀琀栀㬀ഀ਀ഀ਀ऀऀऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀搀攀瀀琀栀 ℀㴀 搀攀瀀琀栀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀䐀攀瀀琀栀 䌀栀愀渀最攀∀Ⰰ 洀圀椀搀最攀琀⤀㬀ഀ਀ऀऀऀऀऀ洀圀椀搀最攀琀⸀搀攀瀀琀栀 㴀 搀攀瀀琀栀㬀ഀ਀ऀऀऀऀऀ洀䐀攀瀀琀栀䌀栀攀挀欀 㴀 琀爀甀攀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䔀渀搀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ഀ਀ऀऀऀ唀䤀倀愀渀攀氀 瀀愀渀攀氀 㴀 洀圀椀搀最攀琀⸀瀀愀渀攀氀㬀ഀ਀††⼯†ഀ਀††ऀऀऀ椀昀 ⠀瀀愀渀攀氀 ℀㴀 渀甀氀氀⤀ഀ਀ऀऀऀ笀ഀ਀ऀऀऀऀ椀渀琀 挀漀甀渀琀 㴀 　㬀ഀ਀ഀ਀ऀऀऀऀ昀漀爀 ⠀椀渀琀 椀 㴀 　㬀 椀 㰀 瀀愀渀攀氀⸀眀椀搀最攀琀猀⸀猀椀稀攀㬀 ⬀⬀椀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ唀䤀圀椀搀最攀琀 眀 㴀 瀀愀渀攀氀⸀眀椀搀最攀琀猀嬀椀崀㬀ഀ਀ऀऀऀऀऀ椀昀 ⠀眀 ℀㴀 渀甀氀氀 ☀☀ 眀⸀搀攀瀀琀栀 㴀㴀 洀圀椀搀最攀琀⸀搀攀瀀琀栀 ☀☀ 眀⸀洀愀琀攀爀椀愀氀 㴀㴀 洀圀椀搀最攀琀⸀洀愀琀攀爀椀愀氀⤀ ⬀⬀挀漀甀渀琀㬀ഀ਀ऀऀऀऀ紀ഀ਀ഀ਀ऀऀऀऀ椀昀 ⠀挀漀甀渀琀 㸀 ㄀⤀ഀ਀ऀऀऀऀ笀ഀ਀ऀऀऀऀऀ䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀䠀攀氀瀀䈀漀砀⠀挀漀甀渀琀 ⬀ ∀ 眀椀搀最攀琀猀 愀爀攀 甀猀椀渀最 琀栀攀 搀攀瀀琀栀 瘀愀氀甀攀 漀昀 ∀ ⬀ 洀圀椀搀最攀琀⸀搀攀瀀琀栀 ⬀ഀ਀††ऀऀऀऀऀऀ∀⸀ 䤀琀 洀愀礀 渀漀琀 戀攀 挀氀攀愀爀 眀栀愀琀 猀栀漀甀氀搀 戀攀 椀渀 昀爀漀渀琀 漀昀 眀栀愀琀⸀∀Ⰰ 䴀攀猀猀愀最攀吀礀瀀攀⸀圀愀爀渀椀渀最⤀㬀ഀ਀ऀऀऀऀ紀ഀ਀ഀ਀†⼯† 				if (mDepthCheck)
				{
					if (panel.drawCalls.size > 1)
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
			GUILayout.BeginHorizontal(); ††⼯† 			{
				EditorGUILayout.PrefixLabel("Correction"); †⼯† 
				if (GUILayout.Button("Make Pixel-Perfect"))
				{
					NGUIEditorTools.RegisterUndo("Make Pixel-Perfect", mWidget.transform); ††††ऀऀऀऀऀ洀圀椀搀最攀琀⸀䴀愀欀攀倀椀砀攀氀倀攀爀昀攀挀琀⠀⤀㬀ഀ਀ऀऀऀऀ紀ഀ਀ऀऀऀ紀ഀ਀ऀऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䔀渀搀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ऀऀ紀ഀ਀ഀ਀ऀऀ⼀⼀一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀䐀爀愀眀匀攀瀀愀爀愀琀漀爀⠀⤀㬀ഀ਀ऀऀ䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀匀瀀愀挀攀⠀⤀㬀ഀ਀ഀ਀ऀऀ⼀⼀ 䌀漀氀漀爀 琀椀渀琀ഀ਀ऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䈀攀最椀渀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ऀऀ䌀漀氀漀爀 挀漀氀漀爀 㴀 䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀䌀漀氀漀爀䘀椀攀氀搀⠀∀䌀漀氀漀爀 吀椀渀琀∀Ⰰ 洀圀椀搀最攀琀⸀挀漀氀漀爀⤀㬀ഀ਀††ऀऀ椀昀 ⠀䜀唀䤀䰀愀礀漀甀琀⸀䈀甀琀琀漀渀⠀∀䌀漀瀀礀∀Ⰰ 䜀唀䤀䰀愀礀漀甀琀⸀圀椀搀琀栀⠀㔀　昀⤀⤀⤀ഀ਀ऀऀऀ一䜀唀䤀匀攀琀琀椀渀最猀⸀挀漀氀漀爀 㴀 挀漀氀漀爀㬀ഀ਀ऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䔀渀搀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䔀渀搀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ऀऀഀ਀ऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䈀攀最椀渀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ऀऀ一䜀唀䤀匀攀琀琀椀渀最猀⸀挀漀氀漀爀 㴀 䔀搀椀琀漀爀䜀唀䤀䰀愀礀漀甀琀⸀䌀漀氀漀爀䘀椀攀氀搀⠀∀䌀氀椀瀀戀漀愀爀搀∀Ⰰ 一䜀唀䤀匀攀琀琀椀渀最猀⸀挀漀氀漀爀⤀㬀ഀ਀ऀऀ椀昀 ⠀䜀唀䤀䰀愀礀漀甀琀⸀䈀甀琀琀漀渀⠀∀倀愀猀琀攀∀Ⰰ 䜀唀䤀䰀愀礀漀甀琀⸀圀椀搀琀栀⠀㔀　昀⤀⤀⤀ഀ਀ऀऀऀ挀漀氀漀爀 㴀 一䜀唀䤀匀攀琀琀椀渀最猀⸀挀漀氀漀爀㬀ഀ਀ऀऀ䜀唀䤀䰀愀礀漀甀琀⸀䔀渀搀䠀漀爀椀稀漀渀琀愀氀⠀⤀㬀ഀ਀ഀ਀ऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀挀漀氀漀爀 ℀㴀 挀漀氀漀爀⤀ഀ਀ऀऀ笀ഀ਀ऀऀऀ一䜀唀䤀䔀搀椀琀漀爀吀漀漀氀猀⸀刀攀最椀猀琀攀爀唀渀搀漀⠀∀䌀漀氀漀爀 䌀栀愀渀最攀∀Ⰰ 洀圀椀搀最攀琀⤀㬀ഀ਀ऀऀऀ洀圀椀搀最攀琀⸀挀漀氀漀爀 㴀 挀漀氀漀爀㬀ഀ਀† 		}
	}

	/// <summary>
	/// Draw a toggle button for the pivot point.
	/// </summary>

	void Toggle (string text, string style, UIWidget.Pivot pivot, bool isHorizontal)
	{
		bool isActive = false;

		switch (pivot)
		{
			case UIWidget.Pivot.Left:
			isActive = IsLeft(mWidget.pivot);
			break;

			case UIWidget.Pivot.Right:
			isActive = IsRight(mWidget.pivot);ऊउउऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀㨀ഀ਀ऀऀऀ椀猀䄀挀琀椀瘀攀 㴀 䤀猀吀漀瀀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀㨀ഀ਀ऀऀऀ椀猀䄀挀琀椀瘀攀 㴀 䤀猀䈀漀琀琀漀洀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ഀ਀ऀऀऀ挀愀猀攀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䌀攀渀琀攀爀㨀ഀ਀ऀऀऀ椀猀䄀挀琀椀瘀攀 㴀 椀猀䠀漀爀椀稀漀渀琀愀氀 㼀 瀀椀瘀漀琀 㴀㴀 䜀攀琀䠀漀爀椀稀漀渀琀愀氀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀ 㨀 瀀椀瘀漀琀 㴀㴀 䜀攀琀嘀攀爀琀椀挀愀氀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ऀऀऀ戀爀攀愀欀㬀ഀ਀ऀऀ紀ഀ਀ഀ਀ऀऀ椀昀 ⠀䜀唀䤀䰀愀礀漀甀琀⸀吀漀最最氀攀⠀椀猀䄀挀琀椀瘀攀Ⰰ 琀攀砀琀Ⰰ 猀琀礀氀攀⤀ ℀㴀 椀猀䄀挀琀椀瘀攀⤀ഀ਀ऀऀऀ匀攀琀倀椀瘀漀琀⠀瀀椀瘀漀琀Ⰰ 椀猀䠀漀爀椀稀漀渀琀愀氀⤀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ笀ഀ਀ऀऀ爀攀琀甀爀渀 瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䰀攀昀琀 簀簀ഀ਀††††† 			pivot == UIWidget.Pivot.TopLeft ||
			pivot == UIWidget.Pivot.BottomLeft;
	}

	static bool IsRight (UIWidget.Pivot pivot) ††ऀ笀ഀ਀ऀऀ爀攀琀甀爀渀 瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀刀椀最栀琀 簀簀ഀ਀ऀऀऀ瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀 簀簀ഀ਀ऀऀऀ瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀䈀漀琀琀漀洀刀椀最栀琀㬀ഀ਀ऀ紀ഀ਀ഀ਀ऀ猀琀愀琀椀挀 戀漀漀氀 䤀猀吀漀瀀 ⠀唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀⤀ഀ਀ऀ笀ഀ਀ऀऀ爀攀琀甀爀渀 瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀 簀簀ഀ਀ऀऀऀ瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀䰀攀昀琀 簀簀ഀ਀ऀऀऀ瀀椀瘀漀琀 㴀㴀 唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀⸀吀漀瀀刀椀最栀琀㬀ഀ਀उउ		}

	static bool IsBottom (UIWidget.Pivot pivot)
	{
		return pivot == UIWidget.Pivot.Bottom ||
			pivot == UIWidget.Pivot.BottomLeft ||
			pivot == UIWidget.Pivot.BottomRight;
	}

	static UIWidget.Pivot GetHorizontal (UIWidget.Pivot pivot)
	{
		if (IsLeft(pivot)) return UIWidget.Pivot.Left;
		if (IsRight(pivot)) return UIWidget.Pivot.Right;
		return UIWidget.Pivot.Center;
	}
 †† 	static UIWidget.Pivot GetVertical (UIWidget.Pivot pivot)
	{
		if (IsTop(pivot)) return UIWidget.Pivot.Top;
		if (IsBottom(pivot)) return UIWidget.Pivot.Bottom;
		return UIWidget.Pivot.Center;
	}

	static UIWidget.Pivot Combine (UIWidget.Pivot horizontal, UIWidget.Pivot vertical)
	{
		if (horizontal == UIWidget.Pivot.Left)
		{
			if (vertical == UIWidget.Pivot.Top) return UIWidget.Pivot.TopLeft;
			if (vertical == UIWidget.Pivot.Bottom) return UIWidget.Pivot.BottomLeft;
			return UIWidget.Pivot.Left;
		}

		if (horizontal == UIWidget.Pivot.Right)
		{
			if (vertical == UIWidget.Pivot.Top) return UIWidget.Pivot.TopRight;
			if (vertical == UIWidget.Pivot.Bottom) return UIWidget.Pivot.BottomRight;
			return UIWidget.Pivot.Right;
		} ††† 		return vertical;
 ††††††††ऀ瘀漀椀搀 匀攀琀倀椀瘀漀琀 ⠀唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瀀椀瘀漀琀Ⰰ 戀漀漀氀 椀猀䠀漀爀椀稀漀渀琀愀氀⤀ഀ਀ऀ笀ഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 栀漀爀椀稀漀渀琀愀氀 㴀 䜀攀琀䠀漀爀椀稀漀渀琀愀氀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ऀऀ唀䤀圀椀搀最攀琀⸀倀椀瘀漀琀 瘀攀爀琀椀挀愀氀 㴀 䜀攀琀嘀攀爀琀椀挀愀氀⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀⤀㬀ഀ਀ഀ਀ऀऀ瀀椀瘀漀琀 㴀 椀猀䠀漀爀椀稀漀渀琀愀氀 㼀 䌀漀洀戀椀渀攀⠀瀀椀瘀漀琀Ⰰ 瘀攀爀琀椀挀愀氀⤀ 㨀 䌀漀洀戀椀渀攀⠀栀漀爀椀稀漀渀琀愀氀Ⰰ 瀀椀瘀漀琀⤀㬀ഀ਀††⼠ †ഀ਀ऀऀ椀昀 ⠀洀圀椀搀最攀琀⸀瀀椀瘀漀琀 ℀㴀 瀀椀瘀漀琀⤀ഀ਀††† 		{
			NGUIEditorTools.RegisterUndo("Pivot change", mWidget); †††††ऀऀऀ洀圀椀搀最攀琀⸀瀀椀瘀漀琀 㴀 瀀椀瘀漀琀㬀ഀ਀ऀऀ紀ഀ਀ऀ紀ഀ਀ഀ਀ऀ⼀⼀⼀ 㰀猀甀洀洀愀爀礀㸀ഀ਀ऀ⼀⼀⼀ 䄀渀礀 愀渀搀 愀氀氀 搀攀爀椀瘀攀搀 昀甀渀挀琀椀漀渀愀氀椀琀礀⸀ഀ਀ऀ⼀⼀⼀ 㰀⼀猀甀洀洀愀爀礀㸀ഀ਀ഀ਀ऀ瀀爀漀琀攀挀琀攀搀 瘀椀爀琀甀愀氀 瘀漀椀搀 伀渀䤀渀椀琀⠀⤀ 笀 紀ഀ਀ऀ瀀爀漀琀攀挀琀攀搀 瘀椀爀琀甀愀氀 戀漀漀氀 䐀爀愀眀倀爀漀瀀攀爀琀椀攀猀 ⠀⤀ 笀 爀攀琀甀爀渀 琀爀甀攀㬀 紀ഀ਀ऀ瀀爀漀琀攀挀琀攀搀 瘀椀爀琀甀愀氀 瘀漀椀搀 䐀爀愀眀䔀砀琀爀愀倀爀漀瀀攀爀琀椀攀猀 ⠀⤀ 笀 紀ഀ਀紀ഀ਀�