using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Helper
{
    public Component component;

    public Helper(Component component) => this.component = component;
}
