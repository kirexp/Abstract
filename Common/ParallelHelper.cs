using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;

namespace Common {
    public static class ParallelHelper {
        public static T RunInMutex<T>(string id, Func<T> func) {
            using (new SingleGlobalInstance(id)) {
                return func.Invoke();
            }
        }

        public static void RunInMutex(string id, Action action) {
            using (new SingleGlobalInstance(id)) {
                action.Invoke();
            }
        }
    }

    internal class SingleGlobalInstance : IDisposable {
        private readonly bool _hasHandle;
        private Mutex _mutex;

        private void InitMutex(string id) {
            if (string.IsNullOrEmpty(id)) {
                id = Guid.NewGuid().ToString();
            }
            id = id.Replace("\\", string.Empty);
            var mutexId = string.Format("Global\\{{{0}}}", id);
            var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                MutexRights.FullControl, AccessControlType.Allow);
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);
            bool createdNew;
            this._mutex = new Mutex(false, mutexId, out createdNew, securitySettings);
        }

        public SingleGlobalInstance(string id) {
            this.InitMutex(id);
            try {
                this._hasHandle = this._mutex.WaitOne(Timeout.Infinite, false);
            }
            catch (AbandonedMutexException) {
                this._hasHandle = true;
            }
        }


        public void Dispose() {
            if (this._mutex == null) {
                return;
            }
            if (this._hasHandle) {
                this._mutex.ReleaseMutex();
            }
            this._mutex.Dispose();
        }
    }
}