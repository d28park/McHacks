using UnityEngine;
using System.Collections;

public class SwitchTexture : MonoBehaviour {
	
	private UITexture m_texture;
	private Texture[] m_textures;
	
	// Use this for initialization
	void Awake () {
		m_texture = GetComponent<UITexture>();
	}
	
	public void init(int numOfTextures)
	{
		m_textures = new Texture[numOfTextures];
	}
	
	public void Switch (int id)
	{
		m_texture.mainTexture = m_textures[id];
	}
	
	public void LoadTexture(string name, int id)
	{
		m_textures[id] = Resources.Load(name,typeof(Texture)) as Texture;
	}
	
	public void SetTextureSize(int width, int height)
	{
		this.transform.gameObject.transform.localScale = new Vector3(width,height);
	}
}
