using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace AvtoNetNotifier.UWP
{
    public sealed class AvtoNetTaskRegister
    {
        private static AvtoNetTaskRegister _instance;

        private const string TASK_NAME = "BackgroundNotifier";
        private const string TASK_ENTRY_POINT = "Component.UWP.BackgroundNotifier";

        private AvtoNetTaskRegister()
        { }

        public static AvtoNetTaskRegister Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = new AvtoNetTaskRegister();
                }
                return _instance;
            }
        }

        public bool IsRegistered()
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == TASK_NAME)
                    return true;
            }
            return false;
        }

        public void Unregister()
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == TASK_NAME)
                    task.Value.Unregister(true);
            }
        }

        public async Task Register()
        {
            BackgroundExecutionManager.RemoveAccess();

            var requestStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (requestStatus != BackgroundAccessStatus.Unspecified)
            {
                var builder = new BackgroundTaskBuilder();
                builder.Name = TASK_NAME;
                builder.TaskEntryPoint = TASK_ENTRY_POINT;
                //Triggers
                //builder.SetTrigger(new TimeTrigger(15, false));
                ApplicationTrigger trigger = new ApplicationTrigger();
                builder.SetTrigger(trigger);
                //Conditions
                builder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
                builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));

                builder.Register();

                var status = await trigger.RequestAsync();
            }
        }
    }
}
