//using Castle.DynamicProxy;
//using System;
//using System.Diagnostics;

//namespace ElGuerre.AspNetCore.Cross.Middleware
//{
//    [Serializable]
//    public class ServiceInterceptor : IInterceptor
//    {     
//        public ServiceInterceptor()
//        {

//        }

//        public void Intercept(IInvocation invocation)
//        {
//            Stopwatch sw = new Stopwatch();
//            sw.Start();

//            invocation.Proceed();

//            sw.Stop();

//            Console.Out.WriteLine("After base method call, '{0}'", sw.Elapsed);

//        }
//    }
//}
