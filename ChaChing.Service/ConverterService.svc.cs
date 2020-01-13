namespace ChaChing.Service
{
    public class ConverterService : IConverterService
    {
        public string NumberToEnglish(decimal value)
        {
            var conventer = new Converter();
            return conventer.ToWords(value);
        }
    }
}
