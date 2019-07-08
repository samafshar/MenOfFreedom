using UnityEngine;
using System.Collections;

public class KeyPropsList : MonoBehaviour {

    public KeyPropsInfo[] keyProps;

    public KeyPropsInfo noneKeyProp;

    public KeyPropsInfo GetKeyPropsInfoByKeyCode(KeyCode _keyCode)
    {
        KeyCode keyCode = _keyCode;

        if (keyCode == KeyCode.None)
            return noneKeyProp;

        for (int i = 0; i < keyProps.Length; i++)
        {
            if (keyProps[i].keyCode == keyCode)
                return keyProps[i];
        }

        return null;
    }
}
