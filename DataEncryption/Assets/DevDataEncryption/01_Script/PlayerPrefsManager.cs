using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	static PlayerPrefsManager _instance = null;

	public PlayerPrefsManager instance
	{
		get
		{
			if (_instance == null)
				_instance = FindObjectOfType<PlayerPrefsManager>();

			return _instance;
		}
	}

	HashHelper _hashHelper = null;
	SecurityHelperEx _securityHelper = null;

	// Method

	void Awake () {
		_instance = this;

		_hashHelper = FindObjectOfType<HashHelper>();
		_securityHelper = FindObjectOfType<SecurityHelperEx>();
	}

	// Use this for initialization
	void Start () {
		string key = "TestGoGoKey";
		string value = "1";

		DeleteAll();

		SetString(key, value);
	
		string getValueString = GetString(key);
		Debug.LogFormat("Get PlayerPrefs Key:{0}, Value:{1}", key, getValueString);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Save()
	{
		PlayerPrefs.Save();
	}

	public void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	public void SetString(string key, string value)
	{
		string hashKey = _hashHelper.Hash(key);
		string hashValue = _hashHelper.Hash(value);
		string encryptValue = _securityHelper.Encrypt(value + hashValue);

		PlayerPrefs.SetString(hashKey, encryptValue);
	}

	public string GetString(string key)
	{
		string hashKey = _hashHelper.Hash(key);

		if (PlayerPrefs.HasKey(hashKey) == false)
			return "";

		string encryptValue = PlayerPrefs.GetString(hashKey, "");
		if (encryptValue.Length <= 0)
			return "";

		string decryptValue = _securityHelper.Decrypt(encryptValue);
		if (decryptValue.Length < HashHelper.HashSize)
			return "";

		string value = decryptValue.Substring(0, decryptValue.Length - HashHelper.HashSize);
		string valueHash = decryptValue.Substring(decryptValue.Length - HashHelper.HashSize);

		if (_hashHelper.Hash(value) != valueHash)
			return "";

		return value;
	}
}
