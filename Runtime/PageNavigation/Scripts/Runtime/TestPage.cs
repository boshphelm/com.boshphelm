using System.Collections;
using System.Collections.Generic;
using Boshphelm.Pages;
using UnityEngine;

namespace Boshphelm.PageNavigation
{
    public class TestPage : Page
    {
        public override void Open()
        {
            gameObject.SetActive(true);
            Debug.Log("On Page Open : " + gameObject, gameObject);
        }
        public override void Close()
        {
            gameObject.SetActive(false);
            Debug.Log("On Page Close : " + gameObject, gameObject);
        }
    }
}
