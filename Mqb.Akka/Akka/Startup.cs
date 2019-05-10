using Akka.Actor;
using Akka.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Akka
{
    public static class Startup
    {
        #region Variables

        private static ActorSystem _actorSystem = null;
        
        #endregion

        #region Registration Methods

        public static void RegisterAkka(this IServiceCollection services, string systemName)
        {
            RegisterAkkaSystemSingleton(services, ActorSystem.Create(systemName));
        }
        public static void RegisterAkka(this IServiceCollection services, string systemName, Config config)
        {
            RegisterAkkaSystemSingleton(services, ActorSystem.Create(systemName, config));
        }
        public static void RegisterAkka_SqlPersistence(this IServiceCollection services, string systemName, IConfiguration configuration)
        {
            RegisterAkka_SqlPersistence(services, systemName, configuration, "DefaultConnection");
        }
        public static void RegisterAkka_SqlPersistence(this IServiceCollection services, string systemName, IConfiguration configuration, string connectionName)
        {
            RegisterAkkaSystemSingleton(services, ActorSystem.Create(systemName, GetPersistenceConfig(configuration, connectionName)));
        }

        #endregion

        #region Configuration Methods

        public static Action ConfigureAkka_GetActorSystemShutdownAction(this IServiceProvider serviceProvider)
        {
            return Shutdown;
        }

        #endregion

        #region Utility Methods

        private static Config GetPersistenceConfig(IConfiguration configuration, string connectionName)
        {
            var connectionString = configuration.GetConnectionString(connectionName);

            connectionString = connectionString.Replace("\\", "\\\\");

            return ConfigurationFactory.ParseString(string.Concat(GetPersistenceJournalConfig(connectionString), GetPersistenceSnapshotConfig(connectionString)));
        }

        private static string GetPersistenceJournalConfig(string connectionString)
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
        private static string GetPersistenceSnapshotConfig(string connectionString)
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
        private static void RegisterAkkaSystemSingleton(IServiceCollection services, ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
            services.AddSingleton(actorSystem);
        }
        private static void Shutdown()
        {
            if (_actorSystem != null)
                _actorSystem.Terminate();
        }

        #endregion
        
    }
}
