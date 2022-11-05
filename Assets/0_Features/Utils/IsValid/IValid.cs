namespace _0_Features.Utils.IsValid
{
    public interface IValid
    {
        public string NotValidMessage { get; }
        public bool IsValid { get; }

        public void NotValidError();
    }
}
