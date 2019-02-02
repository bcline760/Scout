using System.Linq;
using System;
using System.Text.RegularExpressions;

using Scout.Core.Configuration;
using Scout.Core;
using Scout.Model.DB.Mongo.Repository;
using Scout.Core.Repository;

using Autofac;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using AutoMapper;

namespace Scout.Model.DB.Mongo
{
    public class IocRegistrations : IRegister
    {
        public void Register(ContainerBuilder container)
        {
            container.Register(r =>
            {
                IScoutConfiguration config = r.Resolve<IScoutConfiguration>();
                var settings = MongoClientSettings.FromUrl(new MongoUrl(config.MongoConnectionString));
                var client = new MongoClient(settings);

                BsonSerializer.RegisterIdGenerator(typeof(Guid), GuidGenerator.Instance);
                ConventionPack pack = new ConventionPack
                {
                    new LowerCaseNamingConvention(),
                    new ProperPluralGrammarNamingConvention(),
                    new SeparateWordsNamingConvetion()
                };
                ConventionRegistry.Register("standard", pack, type => true);
                return client.GetDatabase(config.DatabaseName);

            }).As<IMongoDatabase>().SingleInstance();

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new DatabaseModelMap());
            });

            container.RegisterType<PlayerRepository>().As<IPlayerRepository>();
            container.RegisterType<AccountRepository>().As<IAccountRepository>();
        }

        private class LowerCaseNamingConvention : IMemberMapConvention
        {
            public string Name => nameof(LowerCaseNamingConvention);

            public void Apply(BsonMemberMap memberMap)
            {
                memberMap.SetElementName(memberMap.ElementName.ToLowerInvariant());
            }
        }

        private class SeparateWordsNamingConvetion : IMemberMapConvention
        {
            public string Name => nameof(SeparateWordsNamingConvetion);
            private static readonly Regex s_seperateWordRegex =
                new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);

            public void Apply(BsonMemberMap memberMap)
            {
                var replace = s_seperateWordRegex.Replace(memberMap.ElementName, "_");
                memberMap.SetElementName(replace);
            }
        }

        private class ProperPluralGrammarNamingConvention : IMemberMapConvention
        {
            public string Name => nameof(ProperPluralGrammarNamingConvention);

            public void Apply(BsonMemberMap memberMap)
            {
                string modelName = memberMap.ElementName;
                string collectionName = string.Empty;
                char endingCharacter = modelName[modelName.Length - 1];

                //Be smart about English
                if (char.ToLowerInvariant(endingCharacter) == 'y')
                {
                    char nextChar = modelName[modelName.Length - 2];
                    char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
                    if (vowels.Any(v => v == nextChar))
                        collectionName = string.Concat(modelName, 's'); //bays, toys, keys
                    else
                        collectionName = string.Concat(modelName.Substring(0, modelName.Length - 2), "ies"); //histories, flies, countries, etc.
                }
                else if (char.ToLowerInvariant(endingCharacter) == 'o') //Gonna have the odd case here...pianoes
                {
                    collectionName = string.Concat(modelName, "es");
                }
                else
                    collectionName = string.Concat(modelName, 's'); //Bows, Arrows

                memberMap.SetElementName(collectionName);
            }
        }
    }
}
