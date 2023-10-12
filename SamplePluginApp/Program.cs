using System;
using System.Linq;
using System.Reflection;
using Plugin;

namespace SamplePluginApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("로드할 라이브러리 이름 입력(예: PluginCameraLib.dll): ");

            // 예) PluginCameraLib.dll
            var libName = Console.ReadLine();
            try
            {
                LoadLibrary(libName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("입력한 이름의 라이브러리 동적 로딩 실패 : {0}",ex.Message);
            }
        }
        static void LoadLibrary(string libName)
        {
            Assembly pluginAssembly = null;

            // 라이브러리 동적 로딩
            pluginAssembly = Assembly.LoadFrom(libName);


            // 플러그인 어셈블리의 모든 타이을 불러와 ICameraController기능을 제공하면 실행 
            var classTypes =
                pluginAssembly.GetTypes()
                .Where(t => t.IsClass && (t.GetInterface("ICameraController") != null))
                .ToList();
            if (!classTypes.Any())
            {
                Console.WriteLine("인터페이스 ICameraController을 지원하는 타입이 없음");
            }

            // 플러그인 라이브러리 기능 사용
            foreach (Type t in classTypes)
            {
                // 카메라 인스턴스 생성 및 실행
                ICameraController iCameraController = (ICameraController)pluginAssembly.CreateInstance(t.FullName, true);
                
                iCameraController.StartCamera();
                
                MappingModel mappingModel = iCameraController.GetCameraTexture();
                Console.WriteLine($"카메라에서 받은 정보 : {mappingModel.Data1}, {mappingModel.Data2}");

                iCameraController.StopCamera();
            }
        }
    }
}
