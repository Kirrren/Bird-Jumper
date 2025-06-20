using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IData
{
    void LoadSave(gameData data);
    void WriteSave(ref gameData data);
}
