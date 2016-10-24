﻿using System.Linq;

using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Facilities;

namespace DynamicTranslator.Application
{
    public class InterceptorFacility : AbstractFacility
    {
        protected override void Init()
        {
            Kernel.ComponentRegistered += KernelOnComponentRegistered;
        }

        private static void ApplyForDetector(IHandler handler)
        {
            bool isDetector = handler.ComponentModel.Implementation.GetInterfaces().Contains(typeof(ILanguageDetector));
            if (isDetector)
            {
                handler.ComponentModel.Interceptors.AddFirst(new InterceptorReference(typeof(ExceptionInterceptor)));
            }
        }

        private static void ApplyForMeanFinder(IHandler handler)
        {
            bool isMeanFinder = handler.ComponentModel.Implementation.GetInterfaces().Contains(typeof(IMeanFinder));
            if (isMeanFinder)
            {
                handler.ComponentModel.Interceptors.AddFirst(new InterceptorReference(typeof(ExceptionInterceptor)));
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(TextGuardInterceptor)));
            }
        }

        private static void ApplyForMeanOrganizer(IHandler handler)
        {
            bool isMeanOrganizer = handler.ComponentModel.Implementation.GetInterfaces().Contains(typeof(IMeanOrganizer));
            if (isMeanOrganizer)
            {
                handler.ComponentModel.Interceptors.AddFirst(new InterceptorReference(typeof(ExceptionInterceptor)));
            }
        }

        private void KernelOnComponentRegistered(string key, IHandler handler)
        {
            ApplyForMeanFinder(handler);
            ApplyForMeanOrganizer(handler);
            ApplyForDetector(handler);
        }
    }
}
