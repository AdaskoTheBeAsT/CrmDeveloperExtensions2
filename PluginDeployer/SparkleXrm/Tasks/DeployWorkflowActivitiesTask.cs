﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using SparkleXrm.Tasks.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SparkleXrm.Tasks
{
    public class DeployWorkflowActivitiesTask : BaseTask
    {
        public DeployWorkflowActivitiesTask(IOrganizationService service, ITrace trace) : base(service, trace)
        {
        }

        protected override void ExecuteInternal(string folder, OrganizationServiceContext ctx)
        {
            _trace.WriteLine("Searching for workflow config in '{0}'", folder);

            var configs = ConfigFile.FindConfig(folder);
            foreach (var config in configs)
            {
                _trace.WriteLine("Using Config '{0}'", config.filePath);
                DeployWorkflowActivities(ctx, config);
            }
            _trace.WriteLine("Processed {0} config(s)", configs.Count);
        }

        private void DeployWorkflowActivities(OrganizationServiceContext ctx, ConfigFile config)
        {
            var plugins = config.GetPluginsConfig(this.Profile);

            foreach (var plugin in plugins)
            {
                List<string> assemblies = ConfigFile.GetAssemblies(config, plugin);

                var pluginRegistration = new PluginRegistraton(_service, ctx, _trace);

                foreach (var assemblyFilePath in assemblies)
                {
                    try
                    {
                        pluginRegistration.RegisterWorkflowActivities(assemblyFilePath);
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        throw new Exception(ex.LoaderExceptions.First().Message);
                    }
                }
            }
        }
    }
}
