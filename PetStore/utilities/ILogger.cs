using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetStore.utilities
{
    public interface ILogger
    {

        int StepCount { get; set; }

        void Info(string message);
        void Warning(string message);
        void Error(Exception e);
        void Step(string pageName, string message);
    }
}