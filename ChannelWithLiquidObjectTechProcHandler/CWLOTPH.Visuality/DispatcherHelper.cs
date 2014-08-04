using System;
using System.Net.Mime;
using System.Security.Permissions;
using System.Windows.Threading;

namespace CWLOTPH.Visuality
{
    /// <summary>
    /// 
    /// </summary>
    public static class DispatcherHelper
    {
        /// <summary>
        /// Simulate Application.DoEvents function of <see cref=" MediaTypeNames.Application"/> class.
        /// </summary>
        [SecurityPermissionAttribute ( SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode )]
        public static void DoEvents ( )
        {
            var frame = new DispatcherFrame ( );
            Dispatcher.CurrentDispatcher.BeginInvoke ( DispatcherPriority.Background,
                new DispatcherOperationCallback ( ExitFrames ), frame );

            try
            {
                Dispatcher.PushFrame ( frame );
            }
            catch ( InvalidOperationException )
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static object ExitFrames ( object frame )
        {
            ( ( DispatcherFrame ) frame ).Continue = false;

            return null;
        }
    }
}
