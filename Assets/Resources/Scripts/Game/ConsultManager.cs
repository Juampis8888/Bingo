using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class ConsultManager : MonoBehaviour
{
    public const string PATH_ADMIN = "C:/Bingo/Rest.json";

    public List<Awards.Award> AwardsBingo = new List<Awards.Award>();

    private void Awake()
    {
        if (File.Exists(PATH_ADMIN))
        {
            StreamReader streamReader = new StreamReader(PATH_ADMIN, Encoding.UTF8);
            string json = streamReader.ReadToEnd();
            Debug.Log(json);
            var rootObject = JsonUtility.FromJson<Awards.Root>(json);

            AwardsBingo = rootObject.body.awards;
            Debug.Log(AwardsBingo.Count + " " + rootObject.message);
        }
    }
}
