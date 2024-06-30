using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class EncryptedPlayerPrefs
{
    public static void SetObject<T>(string key, T value, bool encryption = true)
    {
        string jsonValue = JsonUtility.ToJson(value);

        string savingKey = encryption ? Encrypt(key) : key;
        string savingValue = encryption ? Encrypt(jsonValue) : jsonValue;

        PlayerPrefs.SetString(savingKey, savingValue);
        PlayerPrefs.Save();
    }

    public static void SetValue<T>(string key, T value, bool encryption = true)
    {
        Debug.Log($"value is : {value} and type is {typeof(T)}");

        string inputValue = value.ToString();

        Debug.Log($"input value is : {inputValue}");

        string savingKey = encryption ? Encrypt(key) : key;
        string savingValue = encryption ? Encrypt(inputValue) : inputValue;

        Debug.Log($"saving key is : {savingKey}");

        PlayerPrefs.SetString(savingKey, savingValue);
        PlayerPrefs.Save();
    }

    public static T GetObject<T>(
        string originalKey,
        T defaultValue = default,
        bool encryption = true
    )
    {
        string savedKey = encryption ? Encrypt(originalKey) : originalKey;
        string savedValue;

        Debug.Log($"saved key is : {savedKey}");

        if (!PlayerPrefs.HasKey(savedKey))
        {
            return defaultValue;
        }

        savedValue = PlayerPrefs.GetString(savedKey, "");
        string originalValue = encryption ? Decrypt(savedValue) : savedValue;

        if (originalValue == "")
            return defaultValue;

        return JsonUtility.FromJson<T>(originalValue);
    }

    public static bool HasKey(string key, bool encryption = true)
    {
        key = encryption ? Encrypt(key) : key;
        return PlayerPrefs.HasKey(key);
    }

    public static void DeleteKey(string key, bool encryption = true)
    {
        key = encryption ? Encrypt(key) : key;
        PlayerPrefs.DeleteKey(key);
    }

    private static string Encrypt(string plainText)
    {
        using Aes aes256 = CreateAes();
        ICryptoTransform encryptor = aes256.CreateEncryptor();
        using MemoryStream memoryStream = new();
        using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
        using StreamWriter streamWriter = new(cryptoStream);
        streamWriter.Write(plainText);
        streamWriter.Flush();
        cryptoStream.FlushFinalBlock();

        return Convert.ToBase64String(memoryStream.ToArray());
    }

    private static string Decrypt(string cipherText)
    {
        using Aes aes256 = CreateAes();
        ICryptoTransform decryptor = aes256.CreateDecryptor();
        using MemoryStream memoryStream = new(Convert.FromBase64String(cipherText));
        using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new(cryptoStream);

        return streamReader.ReadToEnd();
    }

    private static Aes CreateAes()
    {
        Aes aes = Aes.Create();
        aes.BlockSize = 128;
        aes.IV = Convert.FromBase64String("");
        aes.Key = Convert.FromBase64String("");
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }
}
