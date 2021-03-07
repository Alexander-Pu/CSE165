using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CSE165
{ 
    public interface Selectable
    {
        void Focus();

        void Unfocus();

        void Select(GameObject selectorGameObject);

        void Deselect();
    }
}
