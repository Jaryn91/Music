using Musiction.API.Entities;
using System.Collections.Generic;

namespace Musiction.API.IBusinessLogic
{
    public interface ICreatePresentationResponse
    {
        PresentationResponse CreatePptxResponse(List<int> ids);
        PresentationResponse CreateZipResponse(List<int> ids);
    }
}
