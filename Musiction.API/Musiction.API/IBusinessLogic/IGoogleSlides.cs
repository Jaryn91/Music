namespace Musiction.API.IBusinessLogic
{
    public interface IGoogleSlides
    {
        string Create(string title);
        void Remove(string presentationId);
    }
}
