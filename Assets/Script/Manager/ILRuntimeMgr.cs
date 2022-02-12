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
                    Debug.LogError("�����ȸ�DLLʧ�ܣ���ȷ���Ѿ�ͨ��VS��Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln������ȸ�DLL");
                }

            }));
        }
        public void UnloadDll()
        {
        }
    }
}

