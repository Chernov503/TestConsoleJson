using System.Reflection.Metadata.Ecma335;

namespace ConsoleApp1.Application.Services
{
    public static class TryGetValueFromArgs
    {
        #region Methods
        public static string? GetStringByKey(string[] args, string key)
        {
            var arg = args.SingleOrDefault(x => x.StartsWith(key + ":"));

            if (arg == null) return null;

            var value = arg.Split(':').Skip(1).First();

            return value;

        }
        public static decimal? GetDecimalByKey(string[] args, string key)
        {
            var value = GetStringByKey(args, key);
            if (value == null) return null;

            var isCorrect = decimal.TryParse(value.Replace('.', ','), out var decimalValue);

            return isCorrect ? decimalValue : null;
        }
        public static int? GetIntByKey(string[] args, string key)
        {
            var value = GetStringByKey(args, key);
            if (value == null) return null;

            var isCorrect = int.TryParse(value, out var intValue);

            return isCorrect ? intValue : null;
        }

        #endregion
    }
}
