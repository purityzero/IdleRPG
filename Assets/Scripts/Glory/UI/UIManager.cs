
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
#if Addresable
using UnityEngine.AddressableAssets;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<string, UIBase> m_UIDictionary = new Dictionary<string, UIBase>();
    private Stack<UIBase> m_UIPopupStack = new Stack<UIBase>();

    private FlowCommand m_FlowCommand = new FlowCommand();

    public async Task<T> ShowUI<T>(string _name) where T : UIBase
    {
        if (!m_UIDictionary.TryGetValue(_name, out UIBase obj))
        {
            obj = await LoadUIAsync<T>(_name);
            m_UIDictionary.Add(_name, obj);
        }

        obj.transform.SetAsLastSibling();
        obj.Show();
        return obj as T;
    }

    public void HideUI(string _name)
    {
        if (m_UIDictionary.TryGetValue(_name, out UIBase obj))
        {
            obj.Close();
        }
    }

    public async Task<T> ShowPopup<T>(string _name) where T : UIBase
    {
        T popup = await ShowUI<T>(_name);
        m_UIPopupStack.Push(popup);
        return popup;
    }

    public void CloseTopPopup()
    {
        if (m_UIPopupStack.Count > 0)
        {
            UIBase popup = m_UIPopupStack.Pop();
            popup.Close();
        }
    }

    private async Task<T> LoadUIAsync<T>(string _name) where T : UIBase
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(_name);
        await handle.Task;

        GameObject instance = Instantiate(handle.Result);
        T uiComponent = instance.GetComponent<T>();
        return uiComponent;
    }

    private void Update()
    {
        m_FlowCommand?.Update();
    }
}

public abstract class UIBase : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
#endif