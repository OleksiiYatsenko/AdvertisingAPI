using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AdvertisingServer.Models;
using AdvertisingServer.Models.DbContext;
using AdvertisingServer.Models.Dto.Advertising;
using Bogus;
using Microsoft.EntityFrameworkCore.Internal;

namespace AdvertisingServer.UnitTests.DataFixture
{
    public class AdvertisingDataFixture
    {
        private readonly Faker<Advertising> _testAdvertising;
        private readonly Faker<AdvertisingBase> _testAdvertisingDto;

        private static readonly string[] Tokens =
            {"usertoken1", "usertoken2", "usertoken3", "usertoken4", "usertoken5"};

        public AdvertisingDataFixture()
        {
            _testAdvertising = new Faker<Advertising>()
                .RuleFor(a => a.Token, x => x.PickRandom(Tokens))
                .RuleFor(a => a.Content, x => x.Random.Bytes(255))
                .RuleFor(a => a.Text, x => x.Lorem.Lines(4))
                .RuleFor(a => a.CreatedDate, x => x.Date.Past(1))
                .RuleFor(a => a.UpdatedDate, x => x.Date.Recent(30));

            _testAdvertisingDto = new Faker<AdvertisingBase>()
                .RuleFor(a => a.Token, x => x.PickRandom(Tokens));
        }

        public void InsertTestDataToDb(MarketingDbContext db)
        {
            if (db.Advertisings.Any())
            {
                return;
            }

            var advertisings = _testAdvertising.Generate(30);
            db.Advertisings.AddRange(advertisings);
            db.SaveChanges();
        }

        public AdvertisingBase GenerateNewAdvertisingRequest()
        {
            return _testAdvertisingDto.Generate(1).FirstOrDefault();
        }

        public Advertising GetRandomAdvertising(MarketingDbContext db)
        {
            var advertisings = db.Advertisings.ToArray();
            var ad = advertisings.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();
            if (ad == null)
            {
                ad = _testAdvertising.Generate(1).FirstOrDefault();
                db.Advertisings.Add(ad);
                db.SaveChanges();
            }

            return ad;
        }

        public int GetNoneExistingAdvertisingId(MarketingDbContext db)
        {
            return db.Advertisings.Max(x => x.AdvertisingId) + 100;
        }

        public string GetRandomToken()
        {
            return new Faker().PickRandom(Tokens);
        }
}
}
