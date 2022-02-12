using AIOFrame.Util;
using System.IO;
using UnityEngine;
namespace AIOFrame.Mgr
{
    public class ILRuntimeMgr : MonoSingleTonBase<ILRuntimeMgr>
    {
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;
        System.IO.MemoryStream fs;
        System.IO.MemoryStream p;
        public void LoadDll()
        {
            appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
            StartCoroutine(WWWUtil.LoadFromStreamingAssetsPathASyn("HotFixDll/HotFixProj.dll", (errorDll, dataDll) => {
                try
                {
                    fs = new MemoryStream(dataDll);
                    StartCoroutine(WWWUtil.LoadFromStreamingAssetsPathASyn("HotFixDll/HotFixProj.pdb", (errorPdb, dataPdbl) => {
                        p = new MemoryStream(dataPdbl);
                        appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
                    }));
                        
                }
                catch
                {
                    Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
                }

            }));
        }
        public void UnloadDll()
        {
        }
    }
}

