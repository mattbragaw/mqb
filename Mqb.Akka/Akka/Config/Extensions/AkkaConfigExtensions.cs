using Akka.Actor;
using Akka.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mqb.Akka.Config
{
    public static class AkkaConfigExtensions
    {
        // config add-ons
        public static AkkaPersistenceConfig UseSqlPersistence(this AkkaConfig akkaConfig, IConfiguration configuration, string connectionName)
        {
            // get connection string
            var connectionString = configuration.GetConnectionString(connectionName);
            connectionString = connectionString.Replace("\\", "\\\\");

            // create SQL hocon sections with connection string injected
            var hoconSections = akkaConfig.HoconSections.ToList();
            hoconSections.Add(GetPersistenceSqlJournalConfig(connectionString));
            hoconSections.Add(GetPersistenceSqlSnapshotConfig(connectionString));

            return new AkkaPersistenceConfig(akkaConfig.ActorSystemName, akkaConfig.ActorRefsType, hoconSections);
        }

        // build
        public static AkkaBuild Build(this AkkaConfig akkaConfig, IServiceCollection services)
        {
            var response = new AkkaBuild(ActorSystem.Create(akkaConfig.ActorSystemName, ConfigurationFactory.ParseString(string.Concat(akkaConfig.HoconSections))));

            services.AddSingleton(response.ActorSystem);

            return response;
        }

        #region Utility Methods

        private static string GetPersistenceSqlJournalConfig(string connectionString)
        {
            return @"
                    akka.persistence {
                        publish-plugin-commands = on
                        journal {
                            plugin = ""akka.persistence.journal.sql-server""
                            sql-server {
                                class = ""Akka.Persistence.SqlServer.Journal.SqlServerJournal, Akka.Persistence.SqlServer""
                                plugin-dispatcher = ""akka.actor.default-dispatcher""
                                table-name = EventJournal
                                schema-name = dbo
                                auto-initialize = on
                                connection-string = """ + connectionString + @"""
                            }
                        }
                    }";
        }
        private static string GetPersistenceSqlSnapshotConfig(string connectionString)
        {
            return @"
                        akka.persistence {
                            publish-plugin-commands = on
                            snapshot-store {
                                plugin = ""akka.persistence.snapshot-store.sql-server""
                                sql-server {
                                    class = ""Akka.Persistence.SqlServer.Snapshot.SqlServerSnapshotStore, Akka.Persistence.SqlServer""
                                    plugin-dispatcher = ""akka.actor.default-dispatcher""
                                    table-name = SnapshotStore
                                    schema-name = dbo
                                    auto-initialize = on
                                    connection-string = """ + connectionString + @"""
                                }
                            }
                        }";
        }

        #endregion
    }
}
