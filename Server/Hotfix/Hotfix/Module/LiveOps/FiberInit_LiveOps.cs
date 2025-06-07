using System.Net;

namespace ET.Server
{

    [Invoke((long)SceneType.LiveOps)]
    public class FiberInit_LiveOps : AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.Get((int)root.Id);
            root.AddComponent<HttpComponent, string>($"http://*:{startSceneConfig.Port}/");
            root.AddComponent<DBManagerComponent>();

            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<TimerComponent>();
            root.AddComponent<ObjectWait>();
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<ProcessInnerSender>();
            await ETTask.CompletedTask;
        }
    }
}