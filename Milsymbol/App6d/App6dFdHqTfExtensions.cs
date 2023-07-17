using System;
using System.Collections.Generic;
using System.Text;

namespace Milsymbol.App6d
{
    public static class App6dFdHqTfExtensions
    {
        public static bool IsFeintDummy(this App6dFdHqTf value) => (value & App6dFdHqTf.FeintDummy) == App6dFdHqTf.FeintDummy;

        public static bool IsHeadquarters(this App6dFdHqTf value) => (value & App6dFdHqTf.Headquarters) == App6dFdHqTf.Headquarters;

        public static bool IsTaskForce(this App6dFdHqTf value) => (value & App6dFdHqTf.TaskForce) == App6dFdHqTf.TaskForce;
    }
}
