﻿using Core.Logging;
using Core.Modules;
using Ninject;
using WorkTimeTracer;

static async Task main(string[] args)
{
    var kernel = new StandardKernel(new CoreBindings());
    var workTimetracer = kernel.Get<Tracer>();

    try
    {
        if (args.Length == 0)
        {
            await workTimetracer.Logon();
        }
        else
        {
            await workTimetracer.Logoff();
        }
    }
    catch (Exception ex)
    {
        kernel.Get<ILogger>().Error(ex);
    }
}

await main(args);