using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingServer.UnitTests.Extesions
{
    public static class FluentAssertionExtensions
    {
        public static T ToModel<T, V>(this ActionResult<T> value) where V : ObjectResult
        {
            var temp = value.Result.Should().BeAssignableTo<V>().Subject;
            return temp.Value.Should().BeAssignableTo<T>().Subject;
        }
    }
}
