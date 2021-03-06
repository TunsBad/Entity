﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using Entity.Data;
using Entity.Domain;

namespace SomeUI
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            //_context.Database.EnsureCreated();
            //_context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());

            UsingRelatedDataForFiltersAndMore();
            //FilteredEagerLoadViaProjectionNope();
            //EagerLoadManyToManyAkaChildrenGrandchildren();
            //Module4Methods.RunAll();
            //AddOneToOneToExistingObjectWhileTracked();
            //ExplicitLoad();
            //AddManyToManyWithObjects();
            //InsertNewPkFkGraph();
            //InsertNewPkFkGraphMultipleChildren();
            //InsertNewOneToOneGraph();
            //AddChildToExistingObjectWhileTracked();
            //AddOneToOneToExistingObjectWhileTracked();
            //AddBattles();
            //AddManyToManyWithFks();
            //EagerLoadFilterChildrenNope();
            //EagerLoadWithInclude();
            //EagerLoadManyToManyAkaChildrenGrandchildren();
            //EagerLoadManyToManyAkaChildrenGrandchildren();
            //EagerLoadFilteredManyToManyAkaChildrenGrandchildren();
            //EagerLoadWithMultipleBranches();
            //EagerLoadWithFromSql();
            //EagerLoadViaProjection();
            //EagerLoadAllOrNothingChildrenNope();
            //AnonymousTypeViaProjection();
            //AnonymousTypeViaProjectionWithRelated();
            //RelatedObjectsFixUp();
            //ExplicitLoadWithChildFilter();
            //EagerLoadViaProjectionNotQuite();
            //EagerLoadViaProjectionAndScalars();
            //FilteredEagerLoadViaProjectionNope();
            //ExplicitLoad();
            //RelatedObjectsFixUp();
            //ExplicitLoadWithChildFilter();
            // UsingRelatedDataForFiltersAndMore();
            // DisconnectedMethods.VerifyingThatOrphanedNewChildDoesntGetSentToDatabase();
#if false
      DisconnectedMethods.AddGraphAllNew();
      DisconnectedMethods.AddGraphWithKeyValues();
     
      Console.ReadKey(true);
      Console.WriteLine("__________________________");
      DisconnectedMethods.AttachGraphAllNew();
      DisconnectedMethods.AttachGraphWithKeys();


      Console.ReadKey(true);
      Console.WriteLine("__________________________");
     
      DisconnectedMethods.UpdateGraphAllNew();
      DisconnectedMethods.UpdateGraphWithKeys();
      Console.ReadKey(true);
      Console.WriteLine("__________________________");
      
      DisconnectedMethods.DeleteGraphAllNew();
      DisconnectedMethods.DeleteGraphWithKeys();

#endif
#if false
      DisconnectedMethods.AddGraphViaEntryAllNew();
      DisconnectedMethods.AddGraphViaEntryWithKeyValues();
      Console.ReadKey(true);
      Console.WriteLine("__________________________");
      DisconnectedMethods.AttachGraphViaEntryAllNew();
      DisconnectedMethods.AttachGraphViaEntryWithKeyValues();
      Console.ReadKey(true);
      Console.WriteLine("__________________________");
      DisconnectedMethods.UpdateGraphViaEntryAllNew();
      DisconnectedMethods.UpdateGraphViaEntryWithKeyValues();
      Console.WriteLine("__________________________");
      DisconnectedMethods.DeleteGraphViaEntryAllNew();
      DisconnectedMethods.DeleteGraphViaEntryWithKeyValues();
#endif
            // DisconnectedMethods.ChangeStateUsingEntry();
#if false
      DisconnectedMethods.AddGraphViaTrackGraphAllNew();
      DisconnectedMethods.AddGraphViaTrackGraphWithKeyValues();
      Console.ReadKey(true);
      Console.WriteLine("__________________________");
      DisconnectedMethods.AttachGraphViaTrackGraphAllNew();
      DisconnectedMethods.AttachGraphViaTrackGraphWithKeyValues();
      Console.ReadKey(true);
      Console.WriteLine("__________________________");
      DisconnectedMethods.UpdateGraphViaTrackGraphAllNew();
      DisconnectedMethods.UpdateGraphViaTrackGraphWithKeyValues();
      Console.ReadKey(true);
      Console.WriteLine("__________________________");
      DisconnectedMethods.DeleteGraphViaTrackGraphAllNew();
      DisconnectedMethods.DeleteGraphViaTrackGraphWithKeyValues();
      Console.ReadKey(true);
      Console.WriteLine("__________________________");

#endif
            //DisconnectedMethods.StartTrackingUsingCustomFunction();

            #region inactive methods

            //FullGraphDisconnected
            //EditElementsOfGraph();
            //CompareEntryWithDbSetMethods()
            //DeleteRelatedObjects();

            #endregion
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                               {
                                    new Quote {Text = "I've come to save you"}
                               }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewPkFkGraphMultipleChildren()
        {
            var samurai = new Samurai
            {
                Name = "Kyūzō",
                Quotes = new List<Quote> {
                    new Quote {Text = "Watch out for my sharp sword!"},
                    new Quote {Text="I told you to watch out for the sharp sword! Oh well!" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewOneToOneGraph()
        {
            var samurai = new Samurai { Name = "Shichirōji " };
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julie" };
            _context.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddChildToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.First();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }

        private static void AddOneToOneToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais
              .FirstOrDefault(s => s.SecretIdentity == null);
            samurai.SecretIdentity = new SecretIdentity { RealName = "Sampson" };
            _context.SaveChanges();
        }

        private static void AddBattles()
        {
            _context.Battles.AddRange(
              new Battle { Name = "Battle of Shiroyama", StartDate = new DateTime(1877, 9, 24), EndDate = new DateTime(1877, 9, 24) },
              new Battle { Name = "Siege of Osaka", StartDate = new DateTime(1614, 1, 1), EndDate = new DateTime(1615, 12, 31) },
              new Battle { Name = "Boshin War", StartDate = new DateTime(1868, 1, 1), EndDate = new DateTime(1869, 1, 1) }
              );
            _context.SaveChanges();
        }

        private static void AddManyToManyWithFks()
        {
            _context = new SamuraiContext();
            var sb = new SamuraiBattle { SamuraiId = 1, BattleId = 1 };
            _context.SamuraiBattles.Add(sb);
            _context.SaveChanges();
        }

        private static void AddManyToManyWithObjects()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();
            var battle = _context.Battles.FirstOrDefault();
            _context.SamuraiBattles.Add(
             new SamuraiBattle { SamuraiId = 2, BattleId = 2, Samurai = samurai, Battle = battle });
            _context.SaveChanges();
        }

        private static void EagerLoadWithInclude()
        {
            _context = new SamuraiContext();
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }

        private static void EagerLoadManyToManyAkaChildrenGrandchildren()
        {
            _context = new SamuraiContext();
            var samuraiWithBattles = _context.Samurais
              .Include(s => s.SamuraiBattles)
              .ThenInclude(sb => sb.Battle).ToList();
        }
        private static void EagerLoadFilteredManyToManyAkaChildrenGrandchildren()
        {
            _context = new SamuraiContext();
            var samuraiWithBattles = _context.Samurais
              .Include(s => s.SamuraiBattles)
              .ThenInclude(sb => sb.Battle)
              .Where(s => s.Name == "Kyūzō").ToList();
        }

        private static void EagerLoadWithMultipleBranches()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
              .Include(s => s.SecretIdentity)
              .Include(s => s.Quotes).ToList();
        }

        private static void EagerLoadWithFindNope()
        {

        }

        private static void EagerLoadWithFromSql()
        {
            var samurais = _context.Samurais.FromSql("select * from samurais")
              .Include(s => s.Quotes)
              .ToList();
        }

        private static void EagerLoadFilterChildrenNope()
        { //this won't work. No filtering, no sorting on Include
            _context = new SamuraiContext();
            var samurais = _context.Samurais
              .Include(s => s.Quotes.Where(q => q.Text.Contains("happy")))
              .ToList();
        }



        private static void AnonymousTypeViaProjection()
        { //Anonymous type because model is created on the fly.
            _context = new SamuraiContext();
            var quotes = _context.Quotes
              .Select(q => new { q.Id, q.Text })
              .ToList();
        }

        private static void AnonymousTypeViaProjectionWithRelated()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
              .Select(s => new
              {
                  s.Id,
                  s.SecretIdentity.RealName,
                  QuoteCount = s.Quotes.Count
              })
              .ToList();
        }

        private static void RelatedObjectsFixUp()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.Find(31);
            var quotes = _context.Quotes.Where(q => q.SamuraiId == 31).ToList();
            var quotes2 = _context.Quotes.Where(q => q.SamuraiId == 32).ToList();
        }

        private static void EagerLoadViaProjectionNotQuite()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
              .Select(s => new { Samurai = s, Quotes = s.Quotes })
              .ToList();
            //all results are in memory, but navigations are not fixed up
            //watch this github issue:https://github.com/aspnet/EntityFramework/issues/7131
        }

        private static void EagerLoadViaProjectionAndScalars()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
              .Select(s => new { s.Id, Quotes = s.Quotes })
              .ToList();
        }

        private static void FilteredEagerLoadViaProjectionNope()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
              .Select(s => new
              {
                  Samurai = s,
                  Quotes = s.Quotes
                          .Where(q => q.Text.Contains("sharp"))
                          .ToList()
              })
              .ToList();
            //quotes are not even retrieved in query.
            //https://github.com/aspnet/EntityFramework/issues/7131
        }

        private static void ExplicitLoad()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            
            _context.Entry(samurai).Reference(s => s.SecretIdentity).Load();

        }

        private static void ExplicitLoadWithChildFilter()
        {
            _context = new SamuraiContext();
            var samurai = _context.Samurais.FirstOrDefault();

            // _context.Entry(samurai)
            //   .Collection(s => s.Quotes.Where(q=>q.Text.Contains("happy"))).Load();

            _context.Entry(samurai)
              .Collection(s => s.Quotes)
              .Query() //Explicit loading allows you to use LINQ queries.
              .Where(q => q.Text.Contains("happy"))
              .Load();
        }

        private static void UsingRelatedDataForFiltersAndMore()
        {
            _context = new SamuraiContext();
            var samurais = _context.Samurais
              .Where(s => s.Quotes.Any(q => q.Text.Contains("sharp")))
              .ToList();
        }



    }
}