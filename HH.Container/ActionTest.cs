using System;
using System.Collections.Generic;
using System.Text;

namespace HH.Container
{
    public class ActionTest
    {
        public ActionTest() { }
        public void Test()
        {
            this.ActionMethod<TaskAwait>((x) =>
            {
                Console.WriteLine("Test");
            });

            foreach (object act in this.actions)
            {
                Action<TaskAwait> action = act as Action<TaskAwait>;
                action(null);
            }
        }

        List<object> actions = new List<object>();

        public void ActionMethod<T>(Action<T> action)
        {
            this.actions.Add(action);
        }
    }
}
