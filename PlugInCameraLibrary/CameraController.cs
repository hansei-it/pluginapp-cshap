using System;
using Plugin;
namespace PlugInCameraLibrary
{
    public class CameraController : ICameraController
    {       
        public void StartCamera()
        {
            Console.WriteLine("카메라 시작!!");
        }

        public void StopCamera()
        {
            Console.WriteLine("카메라 종료!!");
        }
        public MappingModel GetCameraTexture()
        {
            return new MappingModel { Data1 = "테스트 데이터1", Data2 = "테스트 데이터2" };
        }
    }
}
