using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class EncryptedPlayerPrefs
{
    public static void SetValue<T>(string key, T value, bool encryption = true)
        where T : struct
    {
        string inputValue = value.ToString();

        string savingKey = encryption ? Encrypt(key) : key;
        string savingValue = encryption ? Encrypt(inputValue) : inputValue;

        PlayerPrefs.SetString(savingKey, savingValue);
        PlayerPrefs.Save();
    }

    public static T GetValue<T>(
        string originalKey,
        T defaultValue = default,
        bool encryption = true
    )
        where T : struct
    {
        string savedKey = encryption ? Encrypt(originalKey) : originalKey;
        string savedValue;

        if (!PlayerPrefs.HasKey(savedKey))
        {
            return defaultValue;
        }

        savedValue = PlayerPrefs.GetString(savedKey, "");
        string originalValue = encryption ? Decrypt(savedValue) : savedValue;

        if (originalValue == "")
            return defaultValue;

        return (T)Convert.ChangeType(originalValue, typeof(T));
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
        aes.KeySize = 256;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        // 자세한 원인은 파악이 안되었지만
        // AES 객체의 키 값을 다른 필드보다 먼저 초기화 해 줄 경우
        // 다른 값들이 초기화 되면서 키 값이 계속 변경되는 문제가 있었음
        // 따라서 키 값은 가장 마지막에 초기화 해줌
        aes.IV = Convert.FromBase64String("");
        aes.Key = Convert.FromBase64String("");

        return aes;
    }
}
