using System;
using System.Collections.Generic;
using System.Text;

namespace Milsymbol.App6d
{
    public static class App6dHqTfFdExtensions
    {
        public static bool IsFeintDummy(this App6dHqTfFd value) => (value & App6dHqTfFd.FeintDummy) == App6dHqTfFd.FeintDummy;

        public static bool IsHeadquarters(this App6dHqTfFd value) => (value & App6dHqTfFd.Headquarters) == App6dHqTfFd.Headquarters;

        public static bool IsTaskForce(this App6dHqTfFd value) => (value & App6dHqTfFd.TaskForce) == App6dHqTfFd.TaskForce;
    }
}
