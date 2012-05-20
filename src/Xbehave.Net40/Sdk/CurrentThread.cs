﻿// <copyright file="CurrentThread.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace Xbehave.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit.Sdk;

    internal static class CurrentThread
    {
        private static ICommandFactory commandFactory = new CommandFactory();

        [ThreadStatic]
        private static Scenario scenario;

        public static Scenario Scenario
        {
            get { return scenario ?? (scenario = new Scenario(commandFactory)); }
        }

        [SuppressMessage(
            "Microsoft.Design",
            "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "Required to prevent infinite loops in test runners (TestDrive.NET, Resharper) when they are allowed to handle exceptions.")]
        public static IEnumerable<ITestCommand> CreateCommands(ScenarioDefinition definition)
        {
            // NOTE: I've tried to move this into Scenario, with the finally block clearing the steps but it just doesn't seem to work
            try
            {
                try
                {
                    definition.Execute();
                }
                catch (Exception ex)
                {
                    return new ITestCommand[] { new ExceptionCommand(definition.Method, ex) };
                }

                return Scenario.GetTestCommands(definition);
            }
            finally
            {
                scenario = null;
            }
        }
    }
}