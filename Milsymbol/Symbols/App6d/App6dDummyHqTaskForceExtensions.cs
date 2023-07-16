using System;
using System.Collections.Generic;
using System.Text;

namespace Milsymbol.Symbols.App6d
{
    public static class App6dDummyHqTaskForceExtensions
    {
        public static bool IsDummy(this App6dDummyHqTaskForce value) => (value & App6dDummyHqTaskForce.FeintDummy) == App6dDummyHqTaskForce.FeintDummy;

        public static bool IsHeadquarters(this App6dDummyHqTaskForce value) => (value & App6dDummyHqTaskForce.Headquarters) == App6dDummyHqTaskForce.Headquarters;

        public static bool IsTaskForce(this App6dDummyHqTaskForce value) => (value & App6dDummyHqTaskForce.TaskForce) == App6dDummyHqTaskForce.TaskForce;
    }
}
