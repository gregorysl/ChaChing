namespace ChaChing.Service
{
    public class ConverterService : IConverterService
    {
        public string NumberToEnglish(string input)
        {
            var conventer = new Converter();
            return conventer.ToWords(input);
        }
    }
}
