using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Services;
using System.Threading.Tasks;
using Core.Models;
using Autofac.Core;
using Data.CSV;

namespace GUI.Modules
{
    public sealed class Module : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<CsvBackedYardItemDataStore>()
                .WithParameters(new[]
                {
                    new TypedParameter(
                        typeof(string),
                        AppConfig.Config.SpreadsheetFile),
                    new TypedParameter(
                        typeof(IColumnSchema),
                        AppConfig.Config.Schema)
                })
                .As<IDataStore<YardItem, string>>()
                .InstancePerLifetimeScope();
        }
    }
}
