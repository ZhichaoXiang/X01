using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace BrunieBCL.UnitTest {
    [TestClass]
    public class TaskTest {
        [TestMethod]
        public void Test() {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.WriteLine("Press 'C' to terminate the application...\n");
            // Allow the UI thread to capture the token source, so that it
            // can issue the cancel command.
            Thread t1 = new Thread(() => {
                Thread.Sleep(2000);
                System.Diagnostics.Debug.WriteLine($"====> cancelled");
                cts.Cancel();
            });
            // ServerClass sees only the token, not the token source.
            Thread t2 = new Thread(new ParameterizedThreadStart(test1));
            // Start the UI thread.
            t1.Start();

            // Start the worker thread and pass it the token.
            t2.Start(cts.Token);

            t2.Join();
            cts.Dispose();
        }
        void test1(object tt) {
            while(true) {
                Thread.Sleep(200);
                System.Diagnostics.Debug.WriteLine($"====>loop " );
            }
        }
        //[TestMethod]
        //public void Test() {
        //    CancellationTokenSource cts = new CancellationTokenSource();
        //    Task t = Task.Run(() => {
        //        int i = 0;
        //        while(i < int.MaxValue) {
        //            System.Diagnostics.Debug.WriteLine($"====>" + i);
        //            i++;
        //        }
        //    }, cts.Token);
        //    Task.Run(() => {
        //        Thread.Sleep(2000);
        //        cts.Cancel();
        //    });
        //    t.Wait();
        //    System.Diagnostics.Debug.WriteLine($"====>finish");
        //}
    }
}
