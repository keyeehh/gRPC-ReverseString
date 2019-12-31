using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Generated;

namespace GrpcServiceExample.ReverseService.ServiceImplementation
{
    public class ReverseServiceImplementation : RevService.RevServiceBase
    {
        public override Task<Data> Reverse(Data request, ServerCallContext context)
        {
            var response = new Data() { Str = request.Str.Reverse() };

            return Task.FromResult(response);
        }
    }   //  class ReverseServiceImplementation

    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that reverses the given string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The string reversed.</returns>
        public static string Reverse(this string @this)
        {
            if (@this.Length <= 1)
            {
                return @this;
            }

            char[] chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
    }   //  class Extensions
}   //  namespace GrpcServiceExample.ReverseService.ServiceImplementation
